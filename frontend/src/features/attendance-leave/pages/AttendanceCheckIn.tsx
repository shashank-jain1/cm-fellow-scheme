import FaceCaptureWidget from '../components/FaceCaptureWidget';
import { useMarkAttendance } from '../queries';

interface AttendanceCheckInProps {
  userId: number;
}

export default function AttendanceCheckIn({ userId }: AttendanceCheckInProps) {
  const markAttendance = useMarkAttendance();

  const handleCapture = (imageBase64: string, latitude: number, longitude: number) => {
    markAttendance.mutate({
      applicantId: userId,
      latitude,
      longitude,
      faceImageBase64: imageBase64,
    });
  };

  return (
    <div>
      <h2 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16, color: 'var(--text-primary)' }}>
        Face Capture & GPS
      </h2>
      <FaceCaptureWidget onCapture={handleCapture} disabled={markAttendance.isPending} />
    </div>
  );
}
