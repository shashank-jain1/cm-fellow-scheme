import { Dialog } from 'primereact/dialog';
import AppButton from './AppButton';

interface ConfirmDialogProps {
  visible: boolean;
  header: string;
  message: string;
  onConfirm: () => void;
  onCancel: () => void;
  loading?: boolean;
}

export default function ConfirmDialog({ visible, header, message, onConfirm, onCancel, loading }: ConfirmDialogProps) {
  return (
    <Dialog header={header} visible={visible} style={{ width: '400px' }} modal onHide={onCancel}>
      <p style={{ marginBottom: 20, color: 'var(--text-secondary)' }}>{message}</p>
      <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8 }}>
        <AppButton variant="secondary" onClick={onCancel}>Cancel</AppButton>
        <AppButton variant="danger" onClick={onConfirm} loading={loading}>Confirm</AppButton>
      </div>
    </Dialog>
  );
}
