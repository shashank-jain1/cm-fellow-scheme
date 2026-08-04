import { useState } from 'react';
import { Button } from 'primereact/button';
import { useProjects, useCreateProject, useUpdateProject, useDeleteProject } from '../../queries';
import type { CreateProjectCommand, ProjectDto } from '../../types';
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
  const updateMutation = useUpdateProject();
  const deleteMutation = useDeleteProject();

  const [showForm, setShowForm] = useState(false);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [form, setForm] = useState<CreateProjectCommand>(emptyForm);

  const handleSave = async () => {
    if (
      !form.projectName ||
      !form.projectCode ||
      !form.departmentName ||
      !form.startDate ||
      !form.endDate ||
      !form.projectIncharge
    )
      return;

    if (editingId) {
      await updateMutation.mutateAsync({ id: editingId, data: form });
    } else {
      await createMutation.mutateAsync(form);
    }
    setForm(emptyForm);
    setEditingId(null);
    setShowForm(false);
  };

  const handleEdit = (p: ProjectDto) => {
    setEditingId(p.projectId);
    setForm({
      projectName: p.projectName,
      projectCode: p.projectCode,
      projectDescription: p.projectDescription ?? '',
      departmentName: p.departmentName,
      startDate: p.startDate ? new Date(p.startDate).toISOString().split('T')[0] : '',
      endDate: p.endDate ? new Date(p.endDate).toISOString().split('T')[0] : '',
      projectIncharge: p.projectIncharge,
      budgetAmount: p.budgetAmount ?? undefined,
    });
    setShowForm(true);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this project?')) {
      await deleteMutation.mutateAsync(id);
    }
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
          onClick={() => {
            setEditingId(null);
            setForm(emptyForm);
            setShowForm(!showForm);
          }}
        />
      </div>
      {showForm && (
        <ProjectForm
          form={form}
          setForm={setForm}
          onSubmit={handleSave}
          onCancel={() => {
            setShowForm(false);
            setEditingId(null);
            setForm(emptyForm);
          }}
          isPending={createMutation.isPending || updateMutation.isPending}
        />
      )}
      <div className="table-wrapper">
        <ProjectTable
          projects={projects ?? []}
          isLoading={isLoading}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      </div>
    </div>
  );
}
