import type { StepProps } from '../form.hook';
import AddressFields from './AddressFields';
import LocationSelectFields from './LocationSelectFields';

export default function AddressLocationFields(props: StepProps) {
  return (
    <>
      <AddressFields {...props} />
      <LocationSelectFields {...props} />
    </>
  );
}
