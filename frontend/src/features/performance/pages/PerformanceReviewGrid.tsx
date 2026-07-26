import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Tag } from 'primereact/tag';
import { usePerformanceSummary } from '../queries';
import PerformanceSummaryCard from '../components/PerformanceSummaryCard';

const scoreColor = (score: number) => {
  if (score >= 80) return 'var(--badge-emerald-text)';
  if (score >= 60) return 'var(--badge-amber-text)';
  return 'var(--badge-red-text)';
};

const scoreTag = (score: number): 'success' | 'warning' | 'danger' => {
  if (score >= 80) return 'success';
  if (score >= 60) return 'warning';
  return 'danger';
};

export default function PerformanceReviewGrid() {
  const [search, setSearch] = useState('');
  const { data: records, isLoading } = usePerformanceSummary();

  const filtered = (records ?? []).filter((r) =>
    r.applicantName.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Performance Tracking</h1>
          <p>Monitor and evaluate CM Fellow performance metrics</p>
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(280px, 1fr))', gap: 16, marginBottom: 24 }}>
        {isLoading
          ? [1, 2, 3].map((n) => (
              <div key={n} className="kpi-card" style={{ padding: 20 }}>
                <div className="skeleton" style={{ width: '60%', height: 16, marginBottom: 12 }} />
                <div className="skeleton" style={{ width: '40%', height: 28 }} />
              </div>
            ))
          : (records ?? []).slice(0, 3).map((r) => (
              <PerformanceSummaryCard key={r.performanceEvaluationId} record={r} />
            ))}
      </div>

      <div className="search-input-wrapper" style={{ width: '100%', maxWidth: 360, marginBottom: 24 }}>
        <i className="pi pi-search" />
        <InputText
          value={search}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearch(e.target.value)}
          placeholder="Search by fellow name..."
          style={{ width: '100%' }}
        />
      </div>

      <div className="table-wrapper">
        {isLoading ? (
          <div style={{ padding: 20 }}>
            {[1, 2, 3, 4, 5].map((n) => (
              <div key={n} style={{ display: 'flex', gap: 16, padding: '14px 0', borderBottom: '1px solid var(--border-light)' }}>
                <div className="skeleton" style={{ width: '20%', height: 14 }} />
                <div className="skeleton" style={{ width: '20%', height: 14 }} />
                <div className="skeleton" style={{ width: '12%', height: 14 }} />
                <div className="skeleton" style={{ width: '10%', height: 14 }} />
                <div className="skeleton" style={{ width: '10%', height: 14 }} />
              </div>
            ))}
          </div>
        ) : filtered.length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                {['Fellow', 'Project', 'Completion', 'Score', 'Grade', 'Status'].map((h) => (
                  <th
                    key={h}
                    style={{
                      padding: '12px 16px',
                      textAlign: 'left',
                      fontSize: 12,
                      fontWeight: 700,
                      color: 'var(--text-secondary)',
                      textTransform: 'uppercase',
                      letterSpacing: '0.5px',
                      borderBottom: '1px solid var(--border-color)',
                    }}
                  >
                    {h}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              {filtered.map((r) => (
                <tr key={r.performanceEvaluationId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                  <td style={{ padding: '14px 16px', fontWeight: 600, fontSize: 14, color: 'var(--text-primary)' }}>{r.applicantName}</td>
                  <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{r.projectName}</td>
                  <td style={{ padding: '14px 16px' }}>
                    <Tag value={`${r.completionPercentage}%`} severity={scoreTag(r.completionPercentage)} />
                  </td>
                  <td style={{ padding: '14px 16px' }}>
                    <span style={{ fontWeight: 800, fontSize: 15, color: scoreColor(r.performanceScore) }}>
                      {r.performanceScore}
                    </span>
                  </td>
                  <td style={{ padding: '14px 16px', fontSize: 13, fontWeight: 700, color: 'var(--text-primary)' }}>{r.performanceGrade}</td>
                  <td style={{ padding: '14px 16px' }}>
                    <Tag
                      value={r.performanceStatus}
                      severity={r.performanceStatus === 'Completed' ? 'success' : 'warning'}
                    />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <div className="empty-state">
            <i className="pi pi-chart-bar" />
            <h3>No performance records</h3>
            <p>Performance data will appear here after reviews</p>
          </div>
        )}
      </div>
    </div>
  );
}
