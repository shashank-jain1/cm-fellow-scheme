import { Button } from 'primereact/button';
import { useRegistrationForm } from '../components/form.hook';
import PersonalInfoStep from '../components/PersonalInfoStep';
import AddressInfoStep from '../components/AddressInfoStep';
import TrainingInfoStep from '../components/TrainingInfoStep';
import EducationalDetailsStep from '../components/EducationalDetailsStep';
import DocumentUploadStep from '../components/DocumentUploadStep';
import DeclarationStep from '../components/DeclarationStep';

const steps = [
  { label: 'Personal Info', icon: 'pi pi-user' },
  { label: 'Address', icon: 'pi pi-map-marker' },
  { label: 'Training', icon: 'pi pi-book' },
  { label: 'Education', icon: 'pi pi-graduation-cap' },
  { label: 'Documents', icon: 'pi pi-file' },
  { label: 'Declaration', icon: 'pi pi-check-circle' },
];

export default function RegistrationWizard() {
  const { formData, update, currentStep, next, prev, goToStep, handleSubmit } = useRegistrationForm();

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Fellow Registration</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Register a new CM Fellow through the multi-step wizard
          </p>
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
            <span className="step-label">{step.label}</span>
          </div>
        ))}
      </div>

      <div className="card" style={{ padding: 32 }}>
        {currentStep === 0 && <PersonalInfoStep formData={formData} update={update} />}

        {currentStep === 1 && <AddressInfoStep formData={formData} update={update} />}

        {currentStep === 2 && <TrainingInfoStep formData={formData} update={update} />}

        {currentStep === 3 && <EducationalDetailsStep formData={formData} update={update} />}

        {currentStep === 4 && <DocumentUploadStep />}

        {currentStep === 5 && <DeclarationStep formData={formData} update={update} />}

        <div
          style={{
            display: 'flex',
            justifyContent: 'space-between',
            marginTop: 32,
            paddingTop: 24,
            borderTop: '1px solid var(--border-light)',
          }}
        >
          <Button
            label="Previous"
            icon="pi pi-arrow-left"
            className="btn btn-secondary"
            onClick={prev}
            disabled={currentStep === 0}
          />
          {currentStep < steps.length - 1 ? (
            <Button
              label="Next"
              icon="pi pi-arrow-right"
              iconPos="right"
              className="btn btn-primary"
              onClick={next}
            />
          ) : (
            <Button
              label="Submit Registration"
              icon="pi pi-check"
              className="btn btn-primary"
              onClick={handleSubmit}
              disabled={!formData.declaration}
            />
          )}
        </div>
      </div>
    </div>
  );
}
