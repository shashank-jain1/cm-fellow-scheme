import { useRef } from 'react';
import { AppTextarea, AppSwitch, AppInput } from '../../../shared/components/forms';

interface Props {
  trainingDescription: string;
  trainerName: string;
  trainerMobile: string;
  attendanceRequired: boolean;
  certificateRequired: boolean;
  trainingMaterialFile: File | null;
  onTrainingDescriptionChange: (v: string) => void;
  onTrainerNameChange: (v: string) => void;
  onTrainerMobileChange: (v: string) => void;
  onAttendanceRequiredChange: (v: boolean) => void;
  onCertificateRequiredChange: (v: boolean) => void;
  onTrainingMaterialChange: (v: File | null) => void;
}

export default function TrainingTopicFields({
  trainingDescription, trainerName, trainerMobile,
  attendanceRequired, certificateRequired, trainingMaterialFile,
  onTrainingDescriptionChange, onTrainerNameChange, onTrainerMobileChange,
  onAttendanceRequiredChange, onCertificateRequiredChange, onTrainingMaterialChange,
}: Props) {
  const fileInputRef = useRef<HTMLInputElement>(null);

  const switchField = (label: string, checked: boolean, onChange: (v: boolean) => void) => (
    <div className="form-field">
      <label>{label}</label>
      <div style={{ display: 'flex', alignItems: 'center', gap: 10, height: 40 }}>
        <AppSwitch checked={checked} onChange={(e) => onChange(e.value ?? false)} />
        <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>{checked ? 'Yes' : 'No'}</span>
      </div>
    </div>
  );

  return (
    <>
      <div className="form-field full-width">
        <label>Training Description</label>
        <AppTextarea value={trainingDescription}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onTrainingDescriptionChange(e.target.value)}
          rows={3} placeholder="Enter training description" />
      </div>
      <div className="form-field">
        <label>Trainer Name *</label>
        <AppInput value={trainerName} onChange={(e) => onTrainerNameChange(e.target.value)}
          placeholder="Enter trainer name" />
      </div>
      <div className="form-field">
        <label>Trainer Mobile</label>
        <AppInput value={trainerMobile} onChange={(e) => onTrainerMobileChange(e.target.value)}
          placeholder="Enter 10-digit mobile number" maxLength={10} />
      </div>
      {switchField('Attendance Required', attendanceRequired, onAttendanceRequiredChange)}
      {switchField('Certificate Required', certificateRequired, onCertificateRequiredChange)}
      <div className="form-field full-width">
        <label>Training Material (PDF, PPT, DOC — max 20MB)</label>
        <input ref={fileInputRef} type="file" accept=".pdf,.ppt,.pptx,.doc,.docx" style={{ display: 'none' }}
          onChange={(e) => onTrainingMaterialChange(e.target.files?.[0] ?? null)} />
        <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
          <button type="button" className="file-upload-btn" onClick={() => fileInputRef.current?.click()}>
            <i className={`pi ${trainingMaterialFile ? 'pi-check-circle' : 'pi-upload'}`}
              style={{ color: trainingMaterialFile ? 'var(--accent-primary)' : 'var(--text-secondary)' }} />
            {trainingMaterialFile ? trainingMaterialFile.name : 'Choose File'}
          </button>
          {trainingMaterialFile && (
            <button type="button" onClick={() => onTrainingMaterialChange(null)}
              className="btn btn-ghost btn-sm"
              style={{ width: 32, height: 32, padding: 0, display: 'inline-flex', alignItems: 'center', justifyContent: 'center', color: 'var(--danger)' }}>
              <i className="pi pi-times" />
            </button>
          )}
        </div>
      </div>
    </>
  );
}
