interface KpiCardProps {
  label: string;
  value: string | number;
  icon: string;
  accent?: 'emerald' | 'amber' | 'red' | 'navy';
  trend?: string;
  isLoading?: boolean;
}

const accentMap = {
  emerald: { bg: 'var(--emerald-100)', color: 'var(--emerald-500)' },
  amber: { bg: 'var(--amber-100)', color: 'var(--amber-500)' },
  red: { bg: 'var(--red-100)', color: 'var(--red-500)' },
  navy: { bg: 'var(--navy-100)', color: 'var(--navy-600)' },
};

export default function KpiCard({ label, value, icon, accent = 'emerald', trend, isLoading }: KpiCardProps) {
  const colors = accentMap[accent];

  if (isLoading) {
    return (
      <div className="kpi-card" style={{ padding: 20 }}>
        <div className="skeleton" style={{ width: 100, height: 16, marginBottom: 12 }} />
        <div className="skeleton" style={{ width: 60, height: 32, marginBottom: 8 }} />
        <div className="skeleton" style={{ width: 120, height: 12 }} />
      </div>
    );
  }

  return (
    <div className={`kpi-card ${accent}`}>
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: 16 }}>
        <div
          style={{
            width: 42,
            height: 42,
            borderRadius: 10,
            background: colors.bg,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
          }}
        >
          <i className={icon} style={{ fontSize: 18, color: colors.color }} />
        </div>
      </div>
      <div style={{ fontSize: 28, fontWeight: 700, color: 'var(--text-primary)', marginBottom: 4 }}>
        {value}
      </div>
      <div style={{ fontSize: 13, color: 'var(--text-secondary)', marginBottom: 4 }}>{label}</div>
      {trend && <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>{trend}</div>}
    </div>
  );
}
