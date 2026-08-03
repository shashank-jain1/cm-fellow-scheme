import type { SurveyDetailDto } from '../types';
import SurveyTable from './SurveyTable';

function SurveyLoadingSkeleton() {
  return (
    <div style={{ padding: 20 }}>
      {[1, 2, 3, 4, 5].map((n) => (
        <div key={n} style={{ display: 'flex', gap: 16, padding: '14px 0', borderBottom: '1px solid var(--border-light)' }}>
          <div className="skeleton" style={{ width: '15%', height: 14 }} />
          <div className="skeleton" style={{ width: '15%', height: 14 }} />
          <div className="skeleton" style={{ width: '12%', height: 14 }} />
          <div className="skeleton" style={{ width: '18%', height: 14 }} />
          <div className="skeleton" style={{ width: '12%', height: 14 }} />
        </div>
      ))}
    </div>
  );
}

interface SurveyDetailsDrilldownGridProps {
  data: SurveyDetailDto[];
  isLoading?: boolean;
  workProject?: string;
}

export default function SurveyDetailsDrilldownGrid({
  data,
  isLoading = false,
  workProject,
}: SurveyDetailsDrilldownGridProps) {
  if (isLoading) return <SurveyLoadingSkeleton />;

  if (data.length === 0) {
    return (
      <div style={{ padding: 20, textAlign: 'center', color: 'var(--text-secondary)' }}>
        No survey records found
      </div>
    );
  }

  return (
    <div style={{ padding: '16px 0' }}>
      {workProject && (
        <div style={{ padding: '0 16px 12px', fontWeight: 600, fontSize: 14 }}>
          Survey Records for: {workProject}
        </div>
      )}
      <SurveyTable data={data} />
    </div>
  );
}
