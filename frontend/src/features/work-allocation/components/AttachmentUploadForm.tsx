import React from 'react';
import { AppButton } from '../../../shared/components/ui';

interface AttachmentUploadFormProps {
  fileInputRef: React.RefObject<HTMLInputElement | null>;
  onFileSelect: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onUpload: () => void;
  isPending: boolean;
  hasFile: boolean;
}

export default function AttachmentUploadForm({ fileInputRef, onFileSelect, onUpload, isPending, hasFile }: AttachmentUploadFormProps) {
  return (
    <div style={{ marginBottom: 20, padding: 16, background: 'var(--surface-ground)', borderRadius: 8 }}>
      <h4 style={{ marginTop: 0, marginBottom: 12, fontSize: 14, fontWeight: 600 }}>Upload Deliverable</h4>
      <div style={{ display: 'flex', gap: 12, alignItems: 'flex-end' }}>
        <div style={{ flex: 1 }}>
          <input ref={fileInputRef} type="file" onChange={onFileSelect} style={{ fontSize: 13, width: '100%' }} />
        </div>
        <AppButton onClick={onUpload} loading={isPending} disabled={!hasFile} size="sm" icon="pi pi-upload">Upload</AppButton>
      </div>
    </div>
  );
}
