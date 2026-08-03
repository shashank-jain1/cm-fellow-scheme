import { useState } from 'react';
import { AppTextarea } from '../../../shared/components/forms';
import { Button } from 'primereact/button';
import { useResolveTicket, useEscalateTicket, useCloseTicket } from '../queries';

interface TicketResolutionFormProps {
  ticketId: number;
  currentStatus: string;
  onResolved?: () => void;
}

export default function TicketResolutionForm({ ticketId, currentStatus, onResolved }: TicketResolutionFormProps) {
  const [remarks, setRemarks] = useState('');
  const resolveMutation = useResolveTicket();
  const escalateMutation = useEscalateTicket();
  const closeMutation = useCloseTicket();

  const handleClose = async () => {
    await closeMutation.mutateAsync({ id: ticketId, resolutionRemarks: remarks });
    setRemarks('');
    onResolved?.();
  };

  const handleResolve = async () => {
    await resolveMutation.mutateAsync({ id: ticketId, resolutionRemarks: remarks });
    setRemarks('');
    onResolved?.();
  };

  const handleEscalate = async () => {
    await escalateMutation.mutateAsync(ticketId);
    onResolved?.();
  };

  const isOpen = currentStatus === 'open' || currentStatus === 'in_progress';

  if (!isOpen) {
    return (
      <div className="card" style={{ padding: 24 }}>
        <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 12 }}>Resolution</h3>
        <p style={{ fontSize: 13, color: 'var(--text-muted)' }}>
          This ticket has been {currentStatus}.
        </p>
      </div>
    );
  }

  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Resolve / Escalate</h3>
      <div className="form-group" style={{ marginBottom: 16 }}>
        <label className="form-label">Resolution Remarks</label>
        <AppTextarea
          value={remarks}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setRemarks(e.target.value)}
          placeholder="Enter resolution details..."
          rows={3}
          style={{ width: '100%', resize: 'vertical' }}
        />
      </div>
      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end' }}>
        <Button
          label="Escalate"
          icon="pi pi-arrow-up"
          className="btn btn-secondary"
          onClick={handleEscalate}
          disabled={escalateMutation.isPending}
          loading={escalateMutation.isPending}
        />
        <Button
          label="Close"
          icon="pi pi-times"
          className="btn btn-secondary"
          onClick={handleClose}
          disabled={closeMutation.isPending}
          loading={closeMutation.isPending}
        />
        <Button
          label="Mark Resolved"
          icon="pi pi-check"
          className="btn btn-primary"
          onClick={handleResolve}
          disabled={!remarks.trim() || resolveMutation.isPending}
          loading={resolveMutation.isPending}
        />
      </div>
    </div>
  );
}
