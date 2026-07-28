import type { TicketDto } from '../types';

interface TicketDetailDescriptionProps {
  ticket: TicketDto;
}

export default function TicketDetailDescription({ ticket }: TicketDetailDescriptionProps) {
  return (
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
  );
}
