import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import FormSelect from '../../../../shared/components/FormSelect';
import { useLookupOptions } from '../../../../shared/hooks/useMasters';
import type { StepProps } from '../form.hook';

export default function PersonalBasicFields({ formData, update }: StepProps) {
  const genderOptions = useLookupOptions('Gender');
  return (
    <>
      <div className="form-field">
        <label>First Name *</label>
        <InputText
          value={formData.firstName}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('firstName', e.target.value)}
          placeholder="Enter first name"
        />
      </div>
      <div className="form-field">
        <label>Middle Name</label>
        <InputText
          value={formData.middleName}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('middleName', e.target.value)}
          placeholder="Enter middle name"
        />
      </div>
      <div className="form-field">
        <label>Last Name *</label>
        <InputText
          value={formData.lastName}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('lastName', e.target.value)}
          placeholder="Enter last name"
        />
      </div>
      <div className="form-field">
        <label>Father's Name *</label>
        <InputText
          value={formData.fatherName}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('fatherName', e.target.value)}
          placeholder="Enter father's name"
        />
      </div>
      <div className="form-field">
        <label>Gender</label>
        <FormSelect
          value={formData.gender}
          onChange={(val: string) => update('gender', val)}
          options={genderOptions}
          placeholder="Select gender"
        />
      </div>
      <div className="form-field">
        <label>Date of Birth *</label>
        <InputText
          type="date"
          value={formData.dateOfBirth}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('dateOfBirth', e.target.value)}
        />
      </div>
    </>
  );
}
