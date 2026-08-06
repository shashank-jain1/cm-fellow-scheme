using CmScheme.Common.Core.Services;
using OpenCvSharp;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class FaceMatchService : IFaceMatchService
{
    private static readonly string[] CascadePaths =
    [
        "haarcascade_frontalface_default.xml",
        Path.Combine(AppContext.BaseDirectory, "haarcascade_frontalface_default.xml"),
        Path.Combine(AppContext.BaseDirectory, "data", "haarcascade_frontalface_default.xml"),
    ];

    private string? _cascadePath;

    public async Task<(decimal MatchPercentage, string VerificationStatus)> CompareFacesAsync(
        string selfieBase64, string referencePhotoPath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(selfieBase64))
        {
            return (0m, "No selfie provided");
        }

        string cascadePath = ResolveCascadePath();
        using var faceCascade = new CascadeClassifier(cascadePath);

        // 1. Decode and detect face in selfie
        byte[] selfieBytes = Convert.FromBase64String(selfieBase64);
        using Mat selfieMat = Cv2.ImDecode(selfieBytes, ImreadModes.Color);
        if (selfieMat.Empty())
        {
            return (0m, "Invalid selfie image");
        }

        Rect[] selfieFaces = faceCascade.DetectMultiScale(
            selfieMat,
            scaleFactor: 1.1,
            minNeighbors: 5,
            flags: HaarDetectionTypes.ScaleImage,
            minSize: new Size(80, 80));

        if (selfieFaces.Length == 0)
        {
            return (0m, "No face detected in selfie");
        }

        // Use the largest face detected
        Rect selfieFace = selfieFaces.OrderByDescending(f => f.Width * f.Height).First();
        using Mat selfieRegion = new Mat(selfieMat, selfieFace);
        using Mat selfieGray = new Mat();
        Cv2.CvtColor(selfieRegion, selfieGray, ColorConversionCodes.BGR2GRAY);
        Cv2.Resize(selfieGray, selfieGray, new Size(128, 128));

        // 2. Load reference photo and detect face
        if (string.IsNullOrWhiteSpace(referencePhotoPath) || !File.Exists(referencePhotoPath))
        {
            // No reference photo available — cannot compare, but selfie face is valid
            // Return a moderate score indicating face was detected but not verified against reference
            return (75m, "Face detected — no reference photo for comparison");
        }

        using Mat referenceMat = Cv2.ImRead(referencePhotoPath, ImreadModes.Color);
        if (referenceMat.Empty())
        {
            return (75m, "Face detected — reference photo unreadable");
        }

        Rect[] referenceFaces = faceCascade.DetectMultiScale(
            referenceMat,
            scaleFactor: 1.1,
            minNeighbors: 5,
            flags: HaarDetectionTypes.ScaleImage,
            minSize: new Size(80, 80));

        if (referenceFaces.Length == 0)
        {
            return (75m, "Face detected — no face in reference photo");
        }

        Rect referenceFace = referenceFaces.OrderByDescending(f => f.Width * f.Height).First();
        using Mat referenceRegion = new Mat(referenceMat, referenceFace);
        using Mat referenceGray = new Mat();
        Cv2.CvtColor(referenceRegion, referenceGray, ColorConversionCodes.BGR2GRAY);
        Cv2.Resize(referenceGray, referenceGray, new Size(128, 128));

        // 3. Compare using histogram correlation + structural similarity
        decimal histogramScore = CompareHistograms(selfieGray, referenceGray);
        decimal structuralScore = CompareStructural(selfieGray, referenceGray);

        // Weighted combination: 60% histogram, 40% structural
        decimal matchPercentage = Math.Round(histogramScore * 0.6m + structuralScore * 0.4m, 2);
        matchPercentage = Math.Clamp(matchPercentage, 0m, 100m);

        string status = matchPercentage >= 80m ? "Verified" :
                        matchPercentage >= 60m ? "Low Match" :
                        "Mismatch";

        await Task.CompletedTask;
        return (matchPercentage, status);
    }

    private static decimal CompareHistograms(Mat img1, Mat img2)
    {
        using Mat hist1 = new Mat();
        using Mat hist2 = new Mat();

        int[] histSize = [256];
        Rangef[] ranges = [new Rangef(0, 256)];

        Cv2.CalcHist([img1], [0], null, hist1, 1, histSize, ranges);
        Cv2.CalcHist([img2], [0], null, hist2, 1, histSize, ranges);

        Cv2.Normalize(hist1, hist1, 0, 1, NormTypes.MinMax);
        Cv2.Normalize(hist2, hist2, 0, 1, NormTypes.MinMax);

        double correlation = Cv2.CompareHist(hist1, hist2, HistCompMethods.Correl);
        return (decimal)Math.Max(0, correlation) * 100m;
    }

    private static decimal CompareStructural(Mat img1, Mat img2)
    {
        // Mean Squared Error based similarity
        using Mat diff = new Mat();
        Cv2.Absdiff(img1, img2, diff);

        Scalar mean = Cv2.Mean(diff);
        double mse = mean.Val0;

        // Convert MSE to a 0-100 similarity score (lower MSE = higher similarity)
        // MSE of 0 = 100% match, MSE of 255 = 0% match
        double similarity = Math.Max(0, 100.0 - (mse / 255.0 * 100.0));
        return (decimal)similarity;
    }

    private string ResolveCascadePath()
    {
        if (_cascadePath != null && File.Exists(_cascadePath))
        {
            return _cascadePath;
        }

        foreach (string path in CascadePaths)
        {
            if (File.Exists(path))
            {
                _cascadePath = path;
                return _cascadePath;
            }
        }

        // Download the cascade file if not found locally
        string cascadeDir = Path.Combine(AppContext.BaseDirectory, "data");
        Directory.CreateDirectory(cascadeDir);
        string cascadeFile = Path.Combine(cascadeDir, "haarcascade_frontalface_default.xml");

        if (!File.Exists(cascadeFile))
        {
            using HttpClient client = new();
            string url = "https://raw.githubusercontent.com/opencv/opencv/master/data/haarcascades/haarcascade_frontalface_default.xml";
            try
            {
                byte[] data = client.GetByteArrayAsync(url).GetAwaiter().GetResult();
                File.WriteAllBytes(cascadeFile, data);
            }
            catch
            {
                // If download fails, throw a clear error
                throw new FileNotFoundException(
                    "Face detection cascade file not found and could not be downloaded. " +
                    $"Expected at: {cascadeFile}. Download manually from OpenCV GitHub.");
            }
        }

        _cascadePath = cascadeFile;
        return _cascadePath;
    }
}
