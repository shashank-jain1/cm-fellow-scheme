import { useState } from 'react';
import { AppButton } from '../../../shared/components/ui';
import { formatDate } from '../../../shared/utils/format';
import { downloadMaterial } from '../api';
import { ToastService } from '../../../shared/utils/toast';
import type { TrainingMaterial } from '../types';

interface MaterialDownloadListProps {
  materials: TrainingMaterial[];
  isLoading: boolean;
}

function formatFileSize(bytes: number): string {
  if (!bytes) return '—';
  if (bytes < 1024) return `${bytes} B`;
  if (bytes < 1024 * 1024) return `${(bytes / 1024).toFixed(1)} KB`;
  return `${(bytes / (1024 * 1024)).toFixed(1)} MB`;
}

export default function MaterialDownloadList({ materials, isLoading }: MaterialDownloadListProps) {
  const [downloadingId, setDownloadingId] = useState<number | null>(null);

  const handleDownload = async (material: TrainingMaterial) => {
    setDownloadingId(material.trainingMaterialId);
    try {
      await downloadMaterial(material.trainingMaterialId, material.materialName);
    } catch (err) {
      ToastService.error(err instanceof Error ? err.message : 'Could not download the material.');
    } finally {
      setDownloadingId(null);
    }
  };

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
        <div key={material.trainingMaterialId} style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', padding: '12px 16px', borderBottom: '1px solid var(--border-light)' }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: 12, flex: 1, minWidth: 0 }}>
            <i className="pi pi-file" style={{ fontSize: 18, color: 'var(--text-muted)' }} />
            <div style={{ minWidth: 0 }}>
              <div style={{ fontSize: 14, fontWeight: 500, overflow: 'hidden', textOverflow: 'ellipsis' }}>{material.materialName}</div>
              <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>
                {formatFileSize(material.fileSize)} &middot; Uploaded {formatDate(material.uploadedOn)}
              </div>
            </div>
          </div>
          <AppButton
            size="sm"
            variant="ghost"
            icon="pi pi-download"
            loading={downloadingId === material.trainingMaterialId}
            onClick={() => handleDownload(material)}
            title="Download"
          />
        </div>
      ))}
    </div>
  );
}
