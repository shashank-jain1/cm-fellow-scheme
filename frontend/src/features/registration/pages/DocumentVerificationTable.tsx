import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { AppButton, EmptyState, SkeletonTable } from '../../../shared/components/ui';

interface Props {
  data: any[];
  isLoading: boolean;
  selectedIds: number[];
  onSelectionChange: (ids: number[]) => void;
  onApprove: (id: number) => void;
  onReject: (id: number, name: string) => void;
}

const statusBody = (row: any) => {
  const severityMap: Record<string, 'success' | 'warning' | 'danger' | 'info' | 'secondary'> = {
    approved: 'success', pending: 'warning', rejected: 'danger',
  };
  return <Tag value={row.status} severity={severityMap[row.status?.toLowerCase()] ?? 'secondary'} />;
};

const docLink = (path: string | null, label: string) =>
  path ? (
    <a href={path} target="_blank" rel="noreferrer" style={{ fontSize: 13 }}>{label}</a>
  ) : (
    <span style={{ color: 'var(--text-muted)', fontSize: 13 }}>Not uploaded</span>
  );

export default function DocumentVerificationTable({ data, isLoading, selectedIds, onSelectionChange, onApprove, onReject }: Props) {
  return (
    <div className="table-wrapper">
      {isLoading ? (
        <SkeletonTable columns={4} />
      ) : data.length > 0 ? (
        <DataTable
          value={data}
          rows={10}
          paginator
          emptyMessage=" "
          selection={data.filter((r) => selectedIds.includes(r.applicantId))}
          onSelectionChange={(e) => onSelectionChange(e.value.map((r: any) => r.applicantId))}
          selectionMode="multiple"
          dataKey="applicantId"
        >
          <Column field="firstName" header="Applicant"
            body={(row: any) => (
              <div>
                <div style={{ fontWeight: 600, fontSize: 14 }}>{row.firstName} {row.lastName}</div>
                <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>{row.mobileNumber}</div>
              </div>
            )} />
          <Column header="Documents"
            body={(row: any) => (
              <div style={{ display: 'flex', flexDirection: 'column', gap: 4 }}>
                {docLink(row.photographPath, 'Photograph')}
                {docLink(row.identityProofPath, 'Identity Proof')}
                {docLink(row.educationalCertificatePath, 'Education Cert')}
              </div>
            )} />
          <Column field="status" header="Status" body={statusBody} />
          <Column header="Actions"
            body={(row: any) => (
              <div style={{ display: 'flex', gap: 8 }}>
                <AppButton size="sm" onClick={() => onApprove(row.applicantId)}>Approve</AppButton>
                <AppButton size="sm" variant="danger"
                  onClick={() => onReject(row.applicantId, `${row.firstName} ${row.lastName}`)}>Reject</AppButton>
              </div>
            )} />
        </DataTable>
      ) : (
        <EmptyState icon="pi pi-file-check" title="No registrations found" description="No pending registrations to review" />
      )}
    </div>
  );
}
