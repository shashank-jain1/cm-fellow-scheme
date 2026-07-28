import TicketStatusBadge from './TicketStatusBadge';
import { formatDateTime } from '../../../shared/utils/format';
import { EmptyState } from '../../../shared/components/ui';
import type { TicketDto } from '../types';

interface TicketTableProps {
  tickets: TicketDto[];
}

const tableHeaders = ['Ticket ID', 'Category', 'Email', 'Priority', 'Status', 'SLA Deadline', 'Created'];

export default function TicketTable({ tickets }: TicketTableProps) {
  if (tickets.length === 0) {
    return <EmptyState icon="pi pi-question-circle" title="No tickets found" description="Raise a ticket to get support from the admin team" />;
  }

  return (
    <table style={{ width: '100%', borderCollapse: 'collapse' }}>
      <thead>
        <tr style={{ background: 'var(--bg-primary)' }}>
          {tableHeaders.map((h) => (
            <th
              key={h}
              style={{
                padding: '12px 16px',
                textAlign: 'left',
                fontSize: 12,
                fontWeight: 700,
                color: 'var(--text-secondary)',
                textTransform: 'uppercase',
                letterSpacing: '0.5px',
                borderBottom: '1px solid var(--border-color)',
              }}
            >
              {h}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {tickets.map((t) => (
          <tr key={t.ticketId} style={{ borderBottom: '1px solid var(--border-light)' }}>
            <td style={{ padding: '14px 16px', fontSize: 13, fontFamily: 'monospace', color: 'var(--accent-primary)', fontWeight: 600 }}>
              #{t.ticketId}
            </td>
            <td style={{ padding: '14px 16px', fontWeight: 600, fontSize: 14, color: 'var(--text-primary)' }}>{t.issueCategory}</td>
            <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{t.email}</td>
            <td style={{ padding: '14px 16px' }}>
              <span
                className="badge"
                style={{
                  background: t.priority === 'High' ? 'var(--badge-red-bg)' : t.priority === 'Low' ? 'var(--badge-emerald-bg)' : 'var(--badge-amber-bg)',
                  color: t.priority === 'High' ? 'var(--badge-red-text)' : t.priority === 'Low' ? 'var(--badge-emerald-text)' : 'var(--badge-amber-text)',
                }}
              >
                {t.priority}
              </span>
            </td>
            <td style={{ padding: '14px 16px' }}>
              <TicketStatusBadge status={t.status} slaBreached={t.slaBreached} />
            </td>
            <td style={{ padding: '14px 16px', fontSize: 12, color: t.slaBreached ? 'var(--badge-red-text)' : 'var(--text-muted)', fontWeight: t.slaBreached ? 600 : 400 }}>
              {t.slaDeadline ? formatDateTime(t.slaDeadline) : '-'}
            </td>
            <td style={{ padding: '14px 16px', fontSize: 12, color: 'var(--text-muted)' }}>{formatDateTime(t.createdOn)}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
