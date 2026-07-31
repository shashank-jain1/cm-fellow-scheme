import React from 'react';
import { AppInput, AppTextarea, AppCalendar } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';

interface ImprovementPlanCreateFormProps {
  title: string;
  setTitle: (v: string) => void;
  description: string;
  setDescription: (v: string) => void;
  startDate: Date | null;
  setStartDate: (v: Date | null) => void;
  endDate: Date | null;
  setEndDate: (v: Date | null) => void;
  onSubmit: () => void;
  onCancel: () => void;
  isPending: boolean;
}

export default function ImprovementPlanCreateForm({
  title, setTitle, description, setDescription,
  startDate, setStartDate, endDate, setEndDate,
  onSubmit, onCancel, isPending,
}: ImprovementPlanCreateFormProps) {
  return (
    <div className="card" style={{ padding: 24, marginBottom: 'var(--space-4)' }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Create Improvement Plan</h3>
      <div className="form-group" style={{ marginBottom: 16 }}>
        <label className="form-label">Title</label>
        <AppInput value={title} onChange={(e: React.ChangeEvent<HTMLInputElement>) => setTitle(e.target.value)} placeholder="Enter plan title" />
      </div>
      <div className="form-group" style={{ marginBottom: 16 }}>
        <label className="form-label">Description</label>
        <AppTextarea value={description} onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setDescription(e.target.value)} placeholder="Describe the improvement plan" rows={3} />
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 16, marginBottom: 16 }}>
        <div className="form-group">
          <label className="form-label">Start Date</label>
          <AppCalendar value={startDate} onChange={(e) => setStartDate(e.value as Date | null)} placeholder="Select start date" dateFormat="dd/mm/yy" />
        </div>
        <div className="form-group">
          <label className="form-label">End Date</label>
          <AppCalendar value={endDate} onChange={(e) => setEndDate(e.value as Date | null)} placeholder="Select end date" dateFormat="dd/mm/yy" />
        </div>
      </div>
      <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 12 }}>
        <AppButton variant="secondary" onClick={onCancel}>Cancel</AppButton>
        <AppButton variant="primary" onClick={onSubmit} loading={isPending} disabled={!title.trim() || !startDate || !endDate}>Create Plan</AppButton>
      </div>
    </div>
  );
}
