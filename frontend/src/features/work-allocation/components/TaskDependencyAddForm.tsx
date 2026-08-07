import { AppButton } from '../../../shared/components/ui';
import type { TaskProgressDto } from '../types';

interface TaskDependencyAddFormProps {
  tasks: TaskProgressDto[];
  selectedTaskId: number | null;
  setSelectedTaskId: (id: number | null) => void;
  prerequisiteId: number | null;
  setPrerequisiteId: (id: number | null) => void;
  onSubmit: () => void;
  onCancel: () => void;
  isPending: boolean;
}

export default function TaskDependencyAddForm({
  tasks, selectedTaskId, setSelectedTaskId, prerequisiteId, setPrerequisiteId, onSubmit, onCancel, isPending,
}: TaskDependencyAddFormProps) {
  return (
    <div style={{ padding: 16, background: 'var(--surface-ground)', borderRadius: 8, marginBottom: 16 }}>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 16, marginBottom: 12 }}>
        <div>
          <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Task</label>
          <select
            value={selectedTaskId ?? ''}
            onChange={(e) => setSelectedTaskId(Number(e.target.value) || null)}
            style={{ width: '100%', padding: '8px 12px', borderRadius: 6, border: '1px solid var(--border-color)' }}
          >
            <option value="">Select task</option>
            {tasks.map((t) => (
              <option key={t.workAllocationId} value={t.workAllocationId}>{t.projectName} - {t.workProject}</option>
            ))}
          </select>
        </div>
        <div>
          <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Prerequisite Task</label>
          <select
            value={prerequisiteId ?? ''}
            onChange={(e) => setPrerequisiteId(Number(e.target.value) || null)}
            style={{ width: '100%', padding: '8px 12px', borderRadius: 6, border: '1px solid var(--border-color)' }}
          >
            <option value="">Select prerequisite</option>
            {tasks.map((t) => (
              <option key={t.workAllocationId} value={t.workAllocationId}>{t.projectName} - {t.workProject}</option>
            ))}
          </select>
        </div>
      </div>
      <div style={{ display: 'flex', gap: 8 }}>
        <AppButton size="sm" onClick={onSubmit} loading={isPending}>Save</AppButton>
        <AppButton size="sm" variant="ghost" onClick={onCancel}>Cancel</AppButton>
      </div>
    </div>
  );
}
