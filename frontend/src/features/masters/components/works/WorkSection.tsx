import { useState } from 'react';
import { Button } from 'primereact/button';
import { AppInput, AppTextarea, AppCalendar, AppSelect } from '../../../../shared/components/forms';
import { useWorks, useProjects, useCreateWork } from '../../queries';
import { useLookupOptions } from '../../../../shared/hooks/useMasters';
import type { CreateWorkCommand } from '../../types';

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

  const projectOptions = (projects ?? []).map((p) => ({ label: `${p.projectCode} - ${p.projectName}`, value: String(p.projectId) }));

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
          <Button label="Add Work" icon="pi pi-plus" className="btn btn-primary" size="small" onClick={() => setShowForm(!showForm)} />
        </div>
      </div>

      {showForm && (
        <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
          <div className="form-grid">
            <div className="form-field">
              <label>Project *</label>
              <AppSelect value={form.projectId ? String(form.projectId) : ''} options={projectOptions} onChange={(val) => setForm({ ...form, projectId: Number(val) })} placeholder="Select project" />
            </div>
            <div className="form-field">
              <label>Work Name *</label>
              <AppInput value={form.workName} onChange={(e) => setForm({ ...form, workName: e.target.value })} placeholder="Enter work name" />
            </div>
            <div className="form-field">
              <label>Priority *</label>
              <AppSelect value={form.priority} options={priorityOptions} onChange={(val) => setForm({ ...form, priority: val })} placeholder="Select priority" />
            </div>
            <div className="form-field">
              <label>Assigned To *</label>
              <AppInput value={form.assignedTo} onChange={(e) => setForm({ ...form, assignedTo: e.target.value })} placeholder="Assignee name" />
            </div>
            <div className="form-field">
              <label>Start Date *</label>
              <AppCalendar value={form.startDate ? new Date(form.startDate) : null} onChange={(e) => setForm({ ...form, startDate: e.value?.toISOString().split('T')[0] ?? '' })} showOnFocus={false} />
            </div>
            <div className="form-field">
              <label>End Date *</label>
              <AppCalendar value={form.endDate ? new Date(form.endDate) : null} onChange={(e) => setForm({ ...form, endDate: e.value?.toISOString().split('T')[0] ?? '' })} showOnFocus={false} />
            </div>
            <div className="form-field full-width">
              <label>Description</label>
              <AppTextarea value={form.workDescription ?? ''} onChange={(e) => setForm({ ...form, workDescription: e.target.value })} rows={3} placeholder="Work description" />
            </div>
            <div className="form-field full-width">
              <label>Remarks</label>
              <AppTextarea value={form.remarks ?? ''} onChange={(e) => setForm({ ...form, remarks: e.target.value })} rows={2} placeholder="Remarks" />
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
        ) : (works ?? []).length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                {['Work Name', 'Project', 'Priority', 'Assigned To', 'Duration', 'Status'].map((h) => (
                  <th key={h} style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', letterSpacing: '0.5px', borderBottom: '1px solid var(--border-color)' }}>{h}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {(works ?? []).map((w) => {
                const project = (projects ?? []).find((p) => p.projectId === w.projectId);
                return (
                  <tr key={w.workId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                    <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>{w.workName}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{project?.projectName ?? w.projectId}</td>
                    <td style={{ padding: '12px 16px' }}>
                      <span className="badge" style={{ background: w.priority === 'High' ? 'var(--badge-red-bg)' : w.priority === 'Low' ? 'var(--badge-emerald-bg)' : 'var(--badge-amber-bg)', color: w.priority === 'High' ? 'var(--badge-red-text)' : w.priority === 'Low' ? 'var(--badge-emerald-text)' : 'var(--badge-amber-text)', padding: '4px 10px', borderRadius: 12, fontSize: 12, fontWeight: 600 }}>
                        {w.priority}
                      </span>
                    </td>
                    <td style={{ padding: '12px 16px', fontSize: 13 }}>{w.assignedTo}</td>
                    <td style={{ padding: '12px 16px', fontSize: 12, color: 'var(--text-muted)' }}>{new Date(w.startDate).toLocaleDateString()} - {new Date(w.endDate).toLocaleDateString()}</td>
                    <td style={{ padding: '12px 16px' }}>
                      <span className="badge" style={{ background: w.isActive ? 'var(--badge-emerald-bg)' : 'var(--badge-red-bg)', color: w.isActive ? 'var(--badge-emerald-text)' : 'var(--badge-red-text)', padding: '4px 10px', borderRadius: 12, fontSize: 12, fontWeight: 600 }}>
                        {w.isActive ? 'Active' : 'Inactive'}
                      </span>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-briefcase" /><h3>No works yet</h3><p>{selectedProjectId ? 'No works for this project' : 'Select a project or create your first work'}</p></div>
        )}
      </div>
    </div>
  );
}
