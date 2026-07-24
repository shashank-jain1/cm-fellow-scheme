import { Tag } from 'primereact/tag';

interface AttendanceStatusBadgeProps {
  status: string;
}

const statusConfig: Record<string, { label: string; severity: string }> = {
  present: { label: 'Present', severity: 'success' },
  absent: { label: 'Absent', severity: 'danger' },
  late: { label: 'Late', severity: 'warn' },
  leave: { label: 'On Leave', severity: 'info' },
  approved: { label: 'Approved', severity: 'success' },
  rejected: { label: 'Rejected', severity: 'danger' },
  pending: { label: 'Pending', severity: 'warn' },
};

export default function AttendanceStatusBadge({ status }: AttendanceStatusBadgeProps) {
  const config = statusConfig[status.toLowerCase()] ?? { label: status, severity: undefined };
  return <Tag value={config.label} severity={config.severity as any} />;
}
