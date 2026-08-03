import { useState } from 'react';
import { Button } from 'primereact/button';
import { AppSelect } from '../../../../shared/components/forms';
import { useWorks, useProjects, useCreateWork } from '../../queries';
import { useLookupOptions } from '../../../../shared/hooks/useMasters';
import type { CreateWorkCommand } from '../../types';
import WorkForm from './WorkForm';
import WorkTable from './WorkTable';

const emptyForm: CreateWorkCommand = {
  projectId: 0,
  workName: '',
  workDescription: '',
  priority: 'Medium',
  startDate: '',
  endDate: '',
  assignedTo: '',
  remarks: '',
};

export default function WorkSection() {
  const { data: projects } = useProjects();
  const [selectedProjectId, setSelectedProjectId] = useState<number | null>(null);
  const { data: works, isLoading } = useWorks(selectedProjectId ?? undefined);
  const createMutation = useCreateWork();
  const [showForm, setShowForm] = useState(false);
  const [form, setForm] = useState<CreateWorkCommand>(emptyForm);
  const priorityOptions = useLookupOptions('Priority');

  const projectOptions = (projects ?? []).map((p) => ({
    label: `${p.projectCode} - ${p.projectName}`,
    value: String(p.projectId),
  }));

  const handleCreate = async () => {
    if (!form.projectId || !form.workName || !form.startDate || !form.endDate || !form.assignedTo) return;
    await createMutation.mutateAsync(form);
    setForm(emptyForm);
    setShowForm(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>Works ({works?.length ?? 0})</h3>
        <div style={{ display: 'flex', gap: 12, alignItems: 'center' }}>
          <AppSelect
            value={selectedProjectId ? String(selectedProjectId) : ''}
            options={[{ label: 'All Projects', value: '' }, ...projectOptions]}
            onChange={(val) => setSelectedProjectId(val ? Number(val) : null)}
            style={{ width: 240 }}
          />
          <Button
            label="Add Work"
            icon="pi pi-plus"
            className="btn btn-primary"
            size="small"
            onClick={() => setShowForm(!showForm)}
          />
        </div>
      </div>
      {showForm && (
        <WorkForm
          form={form}
          setForm={setForm}
          onSubmit={handleCreate}
          onCancel={() => {
            setShowForm(false);
            setForm(emptyForm);
          }}
          isPending={createMutation.isPending}
          projectOptions={projectOptions}
          priorityOptions={priorityOptions}
        />
      )}
      <div className="table-wrapper">
        <WorkTable
          works={works ?? []}
          projects={projects ?? []}
          isLoading={isLoading}
          selectedProjectId={selectedProjectId}
        />
      </div>
    </div>
  );
}
