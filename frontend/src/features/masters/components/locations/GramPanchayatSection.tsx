import { useState } from 'react';
import { useBlocks, useGramPanchayats, useCreateGramPanchayat } from '../../queries';
import LocationSection from './LocationSection';

export default function GramPanchayatSection() {
  const { data: blocks } = useBlocks();
  const [selectedParentId, setSelectedParentId] = useState<number | null>(null);
  const { data: items, isLoading } = useGramPanchayats(selectedParentId ?? undefined);
  const createMutation = useCreateGramPanchayat();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const parentOptions = (blocks ?? []).map((b) => ({ label: `${b.blockCode ?? ''} - ${b.blockName}`, value: String(b.blockId) }));
  const getParentName = (id: number) => (blocks ?? []).find((b) => b.blockId === id)?.blockName ?? String(id);

  const handleCreate = async () => {
    if (!formName.trim()) return;
    const parentId = selectedParentId ?? (blocks ?? [])[0]?.blockId;
    if (!parentId) return;
    await createMutation.mutateAsync({ blockId: parentId, gramPanchayatName: formName, gpCode: formCode || undefined });
    setFormName(''); setFormCode(''); setShowForm(false);
  };

  return (
    <LocationSection title="Gram Panchayats" parentOptions={parentOptions} selectedParentId={selectedParentId}
      onParentChange={setSelectedParentId} items={items ?? []} isLoading={isLoading}
      formName={formName} formCode={formCode} onFormNameChange={setFormName} onFormCodeChange={setFormCode}
      onCreate={handleCreate} showForm={showForm} onToggleForm={() => setShowForm(!showForm)}
      parentLabel="Block" idKey="gramPanchayatId" nameKey="gramPanchayatName" codeKey="gpCode"
      parentIdKey="blockId" getParentName={getParentName} nameLabel="GP Name *"
      codeLabel="GP Code" codeRequired={false} addButtonLabel="Add Gram Panchayat"
      namePlaceholder="e.g. Gram Panchayat name" codePlaceholder="Optional code" />
  );
}
