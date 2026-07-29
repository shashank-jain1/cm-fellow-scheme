import type { PerformanceSummaryDto } from '../types';

interface PerformanceSummaryCardProps {
  record: PerformanceSummaryDto;
}

const gradeColor: Record<string, { color: string; bg: string }> = {
  A: { color: 'var(--success)', bg: 'var(--success-light)' },
  B: { color: 'var(--kpi-2, var(--success))', bg: 'var(--success-light)' },
  C: { color: 'var(--pending)', bg: 'var(--pending-light)' },
  D: { color: 'var(--danger)', bg: 'var(--danger-light)' },
  F: { color: 'var(--danger)', bg: 'var(--danger-light)' },
};

export default function PerformanceSummaryCard({ record }: PerformanceSummaryCardProps) {
  const g = gradeColor[record.performanceGrade] ?? { color: 'var(--text-muted)', bg: 'var(--carbon-50)' };

  return (
    <div className="card" style={{ display: 'flex', alignItems: 'center', gap: 'var(--space-5)', padding: 'var(--space-5)' }}>
      <div
        style={{
          width: 52,
          height: 52,
          borderRadius: 'var(--radius-lg)',
          background: g.bg,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          flexShrink: 0,
        }}
      >
        <span style={{ fontFamily: 'var(--font-display)', fontSize: 24, fontWeight: 700, color: g.color }}>{record.performanceGrade}</span>
      </div>
      <div style={{ flex: 1, minWidth: 0 }}>
        <div style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-base)', fontWeight: 600, color: 'var(--text-heading)' }}>{record.applicantName}</div>
        <div style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-sm)', color: 'var(--text-muted)', marginTop: 2 }}>{record.projectName}</div>
      </div>
      <div style={{ textAlign: 'right' }}>
        <div style={{ fontFamily: 'var(--font-display)', fontSize: 'var(--text-xl)', fontWeight: 700, color: g.color }}>{record.performanceScore}</div>
        <div style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-xs)', color: 'var(--text-muted)' }}>{record.completionPercentage}% complete</div>
      </div>
    </div>
  );
}
