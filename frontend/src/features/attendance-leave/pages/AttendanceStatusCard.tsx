import AttendanceStatusBadge from '../components/AttendanceStatusBadge';
import CheckOutButton from '../components/CheckOutButton';
import type { AttendanceDto } from '../types';

interface AttendanceStatusCardProps {
  record: AttendanceDto;
  userId: number;
  isPending: boolean;
}

export default function AttendanceStatusCard({ record, userId, isPending }: AttendanceStatusCardProps) {
  return (
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
        <AttendanceStatusBadge status={record.attendanceStatus} />
      </div>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Check In</span>
        <span style={{ fontSize: 14, fontWeight: 500 }}>{record.checkInTime || '—'}</span>
      </div>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Check Out</span>
        <span style={{ fontSize: 14, fontWeight: 500 }}>{record.checkOutTime || '—'}</span>
      </div>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Location</span>
        <span style={{ fontSize: 13 }}>
          {record.latitude.toFixed(4)}, {record.longitude.toFixed(4)}
        </span>
      </div>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Face Verification</span>
        <span style={{ fontSize: 13 }}>
          {record.faceVerificationStatus}
          {record.faceMatchPercentage != null && ` (${record.faceMatchPercentage.toFixed(1)}%)`}
        </span>
      </div>
      {!record.checkOutTime && (
        <div style={{ marginTop: 8 }}>
          <CheckOutButton
            applicantId={userId}
            attendanceDate={new Date().toISOString().split('T')[0]}
            disabled={isPending}
          />
        </div>
      )}
    </div>
  );
}
