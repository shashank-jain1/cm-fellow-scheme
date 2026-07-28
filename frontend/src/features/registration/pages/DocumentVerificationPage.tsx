import { useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { useListRegistrationsQuery, useApproveRegistrationMutation, useRejectRegistrationMutation } from '../queries';
import { PageHeader, EmptyState, AppButton, SearchInput } from '../../../shared/components/ui';

export default function DocumentVerificationPage() {
  const [search, setSearch] = useState('');
  const [rejectDialogVisible, setRejectDialogVisible] = useState(false);
  const [rejectTarget, setRejectTarget] = useState<{ id: number; name: string } | null>(null);
  const [rejectReason, setRejectReason] = useState('');
  const toast = useRef<Toast>(null);

  const { data: result, isLoading } = useListRegistrationsQuery();
  const registrations = result?.items ?? [];
  const approveMutation = useApproveRegistrationMutation();
  const rejectMutation = useRejectRegistrationMutation();

  const pendingDocs = registrations.filter((r: any) => {
    const matchSearch = !search ||
      `${r.firstName} ${r.lastName}`.toLowerCase().includes(search.toLowerCase()) ||
      r.mobileNumber?.includes(search);
    return matchSearch;
  });

  const handleApprove = async (id: number) => {
    await approveMutation.mutateAsync({ applicantId: id, approvedBy: 1 });
    toast.current?.show({ severity: 'success', summary: 'Approved', detail: 'Registration approved' });
  };

  const handleReject = async () => {
    if (!rejectTarget || !rejectReason.trim()) return;
    await rejectMutation.mutateAsync({
      applicantId: rejectTarget.id,
      rejectedBy: 1,
      reason: rejectReason,
    });
    toast.current?.show({ severity: 'info', summary: 'Rejected', detail: 'Registration rejected' });
    setRejectDialogVisible(false);
    setRejectTarget(null);
    setRejectReason('');
  };

  const statusBody = (row: any) => {
    const severityMap: Record<string, 'success' | 'warning' | 'danger' | 'info' | 'secondary'> = {
      approved: 'success',
      pending: 'warning',
      rejected: 'danger',
    };
    return <Tag value={row.status} severity={severityMap[row.status?.toLowerCase()] ?? 'secondary'} />;
  };

  const docLink = (path: string | null, label: string) =>
    path ? (
      <a href={path} target="_blank" rel="noreferrer" style={{ fontSize: 13 }}>{label}</a>
    ) : (
      <span style={{ color: 'var(--text-muted)', fontSize: 13 }}>Not uploaded</span>
    );

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader
        title="Document Verification"
        subtitle="Review and verify applicant documents"
      />

      <div style={{ marginBottom: 16 }}>
        <SearchInput value={search} onChange={setSearch} placeholder="Search by name or mobile..." style={{ width: 320 }} />
      </div>

      <div className="table-wrapper">
        <DataTable value={pendingDocs} loading={isLoading} rows={10} paginator emptyMessage=" ">
          <Column
            field="firstName"
            header="Applicant"
            body={(row: any) => (
              <div>
                <div style={{ fontWeight: 600, fontSize: 14 }}>{row.firstName} {row.lastName}</div>
                <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>{row.mobileNumber}</div>
              </div>
            )}
          />
          <Column
            header="Documents"
            body={(row: any) => (
              <div style={{ display: 'flex', flexDirection: 'column', gap: 4 }}>
                {docLink(row.photographPath, 'Photograph')}
                {docLink(row.identityProofPath, 'Identity Proof')}
                {docLink(row.educationalCertificatePath, 'Education Cert')}
              </div>
            )}
          />
          <Column field="status" header="Status" body={statusBody} />
          <Column
            header="Actions"
            body={(row: any) => (
              <div style={{ display: 'flex', gap: 8 }}>
                <AppButton size="sm" onClick={() => handleApprove(row.applicantId)}>
                  Approve
                </AppButton>
                <AppButton
                  size="sm"
                  variant="danger"
                  onClick={() => {
                    setRejectTarget({ id: row.applicantId, name: `${row.firstName} ${row.lastName}` });
                    setRejectDialogVisible(true);
                  }}
                >
                  Reject
                </AppButton>
              </div>
            )}
          />
        </DataTable>
        {pendingDocs.length === 0 && !isLoading && (
          <EmptyState icon="pi pi-file-check" title="No registrations found" description="No pending registrations to review" />
        )}
      </div>

      <Dialog
        header="Reject Registration"
        visible={rejectDialogVisible}
        style={{ width: '420px' }}
        modal
        onHide={() => setRejectDialogVisible(false)}
      >
        <p style={{ marginBottom: 12, color: 'var(--text-secondary)' }}>
          Reject registration for <strong>{rejectTarget?.name}</strong>?
        </p>
        <div className="form-field">
          <label>Reason *</label>
          <InputText
            value={rejectReason}
            onChange={(e) => setRejectReason(e.target.value)}
            placeholder="Enter rejection reason"
            style={{ width: '100%' }}
          />
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 16 }}>
          <AppButton variant="secondary" onClick={() => setRejectDialogVisible(false)}>Cancel</AppButton>
          <AppButton variant="danger" onClick={handleReject} disabled={!rejectReason.trim()}>Reject</AppButton>
        </div>
      </Dialog>
    </div>
  );
}
