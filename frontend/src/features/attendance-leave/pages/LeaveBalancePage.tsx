import { useLeaveBalance } from '../queries';
import { useAuth } from '../../auth';
import LeaveBalanceCard from '../components/LeaveBalanceCard';
import PageHeader from '../../../shared/components/ui/PageHeader';

export default function LeaveBalancePage() {
  const { user } = useAuth();
  const currentYear = new Date().getFullYear();
  const { data: balances, isLoading } = useLeaveBalance(user?.userAccountId ?? 0, currentYear);

  return (
    <div>
      <PageHeader
        title="Leave Balance"
        subtitle={`View your leave balance for ${currentYear}`}
      />

      {isLoading ? (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: 16 }}>
          {[1, 2, 3].map((n) => (
            <div key={n} className="skeleton" style={{ height: 160, borderRadius: 12 }} />
          ))}
        </div>
      ) : balances && balances.length > 0 ? (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: 16 }}>
          {balances.map((balance) => (
            <LeaveBalanceCard key={balance.leaveBalanceId} balance={balance} />
          ))}
        </div>
      ) : (
        <div className="empty-state">
          <i className="pi pi-wallet" />
          <h3>No leave balance data</h3>
          <p>Leave balance information is not available.</p>
        </div>
      )}
    </div>
  );
}
