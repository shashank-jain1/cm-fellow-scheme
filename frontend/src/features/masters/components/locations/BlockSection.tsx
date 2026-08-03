import { useState } from 'react';
import { useDistricts, useBlocks, useCreateBlock } from '../../queries';
import LocationSection from './LocationSection';

export default function BlockSection() {
  const { data: districts } = useDistricts();
  const [selectedParentId, setSelectedParentId] = useState<number | null>(null);
  const { data: items, isLoading } = useBlocks(selectedParentId ?? undefined);
  const createMutation = useCreateBlock();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const parentOptions = (districts ?? []).map((d) => ({ label: `${d.districtCode ?? ''} - ${d.districtName}`, value: String(d.districtId) }));
  const getParentName = (id: number) => (districts ?? []).find((d) => d.districtId === id)?.districtName ?? String(id);

  const handleCreate = async () => {
    if (!formName.trim() || !formCode.trim()) return;
    const parentId = selectedParentId ?? (districts ?? [])[0]?.districtId;
    if (!parentId) return;
    await createMutation.mutateAsync({ districtId: parentId, blockName: formName, blockCode: formCode });
    setFormName(''); setFormCode(''); setShowForm(false);
  };

  return (
    <LocationSection title="Blocks" parentOptions={parentOptions} selectedParentId={selectedParentId}
      onParentChange={setSelectedParentId} items={items ?? []} isLoading={isLoading}
      formName={formName} formCode={formCode} onFormNameChange={setFormName} onFormCodeChange={setFormCode}
      onCreate={handleCreate} showForm={showForm} onToggleForm={() => setShowForm(!showForm)}
      parentLabel="District" idKey="blockId" nameKey="blockName" codeKey="blockCode"
      parentIdKey="districtId" getParentName={getParentName} nameLabel="Block Name *"
      codeLabel="Block Code *" addButtonLabel="Add Block"
      namePlaceholder="e.g. Huzur" codePlaceholder="e.g. HZ" />
  );
}
