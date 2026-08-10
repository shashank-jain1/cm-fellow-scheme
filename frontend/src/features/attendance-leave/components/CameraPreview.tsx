interface Props {
  capturedImage: string | null;
  cameraLoading: boolean;
  cameraError: string | null;
  videoRef: React.RefObject<HTMLVideoElement | null>;
}

export default function CameraPreview({ capturedImage, cameraLoading, cameraError, videoRef }: Props) {
  return (
    <div style={{ position: 'relative', width: '100%', maxWidth: 480, borderRadius: 12, overflow: 'hidden', background: 'var(--navy-50)', aspectRatio: '4/3' }}>
      {cameraLoading && (
        <div style={{ position: 'absolute', inset: 0, display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: 'column', gap: 8, color: 'var(--text-secondary)' }}>
          <i className="pi pi-spin pi-camera" style={{ fontSize: 24 }} />
          <span style={{ fontSize: 13 }}>Starting camera...</span>
        </div>
      )}
      {cameraError && (
        <div style={{ position: 'absolute', inset: 0, display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: 'column', gap: 8, color: 'var(--red-600)', padding: 20, textAlign: 'center' }}>
          <i className="pi pi-exclamation-triangle" style={{ fontSize: 24 }} />
          <span style={{ fontSize: 13 }}>{cameraError}</span>
        </div>
      )}
      {!cameraError && !capturedImage && (
        <video ref={videoRef} autoPlay playsInline muted style={{ width: '100%', height: '100%', objectFit: 'cover', display: cameraLoading ? 'none' : 'block' }} />
      )}
      {capturedImage && (
        <img src={capturedImage} alt="Captured face" style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
      )}
    </div>
  );
}
