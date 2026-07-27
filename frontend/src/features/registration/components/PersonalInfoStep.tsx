import type { StepProps } from './form.hook';
import PersonalBasicFields from './fields/PersonalBasicFields';
import PersonalContactFields from './fields/PersonalContactFields';
import PersonalIdentityFields from './fields/PersonalIdentityFields';

export default function PersonalInfoStep(props: StepProps) {
  return (
    <div className="fade-in">
      <h3 className="form-section-header">Personal Information</h3>
      <div className="form-grid">
        <PersonalBasicFields {...props} />
        <PersonalContactFields {...props} />
        <PersonalIdentityFields {...props} />
      </div>
    </div>
  );
}
