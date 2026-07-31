import { useState } from 'react';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { useAuth } from '../../auth/useAuth';
import { useBulkApprovalMutation } from '../queries';
import { AppButton, ConfirmDialog } from '../../../shared/components/ui';

interface BulkApprovalToolbarProps {
  selectedIds: number[];
  onClearSelection: () => void;
}

export default function BulkApprovalToolbar({ selectedIds, onClearSelection }: BulkApprovalToolbarProps) {
  const { user } = useAuth();
  const toast = useRef<Toast>(null);
  const bulkApproval = useBulkApprovalMutation();
  const [confirmVisible, setConfirmVisible] = useState(false);
  const [confirmAction, setConfirmAction] = useState<'Approved' | 'Rejected'>('Approved');

  const handleBulkAction = async (action: 'Approved' | 'Rejected') => {
    await bulkApproval.mutateAsync({
      applicantIds: selectedIds,
      approvedBy: user?.userAccountId ?? 0,
      action,
    });
    toast.current?.show({
      severity: action === 'Approved' ? 'success' : 'info',
      summary: action === 'Approved' ? 'Approved' : 'Rejected',
      detail: `${selectedIds.length} registration(s) ${action.toLowerCase()}`,
    });
    setConfirmVisible(false);
    onClearSelection();
  };

  if (selectedIds.length === 0) return null;

  return (
    <>
      <Toast ref={toast} />
      <div
        style={{
          display: 'flex',
          alignItems: 'center',
          gap: 12,
          padding: '12px 16px',
          background: 'var(--blue-50)',
          border: '1px solid var(--blue-200)',
          borderRadius: 8,
          marginBottom: 16,
        }}
      >
        <span style={{ fontSize: 14, fontWeight: 500, color: 'var(--blue-700)' }}>
          {selectedIds.length} selected
        </span>
        <AppButton
          size="sm"
          onClick={() => {
            setConfirmAction('Approved');
            setConfirmVisible(true);
          }}
          loading={bulkApproval.isPending}
        >
          Bulk Approve
        </AppButton>
        <AppButton
          size="sm"
          variant="danger"
          onClick={() => {
            setConfirmAction('Rejected');
            setConfirmVisible(true);
          }}
          loading={bulkApproval.isPending}
        >
          Bulk Reject
        </AppButton>
        <AppButton size="sm" variant="ghost" onClick={onClearSelection}>
          Clear
        </AppButton>
      </div>

      <ConfirmDialog
        visible={confirmVisible}
        header={`${confirmAction === 'Approved' ? 'Approve' : 'Reject'} Registrations`}
        message={`Are you sure you want to ${confirmAction.toLowerCase()} ${selectedIds.length} registration(s)?`}
        onConfirm={() => handleBulkAction(confirmAction)}
        onCancel={() => setConfirmVisible(false)}
        loading={bulkApproval.isPending}
      />
    </>
  );
}
