import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { useUploadTrainingMaterial } from '../queries';
import type { TrainingMaterial } from '../types';
import MaterialUploadForm from './MaterialUploadForm';
import MaterialDownloadList from './MaterialDownloadList';

interface MaterialListProps {
  trainingScheduleId: number;
  materials: TrainingMaterial[];
  isLoading?: boolean;
}

export default function MaterialList({ trainingScheduleId, materials, isLoading }: MaterialListProps) {
  const toast = useRef<Toast>(null);
  const fileInputRef = useRef<HTMLInputElement>(null);
  const uploadMutation = useUploadTrainingMaterial();
  const [selectedFile, setSelectedFile] = useState<File | null>(null);

  const handleFileSelect = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) setSelectedFile(file);
  };

  const handleUpload = async () => {
    if (!selectedFile) {
      toast.current?.show({ severity: 'warn', summary: 'Validation', detail: 'Select a file to upload' });
      return;
    }
    try {
      await uploadMutation.mutateAsync({ trainingScheduleId, file: selectedFile });
      toast.current?.show({ severity: 'success', summary: 'Success', detail: 'Material uploaded successfully' });
      setSelectedFile(null);
      if (fileInputRef.current) fileInputRef.current.value = '';
    } catch {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: 'Failed to upload material' });
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <MaterialUploadForm fileInputRef={fileInputRef} onFileSelect={handleFileSelect} onUpload={handleUpload} isPending={uploadMutation.isPending} hasFile={!!selectedFile} />
      <MaterialDownloadList materials={materials} isLoading={!!isLoading} />
    </div>
  );
}
