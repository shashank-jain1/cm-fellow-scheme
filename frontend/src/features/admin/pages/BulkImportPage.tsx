import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { FileUpload } from 'primereact/fileupload';
import { useBulkImportMutation } from '../../registration/queries';
import { PageHeader, AppButton } from '../../../shared/components/ui';

export default function BulkImportPage() {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const toast = useRef<Toast>(null);
  const importMutation = useBulkImportMutation();

  const handleImport = async () => {
    if (!selectedFile) {
      toast.current?.show({ severity: 'warn', summary: 'No file', detail: 'Select a file to import' });
      return;
    }
    try {
      const result = await importMutation.mutateAsync(selectedFile);
      const imported = result?.imported ?? 0;
      const errors = result?.errors ?? [];
      toast.current?.show({
        severity: errors.length > 0 ? 'warn' : 'success',
        summary: 'Import Complete',
        detail: `${imported} user(s) imported${errors.length > 0 ? `. ${errors.length} error(s).` : ''}`,
      });
      setSelectedFile(null);
    } catch (err) {
      toast.current?.show({
        severity: 'error',
        summary: 'Import Failed',
        detail: err instanceof Error ? err.message : 'Import failed',
      });
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader title="Bulk Import Users" subtitle="Import users from an Excel or CSV file" />
      <div className="card" style={{ padding: 24 }}>
        <FileUpload
          name="importFile"
          accept=".xlsx,.xls,.csv"
          maxFileSize={10_000_000}
          chooseLabel="Select File"
          uploadOptions={{ showUploadButton: false }}
          cancelOptions={{ showCancelButton: false }}
          onSelect={(e) => setSelectedFile(e.files[0])}
          onClear={() => setSelectedFile(null)}
          style={{ marginBottom: 16 }}
        />
        <AppButton
          onClick={handleImport}
          disabled={!selectedFile || importMutation.isPending}
          loading={importMutation.isPending}
        >
          <i className="pi pi-upload" style={{ marginRight: 8 }} />
          {importMutation.isPending ? 'Importing...' : 'Import Users'}
        </AppButton>
      </div>
    </div>
  );
}
