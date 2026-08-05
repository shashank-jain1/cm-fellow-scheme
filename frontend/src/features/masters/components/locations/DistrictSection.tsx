import { useState } from 'react';
import { useDivisions, useDistricts, useCreateDistrict, useUpdateDistrict, useDeleteDistrict } from '../../queries';
import LocationSection from './LocationSection';

export default function DistrictSection() {
  const { data: divisions } = useDivisions();
  const [selectedParentId, setSelectedParentId] = useState<number | null>(null);
  const { data: items, isLoading } = useDistricts(selectedParentId ?? undefined);
  const createMutation = useCreateDistrict();
  const updateMutation = useUpdateDistrict();
  const deleteMutation = useDeleteDistrict();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const parentOptions = (divisions ?? []).map((d) => ({ label: `${d.divisionCode ?? ''} - ${d.divisionName}`, value: String(d.divisionId) }));
  const getParentName = (id: number) => (divisions ?? []).find((d) => d.divisionId === id)?.divisionName ?? String(id);

  const handleCreate = async () => {
    if (!formName.trim() || !formCode.trim()) return;
    const parentId = selectedParentId ?? (divisions ?? [])[0]?.divisionId;
    if (!parentId) return;
    await createMutation.mutateAsync({ divisionId: parentId, districtName: formName, districtCode: formCode });
    setFormName(''); setFormCode(''); setShowForm(false);
  };

  const handleUpdate = async (id: number, name: string, code: string, parentId: number) => {
    await updateMutation.mutateAsync({ id, data: { districtId: id, divisionId: parentId, districtName: name, districtCode: code } });
  };

  return (
    <LocationSection title="Districts" parentOptions={parentOptions} selectedParentId={selectedParentId}
      onParentChange={setSelectedParentId} items={items ?? []} isLoading={isLoading}
      formName={formName} formCode={formCode} onFormNameChange={setFormName} onFormCodeChange={setFormCode}
      onCreate={handleCreate} showForm={showForm} onToggleForm={() => setShowForm(!showForm)}
      parentLabel="Division" idKey="districtId" nameKey="districtName" codeKey="districtCode"
      parentIdKey="divisionId" getParentName={getParentName} nameLabel="District Name *"
      codeLabel="District Code *" addButtonLabel="Add District"
      namePlaceholder="e.g. Bhopal" codePlaceholder="e.g. BPL"
      onUpdate={handleUpdate} onDelete={(id) => deleteMutation.mutateAsync(id)}
      onUpdateLoading={updateMutation.isPending} onDeleteLoading={deleteMutation.isPending} />
  );
}
