import { AppTextarea } from '../../../shared/components/forms';
import AppButton from '../../../shared/components/ui/AppButton';

interface LeaveApprovalActionsProps {
  leaveApplicationId: number;
  remarks: string;
  onRemarksChange: (value: string) => void;
  onApprove: (id: number) => void;
  onReject: (id: number) => void;
  isPending: boolean;
}

export default function LeaveApprovalActions({
  leaveApplicationId,
  remarks,
  onRemarksChange,
  onApprove,
  onReject,
  isPending,
}: LeaveApprovalActionsProps) {
  return (
    <>
      <AppTextarea
        value={remarks}
        onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onRemarksChange(e.target.value)}
        placeholder="Add remarks (optional)"
        rows={2}
        style={{ width: '100%', marginBottom: 12 }}
      />

      <div style={{ display: 'flex', gap: 12 }}>
        <AppButton
          loading={isPending}
          onClick={() => onApprove(leaveApplicationId)}
          icon="pi pi-check"
        >
          Approve
        </AppButton>
        <AppButton
          variant="danger"
          loading={isPending}
          onClick={() => onReject(leaveApplicationId)}
          icon="pi pi-times"
        >
          Reject
        </AppButton>
      </div>
    </>
  );
}
