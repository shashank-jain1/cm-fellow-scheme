import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import type { StepProps } from './form.hook';

export default function TrainingInfoStep({ formData, update }: StepProps) {
  return (
    <div className="fade-in">
      <h3 className="form-section-header">Training Details</h3>
      <div className="form-grid">
        <div className="form-field">
          <label>Training Center</label>
          <InputText
            value={formData.trainingCenter}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('trainingCenter', e.target.value)}
            placeholder="Enter training center"
          />
        </div>
        <div className="form-field">
          <label>Batch Number</label>
          <InputText
            value={formData.trainingBatch}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('trainingBatch', e.target.value)}
            placeholder="e.g. Batch-2024-A"
          />
        </div>
      </div>
    </div>
  );
}
