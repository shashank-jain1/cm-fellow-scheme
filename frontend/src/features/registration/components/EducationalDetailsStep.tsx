import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import FormSelect from '../../../shared/components/FormSelect';
import type { StepProps } from './form.hook';

const qualificationOptions = [
  { label: "Bachelor's Degree", value: 'bachelors' },
  { label: "Master's Degree", value: 'masters' },
  { label: 'PhD', value: 'phd' },
  { label: 'Diploma', value: 'diploma' },
];

export default function EducationalDetailsStep({ formData, update }: StepProps) {
  return (
    <div className="fade-in">
      <h3 style={{ marginBottom: 24, fontSize: 18 }}>Education Qualification</h3>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20, maxWidth: 640 }}>
        <div className="form-group">
          <label className="form-label">Highest Qualification</label>
          <FormSelect
            value={formData.qualification}
            onChange={(val: string) => update('qualification', val)}
            options={qualificationOptions}
            placeholder="Select qualification"
          />
        </div>
        <div className="form-group">
          <label className="form-label">University / Institution</label>
          <InputText
            value={formData.university}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('university', e.target.value)}
            placeholder="Enter university name"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Year of Passing</label>
          <InputText
            value={formData.yearOfPassing}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('yearOfPassing', e.target.value)}
            placeholder="e.g. 2023"
            style={{ width: '100%' }}
          />
        </div>
      </div>
    </div>
  );
}
