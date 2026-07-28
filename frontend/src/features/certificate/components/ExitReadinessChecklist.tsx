import { useState } from 'react';
import { AppButton } from '../../../shared/components/ui';

interface ChecklistItem {
  id: string;
  label: string;
  checked: boolean;
}

interface ExitReadinessChecklistProps {
  applicantId: number;
  onSubmit?: (completionStatus: string, verificationFlags: string) => void;
  isSubmitting?: boolean;
}

const defaultItems: ChecklistItem[] = [
  { id: 'surveys', label: 'All assigned surveys completed', checked: false },
  { id: 'attendance', label: 'Attendance records are up to date', checked: false },
  { id: 'feedback', label: 'Supervisor feedback submitted', checked: false },
  { id: 'assets', label: 'All assets returned', checked: false },
  { id: 'clearance', label: 'No pending clearances', checked: false },
  { id: 'exit-interview', label: 'Exit interview completed', checked: false },
];

export default function ExitReadinessChecklist({ onSubmit, isSubmitting }: ExitReadinessChecklistProps) {
  const [items, setItems] = useState<ChecklistItem[]>(defaultItems);

  const toggleItem = (id: string) => {
    setItems((prev) => prev.map((item) => (item.id === id ? { ...item, checked: !item.checked } : item)));
  };

  const allChecked = items.every((item) => item.checked);
  const checkedCount = items.filter((item) => item.checked).length;

  const handleSubmit = () => {
    const completionStatus = allChecked ? 'Completed' : 'In Progress';
    const verificationFlags = JSON.stringify(items.filter((i) => i.checked).map((i) => i.id));
    onSubmit?.(completionStatus, verificationFlags);
  };

  return (
    <div className="card" style={{ padding: 24 }}>
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 600 }}>Exit Readiness Checklist</h3>
        <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>
          {checkedCount}/{items.length} completed
        </span>
      </div>

      <div style={{ display: 'flex', flexDirection: 'column', gap: 0 }}>
        {items.map((item, i) => (
          <label
            key={item.id}
            style={{
              display: 'flex',
              alignItems: 'center',
              gap: 12,
              padding: '12px 0',
              borderBottom: i < items.length - 1 ? '1px solid var(--border-light)' : 'none',
              cursor: 'pointer',
            }}
          >
            <input
              type="checkbox"
              checked={item.checked}
              onChange={() => toggleItem(item.id)}
              style={{ width: 18, height: 18, accentColor: 'var(--emerald-500)' }}
            />
            <span
              style={{
                fontSize: 14,
                color: item.checked ? 'var(--text-muted)' : 'var(--text-primary)',
                textDecoration: item.checked ? 'line-through' : 'none',
              }}
            >
              {item.label}
            </span>
          </label>
        ))}
      </div>

      <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 20 }}>
        <AppButton
          icon="pi pi-check"
          onClick={handleSubmit}
          disabled={!allChecked || isSubmitting}
          loading={isSubmitting}
        >
          Mark Ready
        </AppButton>
      </div>
    </div>
  );
}
