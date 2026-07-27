import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import FormSelect from '../../../../shared/components/FormSelect';
import { useLookupOptions } from '../../../../shared/hooks/useMasters';
import type { StepProps } from '../form.hook';

export default function EducationFields({ formData, update }: StepProps) {
  const qualificationOptions = useLookupOptions('Qualification');

  const yearOptions = [
    { label: 'Select year', value: '' },
    ...Array.from({ length: 30 }, (_, i) => {
      const year = new Date().getFullYear() - i;
      return { label: String(year), value: String(year) };
    }),
  ];

  return (
    <>
      <div className="form-field">
        <label>Highest Qualification *</label>
        <FormSelect
          value={formData.qualificationId}
          onChange={(val: string) => update('qualificationId', val)}
          options={qualificationOptions}
          placeholder="Select qualification"
        />
      </div>
      <div className="form-field">
        <label>Board / University *</label>
        <InputText
          value={formData.boardUniversityName}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('boardUniversityName', e.target.value)}
          placeholder="Enter board or university name"
        />
      </div>
      <div className="form-field">
        <label>Year of Passing *</label>
        <FormSelect
          value={formData.passingYear}
          onChange={(val: string) => update('passingYear', val)}
          options={yearOptions}
          placeholder="Select year"
        />
      </div>
      <div className="form-field">
        <label>Percentage / CGPA *</label>
        <InputText
          value={formData.percentageCgpa}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('percentageCgpa', e.target.value)}
          placeholder="e.g. 85.5"
        />
      </div>
    </>
  );
}
