import { useAuth } from '../../auth/useAuth';
import { useFellowDashboard } from '../queries';
import KpiCard from '../components/KpiCard';
import TrainingSurveyChart from '../components/TrainingSurveyChart';

export default function FellowDashboardPage() {
  const { user } = useAuth();
  const { data: stats, isLoading } = useFellowDashboard(user?.userAccountId ?? 0);

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Fellow Dashboard</h1>
          <p>Your assigned tasks and progress</p>
        </div>
      </div>

      <div className="metric-bar" style={{ marginBottom: 'var(--space-6)' }}>
        <KpiCard label="Projects" value={stats?.totalAssignedProjects ?? 0} icon="pi pi-briefcase" accent="accent" isLoading={isLoading} />
        <KpiCard label="Surveys Done" value={stats?.completedSurveys ?? 0} icon="pi pi-check-square" accent="success" isLoading={isLoading} />
        <KpiCard label="Surveys Due" value={stats?.pendingSurveys ?? 0} icon="pi pi-clock" accent="pending" isLoading={isLoading} />
        <KpiCard label="Attendance" value={`${stats?.attendancePercentage ?? 0}%`} icon="pi pi-calendar" accent="success" isLoading={isLoading} />
        <KpiCard label="Trainings" value={stats?.upcomingTraining ?? 0} icon="pi pi-book" accent="accent" isLoading={isLoading} />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 'var(--space-5)' }}>
        <TrainingSurveyChart
          completed={stats?.completedSurveys ?? 0}
          pending={stats?.pendingSurveys ?? 0}
          isLoading={isLoading}
        />
        <div className="card" style={{ padding: 24 }}>
          <h3 style={{ fontSize: 17, fontWeight: 700, marginBottom: 16 }}>Recent Activity</h3>
          <p style={{ color: 'var(--text-secondary)', fontSize: 14 }}>
            {stats?.recentActivity ?? 'No recent activity'}
          </p>
        </div>
      </div>
    </div>
  );
}
