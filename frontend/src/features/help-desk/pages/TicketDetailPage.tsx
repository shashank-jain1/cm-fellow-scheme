import { useParams } from 'react-router-dom';
import { useTicketDetail } from '../queries';
import TicketStatusBadge from '../components/TicketStatusBadge';
import TicketResolutionForm from '../components/TicketResolutionForm';
import { formatDateTime } from '../../../shared/utils/format';

export default function TicketDetailPage() {
  const { id } = useParams<{ id: string }>();
  const ticketId = Number(id);
  const { data: ticket, isLoading } = useTicketDetail(ticketId);

  if (isLoading) {
    return (
      <div style={{ padding: 24 }}>
        <div className="skeleton" style={{ width: 200, height: 24, marginBottom: 16 }} />
        <div className="skeleton" style={{ width: '100%', height: 200 }} />
      </div>
    );
  }

  if (!ticket) {
    return (
      <div className="empty-state">
        <i className="pi pi-exclamation-circle" />
        <h3>Ticket not found</h3>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Ticket #{ticket.ticketId}</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            {ticket.issueCategory}
          </p>
        </div>
        <TicketStatusBadge status={ticket.status} />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20 }}>
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

        <div className="card" style={{ padding: 24 }}>
          <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Description</h3>
          <p style={{ fontSize: 14, color: 'var(--text-secondary)', lineHeight: 1.6 }}>
            {ticket.issueDescription}
          </p>
          {ticket.resolutionRemarks && (
            <div style={{ marginTop: 20, padding: 14, background: 'var(--emerald-50)', borderRadius: 'var(--radius-md)' }}>
              <div style={{ fontSize: 11, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.5px', marginBottom: 6 }}>
                Resolution Remarks
              </div>
              <div style={{ fontSize: 13, color: 'var(--text-secondary)' }}>{ticket.resolutionRemarks}</div>
            </div>
          )}
        </div>
      </div>

      <div style={{ marginTop: 24 }}>
        <TicketResolutionForm
          ticketId={ticket.ticketId}
          currentStatus={ticket.status}
        />
      </div>
    </div>
  );
}
