import { useCoordinatorDashboard } from '../queries';
import KpiCard from '../components/KpiCard';
import TrainingSurveyChart from '../components/TrainingSurveyChart';

export default function CoordinatorFellowDashboardPage() {
  const { data: stats, isLoading } = useCoordinatorDashboard(1);

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Coordinator Dashboard</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Team overview and task management
          </p>
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))', gap: 20, marginBottom: 28 }}>
        <KpiCard label="Team Size" value={stats?.teamSize ?? 0} icon="pi pi-users" accent="emerald" isLoading={isLoading} />
        <KpiCard label="Active Projects" value={stats?.activeProjects ?? 0} icon="pi pi-briefcase" accent="navy" isLoading={isLoading} />
        <KpiCard label="Pending Tasks" value={stats?.pendingTasks ?? 0} icon="pi pi-exclamation-triangle" accent="amber" isLoading={isLoading} />
        <KpiCard label="Completed Surveys" value={stats?.completedSurveys ?? 0} icon="pi pi-check-square" accent="emerald" isLoading={isLoading} />
        <KpiCard label="Pending Surveys" value={stats?.pendingSurveys ?? 0} icon="pi pi-clock" accent="red" isLoading={isLoading} />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20, marginBottom: 20 }}>
        <TrainingSurveyChart
          completed={stats?.completedSurveys ?? 0}
          pending={stats?.pendingSurveys ?? 0}
          isLoading={isLoading}
        />
      </div>
    </div>
  );
}
