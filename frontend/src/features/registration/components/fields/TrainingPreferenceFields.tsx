import type { ChangeEvent } from 'react';
import { AppInput, AppSelect } from '../../../../shared/components/forms';
import { useDivisions } from '../../../../shared/hooks/useMasters';
import type { StepProps } from '../form.hook';

export default function TrainingPreferenceFields({ formData, update }: StepProps) {
  const trainingOptions = [
    { label: 'Select training type', value: '' },
    { label: 'Online', value: '1' },
    { label: 'Offline', value: '2' },
    { label: 'Hybrid', value: '3' },
  ];

  const { data: divisions } = useDivisions(1);
  const locationOptions = [
    { label: 'Select preferred location', value: '' },
    ...(divisions ?? []).map((d) => ({ label: d.divisionName, value: String(d.divisionId) })),
  ];

  return (
    <>
      <div className="form-field">
        <label>Applied For Training *</label>
        <AppSelect
          value={formData.appliedForTraining}
          onChange={(val: string) => update('appliedForTraining', val)}
          options={trainingOptions}
          placeholder="Select training type"
        />
      </div>
      <div className="form-field">
        <label>Preferred Training Location *</label>
        <AppSelect
          value={formData.preferredTrainingLocationId}
          onChange={(val: string) => update('preferredTrainingLocationId', val)}
          options={locationOptions}
          placeholder="Select preferred location"
        />
      </div>
      <div className="form-field full-width">
        <label>Experience Details</label>
        <AppInput
          value={formData.experienceDetails}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('experienceDetails', e.target.value)}
          placeholder="Describe relevant experience (optional)"
        />
      </div>
    </>
  );
}
