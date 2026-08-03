import { AppInput, AppTextarea, AppCalendar, AppSelect } from '../../../../shared/components/forms';
import type { CreateWorkCommand } from '../../types';

interface WorkFormProps {
  form: CreateWorkCommand;
  setForm: (form: CreateWorkCommand) => void;
  onSubmit: () => void;
  onCancel: () => void;
  isPending: boolean;
  projectOptions: { label: string; value: string }[];
  priorityOptions: { label: string; value: string }[];
}

export default function WorkForm({
  form, setForm, onSubmit, onCancel, isPending, projectOptions, priorityOptions,
}: WorkFormProps) {
  const update = (patch: Partial<CreateWorkCommand>) => setForm({ ...form, ...patch });

  return (
    <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
      <div className="form-grid">
        <div className="form-field">
          <label>Project *</label>
          <AppSelect value={form.projectId ? String(form.projectId) : ''} options={projectOptions}
            onChange={(val) => update({ projectId: Number(val) })} placeholder="Select project" />
        </div>
        <div className="form-field">
          <label>Work Name *</label>
          <AppInput value={form.workName} onChange={(e) => update({ workName: e.target.value })} placeholder="Enter work name" />
        </div>
        <div className="form-field">
          <label>Priority *</label>
          <AppSelect value={form.priority} options={priorityOptions}
            onChange={(val) => update({ priority: val })} placeholder="Select priority" />
        </div>
        <div className="form-field">
          <label>Assigned To *</label>
          <AppInput value={form.assignedTo} onChange={(e) => update({ assignedTo: e.target.value })} placeholder="Assignee name" />
        </div>
        <div className="form-field">
          <label>Start Date *</label>
          <AppCalendar value={form.startDate ? new Date(form.startDate) : null}
            onChange={(e) => update({ startDate: e.value?.toISOString().split('T')[0] ?? '' })} showOnFocus={false} />
        </div>
        <div className="form-field">
          <label>End Date *</label>
          <AppCalendar value={form.endDate ? new Date(form.endDate) : null}
            onChange={(e) => update({ endDate: e.value?.toISOString().split('T')[0] ?? '' })} showOnFocus={false} />
        </div>
        <div className="form-field full-width">
          <label>Description</label>
          <AppTextarea value={form.workDescription ?? ''} onChange={(e) => update({ workDescription: e.target.value })}
            rows={3} placeholder="Work description" />
        </div>
        <div className="form-field full-width">
          <label>Remarks</label>
          <AppTextarea value={form.remarks ?? ''} onChange={(e) => update({ remarks: e.target.value })}
            rows={2} placeholder="Remarks" />
        </div>
      </div>
      <div style={{ display: 'flex', gap: 8, justifyContent: 'flex-end', marginTop: 12 }}>
        <button className="btn btn-secondary" onClick={onCancel}>Cancel</button>
        <button className="btn btn-primary" onClick={onSubmit} disabled={isPending}>Create</button>
      </div>
    </div>
  );
}
