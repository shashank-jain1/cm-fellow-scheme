import { useRef } from 'react';
import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';
import { MultiSelect } from 'primereact/multiselect';
import { Button } from 'primereact/button';
import { useLookupOptions } from '../../../shared/hooks/useMasters';

interface Props {
  trainingTitle: string;
  trainingCategory: string;
  trainingDescription: string;
  targetUserTypes: string[];
  trainerName: string;
  trainerMobile: string;
  attendanceRequired: boolean;
  trainingMaterialFile: File | null;
  onTrainingTitleChange: (v: string) => void;
  onTrainingCategoryChange: (v: string) => void;
  onTrainingDescriptionChange: (v: string) => void;
  onTargetUserTypesChange: (v: string[]) => void;
  onTrainerNameChange: (v: string) => void;
  onTrainerMobileChange: (v: string) => void;
  onAttendanceRequiredChange: (v: boolean) => void;
  onTrainingMaterialChange: (v: File | null) => void;
}

export default function TrainingFieldsCard({
  trainingTitle,
  trainingCategory,
  trainingDescription,
  targetUserTypes,
  trainerName,
  trainerMobile,
  attendanceRequired,
  trainingMaterialFile,
  onTrainingTitleChange,
  onTrainingCategoryChange,
  onTrainingDescriptionChange,
  onTargetUserTypesChange,
  onTrainerNameChange,
  onTrainerMobileChange,
  onAttendanceRequiredChange,
  onTrainingMaterialChange,
}: Props) {
  const categoryOptions = useLookupOptions('TrainingCategory');
  const userTypeOptions = [
    { label: 'Fellow', value: 'Fellow' },
    { label: 'Coordinator', value: 'Coordinator' },
    { label: 'Intern', value: 'Intern' },
  ];
  const fileInputRef = useRef<HTMLInputElement>(null);

  return (
    <div className="glass-card" style={{ padding: 24, marginTop: 20 }}>
      <h3 className="form-section-header">Training Details</h3>
      <div className="form-grid">
        <div className="form-field">
          <label>Training Title *</label>
          <InputText
            value={trainingTitle}
            onChange={(e) => onTrainingTitleChange(e.target.value)}
            placeholder="Enter training title"
          />
        </div>
        <div className="form-field">
          <label>Training Category *</label>
          <MultiSelect
            value={trainingCategory ? trainingCategory.split(',') : []}
            options={categoryOptions}
            onChange={(e) => onTrainingCategoryChange(e.value?.join(',') ?? '')}
            placeholder="Select Category"
            display="chip"
            className="w-full"
          />
        </div>
        <div className="form-field full-width">
          <label>Training Description</label>
          <InputTextarea
            value={trainingDescription}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onTrainingDescriptionChange(e.target.value)}
            rows={3}
            placeholder="Enter training description"
          />
        </div>
        <div className="form-field full-width">
          <label>Target User Type *</label>
          <MultiSelect
            value={targetUserTypes}
            options={userTypeOptions}
            onChange={(e) => onTargetUserTypesChange(e.value ?? [])}
            placeholder="Select Target Users"
            display="chip"
            className="w-full"
          />
        </div>
        <div className="form-field">
          <label>Trainer Name *</label>
          <InputText
            value={trainerName}
            onChange={(e) => onTrainerNameChange(e.target.value)}
            placeholder="Enter trainer name"
          />
        </div>
        <div className="form-field">
          <label>Trainer Mobile</label>
          <InputText
            value={trainerMobile}
            onChange={(e) => onTrainerMobileChange(e.target.value)}
            placeholder="Enter 10-digit mobile number"
            maxLength={10}
          />
        </div>
        <div className="form-field">
          <label>Attendance Required</label>
          <div style={{ display: 'flex', alignItems: 'center', gap: 10, height: 40 }}>
            <InputSwitch
              checked={attendanceRequired}
              onChange={(e) => onAttendanceRequiredChange(e.value ?? false)}
            />
            <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>
              {attendanceRequired ? 'Yes' : 'No'}
            </span>
          </div>
        </div>
        <div className="form-field full-width">
          <label>Training Material (PDF, PPT, DOC — max 20MB)</label>
          <input
            ref={fileInputRef}
            type="file"
            accept=".pdf,.ppt,.pptx,.doc,.docx"
            style={{ display: 'none' }}
            onChange={(e) => onTrainingMaterialChange(e.target.files?.[0] ?? null)}
          />
          <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
            <button
              type="button"
              className="file-upload-btn"
              onClick={() => fileInputRef.current?.click()}
            >
              <i className={`pi ${trainingMaterialFile ? 'pi-check-circle' : 'pi-upload'}`}
                 style={{ color: trainingMaterialFile ? 'var(--accent-primary)' : 'var(--text-secondary)' }} />
              {trainingMaterialFile ? trainingMaterialFile.name : 'Choose File'}
            </button>
            {trainingMaterialFile && (
              <Button
                icon="pi pi-times"
                severity="danger"
                text
                rounded
                onClick={() => onTrainingMaterialChange(null)}
                type="button"
                style={{ width: 32, height: 32 }}
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
