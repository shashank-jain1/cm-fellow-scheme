import type { TicketDto } from '../types';

interface TicketKpiCardsProps {
  tickets: TicketDto[];
}

export default function TicketKpiCards({ tickets }: TicketKpiCardsProps) {
  const open = tickets.filter((t) => t.status === 'Open').length;
  const inProgress = tickets.filter((t) => t.status === 'In Progress').length;
  const resolved = tickets.filter((t) => t.status === 'Resolved' || t.status === 'Closed').length;
  const breached = tickets.filter((t) => t.slaBreached).length;

  const items = [
    { label: 'Total', value: tickets.length, color: 'var(--accent)', bg: 'var(--accent-muted)' },
    { label: 'Open', value: open, color: 'var(--pending)', bg: 'var(--pending-light)' },
    { label: 'In Progress', value: inProgress, color: 'var(--kpi-4, #6B5B95)', bg: 'rgba(107, 91, 149, 0.10)' },
    { label: 'Resolved', value: resolved, color: 'var(--success)', bg: 'var(--success-light)' },
    { label: 'SLA Breached', value: breached, color: 'var(--danger)', bg: 'var(--danger-light)', isDanger: breached > 0 },
  ];

  return (
    <div className="metric-bar" style={{ marginBottom: 'var(--space-5)' }}>
      {items.map((item, i) => (
        <div key={i} className="metric-item">
          <span className="metric-label">{item.label}</span>
          <span
            className="metric-value"
            style={{ color: item.isDanger ? 'var(--danger)' : item.color }}
          >
            {item.value}
          </span>
        </div>
      ))}
    </div>
  );
}
