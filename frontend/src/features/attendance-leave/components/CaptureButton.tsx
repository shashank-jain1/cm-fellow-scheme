import { Button } from 'primereact/button';

interface Props {
  capturedImage: string | null;
  cameraLoading: boolean;
  cameraError: string | null;
  canSubmit: boolean;
  disabled?: boolean;
  onCapture: () => void;
  onRetake: () => void;
  onSubmit: () => void;
}

export default function CaptureButton({ capturedImage, cameraLoading, cameraError, canSubmit, disabled, onCapture, onRetake, onSubmit }: Props) {
  return (
    <div style={{ display: 'flex', gap: 12 }}>
      {!capturedImage ? (
        <Button label="Capture Face" icon="pi pi-camera" onClick={onCapture} disabled={cameraLoading || !!cameraError || disabled} className="btn btn-primary" />
      ) : (
        <>
          <Button label="Retake" icon="pi pi-refresh" onClick={onRetake} className="btn btn-secondary" disabled={disabled} />
          <Button label="Submit Attendance" icon="pi pi-check" onClick={onSubmit} disabled={!canSubmit || disabled} className="btn btn-primary" />
        </>
      )}
    </div>
  );
}
