import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import FormSelect from '../../../shared/components/FormSelect';
import { useStates } from '../../../shared/hooks/useMasters';
import type { StepProps } from './form.hook';

export default function AddressInfoStep({ formData, update }: StepProps) {
  const { data: states, isLoading: statesLoading } = useStates();

  const stateOptions = [
    { label: 'Select state', value: '' },
    ...(states ?? []).map((s) => ({ label: s.stateName, value: String(s.stateId) })),
  ];

  return (
    <div className="fade-in">
      <h3 className="form-section-header">Address Details</h3>
      <div className="form-grid">
        <div className="form-field full-width">
          <label>Full Address</label>
          <InputTextarea
            value={formData.address}
            onChange={(e: ChangeEvent<HTMLTextAreaElement>) => update('address', e.target.value)}
            placeholder="Enter full address"
            rows={3}
          />
        </div>
        <div className="form-field">
          <label>City</label>
          <InputText
            value={formData.city}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('city', e.target.value)}
            placeholder="City"
          />
        </div>
        <div className="form-field">
          <label>State</label>
          <FormSelect
            value={formData.state}
            onChange={(val: string) => update('state', val)}
            options={stateOptions}
            placeholder="Select state"
            loading={statesLoading}
          />
        </div>
        <div className="form-field">
          <label>PIN Code</label>
          <InputText
            value={formData.pincode}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('pincode', e.target.value)}
            placeholder="6-digit PIN"
          />
        </div>
      </div>
    </div>
  );
}
