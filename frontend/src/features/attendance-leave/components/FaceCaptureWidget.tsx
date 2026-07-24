import { useRef, useState, useEffect } from 'react';
import { Button } from 'primereact/button';

interface FaceCaptureWidgetProps {
  onCapture: (imageBase64: string, latitude: number, longitude: number) => void;
  disabled?: boolean;
}

interface GeoLocation {
  latitude: number;
  longitude: number;
}

export default function FaceCaptureWidget({ onCapture, disabled }: FaceCaptureWidgetProps) {
  const videoRef = useRef<HTMLVideoElement>(null);
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const [stream, setStream] = useState<MediaStream | null>(null);
  const [capturedImage, setCapturedImage] = useState<string | null>(null);
  const [location, setLocation] = useState<GeoLocation | null>(null);
  const [cameraError, setCameraError] = useState<string | null>(null);
  const [locationError, setLocationError] = useState<string | null>(null);
  const [cameraLoading, setCameraLoading] = useState(true);
  const [locationLoading, setLocationLoading] = useState(true);

  useEffect(() => {
    let currentStream: MediaStream | null = null;

    const startCamera = async () => {
      try {
        setCameraLoading(true);
        setCameraError(null);
        const mediaStream = await navigator.mediaDevices.getUserMedia({
          video: { facingMode: 'user', width: 640, height: 480 },
        });
        currentStream = mediaStream;
        setStream(mediaStream);
        if (videoRef.current) {
          videoRef.current.srcObject = mediaStream;
        }
      } catch (err) {
        setCameraError(
          err instanceof DOMException && err.name === 'NotAllowedError'
            ? 'Camera permission denied. Please allow camera access in your browser settings.'
            : 'Unable to access camera. Please check your device.'
        );
      } finally {
        setCameraLoading(false);
      }
    };

    const fetchLocation = () => {
      if (!navigator.geolocation) {
        setLocationError('Geolocation is not supported by your browser.');
        setLocationLoading(false);
        return;
      }

      setLocationLoading(true);
      setLocationError(null);
      navigator.geolocation.getCurrentPosition(
        (position) => {
          setLocation({
            latitude: position.coords.latitude,
            longitude: position.coords.longitude,
          });
          setLocationLoading(false);
        },
        (err) => {
          setLocationError(
            err.code === err.PERMISSION_DENIED
              ? 'Location permission denied. Please allow location access.'
              : 'Unable to retrieve location. Please check your device settings.'
          );
          setLocationLoading(false);
        },
        { enableHighAccuracy: true, timeout: 10000 }
      );
    };

    startCamera();
    fetchLocation();

    return () => {
      if (currentStream) {
        currentStream.getTracks().forEach((track) => track.stop());
      }
    };
  }, []);

  const captureFrame = () => {
    if (!videoRef.current || !canvasRef.current) return;
    const video = videoRef.current;
    const canvas = canvasRef.current;
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    const ctx = canvas.getContext('2d');
    if (!ctx) return;
    ctx.drawImage(video, 0, 0);
    const dataUrl = canvas.toDataURL('image/jpeg', 0.8);
    setCapturedImage(dataUrl);

    if (stream) {
      stream.getTracks().forEach((track) => track.stop());
      setStream(null);
    }
  };

  const retake = async () => {
    setCapturedImage(null);
    try {
      setCameraLoading(true);
      setCameraError(null);
      const mediaStream = await navigator.mediaDevices.getUserMedia({
        video: { facingMode: 'user', width: 640, height: 480 },
      });
      setStream(mediaStream);
      if (videoRef.current) {
        videoRef.current.srcObject = mediaStream;
      }
    } catch (err) {
      setCameraError('Unable to restart camera.');
    } finally {
      setCameraLoading(false);
    }
  };

  const handleCapture = () => {
    if (!capturedImage || !location) return;
    const base64 = capturedImage.split(',')[1];
    onCapture(base64, location.latitude, location.longitude);
  };

  const canSubmit = capturedImage && location && !cameraLoading && !locationLoading;

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
      <div
        style={{
          position: 'relative',
          width: '100%',
          maxWidth: 480,
          borderRadius: 12,
          overflow: 'hidden',
          background: 'var(--navy-50)',
          aspectRatio: '4/3',
        }}
      >
        {cameraLoading && (
          <div
            style={{
              position: 'absolute',
              inset: 0,
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              flexDirection: 'column',
              gap: 8,
              color: 'var(--text-secondary)',
            }}
          >
            <i className="pi pi-spin pi-camera" style={{ fontSize: 24 }} />
            <span style={{ fontSize: 13 }}>Starting camera...</span>
          </div>
        )}

        {cameraError && (
          <div
            style={{
              position: 'absolute',
              inset: 0,
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              flexDirection: 'column',
              gap: 8,
              color: 'var(--red-600)',
              padding: 20,
              textAlign: 'center',
            }}
          >
            <i className="pi pi-exclamation-triangle" style={{ fontSize: 24 }} />
            <span style={{ fontSize: 13 }}>{cameraError}</span>
          </div>
        )}

        {!cameraError && !capturedImage && (
          <video
            ref={videoRef}
            autoPlay
            playsInline
            muted
            style={{ width: '100%', height: '100%', objectFit: 'cover', display: cameraLoading ? 'none' : 'block' }}
          />
        )}

        {capturedImage && (
          <img
            src={capturedImage}
            alt="Captured face"
            style={{ width: '100%', height: '100%', objectFit: 'cover' }}
          />
        )}

        <canvas ref={canvasRef} style={{ display: 'none' }} />
      </div>

      <div style={{ display: 'flex', flexDirection: 'column', gap: 8 }}>
        {locationLoading && (
          <div style={{ display: 'flex', alignItems: 'center', gap: 8, color: 'var(--text-secondary)', fontSize: 13 }}>
            <i className="pi pi-spin pi-map-marker" />
            <span>Fetching GPS coordinates...</span>
          </div>
        )}

        {locationError && (
          <div style={{ display: 'flex', alignItems: 'center', gap: 8, color: 'var(--red-600)', fontSize: 13 }}>
            <i className="pi pi-exclamation-triangle" />
            <span>{locationError}</span>
          </div>
        )}

        {location && (
          <div
            style={{
              display: 'flex',
              alignItems: 'center',
              gap: 8,
              padding: '8px 12px',
              background: 'var(--emerald-50)',
              borderRadius: 8,
              fontSize: 13,
              color: 'var(--emerald-700)',
            }}
          >
            <i className="pi pi-map-marker" />
            <span>
              Lat: {location.latitude.toFixed(6)}, Long: {location.longitude.toFixed(6)}
            </span>
          </div>
        )}
      </div>

      <div style={{ display: 'flex', gap: 12 }}>
        {!capturedImage ? (
          <Button
            label="Capture Face"
            icon="pi pi-camera"
            onClick={captureFrame}
            disabled={cameraLoading || !!cameraError || disabled}
            className="btn btn-primary"
          />
        ) : (
          <>
            <Button
              label="Retake"
              icon="pi pi-refresh"
              onClick={retake}
              className="btn btn-secondary"
              disabled={disabled}
            />
            <Button
              label="Submit Attendance"
              icon="pi pi-check"
              onClick={handleCapture}
              disabled={!canSubmit || disabled}
              className="btn btn-primary"
            />
          </>
        )}
      </div>
    </div>
  );
}
