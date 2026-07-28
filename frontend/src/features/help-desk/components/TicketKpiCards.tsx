import type { TicketDto } from '../types';

interface TicketKpiCardsProps {
  tickets: TicketDto[];
}

export default function TicketKpiCards({ tickets }: TicketKpiCardsProps) {
  const slaBreachedCount = tickets.filter((t) => t.slaBreached).length;

  return (
    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: 16, marginBottom: 24 }}>
      {[
        { label: 'Total Tickets', value: tickets.length, color: 'var(--accent-primary)' },
        { label: 'Open', value: tickets.filter((t) => t.status === 'Open').length, color: 'var(--badge-amber-text)' },
        { label: 'In Progress', value: tickets.filter((t) => t.status === 'In Progress').length, color: 'var(--badge-emerald-text)' },
        { label: 'Resolved', value: tickets.filter((t) => t.status === 'Resolved' || t.status === 'Closed').length, color: 'var(--accent-secondary)' },
        { label: 'SLA Breached', value: slaBreachedCount, color: 'var(--badge-red-text)' },
      ].map((stat, i) => (
        <div key={i} className="kpi-card" style={{ textAlign: 'center', padding: 20 }}>
          <div style={{ fontSize: 28, fontWeight: 800, color: stat.color }}>{stat.value}</div>
          <div style={{ fontSize: 13, fontWeight: 600, color: 'var(--text-secondary)', marginTop: 4 }}>{stat.label}</div>
        </div>
      ))}
    </div>
  );
}
