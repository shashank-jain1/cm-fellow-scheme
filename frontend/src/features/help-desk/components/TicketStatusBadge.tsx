import { Tag } from 'primereact/tag';

interface TicketStatusBadgeProps {
  status: string;
}

const statusConfig: Record<string, { label: string; severity: 'success' | 'info' | 'warning' | 'danger' | 'secondary' }> = {
  open: { label: 'Open', severity: 'warning' },
  in_progress: { label: 'In Progress', severity: 'info' },
  resolved: { label: 'Resolved', severity: 'success' },
  closed: { label: 'Closed', severity: 'secondary' },
  escalated: { label: 'Escalated', severity: 'danger' },
};

export default function TicketStatusBadge({ status }: TicketStatusBadgeProps) {
  const config = statusConfig[status] ?? { label: status, severity: 'secondary' as const };
  return <Tag value={config.label} severity={config.severity} />;
}
