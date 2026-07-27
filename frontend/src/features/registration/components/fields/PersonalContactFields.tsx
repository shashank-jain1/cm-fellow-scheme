import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import type { StepProps } from '../form.hook';

export default function PersonalContactFields({ formData, update }: StepProps) {
  return (
    <>
      <div className="form-field">
        <label>Mobile Number *</label>
        <InputText
          value={formData.mobileNumber}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('mobileNumber', e.target.value)}
          placeholder="10-digit mobile number"
          maxLength={10}
        />
      </div>
      <div className="form-field">
        <label>Email *</label>
        <InputText
          value={formData.emailId}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('emailId', e.target.value)}
          placeholder="fellow@email.com"
        />
      </div>
    </>
  );
}
