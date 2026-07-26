interface PerformanceWidgetProps {
  attendancePercentage: number;
  surveyCompletionPercentage: number;
  isLoading?: boolean;
}

export default function PerformanceWidget({ attendancePercentage, surveyCompletionPercentage, isLoading }: PerformanceWidgetProps) {
  if (isLoading) {
    return (
      <div className="glass-card" style={{ padding: 24 }}>
        <div className="skeleton" style={{ width: '60%', height: 16, marginBottom: 20 }} />
        <div className="skeleton" style={{ width: '100%', height: 120 }} />
      </div>
    );
  }

  const metrics = [
    { label: 'Attendance', value: attendancePercentage, color: 'var(--accent-primary)' },
    { label: 'Survey Completion', value: surveyCompletionPercentage, color: 'var(--accent-secondary)' },
  ];

  return (
    <div className="glass-card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 17, fontWeight: 700, marginBottom: 20, color: 'var(--text-primary)' }}>
        Performance Summary
      </h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 18 }}>
        {metrics.map((m, i) => (
          <div key={i}>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 8 }}>
              <span style={{ fontSize: 13, color: 'var(--text-secondary)', fontWeight: 500 }}>{m.label}</span>
              <span style={{ fontSize: 13, fontWeight: 700, color: m.color }}>{m.value}%</span>
            </div>
            <div style={{ height: 8, background: 'var(--border-color)', borderRadius: 4, overflow: 'hidden' }}>
              <div
                style={{
                  height: '100%',
                  width: `${m.value}%`,
                  background: m.color,
                  borderRadius: 4,
                  transition: 'width 0.5s ease',
                  boxShadow: '0 0 10px var(--accent-glow)',
                }}
              />
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
