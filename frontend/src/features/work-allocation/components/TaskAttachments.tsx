import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { useTaskAttachments, useUploadTaskAttachment } from '../queries';
import AttachmentUploadForm from './AttachmentUploadForm';
import AttachmentList from './AttachmentList';

interface TaskAttachmentsProps {
  taskProgressId: number;
}

export default function TaskAttachments({ taskProgressId }: TaskAttachmentsProps) {
  const toast = useRef<Toast>(null);
  const fileInputRef = useRef<HTMLInputElement>(null);
  const { data: attachments, isLoading } = useTaskAttachments(taskProgressId);
  const uploadMutation = useUploadTaskAttachment();
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
      await uploadMutation.mutateAsync({ taskProgressId, file: selectedFile });
      toast.current?.show({ severity: 'success', summary: 'Success', detail: 'File uploaded successfully' });
      setSelectedFile(null);
      if (fileInputRef.current) fileInputRef.current.value = '';
    } catch {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: 'Failed to upload file' });
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <AttachmentUploadForm fileInputRef={fileInputRef} onFileSelect={handleFileSelect} onUpload={handleUpload} isPending={uploadMutation.isPending} hasFile={!!selectedFile} />
      <AttachmentList attachments={attachments ?? []} isLoading={isLoading} />
    </div>
  );
}
