import { useCoordinatorDashboard } from '../queries';
import KpiCard from '../components/KpiCard';
import TrainingSurveyChart from '../components/TrainingSurveyChart';
import DashboardExport from '../components/DashboardExport';

export default function CoordinatorFellowDashboardPage() {
  const { data: stats, isLoading } = useCoordinatorDashboard(1);

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Coordinator Dashboard</h1>
          <p>Team overview and task management</p>
        </div>
      </div>

      <DashboardExport />

      <div className="metric-bar" style={{ marginBottom: 'var(--space-6)' }}>
        <KpiCard label="Team" value={stats?.teamSize ?? 0} icon="pi pi-users" accent="accent" isLoading={isLoading} />
        <KpiCard label="Active" value={stats?.activeProjects ?? 0} icon="pi pi-briefcase" accent="success" isLoading={isLoading} />
        <KpiCard label="Pending" value={stats?.pendingTasks ?? 0} icon="pi pi-exclamation-triangle" accent="pending" isLoading={isLoading} />
        <KpiCard label="Surveys Done" value={stats?.completedSurveys ?? 0} icon="pi pi-check-square" accent="success" isLoading={isLoading} />
        <KpiCard label="Surveys Due" value={stats?.pendingSurveys ?? 0} icon="pi pi-clock" accent="danger" isLoading={isLoading} />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 'var(--space-5)' }}>
        <TrainingSurveyChart
          completed={stats?.completedSurveys ?? 0}
          pending={stats?.pendingSurveys ?? 0}
          isLoading={isLoading}
        />
      </div>
    </div>
  );
}
