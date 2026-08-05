import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { EmptyState } from '../../../shared/components/ui';

const scoreColor = (score: number) => {
  if (score >= 80) return 'var(--filing)';
  if (score >= 60) return 'var(--ledger)';
  return 'var(--seal)';
};
const scoreTag = (score: number): 'success' | 'warning' | 'danger' => {
  if (score >= 80) return 'success';
  if (score >= 60) return 'warning';
  return 'danger';
};
const levelSeverity = (level: string): 'secondary' | 'info' | 'warning' | 'success' => {
  switch (level) {
    case 'Admin': return 'success'; case 'Coordinator': return 'warning';
    case 'Fellow': return 'info'; default: return 'secondary';
  }
};
const statusSeverity = (status: string): 'success' | 'warning' | 'danger' | 'info' | 'secondary' => {
  switch (status) {
    case 'Approved': return 'success'; case 'Under Review': return 'warning';
    case 'Submitted': return 'info'; case 'Rejected': return 'danger'; default: return 'secondary';
  }
};

interface Props {
  records: any[];
  isLoading: boolean;
  onRowClick: (id: number) => void;
}

export default function PerformanceTable({ records, isLoading, onRowClick }: Props) {
  if (isLoading) {
    return (
      <div style={{ padding: 'var(--space-4)' }}>
        {[1, 2, 3, 4, 5].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 16, padding: '10px 0', borderBottom: '1px solid var(--border)' }}>
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
            <div className="skeleton" style={{ width: '12%', height: 14 }} />
            <div className="skeleton" style={{ width: '10%', height: 14 }} />
            <div className="skeleton" style={{ width: '10%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (records.length === 0) {
    return <EmptyState icon="pi pi-chart-bar" title="No performance records" description="Performance data will appear here after reviews" />;
  }

  return (
    <div className="table-wrapper">
      <DataTable
        value={records}
        responsiveLayout="scroll"
        emptyMessage="No performance records"
        rowKey="performanceEvaluationId"
        onRowClick={(e) => onRowClick((e.data as any).performanceEvaluationId)}
        style={{ cursor: 'pointer' }}
      >
        <Column field="applicantName" header="Fellow" bodyStyle={{ fontWeight: 600, fontSize: 13, color: 'var(--text-heading)' }} />
        <Column field="projectName" header="Project" bodyStyle={{ fontSize: 13, color: 'var(--text-body)' }} />
        <Column
          header="Completion"
          body={(row: any) => <Tag value={`${row.completionPercentage}%`} severity={scoreTag(row.completionPercentage)} />}
        />
        <Column
          header="Score"
          body={(row: any) => <span style={{ fontWeight: 700, fontSize: 14, color: scoreColor(row.performanceScore) }}>{row.performanceScore}</span>}
        />
        <Column field="performanceGrade" header="Grade" bodyStyle={{ fontSize: 13, fontWeight: 700, color: 'var(--text-heading)' }} />
        <Column
          header="Level"
          body={(row: any) => <Tag value={row.reviewLevel ?? 'Draft'} severity={levelSeverity(row.reviewLevel ?? 'Draft')} />}
        />
        <Column
          header="Status"
          body={(row: any) => <Tag value={row.reviewStatus ?? 'Draft'} severity={statusSeverity(row.reviewStatus ?? 'Draft')} />}
        />
      </DataTable>
    </div>
  );
}
