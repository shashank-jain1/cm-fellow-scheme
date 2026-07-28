import { formatDateTime } from '../../../shared/utils/format';
import type { TicketDto } from '../types';

interface TicketDetailInfoProps {
  ticket: TicketDto;
}

export default function TicketDetailInfo({ ticket }: TicketDetailInfoProps) {
  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Details</h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
        <div>
          <div style={{ fontSize: 11, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.5px', marginBottom: 4 }}>
            Email
          </div>
          <div style={{ fontSize: 14 }}>{ticket.email}</div>
        </div>
        <div>
          <div style={{ fontSize: 11, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.5px', marginBottom: 4 }}>
            Mobile
          </div>
          <div style={{ fontSize: 14 }}>{ticket.mobile}</div>
        </div>
        <div>
          <div style={{ fontSize: 11, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.5px', marginBottom: 4 }}>
            Priority
          </div>
          <div style={{ fontSize: 14 }}>{ticket.priority}</div>
        </div>
        <div>
          <div style={{ fontSize: 11, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.5px', marginBottom: 4 }}>
            Created
          </div>
          <div style={{ fontSize: 14 }}>{formatDateTime(ticket.createdOn)}</div>
        </div>
        {ticket.slaDeadline && (
          <div>
            <div style={{ fontSize: 11, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.5px', marginBottom: 4 }}>
              SLA Deadline
            </div>
            <div style={{ fontSize: 14, color: ticket.slaBreached ? 'var(--badge-red-text)' : 'var(--text-primary)', fontWeight: ticket.slaBreached ? 600 : 400 }}>
              {formatDateTime(ticket.slaDeadline)}
              {ticket.slaBreached && ' (Breached)'}
            </div>
          </div>
        )}
        {ticket.closedOn && (
          <div>
            <div style={{ fontSize: 11, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.5px', marginBottom: 4 }}>
              Closed
            </div>
            <div style={{ fontSize: 14 }}>{formatDateTime(ticket.closedOn)}</div>
          </div>
        )}
      </div>
    </div>
  );
}
