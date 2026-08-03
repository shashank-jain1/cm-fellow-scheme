import { useState } from 'react';
import { AppButton } from '../../../shared/components/ui';
import WorkAllocationForm from '../components/WorkAllocationForm';
import { useWorkAllocationForm } from '../components/form.hook';
import { useWorkAllocations, useCreateWorkAllocation, useUpdateWorkAllocation, useDeactivateWorkAllocation } from '../queries';
import type { WorkAllocationDto } from '../types';
import WorkAllocationFilters from './WorkAllocationFilters';
import WorkAllocationTable from './WorkAllocationTable';
import WorkAllocationStats from './WorkAllocationStats';

export default function WorkAllocationPage() {
  const [search, setSearch] = useState('');
  const [statusFilter, setStatusFilter] = useState('');
  const [showForm, setShowForm] = useState(false);
  const [editingAllocation, setEditingAllocation] = useState<WorkAllocationDto | null>(null);

  const { data: allocations, isLoading } = useWorkAllocations();
  const createMutation = useCreateWorkAllocation();
  const updateMutation = useUpdateWorkAllocation();
  const deactivateMutation = useDeactivateWorkAllocation();
  const { formData, errors, handleChange, validate, resetForm, setFormDataForEdit } = useWorkAllocationForm();

  const filtered = (allocations ?? []).filter((a) => {
    const matchSearch = a.workDescription.toLowerCase().includes(search.toLowerCase());
    const matchStatus = !statusFilter || a.status.toLowerCase() === statusFilter.toLowerCase();
    return matchSearch && matchStatus;
  });

  const handleNewAllocation = () => { resetForm(); setEditingAllocation(null); setShowForm(true); };

  const handleEditAllocation = (allocation: WorkAllocationDto) => {
    setFormDataForEdit({ projectId: allocation.projectId, workProjectId: allocation.workProjectId, workDescription: allocation.workDescription, priority: allocation.priority, startDate: allocation.startDate, endDate: allocation.endDate, surveysPerIntern: allocation.surveysPerIntern, divisionId: 0, districtId: 0, blockId: 0 });
    setEditingAllocation(allocation); setShowForm(true);
  };

  const handleDeactivateAllocation = async (id: number) => {
    if (window.confirm('Are you sure you want to deactivate this allocation?'))
      await deactivateMutation.mutateAsync(id);
  };

  const handleSubmit = async () => {
    if (!validate()) return;
    try {
      if (editingAllocation) await updateMutation.mutateAsync({ id: editingAllocation.workAllocationId, data: formData });
      else await createMutation.mutateAsync(formData);
      setShowForm(false); resetForm();
    } catch (error) { console.error('Error saving allocation:', error); }
  };

  return (
    <div>
      <div className="page-header">
        <div><h1>Work Allocation</h1><p>Manage work allocations for CM Fellows</p></div>
        <AppButton icon="pi pi-plus" onClick={handleNewAllocation}>New Allocation</AppButton>
      </div>
      {showForm && (
        <div className="card" style={{ padding: 'var(--space-6)', marginBottom: 'var(--space-5)' }}>
          <h3 style={{ marginTop: 0, marginBottom: 'var(--space-5)' }}>{editingAllocation ? 'Edit Work Allocation' : 'Create New Work Allocation'}</h3>
          <WorkAllocationForm formData={formData} errors={errors} onChange={handleChange} onSubmit={handleSubmit} onCancel={() => { setShowForm(false); resetForm(); }} isLoading={createMutation.isPending || updateMutation.isPending} isEditing={!!editingAllocation} />
        </div>
      )}
      <WorkAllocationStats allocations={allocations ?? []} completedCount={(allocations ?? []).filter(a => a.status === 'completed').length} totalCount={(allocations ?? []).length} />
      <WorkAllocationFilters search={search} statusFilter={statusFilter} onSearchChange={setSearch} onStatusChange={setStatusFilter} />
      <div className="table-wrapper"><WorkAllocationTable allocations={filtered} isLoading={isLoading} onEdit={handleEditAllocation} onDelete={handleDeactivateAllocation} /></div>
    </div>
  );
}
