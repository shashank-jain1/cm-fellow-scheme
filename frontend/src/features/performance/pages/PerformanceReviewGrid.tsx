import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { usePerformanceSummary } from '../queries';
import PerformanceSummaryCard from '../components/PerformanceSummaryCard';
import { PageHeader } from '../../../shared/components/ui';
import PerformanceFilters from './PerformanceFilters';
import PerformanceTable from './PerformanceTable';

export default function PerformanceReviewGrid() {
  const [search, setSearch] = useState('');
  const navigate = useNavigate();
  const { data: records, isLoading } = usePerformanceSummary();
  const filtered = (records ?? []).filter((r) => r.applicantName.toLowerCase().includes(search.toLowerCase()));

  return (
    <div>
      <PageHeader title="Performance Tracking" subtitle="Monitor and evaluate CM Fellow performance metrics" />
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(280px, 1fr))', gap: 'var(--space-4)', marginBottom: 'var(--space-5)' }}>
        {isLoading ? [1, 2, 3].map((n) => (
          <div key={n} className="metric-item" style={{ border: '1px solid var(--border)', borderRadius: 'var(--radius-lg)' }}>
            <div className="skeleton" style={{ width: '60%', height: 12, marginBottom: 8 }} />
            <div className="skeleton" style={{ width: '40%', height: 24 }} />
          </div>
        )) : (records ?? []).slice(0, 3).map((r) => (
          <PerformanceSummaryCard key={r.performanceEvaluationId} record={r} />
        ))}
      </div>
      <PerformanceFilters search={search} onSearchChange={setSearch} />
      <PerformanceTable records={filtered} isLoading={isLoading}
        onRowClick={(id) => navigate(`/performance/${id}`)} />
    </div>
  );
}
