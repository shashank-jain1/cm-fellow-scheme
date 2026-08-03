import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { AppDialog, AppInput } from '../../../shared/components/forms';
import { useListRegistrationsQuery, useApproveRegistrationMutation, useRejectRegistrationMutation } from '../queries';
import { PageHeader, AppButton } from '../../../shared/components/ui';
import DocumentVerificationTable from './DocumentVerificationTable';
import DocumentVerificationFilters from './DocumentVerificationFilters';
import BulkApprovalToolbar from '../components/BulkApprovalToolbar';

export default function DocumentVerificationPage() {
  const [search, setSearch] = useState('');
  const [rejectDialogVisible, setRejectDialogVisible] = useState(false);
  const [rejectTarget, setRejectTarget] = useState<{ id: number; name: string } | null>(null);
  const [rejectReason, setRejectReason] = useState('');
  const [selectedIds, setSelectedIds] = useState<number[]>([]);
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
    await rejectMutation.mutateAsync({ applicantId: rejectTarget.id, rejectedBy: 1, reason: rejectReason });
    toast.current?.show({ severity: 'info', summary: 'Rejected', detail: 'Registration rejected' });
    setRejectDialogVisible(false); setRejectTarget(null); setRejectReason('');
  };

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader title="Document Verification" subtitle="Review and verify applicant documents" />
      <BulkApprovalToolbar selectedIds={selectedIds} onClearSelection={() => setSelectedIds([])} />
      <DocumentVerificationFilters search={search} onSearchChange={setSearch} />
      <DocumentVerificationTable data={pendingDocs} isLoading={isLoading}
        selectedIds={selectedIds}
        onSelectionChange={setSelectedIds}
        onApprove={handleApprove}
        onReject={(id, name) => { setRejectTarget({ id, name }); setRejectDialogVisible(true); }} />
      <AppDialog header="Reject Registration" visible={rejectDialogVisible}
        style={{ width: '420px' }} modal onHide={() => setRejectDialogVisible(false)}>
        <p style={{ marginBottom: 12, color: 'var(--text-secondary)' }}>
          Reject registration for <strong>{rejectTarget?.name}</strong>?
        </p>
        <div className="form-field">
          <label>Reason *</label>
          <AppInput value={rejectReason} onChange={(e) => setRejectReason(e.target.value)}
            placeholder="Enter rejection reason" style={{ width: '100%' }} />
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 16 }}>
          <AppButton variant="secondary" onClick={() => setRejectDialogVisible(false)}>Cancel</AppButton>
          <AppButton variant="danger" onClick={handleReject} disabled={!rejectReason.trim()}>Reject</AppButton>
        </div>
      </AppDialog>
    </div>
  );
}
