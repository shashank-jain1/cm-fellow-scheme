import { useState } from 'react';
import { Tag } from 'primereact/tag';
import { AppInput, AppSelect } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import WorkAllocationForm from '../components/WorkAllocationForm';
import { useWorkAllocationForm } from '../components/form.hook';
import {
  useWorkAllocations,
  useCreateWorkAllocation,
  useUpdateWorkAllocation,
  useDeleteWorkAllocation,
} from '../queries';
import type { WorkAllocationDto } from '../types';

const statusOptions = [
  { label: 'All Status', value: '' },
  { label: 'Active', value: 'active' },
  { label: 'Pending', value: 'pending' },
  { label: 'Completed', value: 'completed' },
];

const prioritySeverity = (p: string) => {
  switch (p.toLowerCase()) {
    case 'high': return 'danger';
    case 'medium': return 'warning';
    case 'low': return 'info';
    default: return 'secondary';
  }
};

export default function WorkAllocationPage() {
  const [search, setSearch] = useState('');
  const [statusFilter, setStatusFilter] = useState('');
  const [showForm, setShowForm] = useState(false);
  const [editingAllocation, setEditingAllocation] = useState<WorkAllocationDto | null>(null);

  const { data: allocations, isLoading } = useWorkAllocations();
  const createMutation = useCreateWorkAllocation();
  const updateMutation = useUpdateWorkAllocation();
  const deleteMutation = useDeleteWorkAllocation();

  const { formData, errors, handleChange, validate, resetForm, setFormDataForEdit } = useWorkAllocationForm();

  const filtered = (allocations ?? []).filter((a) => {
    const matchSearch =
      a.workDescription.toLowerCase().includes(search.toLowerCase());
    const matchStatus = !statusFilter || a.status.toLowerCase() === statusFilter.toLowerCase();
    return matchSearch && matchStatus;
  });

  const handleNewAllocation = () => {
    resetForm();
    setEditingAllocation(null);
    setShowForm(true);
  };

  const handleEditAllocation = (allocation: WorkAllocationDto) => {
    setFormDataForEdit({
      projectId: allocation.projectId,
      workProjectId: allocation.workProjectId,
      workDescription: allocation.workDescription,
      priority: allocation.priority,
      startDate: allocation.startDate,
      endDate: allocation.endDate,
      surveysPerIntern: allocation.surveysPerIntern,
      divisionId: 0,
      districtId: 0,
      blockId: 0,
    });
    setEditingAllocation(allocation);
    setShowForm(true);
  };

  const handleDeleteAllocation = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this allocation?')) {
      await deleteMutation.mutateAsync(id);
    }
  };

  const handleSubmit = async () => {
    if (!validate()) return;

    try {
      if (editingAllocation) {
        await updateMutation.mutateAsync({
          id: editingAllocation.workAllocationId,
          data: formData,
        });
      } else {
        await createMutation.mutateAsync(formData);
      }
      setShowForm(false);
      resetForm();
    } catch (error) {
      console.error('Error saving allocation:', error);
    }
  };

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Work Allocation</h1>
          <p>Manage work allocations for CM Fellows</p>
        </div>
        <AppButton
          icon="pi pi-plus"
          onClick={handleNewAllocation}
        >
          New Allocation
        </AppButton>
      </div>

      {showForm && (
        <div className="card" style={{ padding: 'var(--space-6)', marginBottom: 'var(--space-5)' }}>
          <h3 style={{ marginTop: 0, marginBottom: 'var(--space-5)' }}>
            {editingAllocation ? 'Edit Work Allocation' : 'Create New Work Allocation'}
          </h3>
          <WorkAllocationForm
            formData={formData}
            errors={errors}
            onChange={handleChange}
            onSubmit={handleSubmit}
            onCancel={() => {
              setShowForm(false);
              resetForm();
            }}
            isLoading={createMutation.isPending || updateMutation.isPending}
            isEditing={!!editingAllocation}
          />
        </div>
      )}

      <div style={{ display: 'flex', gap: 'var(--space-3)', marginBottom: 'var(--space-5)', alignItems: 'center' }}>
        <div className="search-input-wrapper" style={{ flex: '0 0 320px' }}>
          <i className="pi pi-search" />
          <AppInput
            value={search}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearch(e.target.value)}
            placeholder="Search by description..."
            style={{ width: '100%' }}
          />
        </div>
        <AppSelect
          value={statusFilter}
          onChange={(val: string) => setStatusFilter(val)}
          options={statusOptions}
          placeholder="Filter by status"
          showClear
          style={{ width: 180 }}
        />
      </div>

      <div className="table-wrapper">
        {isLoading ? (
          <div style={{ padding: 'var(--space-4)' }}>
            {[1, 2, 3, 4, 5].map((n) => (
              <div key={n} style={{ display: 'flex', gap: 16, padding: '12px 0', borderBottom: '1px solid var(--border)' }}>
                <div className="skeleton" style={{ width: '15%', height: 14 }} />
                <div className="skeleton" style={{ width: '20%', height: 14 }} />
                <div className="skeleton" style={{ width: '15%', height: 14 }} />
                <div className="skeleton" style={{ width: '12%', height: 14 }} />
                <div className="skeleton" style={{ width: '10%', height: 14 }} />
              </div>
            ))}
          </div>
        ) : filtered.length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr>
                {['Description', 'Priority', 'Duration', 'Surveys', 'Completion', 'Status', 'Actions'].map((h) => (
                  <th
                    key={h}
                    style={{
                      padding: '10px 16px',
                      textAlign: 'left',
                      fontSize: 12,
                      fontWeight: 600,
                      color: 'var(--text-muted)',
                      textTransform: 'uppercase',
                      letterSpacing: '0.04em',
                      borderBottom: '1px solid var(--border)',
                      background: 'var(--carbon-50)',
                    }}
                  >
                    {h}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              {filtered.map((a) => (
                <tr key={a.workAllocationId} style={{ borderBottom: '1px solid var(--border)' }}>
                  <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14, color: 'var(--text-heading)' }}>
                    {a.workDescription}
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    <Tag value={a.priority} severity={prioritySeverity(a.priority)} />
                  </td>
                  <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-muted)' }}>
                    {a.startDate} - {a.endDate}
                  </td>
                  <td style={{ padding: '12px 16px', fontSize: 14 }}>
                    {a.surveysPerIntern}
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                      <div style={{ width: 60, height: 4, borderRadius: 2, background: 'var(--border)' }}>
                        <div
                          style={{
                            width: `${a.completionPercentage}%`,
                            height: '100%',
                            borderRadius: 2,
                            background: a.completionPercentage >= 80 ? 'var(--success)' : 'var(--pending)',
                          }}
                        />
                      </div>
                      <span style={{ fontSize: 12, color: 'var(--text-muted)' }}>
                        {a.completionPercentage}%
                      </span>
                    </div>
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    <Tag
                      value={a.status}
                      severity={a.status === 'active' ? 'success' : a.status === 'pending' ? 'warning' : 'secondary'}
                    />
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    <div style={{ display: 'flex', gap: 8 }}>
                      <AppButton
                        variant="ghost"
                        size="sm"
                        icon="pi pi-pencil"
                        onClick={() => handleEditAllocation(a)}
                        title="Edit"
                      />
                      <AppButton
                        variant="ghost"
                        size="sm"
                        icon="pi pi-trash"
                        onClick={() => handleDeleteAllocation(a.workAllocationId)}
                        title="Delete"
                      />
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <div className="empty-state">
            <i className="pi pi-briefcase" />
            <h3>No allocations found</h3>
            <p>Create work allocations to assign tasks to CM Fellows</p>
          </div>
        )}
      </div>
    </div>
  );
}
