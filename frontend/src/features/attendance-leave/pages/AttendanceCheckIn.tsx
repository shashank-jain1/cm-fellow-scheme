import type { UseMutationResult } from '@tanstack/react-query';
import FaceCaptureWidget from '../components/FaceCaptureWidget';
import type { MarkAttendanceCommand } from '../types';

interface AttendanceCheckInProps {
  userId: number;
  markAttendance: UseMutationResult<number, Error, MarkAttendanceCommand>;
}

export default function AttendanceCheckIn({ userId, markAttendance }: AttendanceCheckInProps) {
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
