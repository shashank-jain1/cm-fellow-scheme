import LeaveApprovalQueue from '../components/LeaveApprovalQueue';
import PageHeader from '../../../shared/components/ui/PageHeader';

export default function LeaveApprovalPage() {
  return (
    <div>
      <PageHeader
        title="Leave Approvals"
        subtitle="Review and approve pending leave requests"
      />

      <LeaveApprovalQueue />
    </div>
  );
}
