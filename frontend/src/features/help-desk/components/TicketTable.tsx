import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { EmptyState } from '../../../shared/components/ui';
import TicketStatusBadge from './TicketStatusBadge';
import { formatDateTime } from '../../../shared/utils/format';
import type { TicketDto } from '../types';

interface TicketTableProps {
  tickets: TicketDto[];
}

export default function TicketTable({ tickets }: TicketTableProps) {
  if (tickets.length === 0) {
    return <EmptyState icon="pi pi-question-circle" title="No tickets found" description="Raise a ticket to get support from the admin team" />;
  }

  return (
    <DataTable
      value={tickets}
      responsiveLayout="scroll"
      emptyMessage="No tickets found"
      rowKey="ticketId"
    >
      <Column
        header="Ticket ID"
        body={(row: TicketDto) => (
          <span style={{ fontSize: 13, fontFamily: 'monospace', color: 'var(--accent-primary)', fontWeight: 600 }}>
            #{row.ticketId}
          </span>
        )}
      />
      <Column field="issueCategory" header="Category" bodyStyle={{ fontWeight: 600, fontSize: 14, color: 'var(--text-primary)' }} />
      <Column field="email" header="Email" bodyStyle={{ fontSize: 13, color: 'var(--text-secondary)' }} />
      <Column
        header="Priority"
        body={(row: TicketDto) => (
          <span
            className="badge"
            style={{
              background: row.priority === 'High' ? 'var(--badge-red-bg)' : row.priority === 'Low' ? 'var(--badge-emerald-bg)' : 'var(--badge-amber-bg)',
              color: row.priority === 'High' ? 'var(--badge-red-text)' : row.priority === 'Low' ? 'var(--badge-emerald-text)' : 'var(--badge-amber-text)',
            }}
          >
            {row.priority}
          </span>
        )}
      />
      <Column
        header="Status"
        body={(row: TicketDto) => <TicketStatusBadge status={row.status} slaBreached={row.slaBreached} />}
      />
      <Column
        header="SLA Deadline"
        body={(row: TicketDto) => (
          <span style={{ fontSize: 12, color: row.slaBreached ? 'var(--badge-red-text)' : 'var(--text-muted)', fontWeight: row.slaBreached ? 600 : 400 }}>
            {row.slaDeadline ? formatDateTime(row.slaDeadline) : '-'}
          </span>
        )}
      />
      <Column
        header="Created"
        body={(row: TicketDto) => <span style={{ fontSize: 12, color: 'var(--text-muted)' }}>{formatDateTime(row.createdOn)}</span>}
      />
    </DataTable>
  );
}
