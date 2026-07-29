import { useState } from 'react';
import { useAdminDashboard } from '../queries';
import type { DashboardFilters } from '../types';
import KpiCard from '../components/KpiCard';
import AttendanceLeaveChart from '../components/AttendanceLeaveChart';
import ProjectProgressChart from '../components/ProjectProgressChart';
import TrainingSurveyChart from '../components/TrainingSurveyChart';
import PerformanceWidget from '../components/PerformanceWidget';
import DashboardFilterBar from '../components/DashboardFilterBar';

export default function AdminDashboardPage() {
  const [filters, setFilters] = useState<DashboardFilters>({});
  const { data: stats, isLoading } = useAdminDashboard(filters);

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Admin Dashboard</h1>
          <p>System-wide overview and management</p>
        </div>
      </div>

      <DashboardFilterBar filters={filters} onChange={setFilters} />

      <div className="metric-bar" style={{ marginBottom: 'var(--space-6)' }}>
        <KpiCard label="Users" value={stats?.totalRegisteredUsers ?? 0} icon="pi pi-users" accent="accent" isLoading={isLoading} />
        <KpiCard label="Projects" value={stats?.totalProjects ?? 0} icon="pi pi-briefcase" accent="success" isLoading={isLoading} />
        <KpiCard label="Surveys Done" value={stats?.totalSurveysCompleted ?? 0} icon="pi pi-check-square" accent="success" isLoading={isLoading} />
        <KpiCard label="Surveys Pending" value={stats?.totalSurveysPending ?? 0} icon="pi pi-clock" accent="pending" isLoading={isLoading} />
        <KpiCard label="Open Tickets" value={stats?.totalTicketsOpen ?? 0} icon="pi pi-exclamation-triangle" accent="danger" isLoading={isLoading} />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 'var(--space-5)', marginBottom: 'var(--space-5)' }}>
        <AttendanceLeaveChart
          attendancePercentage={stats?.overallAttendancePercentage ?? 0}
          isLoading={isLoading}
        />
        <ProjectProgressChart />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 'var(--space-5)' }}>
        <TrainingSurveyChart
          completed={stats?.totalSurveysCompleted ?? 0}
          pending={stats?.totalSurveysPending ?? 0}
          isLoading={isLoading}
        />
        <PerformanceWidget
          attendancePercentage={stats?.overallAttendancePercentage ?? 0}
          surveyCompletionPercentage={stats?.overallSurveyCompletionPercentage ?? 0}
          isLoading={isLoading}
        />
      </div>
    </div>
  );
}
