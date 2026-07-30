import type { LeaveBalanceDto } from '../types';

interface LeaveBalanceCardProps {
  balance: LeaveBalanceDto;
}

export default function LeaveBalanceCard({ balance }: LeaveBalanceCardProps) {
  const percentage = balance.totalDays > 0
    ? Math.round((balance.usedDays / balance.totalDays) * 100)
    : 0;

  return (
    <div
      style={{
        padding: 20,
        border: '1px solid var(--border-color)',
        borderRadius: 12,
        background: 'var(--surface-card)',
        display: 'flex',
        flexDirection: 'column',
        gap: 12,
      }}
    >
      <div style={{ fontSize: 14, fontWeight: 600, color: 'var(--text-primary)' }}>
        {balance.leaveTypeName}
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 8, fontSize: 13 }}>
        <div>
          <div style={{ color: 'var(--text-secondary)' }}>Total</div>
          <div style={{ fontWeight: 600, color: 'var(--text-primary)' }}>{balance.totalDays}</div>
        </div>
        <div>
          <div style={{ color: 'var(--text-secondary)' }}>Used</div>
          <div style={{ fontWeight: 600, color: 'var(--red-600)' }}>{balance.usedDays}</div>
        </div>
        <div>
          <div style={{ color: 'var(--text-secondary)' }}>Remaining</div>
          <div style={{ fontWeight: 600, color: 'var(--emerald-600)' }}>{balance.remainingDays}</div>
        </div>
      </div>

      <div>
        <div style={{ display: 'flex', justifyContent: 'space-between', fontSize: 12, color: 'var(--text-secondary)', marginBottom: 4 }}>
          <span>Usage</span>
          <span>{percentage}%</span>
        </div>
        <div style={{ width: '100%', height: 6, background: 'var(--navy-100)', borderRadius: 3, overflow: 'hidden' }}>
          <div
            style={{
              width: `${percentage}%`,
              height: '100%',
              background: percentage > 80 ? 'var(--red-500)' : 'var(--emerald-500)',
              borderRadius: 3,
              transition: 'width 0.3s ease',
            }}
          />
        </div>
      </div>
    </div>
  );
}
