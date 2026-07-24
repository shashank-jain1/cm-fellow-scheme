import type { PerformanceSummaryDto } from '../types';

interface PerformanceSummaryCardProps {
  record: PerformanceSummaryDto;
}

const gradeColor: Record<string, string> = {
  A: 'var(--emerald-500)',
  B: 'var(--emerald-600)',
  C: 'var(--amber-500)',
  D: 'var(--red-500)',
  F: 'var(--red-600)',
};

export default function PerformanceSummaryCard({ record }: PerformanceSummaryCardProps) {
  const color = gradeColor[record.performanceGrade] ?? 'var(--text-secondary)';

  return (
    <div className="kpi-card" style={{ display: 'flex', alignItems: 'center', gap: 20, padding: 20 }}>
      <div
        style={{
          width: 56,
          height: 56,
          borderRadius: 14,
          background: `${color}18`,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          flexShrink: 0,
        }}
      >
        <span style={{ fontSize: 22, fontWeight: 700, color }}>{record.performanceGrade}</span>
      </div>
      <div style={{ flex: 1, minWidth: 0 }}>
        <div style={{ fontSize: 14, fontWeight: 600, color: 'var(--text-primary)' }}>{record.applicantName}</div>
        <div style={{ fontSize: 12, color: 'var(--text-secondary)', marginTop: 2 }}>{record.projectName}</div>
      </div>
      <div style={{ textAlign: 'right' }}>
        <div style={{ fontSize: 24, fontWeight: 700, color }}>{record.performanceScore}</div>
        <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>{record.completionPercentage}% complete</div>
      </div>
    </div>
  );
}
