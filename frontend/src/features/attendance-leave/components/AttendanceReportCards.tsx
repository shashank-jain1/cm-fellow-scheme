import { Card } from '../../../shared/components/ui';

interface SummaryCard {
  label: string;
  value: number;
  icon: string;
  color: string;
}

interface AttendanceReportCardsProps {
  cards: SummaryCard[];
  monthLabel: string;
  year: number;
}

export default function AttendanceReportCards({ cards, monthLabel, year }: AttendanceReportCardsProps) {
  if (cards.length === 0) {
    return (
      <div style={{ padding: 40, textAlign: 'center', color: 'var(--text-secondary)', background: 'var(--surface-card)', border: '1px solid var(--border-color)', borderRadius: 12 }}>
        <i className="pi pi-inbox" style={{ fontSize: 48, marginBottom: 12, display: 'block', opacity: 0.5 }} />
        <h3 style={{ margin: 0, fontSize: 16, fontWeight: 600 }}>No data available</h3>
        <p style={{ margin: '8px 0 0', fontSize: 14 }}>No attendance records found for {monthLabel} {year}</p>
      </div>
    );
  }

  return (
    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: 16 }}>
      {cards.map((card) => (
        <Card key={card.label} style={{ padding: 20 }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
            <div style={{ width: 44, height: 44, borderRadius: 10, background: `color-mix(in srgb, ${card.color} 12%, transparent)`, display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
              <i className={card.icon} style={{ fontSize: 20, color: card.color }} />
            </div>
            <div>
              <div style={{ fontSize: 13, color: 'var(--text-secondary)' }}>{card.label}</div>
              <div style={{ fontSize: 24, fontWeight: 700, color: 'var(--text-primary)' }}>{card.value}</div>
            </div>
          </div>
        </Card>
      ))}
    </div>
  );
}
