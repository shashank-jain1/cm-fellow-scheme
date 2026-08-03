import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { ProgressSpinner } from 'primereact/progressspinner';
import ApiService from '../../../services/ApiService';
import { PageHeader, AppButton } from '../../../shared/components/ui';

interface BackupStatus {
  status: string;
  fileName?: string;
  initiatedOn?: string;
}

export default function BackupPage() {
  const [backupStatus, setBackupStatus] = useState<BackupStatus | null>(null);
  const [loading, setLoading] = useState(false);
  const toast = useRef<Toast>(null);

  const handleBackup = async () => {
    setLoading(true);
    try {
      const res = await ApiService.post<BackupStatus>('admin/backup', {});
      setBackupStatus(res.data ?? { status: 'Completed' });
      toast.current?.show({ severity: 'success', summary: 'Backup Complete', detail: 'Database backup created successfully' });
    } catch (err) {
      toast.current?.show({
        severity: 'error',
        summary: 'Backup Failed',
        detail: err instanceof Error ? err.message : 'Backup failed',
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader title="System Backup" subtitle="Create and manage database backups" />
      <div className="card" style={{ padding: 24 }}>
        <p style={{ marginBottom: 16, color: 'var(--text-secondary)' }}>
          Create a full backup of the system database. This may take a few minutes.
        </p>
        <AppButton onClick={handleBackup} disabled={loading}>
          {loading ? <ProgressSpinner style={{ width: 16, height: 16, marginRight: 8 }} /> : <i className="pi pi-download" style={{ marginRight: 8 }} />}
          {loading ? 'Creating Backup...' : 'Create Backup'}
        </AppButton>

        {backupStatus && (
          <div style={{ marginTop: 24, padding: 16, background: 'var(--surface-50)', borderRadius: 8, border: '1px solid var(--border)' }}>
            <h3 style={{ fontSize: 15, fontWeight: 600, marginBottom: 8 }}>Last Backup Status</h3>
            <div style={{ fontSize: 14, color: 'var(--text-secondary)' }}>
              <div>Status: <strong>{backupStatus.status}</strong></div>
              {backupStatus.fileName && <div>File: {backupStatus.fileName}</div>}
              {backupStatus.initiatedOn && <div>Initiated: {backupStatus.initiatedOn}</div>}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
