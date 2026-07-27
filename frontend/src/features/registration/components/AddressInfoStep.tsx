import type { StepProps } from './form.hook';
import AddressLocationFields from './fields/AddressLocationFields';

export default function AddressInfoStep(props: StepProps) {
  return (
    <div className="fade-in">
      <h3 className="form-section-header">Address Details</h3>
      <div className="form-grid">
        <AddressLocationFields {...props} />
      </div>
    </div>
  );
}
