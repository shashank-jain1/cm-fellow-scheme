import { useState } from 'react';
import { useStates, useDivisions, useCreateDivision, useUpdateDivision, useDeleteDivision } from '../../queries';
import LocationSection from './LocationSection';

export default function DivisionSection() {
  const { data: states } = useStates();
  const [selectedParentId, setSelectedParentId] = useState<number | null>(null);
  const { data: items, isLoading } = useDivisions(selectedParentId ?? undefined);
  const createMutation = useCreateDivision();
  const updateMutation = useUpdateDivision();
  const deleteMutation = useDeleteDivision();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const parentOptions = (states ?? []).map((s) => ({ label: `${s.stateCode} - ${s.stateName}`, value: String(s.stateId) }));
  const getParentName = (id: number) => (states ?? []).find((s) => s.stateId === id)?.stateName ?? String(id);

  const handleCreate = async () => {
    if (!formName.trim() || !formCode.trim()) return;
    const parentId = selectedParentId ?? (states ?? [])[0]?.stateId;
    if (!parentId) return;
    await createMutation.mutateAsync({ stateId: parentId, divisionName: formName, divisionCode: formCode });
    setFormName(''); setFormCode(''); setShowForm(false);
  };

  const handleUpdate = async (id: number, name: string, code: string, parentId: number) => {
    await updateMutation.mutateAsync({ id, data: { divisionId: id, stateId: parentId, divisionName: name, divisionCode: code } });
  };

  return (
    <LocationSection title="Divisions" parentOptions={parentOptions} selectedParentId={selectedParentId}
      onParentChange={setSelectedParentId} items={items ?? []} isLoading={isLoading}
      formName={formName} formCode={formCode} onFormNameChange={setFormName} onFormCodeChange={setFormCode}
      onCreate={handleCreate} showForm={showForm} onToggleForm={() => setShowForm(!showForm)}
      parentLabel="State" idKey="divisionId" nameKey="divisionName" codeKey="divisionCode"
      parentIdKey="stateId" getParentName={getParentName} nameLabel="Division Name *"
      codeLabel="Division Code *" addButtonLabel="Add Division"
      namePlaceholder="e.g. Bhopal Division" codePlaceholder="e.g. BPL"
      onUpdate={handleUpdate} onDelete={(id) => deleteMutation.mutateAsync(id)}
      onUpdateLoading={updateMutation.isPending} onDeleteLoading={deleteMutation.isPending} />
  );
}
