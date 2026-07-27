import type { StepProps } from './form.hook';
import EducationFields from './fields/EducationFields';

export default function EducationalDetailsStep(props: StepProps) {
  return (
    <div className="fade-in">
      <h3 className="form-section-header">Education Qualification</h3>
      <div className="form-grid">
        <EducationFields {...props} />
      </div>
    </div>
  );
}
