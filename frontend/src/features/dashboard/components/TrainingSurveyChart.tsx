interface TrainingSurveyChartProps {
  completed: number;
  pending: number;
  isLoading?: boolean;
}

export default function TrainingSurveyChart({ completed, pending, isLoading }: TrainingSurveyChartProps) {
  if (isLoading) {
    return (
      <div className="glass-card" style={{ padding: 24 }}>
        <div className="skeleton" style={{ width: '60%', height: 16, marginBottom: 20 }} />
        <div className="skeleton" style={{ width: '100%', height: 180 }} />
      </div>
    );
  }

  const total = completed + pending;
  const completedPct = total > 0 ? Math.round((completed / total) * 100) : 0;

  return (
    <div className="glass-card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 17, fontWeight: 700, marginBottom: 20, color: 'var(--text-primary)' }}>
        Training &amp; Survey Stats
      </h3>
      <div style={{ display: 'flex', gap: 24, alignItems: 'center' }}>
        <div style={{ flex: 1, textAlign: 'center' }}>
          <div style={{ position: 'relative', width: 100, height: 100, margin: '0 auto 12px' }}>
            <svg viewBox="0 0 36 36" style={{ width: '100%', height: '100%', transform: 'rotate(-90deg)' }}>
              <circle cx="18" cy="18" r="15.9" fill="none" stroke="var(--border-color)" strokeWidth="3" />
              <circle
                cx="18"
                cy="18"
                r="15.9"
                fill="none"
                stroke="var(--accent-primary)"
                strokeWidth="3"
                strokeDasharray={`${completedPct} ${100 - completedPct}`}
                strokeLinecap="round"
              />
            </svg>
            <div
              style={{
                position: 'absolute',
                inset: 0,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
              }}
            >
              <span style={{ fontSize: 18, fontWeight: 800, color: 'var(--accent-primary)' }}>{completedPct}%</span>
            </div>
          </div>
          <div style={{ fontSize: 12, fontWeight: 600, color: 'var(--text-muted)' }}>Completion Rate</div>
        </div>
        <div style={{ flex: 1, display: 'flex', flexDirection: 'column', gap: 12 }}>
          <div>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 4 }}>
              <span style={{ fontSize: 13, color: 'var(--text-secondary)', fontWeight: 500 }}>Completed</span>
              <span style={{ fontSize: 13, fontWeight: 700, color: 'var(--badge-emerald-text)' }}>{completed}</span>
            </div>
          </div>
          <div>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 4 }}>
              <span style={{ fontSize: 13, color: 'var(--text-secondary)', fontWeight: 500 }}>Pending</span>
              <span style={{ fontSize: 13, fontWeight: 700, color: 'var(--badge-amber-text)' }}>{pending}</span>
            </div>
          </div>
          <div>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 4 }}>
              <span style={{ fontSize: 13, color: 'var(--text-secondary)', fontWeight: 500 }}>Total</span>
              <span style={{ fontSize: 13, fontWeight: 700, color: 'var(--text-primary)' }}>{total}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
