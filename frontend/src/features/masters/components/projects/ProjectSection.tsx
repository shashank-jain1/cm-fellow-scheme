import { useState } from 'react';
import { Button } from 'primereact/button';
import { useProjects, useCreateProject } from '../../queries';
import type { CreateProjectCommand } from '../../types';
import ProjectForm from './ProjectForm';
import ProjectTable from './ProjectTable';

const emptyForm: CreateProjectCommand = {
  projectName: '',
  projectCode: '',
  projectDescription: '',
  departmentName: '',
  startDate: '',
  endDate: '',
  projectIncharge: '',
  budgetAmount: undefined,
};

export default function ProjectSection() {
  const { data: projects, isLoading } = useProjects();
  const createMutation = useCreateProject();
  const [showForm, setShowForm] = useState(false);
  const [form, setForm] = useState<CreateProjectCommand>(emptyForm);

  const handleCreate = async () => {
    if (
      !form.projectName ||
      !form.projectCode ||
      !form.departmentName ||
      !form.startDate ||
      !form.endDate ||
      !form.projectIncharge
    )
      return;
    await createMutation.mutateAsync(form);
    setForm(emptyForm);
    setShowForm(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>
          Projects ({projects?.length ?? 0})
        </h3>
        <Button
          label="Add Project"
          icon="pi pi-plus"
          className="btn btn-primary"
          size="small"
          onClick={() => setShowForm(!showForm)}
        />
      </div>
      {showForm && (
        <ProjectForm
          form={form}
          setForm={setForm}
          onSubmit={handleCreate}
          onCancel={() => {
            setShowForm(false);
            setForm(emptyForm);
          }}
          isPending={createMutation.isPending}
        />
      )}
      <div className="table-wrapper">
        <ProjectTable projects={projects ?? []} isLoading={isLoading} />
      </div>
    </div>
  );
}
