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
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            System-wide overview and management
          </p>
        </div>
      </div>

      <DashboardFilterBar filters={filters} onChange={setFilters} />

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))', gap: 20, marginBottom: 28 }}>
        <KpiCard label="Total Users" value={stats?.totalRegisteredUsers ?? 0} icon="pi pi-users" accent="emerald" isLoading={isLoading} />
        <KpiCard label="Total Projects" value={stats?.totalProjects ?? 0} icon="pi pi-briefcase" accent="navy" isLoading={isLoading} />
        <KpiCard label="Surveys Completed" value={stats?.totalSurveysCompleted ?? 0} icon="pi pi-check-square" accent="emerald" isLoading={isLoading} />
        <KpiCard label="Surveys Pending" value={stats?.totalSurveysPending ?? 0} icon="pi pi-clock" accent="amber" isLoading={isLoading} />
        <KpiCard label="Open Tickets" value={stats?.totalTicketsOpen ?? 0} icon="pi pi-exclamation-triangle" accent="red" isLoading={isLoading} />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20, marginBottom: 20 }}>
        <AttendanceLeaveChart
          attendancePercentage={stats?.overallAttendancePercentage ?? 0}
          isLoading={isLoading}
        />
        <ProjectProgressChart />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20 }}>
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
