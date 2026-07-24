import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import type { StepProps } from './form.hook';

export default function TrainingInfoStep({ formData, update }: StepProps) {
  return (
    <div className="fade-in">
      <h3 style={{ marginBottom: 24, fontSize: 18 }}>Training Details</h3>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20, maxWidth: 640 }}>
        <div className="form-group">
          <label className="form-label">Training Center</label>
          <InputText
            value={formData.trainingCenter}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('trainingCenter', e.target.value)}
            placeholder="Enter training center"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Batch Number</label>
          <InputText
            value={formData.trainingBatch}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('trainingBatch', e.target.value)}
            placeholder="e.g. Batch-2024-A"
            style={{ width: '100%' }}
          />
        </div>
      </div>
    </div>
  );
}
