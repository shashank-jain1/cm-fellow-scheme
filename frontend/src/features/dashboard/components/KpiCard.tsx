interface KpiCardProps {
  label: string;
  value: string | number;
  icon: string;
  accent?: 'accent' | 'success' | 'pending' | 'danger' | 'info';
  trend?: string;
  isLoading?: boolean;
}

const accentMap: Record<string, { color: string; bg: string; border: string }> = {
  accent: { color: 'var(--kpi-1, var(--accent))', bg: 'var(--accent-muted)', border: 'var(--accent)' },
  success: { color: 'var(--kpi-2, var(--success))', bg: 'var(--success-light)', border: 'var(--success)' },
  pending: { color: 'var(--kpi-3, var(--pending))', bg: 'var(--pending-light)', border: 'var(--pending)' },
  danger: { color: 'var(--kpi-5, var(--danger))', bg: 'var(--danger-light)', border: 'var(--danger)' },
  info: { color: 'var(--kpi-4, #6B5B95)', bg: 'rgba(107, 91, 149, 0.10)', border: '#6B5B95' },
};

export default function KpiCard({ label, value, icon, accent = 'accent', trend, isLoading }: KpiCardProps) {
  if (isLoading) {
    return (
      <div className="metric-item" style={{ border: '1px solid var(--border)', borderRadius: 'var(--radius-lg)' }}>
        <div className="skeleton" style={{ width: 80, height: 14, marginBottom: 8 }} />
        <div className="skeleton" style={{ width: 48, height: 28, marginBottom: 4 }} />
        <div className="skeleton" style={{ width: 100, height: 12 }} />
      </div>
    );
  }

  const a = accentMap[accent] ?? accentMap.accent;

  return (
    <div
      className="metric-item"
      style={{
        border: `1px solid var(--border)`,
        borderLeft: `3px solid ${a.border}`,
        borderRadius: 'var(--radius-md)',
      }}
    >
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: 'var(--space-1)' }}>
        <span className="metric-label">{label}</span>
        <div style={{
          width: 34,
          height: 34,
          borderRadius: 'var(--radius-md)',
          background: a.bg,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
        }}>
          <i className={icon} style={{ fontSize: 16, color: a.color }} />
        </div>
      </div>
      <span className="metric-value" style={{ color: a.color }}>{value}</span>
      {trend && <span className="metric-trend metric-trend--flat">{trend}</span>}
    </div>
  );
}
