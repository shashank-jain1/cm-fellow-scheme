import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { Calendar } from 'primereact/calendar';
import { Button } from 'primereact/button';
import { InputNumber } from 'primereact/inputnumber';
import { useProjects, useCreateProject } from '../../queries';
import type { CreateProjectCommand } from '../../types';

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
    if (!form.projectName || !form.projectCode || !form.departmentName || !form.startDate || !form.endDate || !form.projectIncharge) return;
    await createMutation.mutateAsync(form);
    setForm(emptyForm);
    setShowForm(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>Projects ({projects?.length ?? 0})</h3>
        <Button label="Add Project" icon="pi pi-plus" className="btn btn-primary" size="small" onClick={() => setShowForm(!showForm)} />
      </div>

      {showForm && (
        <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
          <div className="form-grid">
            <div className="form-field">
              <label>Project Name *</label>
              <InputText value={form.projectName} onChange={(e) => setForm({ ...form, projectName: e.target.value })} placeholder="Enter project name" />
            </div>
            <div className="form-field">
              <label>Project Code *</label>
              <InputText value={form.projectCode} onChange={(e) => setForm({ ...form, projectCode: e.target.value })} placeholder="e.g. PRJ001" />
            </div>
            <div className="form-field">
              <label>Department *</label>
              <InputText value={form.departmentName} onChange={(e) => setForm({ ...form, departmentName: e.target.value })} placeholder="Department name" />
            </div>
            <div className="form-field">
              <label>Project Incharge *</label>
              <InputText value={form.projectIncharge} onChange={(e) => setForm({ ...form, projectIncharge: e.target.value })} placeholder="Incharge name" />
            </div>
            <div className="form-field">
              <label>Start Date *</label>
              <Calendar value={form.startDate ? new Date(form.startDate) : null} onChange={(e) => setForm({ ...form, startDate: e.value?.toISOString().split('T')[0] ?? '' })} showOnFocus={false} />
            </div>
            <div className="form-field">
              <label>End Date *</label>
              <Calendar value={form.endDate ? new Date(form.endDate) : null} onChange={(e) => setForm({ ...form, endDate: e.value?.toISOString().split('T')[0] ?? '' })} showOnFocus={false} />
            </div>
            <div className="form-field">
              <label>Budget Amount</label>
              <InputNumber value={form.budgetAmount ?? undefined} onValueChange={(e) => setForm({ ...form, budgetAmount: e.value ?? undefined })} mode="currency" currency="INR" locale="en-IN" />
            </div>
            <div className="form-field full-width">
              <label>Description</label>
              <InputTextarea value={form.projectDescription ?? ''} onChange={(e) => setForm({ ...form, projectDescription: e.target.value })} rows={3} placeholder="Project description" />
            </div>
          </div>
          <div style={{ display: 'flex', gap: 8, justifyContent: 'flex-end', marginTop: 12 }}>
            <Button label="Cancel" className="btn btn-secondary" size="small" onClick={() => { setShowForm(false); setForm(emptyForm); }} />
            <Button label="Create" className="btn btn-primary" size="small" onClick={handleCreate} loading={createMutation.isPending} />
          </div>
        </div>
      )}

      <div className="table-wrapper">
        {isLoading ? (
          <div style={{ padding: 20 }}>{[1, 2, 3].map((n) => <div key={n} className="skeleton" style={{ height: 40, marginBottom: 8 }} />)}</div>
        ) : (projects ?? []).length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                {['Code', 'Name', 'Department', 'Incharge', 'Duration', 'Budget', 'Status'].map((h) => (
                  <th key={h} style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', letterSpacing: '0.5px', borderBottom: '1px solid var(--border-color)' }}>{h}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {(projects ?? []).map((p) => (
                <tr key={p.projectId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                  <td style={{ padding: '12px 16px', fontFamily: 'monospace', fontSize: 13, fontWeight: 600, color: 'var(--accent-primary)' }}>{p.projectCode}</td>
                  <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>{p.projectName}</td>
                  <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{p.departmentName}</td>
                  <td style={{ padding: '12px 16px', fontSize: 13 }}>{p.projectIncharge}</td>
                  <td style={{ padding: '12px 16px', fontSize: 12, color: 'var(--text-muted)' }}>{new Date(p.startDate).toLocaleDateString()} - {new Date(p.endDate).toLocaleDateString()}</td>
                  <td style={{ padding: '12px 16px', fontSize: 13 }}>{p.budgetAmount ? `₹${p.budgetAmount.toLocaleString()}` : '-'}</td>
                  <td style={{ padding: '12px 16px' }}>
                    <span className="badge" style={{ background: p.isActive ? 'var(--badge-emerald-bg)' : 'var(--badge-red-bg)', color: p.isActive ? 'var(--badge-emerald-text)' : 'var(--badge-red-text)', padding: '4px 10px', borderRadius: 12, fontSize: 12, fontWeight: 600 }}>
                      {p.isActive ? 'Active' : 'Inactive'}
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-briefcase" /><h3>No projects yet</h3><p>Create your first project to get started</p></div>
        )}
      </div>
    </div>
  );
}
