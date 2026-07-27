import FaceCaptureWidget from '../components/FaceCaptureWidget';
import AttendanceStatusBadge from '../components/AttendanceStatusBadge';
import { useMarkAttendance, useAttendanceHistory } from '../queries';

export default function MarkAttendancePage() {
  const markAttendance = useMarkAttendance();
  const { data: todayRecords } = useAttendanceHistory();

  const handleCapture = (imageBase64: string, latitude: number, longitude: number) => {
    markAttendance.mutate({
      applicantId: 1,
      latitude,
      longitude,
      faceImageBase64: imageBase64,
    });
  };

  const hasCheckedIn = todayRecords && todayRecords.length > 0;
  const lastRecord = todayRecords?.[todayRecords.length - 1];

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Mark Attendance</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Capture your face and location to check in or check out
          </p>
        </div>
      </div>

      {markAttendance.isSuccess && (
        <div
          style={{
            padding: '12px 16px',
            background: 'var(--emerald-50)',
            border: '1px solid var(--emerald-200)',
            borderRadius: 8,
            marginBottom: 20,
            color: 'var(--emerald-700)',
            fontSize: 14,
          }}
        >
          Attendance marked successfully!
        </div>
      )}

      {markAttendance.isError && (
        <div
          style={{
            padding: '12px 16px',
            background: 'var(--red-50)',
            border: '1px solid var(--red-200)',
            borderRadius: 8,
            marginBottom: 20,
            color: 'var(--red-700)',
            fontSize: 14,
          }}
        >
          Failed to mark attendance. Please try again.
        </div>
      )}

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 24 }}>
        <div>
          <h2 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16, color: 'var(--text-primary)' }}>
            Face Capture & GPS
          </h2>
          <FaceCaptureWidget onCapture={handleCapture} disabled={markAttendance.isPending} />
        </div>

        <div>
          <h2 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16, color: 'var(--text-primary)' }}>
            Today's Attendance
          </h2>

          {hasCheckedIn && lastRecord ? (
            <div
              style={{
                padding: 20,
                border: '1px solid var(--border-color)',
                borderRadius: 12,
                background: 'var(--surface-card)',
                display: 'flex',
                flexDirection: 'column',
                gap: 12,
              }}
            >
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Status</span>
                <AttendanceStatusBadge status={lastRecord.attendanceStatus} />
              </div>
              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Check In</span>
                <span style={{ fontSize: 14, fontWeight: 500 }}>{lastRecord.checkInTime || '—'}</span>
              </div>
              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Check Out</span>
                <span style={{ fontSize: 14, fontWeight: 500 }}>{lastRecord.checkOutTime || '—'}</span>
              </div>
              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Location</span>
                <span style={{ fontSize: 13 }}>
                  {lastRecord.latitude.toFixed(4)}, {lastRecord.longitude.toFixed(4)}
                </span>
              </div>
            </div>
          ) : (
            <div className="empty-state">
              <i className="pi pi-clock" />
              <h3>No attendance yet</h3>
              <p>Capture your face to check in for today.</p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
