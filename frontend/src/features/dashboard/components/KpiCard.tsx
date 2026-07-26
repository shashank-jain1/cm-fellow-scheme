interface KpiCardProps {
  label: string;
  value: string | number;
  icon: string;
  accent?: 'emerald' | 'amber' | 'red' | 'navy';
  trend?: string;
  isLoading?: boolean;
}

const accentMap = {
  emerald: { bg: 'var(--badge-emerald-bg)', color: 'var(--badge-emerald-text)' },
  amber: { bg: 'var(--badge-amber-bg)', color: 'var(--badge-amber-text)' },
  red: { bg: 'var(--badge-red-bg)', color: 'var(--badge-red-text)' },
  navy: { bg: 'var(--accent-light)', color: 'var(--accent-primary)' },
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
            width: 44,
            height: 44,
            borderRadius: 'var(--radius-md)',
            background: colors.bg,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            border: '1px solid var(--border-color)',
            boxShadow: 'var(--shadow-sm)',
          }}
        >
          <i className={icon} style={{ fontSize: 20, color: colors.color }} />
        </div>
      </div>
      <div style={{ fontSize: 30, fontWeight: 800, color: 'var(--text-primary)', marginBottom: 4, letterSpacing: '-0.5px' }}>
        {value}
      </div>
      <div style={{ fontSize: 13, fontWeight: 600, color: 'var(--text-secondary)', marginBottom: 4 }}>{label}</div>
      {trend && <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>{trend}</div>}
    </div>
  );
}
