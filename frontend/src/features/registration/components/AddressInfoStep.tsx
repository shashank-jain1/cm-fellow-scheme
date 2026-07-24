import type { ChangeEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import FormSelect from '../../../shared/components/FormSelect';
import type { StepProps } from './form.hook';

const stateOptions = [
  { label: 'Andhra Pradesh', value: 'AP' },
  { label: 'Bihar', value: 'BR' },
  { label: 'Delhi', value: 'DL' },
  { label: 'Karnataka', value: 'KA' },
  { label: 'Maharashtra', value: 'MH' },
  { label: 'Tamil Nadu', value: 'TN' },
  { label: 'Uttar Pradesh', value: 'UP' },
  { label: 'West Bengal', value: 'WB' },
];

export default function AddressInfoStep({ formData, update }: StepProps) {
  return (
    <div className="fade-in">
      <h3 style={{ marginBottom: 24, fontSize: 18 }}>Address Details</h3>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr', gap: 20, maxWidth: 640 }}>
        <div className="form-group">
          <label className="form-label">Full Address</label>
          <InputTextarea
            value={formData.address}
            onChange={(e: ChangeEvent<HTMLTextAreaElement>) => update('address', e.target.value)}
            placeholder="Enter full address"
            rows={3}
            style={{ width: '100%', resize: 'vertical' }}
          />
        </div>
        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20 }}>
          <div className="form-group">
            <label className="form-label">City</label>
            <InputText
              value={formData.city}
              onChange={(e: ChangeEvent<HTMLInputElement>) => update('city', e.target.value)}
              placeholder="City"
              style={{ width: '100%' }}
            />
          </div>
          <div className="form-group">
            <label className="form-label">State</label>
            <FormSelect
              value={formData.state}
              onChange={(val: string) => update('state', val)}
              options={stateOptions}
              placeholder="Select state"
            />
          </div>
        </div>
        <div className="form-group" style={{ maxWidth: 280 }}>
          <label className="form-label">PIN Code</label>
          <InputText
            value={formData.pincode}
            onChange={(e: ChangeEvent<HTMLInputElement>) => update('pincode', e.target.value)}
            placeholder="6-digit PIN"
            style={{ width: '100%' }}
          />
        </div>
      </div>
    </div>
  );
}
