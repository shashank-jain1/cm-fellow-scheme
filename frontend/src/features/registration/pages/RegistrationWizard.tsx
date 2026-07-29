import { ProgressSpinner } from 'primereact/progressspinner';
import { useRegistrationForm } from '../components/form.hook';
import PersonalInfoStep from '../components/PersonalInfoStep';
import OtpVerificationStep from '../components/OtpVerificationStep';
import AddressInfoStep from '../components/AddressInfoStep';
import TrainingInfoStep from '../components/TrainingInfoStep';
import EducationalDetailsStep from '../components/EducationalDetailsStep';
import DocumentUploadStep from '../components/DocumentUploadStep';
import DeclarationStep from '../components/DeclarationStep';
import { AppButton } from '../../../shared/components/ui';

const steps = [
  { label: 'Personal Info', icon: 'pi pi-user' },
  { label: 'Verify OTP', icon: 'pi pi-shield' },
  { label: 'Address', icon: 'pi pi-map-marker' },
  { label: 'Training', icon: 'pi pi-book' },
  { label: 'Education', icon: 'pi pi-graduation-cap' },
  { label: 'Documents', icon: 'pi pi-file' },
  { label: 'Declaration', icon: 'pi pi-check-circle' },
];

export default function RegistrationWizard() {
  const { formData, update, currentStep, next, prev, goToStep, handleSubmit, isSubmitting } = useRegistrationForm();

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Fellow Registration</h1>
          <p>Register a new CM Fellow through the multi-step wizard</p>
        </div>
      </div>

      <div className="step-indicator">
        {steps.map((step, i) => (
          <div
            key={i}
            className={`step-item ${i === currentStep ? 'active' : ''} ${i < currentStep ? 'completed' : ''}`}
            onClick={() => i <= currentStep && goToStep(i)}
          >
            <div className="step-number">
              {i < currentStep ? <i className="pi pi-check" /> : i + 1}
            </div>
            <span className="step-label">
              {step.label}
            </span>
          </div>
        ))}
      </div>

      <div className="card" style={{ padding: 'var(--space-8)' }}>
        {currentStep === 0 && <PersonalInfoStep formData={formData} update={update} />}
        {currentStep === 1 && <OtpVerificationStep formData={formData} update={update} />}
        {currentStep === 2 && <AddressInfoStep formData={formData} update={update} />}
        {currentStep === 3 && <TrainingInfoStep formData={formData} update={update} />}
        {currentStep === 4 && <EducationalDetailsStep formData={formData} update={update} />}
        {currentStep === 5 && <DocumentUploadStep />}
        {currentStep === 6 && <DeclarationStep formData={formData} update={update} />}

        <div
          style={{
            display: 'flex',
            justifyContent: 'space-between',
            marginTop: 'var(--space-8)',
            paddingTop: 'var(--space-6)',
            borderTop: '1px solid var(--border)',
          }}
        >
          <AppButton
            variant="secondary"
            icon="pi pi-arrow-left"
            onClick={prev}
            disabled={currentStep === 0}
          >
            Previous
          </AppButton>
          {currentStep < steps.length - 1 ? (
            <AppButton
              onClick={next}
            >
              Next <i className="pi pi-arrow-right" />
            </AppButton>
          ) : (
            <AppButton
              onClick={handleSubmit}
              disabled={!formData.declarationAccepted || isSubmitting}
            >
              {isSubmitting ? <ProgressSpinner style={{ width: 16, height: 16 }} /> : <i className="pi pi-check" />}
              {isSubmitting ? ' Submitting...' : ' Submit Registration'}
            </AppButton>
          )}
        </div>
      </div>
    </div>
  );
}
