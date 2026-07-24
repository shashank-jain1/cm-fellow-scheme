import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import FormSelect from '../../../shared/components/FormSelect';
import type { StepProps } from './form.hook';

const genderOptions = [
  { label: 'Male', value: 'male' },
  { label: 'Female', value: 'female' },
  { label: 'Other', value: 'other' },
];

export default function PersonalInfoStep({ formData, update }: StepProps) {
  return (
    <div className="fade-in">
      <h3 style={{ marginBottom: 24, fontSize: 18 }}>Personal Information</h3>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20 }}>
        <div className="form-group">
          <label className="form-label">First Name</label>
          <InputText
            value={formData.firstName}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('firstName', e.target.value)}
            placeholder="Enter first name"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Last Name</label>
          <InputText
            value={formData.lastName}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('lastName', e.target.value)}
            placeholder="Enter last name"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Email</label>
          <InputText
            value={formData.email}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('email', e.target.value)}
            placeholder="fellow@email.com"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Phone Number</label>
          <InputText
            value={formData.phone}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('phone', e.target.value)}
            placeholder="+91 XXXXX XXXXX"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Date of Birth</label>
          <InputText
            type="date"
            value={formData.dob}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('dob', e.target.value)}
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Gender</label>
          <FormSelect
            value={formData.gender}
            onChange={(val: string) => update('gender', val)}
            options={genderOptions}
            placeholder="Select gender"
          />
        </div>
      </div>
    </div>
  );
}
