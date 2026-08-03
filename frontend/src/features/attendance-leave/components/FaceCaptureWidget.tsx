import { useRef, useState, useEffect } from 'react';
import CameraPreview from './CameraPreview';
import CaptureButton from './CaptureButton';
import FaceMatchResult from './FaceMatchResult';

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
        setCameraLoading(true); setCameraError(null);
        const mediaStream = await navigator.mediaDevices.getUserMedia({ video: { facingMode: 'user', width: 640, height: 480 } });
        currentStream = mediaStream; setStream(mediaStream);
        if (videoRef.current) videoRef.current.srcObject = mediaStream;
      } catch (err) {
        setCameraError(err instanceof DOMException && err.name === 'NotAllowedError' ? 'Camera permission denied. Please allow camera access in your browser settings.' : 'Unable to access camera. Please check your device.');
      } finally { setCameraLoading(false); }
    };
    const fetchLocation = () => {
      if (!navigator.geolocation) { setLocationError('Geolocation is not supported by your browser.'); setLocationLoading(false); return; }
      setLocationLoading(true); setLocationError(null);
      navigator.geolocation.getCurrentPosition(
        (position) => { setLocation({ latitude: position.coords.latitude, longitude: position.coords.longitude }); setLocationLoading(false); },
        (err) => { setLocationError(err.code === err.PERMISSION_DENIED ? 'Location permission denied. Please allow location access.' : 'Unable to retrieve location. Please check your device settings.'); setLocationLoading(false); },
        { enableHighAccuracy: true, timeout: 10000 }
      );
    };
    startCamera(); fetchLocation();
    return () => { if (currentStream) currentStream.getTracks().forEach((track) => track.stop()); };
  }, []);

  const captureFrame = () => {
    if (!videoRef.current || !canvasRef.current) return;
    const video = videoRef.current; const canvas = canvasRef.current;
    canvas.width = video.videoWidth; canvas.height = video.videoHeight;
    const ctx = canvas.getContext('2d'); if (!ctx) return;
    ctx.drawImage(video, 0, 0);
    setCapturedImage(canvas.toDataURL('image/jpeg', 0.8));
    if (stream) { stream.getTracks().forEach((track) => track.stop()); setStream(null); }
  };

  const retake = async () => {
    setCapturedImage(null);
    try {
      setCameraLoading(true); setCameraError(null);
      const mediaStream = await navigator.mediaDevices.getUserMedia({ video: { facingMode: 'user', width: 640, height: 480 } });
      setStream(mediaStream);
      if (videoRef.current) videoRef.current.srcObject = mediaStream;
    } catch { setCameraError('Unable to restart camera.'); }
    finally { setCameraLoading(false); }
  };

  const handleCapture = () => {
    if (!capturedImage || !location) return;
    onCapture(capturedImage.split(',')[1], location.latitude, location.longitude);
  };

  const canSubmit = capturedImage && location && !cameraLoading && !locationLoading;

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
      <CameraPreview stream={stream} capturedImage={capturedImage} cameraLoading={cameraLoading} cameraError={cameraError} videoRef={videoRef} />
      <canvas ref={canvasRef} style={{ display: 'none' }} />
      <FaceMatchResult location={location} locationLoading={locationLoading} locationError={locationError} />
      <CaptureButton capturedImage={capturedImage} cameraLoading={cameraLoading} cameraError={cameraError} canSubmit={!!canSubmit} disabled={disabled} onCapture={captureFrame} onRetake={retake} onSubmit={handleCapture} />
    </div>
  );
}
