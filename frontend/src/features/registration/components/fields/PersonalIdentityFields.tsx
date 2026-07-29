import type { ChangeEvent } from 'react';
import { AppInput } from '../../../../shared/components/forms';
import type { StepProps } from '../form.hook';

export default function PersonalIdentityFields({ formData, update }: StepProps) {
  return (
    <>
      <div className="form-field">
        <label>Aadhaar Number</label>
        <AppInput
          value={formData.aadhaarNumber}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('aadhaarNumber', e.target.value)}
          placeholder="12-digit Aadhaar"
          maxLength={12}
        />
      </div>
      <div className="form-field">
        <label>PAN Number</label>
        <AppInput
          value={formData.panNumber}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('panNumber', e.target.value.toUpperCase())}
          placeholder="ABCDE1234F"
          maxLength={10}
        />
      </div>
      <div className="form-field">
        <label>Driving License</label>
        <AppInput
          value={formData.drivingLicenseNumber}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('drivingLicenseNumber', e.target.value)}
          placeholder="DL number"
        />
      </div>
      <div className="form-field">
        <label>Samagra ID</label>
        <AppInput
          value={formData.samagraId}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('samagraId', e.target.value)}
          placeholder="9-digit Samagra ID"
          maxLength={9}
        />
      </div>
    </>
  );
}
