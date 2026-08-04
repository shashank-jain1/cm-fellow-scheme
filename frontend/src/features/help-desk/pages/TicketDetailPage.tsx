import { useParams } from 'react-router-dom';
import { useTicketDetail } from '../queries';
import TicketStatusBadge from '../components/TicketStatusBadge';
import SlaIndicator from '../components/SlaIndicator';
import TicketResolutionForm from '../components/TicketResolutionForm';
import TicketDetailInfo from '../components/TicketDetailInfo';
import TicketDetailDescription from '../components/TicketDetailDescription';
import SatisfactionSurvey from '../components/SatisfactionSurvey';
import { SkeletonTable } from '../../../shared/components/ui';

export default function TicketDetailPage() {
  const { id } = useParams<{ id: string }>();
  const ticketId = Number(id);
  const { data: ticket, isLoading } = useTicketDetail(ticketId);

  if (isLoading) {
    return <SkeletonTable columns={4} />;
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
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>{ticket.issueCategory}</p>
        </div>
        <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
          <SlaIndicator status={ticket.slaBreached ? 'Breached' : 'On Track'} />
          <TicketStatusBadge status={ticket.status} slaBreached={ticket.slaBreached} />
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20 }}>
        <TicketDetailInfo ticket={ticket} />
        <TicketDetailDescription ticket={ticket} />
      </div>

      <div style={{ marginTop: 24 }}>
        <TicketResolutionForm ticketId={ticket.ticketId} currentStatus={ticket.status} />
      </div>

      {(ticket.status === 'Resolved' || ticket.status === 'Closed') && (
        <SatisfactionSurvey ticketId={ticket.ticketId} />
      )}
    </div>
  );
}
