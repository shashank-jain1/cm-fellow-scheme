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
      <h3 className="form-section-header">Education Qualification</h3>
      <div className="form-grid">
        <div className="form-field">
          <label>Highest Qualification</label>
          <FormSelect
            value={formData.qualification}
            onChange={(val: string) => update('qualification', val)}
            options={qualificationOptions}
            placeholder="Select qualification"
          />
        </div>
        <div className="form-field">
          <label>University / Institution</label>
          <InputText
            value={formData.university}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('university', e.target.value)}
            placeholder="Enter university name"
          />
        </div>
        <div className="form-field">
          <label>Year of Passing</label>
          <InputText
            value={formData.yearOfPassing}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('yearOfPassing', e.target.value)}
            placeholder="e.g. 2023"
          />
        </div>
      </div>
    </div>
  );
}
