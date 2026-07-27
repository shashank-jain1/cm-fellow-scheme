import { useQuery } from '@tanstack/react-query';
import ApiService from '../../../services/ApiService';

interface ProjectProgress {
  projectName: string;
  completionPercentage: number;
  totalSurveys: number;
  completedSurveys: number;
}

export default function ProjectProgressChart() {
  const { data: projects, isLoading } = useQuery<ProjectProgress[]>({
    queryKey: ['project-progress'],
    queryFn: async () => {
      const res = await ApiService.get<ProjectProgress[]>('dashboards/project-progress');
      return res.data ?? [];
    },
  });

  if (isLoading) {
    return (
      <div className="glass-card" style={{ padding: 24 }}>
        <div className="skeleton" style={{ width: '60%', height: 16, marginBottom: 20 }} />
        <div className="skeleton" style={{ width: '100%', height: 180 }} />
      </div>
    );
  }

  const items = projects ?? [];

  if (items.length === 0) {
    return (
      <div className="glass-card" style={{ padding: 24 }}>
        <h3 style={{ fontSize: 17, fontWeight: 700, marginBottom: 20, color: 'var(--text-primary)' }}>
          Project Progress
        </h3>
        <div className="empty-state" style={{ padding: 20 }}>
          <p>No project data available</p>
        </div>
      </div>
    );
  }

  const colors = [
    'var(--accent-primary)',
    'var(--badge-amber-text)',
    'var(--accent-secondary)',
    'var(--badge-emerald-text)',
    'var(--badge-red-text)',
  ];

  return (
    <div className="glass-card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 17, fontWeight: 700, marginBottom: 20, color: 'var(--text-primary)' }}>
        Project Progress
      </h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
        {items.map((p, i) => {
          const progress = Math.round(p.completionPercentage);
          const color = colors[i % colors.length];
          return (
            <div key={p.projectName}>
              <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 8 }}>
                <span style={{ fontSize: 13, fontWeight: 600, color: 'var(--text-primary)' }}>{p.projectName}</span>
                <span style={{ fontSize: 13, fontWeight: 700, color }}>{progress}%</span>
              </div>
              <div style={{ height: 8, background: 'var(--border-color)', borderRadius: 4, overflow: 'hidden' }}>
                <div
                  style={{
                    height: '100%',
                    width: `${progress}%`,
                    background: color,
                    borderRadius: 4,
                    transition: 'width 0.5s ease',
                    boxShadow: '0 0 10px var(--accent-glow)',
                  }}
                />
              </div>
              <div style={{ fontSize: 11, color: 'var(--text-muted)', marginTop: 4 }}>
                {p.completedSurveys}/{p.totalSurveys} surveys completed
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
