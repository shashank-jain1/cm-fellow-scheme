import { useLeaveBalance } from '../queries';
import LeaveBalanceCard from '../components/LeaveBalanceCard';

export default function LeaveBalancePage() {
  const { data: balances, isLoading } = useLeaveBalance();

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Leave Balance</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            View your leave balance by type
          </p>
        </div>
      </div>

      {isLoading ? (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: 16 }}>
          {[1, 2, 3].map((n) => (
            <div key={n} className="skeleton" style={{ height: 160, borderRadius: 12 }} />
          ))}
        </div>
      ) : balances && balances.length > 0 ? (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: 16 }}>
          {balances.map((balance) => (
            <LeaveBalanceCard key={balance.leaveType} balance={balance} />
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
