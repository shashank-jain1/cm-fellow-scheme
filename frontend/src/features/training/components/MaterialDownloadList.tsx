import { AppButton } from '../../../shared/components/ui';
import { formatDate } from '../../../shared/utils/format';
import type { TrainingMaterial } from '../types';

interface MaterialDownloadListProps {
  materials: TrainingMaterial[];
  isLoading: boolean;
}

export default function MaterialDownloadList({ materials, isLoading }: MaterialDownloadListProps) {
  if (isLoading) {
    return (
      <div style={{ padding: 12 }}>
        {[1, 2, 3].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 12, padding: '10px 0', borderBottom: '1px solid var(--border-light)' }}>
            <div className="skeleton" style={{ width: '40%', height: 14 }} />
            <div className="skeleton" style={{ width: '25%', height: 14 }} />
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (materials.length === 0) {
    return (
      <div style={{ padding: 24, textAlign: 'center', color: 'var(--text-muted)', fontSize: 14 }}>No materials uploaded yet</div>
    );
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 0 }}>
      {materials.map((material) => (
        <div key={material.materialId} style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', padding: '12px 16px', borderBottom: '1px solid var(--border-light)' }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: 12, flex: 1 }}>
            <i className="pi pi-file" style={{ fontSize: 18, color: 'var(--text-muted)' }} />
            <div>
              <div style={{ fontSize: 14, fontWeight: 500 }}>{material.fileName}</div>
              <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>
                Uploaded by {material.uploadedByName} on {formatDate(material.uploadedAt)}
              </div>
            </div>
          </div>
          <AppButton size="sm" variant="ghost" icon="pi pi-download" onClick={() => window.open(material.fileUrl, '_blank')} title="Download" />
        </div>
      ))}
    </div>
  );
}
