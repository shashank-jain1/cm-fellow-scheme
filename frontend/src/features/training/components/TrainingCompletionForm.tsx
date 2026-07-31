import React from 'react';
import { Toast } from 'primereact/toast';
import { AppInput, AppTextarea, AppInputNumber } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { formatDate } from '../../../shared/utils/format';
import type { TrainingScheduleDto } from '../types';

interface TrainingCompletionFormProps {
  toast: React.RefObject<Toast | null>;
  sessions: TrainingScheduleDto[];
  selectedSessionId: number | null;
  setSelectedSessionId: (id: number | null) => void;
  rating: number;
  setRating: (r: number) => void;
  comments: string;
  setComments: (c: string) => void;
  onSubmit: () => void;
  onCancel: () => void;
  isPending: boolean;
}

export default function TrainingCompletionForm({
  toast,
  sessions,
  selectedSessionId,
  setSelectedSessionId,
  rating,
  setRating,
  comments,
  setComments,
  onSubmit,
  onCancel,
  isPending,
}: TrainingCompletionFormProps) {
  return (
    <div className="card" style={{ padding: 24, marginBottom: 20 }}>
      <Toast ref={toast} />
      <h3 style={{ marginTop: 0, marginBottom: 16, fontSize: 16, fontWeight: 600 }}>
        Mark Training as Completed
      </h3>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 16, marginBottom: 16 }}>
        <div>
          <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Training Session</label>
          <select
            value={selectedSessionId ?? ''}
            onChange={(e) => setSelectedSessionId(Number(e.target.value) || null)}
            style={{ width: '100%', padding: '8px 12px', borderRadius: 6, border: '1px solid var(--border-color)' }}
          >
            <option value="">Select session</option>
            {sessions.map((s) => (
              <option key={s.trainingScheduleId} value={s.trainingScheduleId}>
                {s.trainingTitle ?? s.meetingTitle ?? `Session ${s.trainingScheduleId}`} ({formatDate(s.date)})
              </option>
            ))}
          </select>
        </div>
        <div>
          <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Rating (1-5)</label>
          <AppInputNumber
            value={rating}
            onValueChange={(e) => setRating(e.value ?? 3)}
            min={1}
            max={5}
            style={{ width: '100%' }}
          />
        </div>
      </div>
      <div style={{ marginBottom: 16 }}>
        <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Comments</label>
        <AppTextarea
          value={comments}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setComments(e.target.value)}
          placeholder="Feedback comments..."
          rows={3}
        />
      </div>
      <div style={{ display: 'flex', gap: 8 }}>
        <AppButton onClick={onSubmit} loading={isPending}>
          Submit Completion
        </AppButton>
        <AppButton variant="ghost" onClick={onCancel}>
          Cancel
        </AppButton>
      </div>
    </div>
  );
}
