import React from 'react';
import { AppInput, AppTextarea, AppCalendar } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';

interface GoalCreateFormProps {
  title: string;
  setTitle: (v: string) => void;
  description: string;
  setDescription: (v: string) => void;
  targetDate: Date | null;
  setTargetDate: (v: Date | null) => void;
  onSubmit: () => void;
  onCancel: () => void;
  isPending: boolean;
}

export default function GoalCreateForm({
  title, setTitle, description, setDescription,
  targetDate, setTargetDate, onSubmit, onCancel, isPending,
}: GoalCreateFormProps) {
  return (
    <div className="card" style={{ padding: 24, marginBottom: 'var(--space-4)' }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Create New Goal</h3>
      <div className="form-group" style={{ marginBottom: 16 }}>
        <label className="form-label">Title</label>
        <AppInput
          value={title}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => setTitle(e.target.value)}
          placeholder="Enter goal title"
        />
      </div>
      <div className="form-group" style={{ marginBottom: 16 }}>
        <label className="form-label">Description</label>
        <AppTextarea
          value={description}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setDescription(e.target.value)}
          placeholder="Describe the goal"
          rows={3}
        />
      </div>
      <div className="form-group" style={{ marginBottom: 16 }}>
        <label className="form-label">Target Date</label>
        <AppCalendar
          value={targetDate}
          onChange={(e) => setTargetDate(e.value as Date | null)}
          placeholder="Select target date"
          dateFormat="dd/mm/yy"
        />
      </div>
      <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 12 }}>
        <AppButton variant="secondary" onClick={onCancel}>Cancel</AppButton>
        <AppButton variant="primary" onClick={onSubmit} loading={isPending} disabled={!title.trim() || !targetDate}>
          Create Goal
        </AppButton>
      </div>
    </div>
  );
}
