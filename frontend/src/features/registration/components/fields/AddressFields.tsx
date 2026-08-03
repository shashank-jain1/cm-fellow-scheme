import type { ChangeEvent } from 'react';
import { AppTextarea, AppInput } from '../../../../shared/components/forms';
import type { StepProps } from '../form.hook';

export default function AddressFields({ formData, update }: StepProps) {
  return (
    <>
      <div className="form-field full-width">
        <label>Full Address *</label>
        <AppTextarea
          value={formData.permanentAddress}
          onChange={(e: ChangeEvent<HTMLTextAreaElement>) =>
            update('permanentAddress', e.target.value)
          }
          placeholder="Enter full address"
          rows={3}
        />
      </div>
      <div className="form-field">
        <label>PIN Code *</label>
        <AppInput
          value={formData.pinCode}
          onChange={(e: ChangeEvent<HTMLInputElement>) =>
            update('pinCode', e.target.value)
          }
          placeholder="6-digit PIN"
          maxLength={6}
        />
      </div>
    </>
  );
}
