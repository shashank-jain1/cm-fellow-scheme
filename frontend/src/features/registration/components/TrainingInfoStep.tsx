import type { StepProps } from './form.hook';
import TrainingPreferenceFields from './fields/TrainingPreferenceFields';

export default function TrainingInfoStep(props: StepProps) {
  return (
    <div className="fade-in">
      <h3 className="form-section-header">Training Details</h3>
      <div className="form-grid">
        <TrainingPreferenceFields {...props} />
      </div>
    </div>
  );
}
