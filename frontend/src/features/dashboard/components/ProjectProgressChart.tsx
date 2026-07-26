interface ProjectProgressChartProps {
  totalProjects: number;
  isLoading?: boolean;
}

export default function ProjectProgressChart({ totalProjects, isLoading }: ProjectProgressChartProps) {
  if (isLoading) {
    return (
      <div className="glass-card" style={{ padding: 24 }}>
        <div className="skeleton" style={{ width: '60%', height: 16, marginBottom: 20 }} />
        <div className="skeleton" style={{ width: '100%', height: 180 }} />
      </div>
    );
  }

  const projects = [
    { name: 'Project Alpha', progress: 78, color: 'var(--accent-primary)' },
    { name: 'Project Beta', progress: 45, color: 'var(--badge-amber-text)' },
    { name: 'Project Gamma', progress: 92, color: 'var(--accent-secondary)' },
  ].slice(0, totalProjects || 3);

  return (
    <div className="glass-card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 17, fontWeight: 700, marginBottom: 20, color: 'var(--text-primary)' }}>
        Project Progress
      </h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
        {projects.map((p, i) => (
          <div key={i}>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 8 }}>
              <span style={{ fontSize: 13, fontWeight: 600, color: 'var(--text-primary)' }}>{p.name}</span>
              <span style={{ fontSize: 13, fontWeight: 700, color: p.color }}>{p.progress}%</span>
            </div>
            <div style={{ height: 8, background: 'var(--border-color)', borderRadius: 4, overflow: 'hidden' }}>
              <div
                style={{
                  height: '100%',
                  width: `${p.progress}%`,
                  background: p.color,
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
