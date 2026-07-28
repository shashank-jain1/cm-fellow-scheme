import { Tag } from 'primereact/tag';

interface TicketStatusBadgeProps {
  status: string;
  slaBreached?: boolean;
}

const statusConfig: Record<string, { label: string; severity: 'success' | 'info' | 'warning' | 'danger' | 'secondary' }> = {
  'Open': { label: 'Open', severity: 'warning' },
  'In Progress': { label: 'In Progress', severity: 'info' },
  'Resolved': { label: 'Resolved', severity: 'success' },
  'Closed': { label: 'Closed', severity: 'secondary' },
  'Escalated': { label: 'Escalated', severity: 'danger' },
};

export default function TicketStatusBadge({ status, slaBreached }: TicketStatusBadgeProps) {
  const config = statusConfig[status] ?? { label: status, severity: 'secondary' as const };
  return (
    <div style={{ display: 'flex', gap: 6, alignItems: 'center' }}>
      <Tag value={config.label} severity={config.severity} />
      {slaBreached && <Tag value="SLA Breached" severity="danger" />}
    </div>
  );
}
