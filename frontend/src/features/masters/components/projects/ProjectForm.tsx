import { AppInput, AppTextarea, AppCalendar, AppInputNumber } from '../../../../shared/components/forms';
import type { CreateProjectCommand } from '../../types';

interface ProjectFormProps {
  form: CreateProjectCommand;
  setForm: (form: CreateProjectCommand) => void;
  onSubmit: () => void;
  onCancel: () => void;
  isPending: boolean;
}

export default function ProjectForm({
  form,
  setForm,
  onSubmit,
  onCancel,
  isPending,
}: ProjectFormProps) {
  return (
    <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
      <div className="form-grid">
        <div className="form-field">
          <label>Project Name *</label>
          <AppInput
            value={form.projectName}
            onChange={(e) => setForm({ ...form, projectName: e.target.value })}
            placeholder="Enter project name"
          />
        </div>
        <div className="form-field">
          <label>Project Code *</label>
          <AppInput
            value={form.projectCode}
            onChange={(e) => setForm({ ...form, projectCode: e.target.value })}
            placeholder="e.g. PRJ001"
          />
        </div>
        <div className="form-field">
          <label>Department *</label>
          <AppInput
            value={form.departmentName}
            onChange={(e) => setForm({ ...form, departmentName: e.target.value })}
            placeholder="Department name"
          />
        </div>
        <div className="form-field">
          <label>Project Incharge *</label>
          <AppInput
            value={form.projectIncharge}
            onChange={(e) => setForm({ ...form, projectIncharge: e.target.value })}
            placeholder="Incharge name"
          />
        </div>
        <div className="form-field">
          <label>Start Date *</label>
          <AppCalendar
            value={form.startDate ? new Date(form.startDate) : null}
            onChange={(e) => setForm({ ...form, startDate: e.value?.toISOString().split('T')[0] ?? '' })}
            showOnFocus={false}
          />
        </div>
        <div className="form-field">
          <label>End Date *</label>
          <AppCalendar
            value={form.endDate ? new Date(form.endDate) : null}
            onChange={(e) => setForm({ ...form, endDate: e.value?.toISOString().split('T')[0] ?? '' })}
            showOnFocus={false}
          />
        </div>
        <div className="form-field">
          <label>Budget Amount</label>
          <AppInputNumber
            value={form.budgetAmount ?? undefined}
            onValueChange={(e) => setForm({ ...form, budgetAmount: e.value ?? undefined })}
            mode="currency"
            currency="INR"
            locale="en-IN"
          />
        </div>
        <div className="form-field full-width">
          <label>Description</label>
          <AppTextarea
            value={form.projectDescription ?? ''}
            onChange={(e) => setForm({ ...form, projectDescription: e.target.value })}
            rows={3}
            placeholder="Project description"
          />
        </div>
      </div>
      <div style={{ display: 'flex', gap: 8, justifyContent: 'flex-end', marginTop: 12 }}>
        <button className="btn btn-secondary" onClick={onCancel}>
          Cancel
        </button>
        <button className="btn btn-primary" onClick={onSubmit} disabled={isPending}>
          Create
        </button>
      </div>
    </div>
  );
}
