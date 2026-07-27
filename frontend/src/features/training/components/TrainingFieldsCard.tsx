import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';
import FormSelect from '../../../shared/components/FormSelect';
import { useLookupOptions } from '../../../shared/hooks/useMasters';

interface Props {
  trainingTitle: string;
  trainingCategory: string;
  trainingDescription: string;
  trainerName: string;
  trainerMobile: string;
  attendanceRequired: boolean;
  onTrainingTitleChange: (v: string) => void;
  onTrainingCategoryChange: (v: string) => void;
  onTrainingDescriptionChange: (v: string) => void;
  onTrainerNameChange: (v: string) => void;
  onTrainerMobileChange: (v: string) => void;
  onAttendanceRequiredChange: (v: boolean) => void;
}

export default function TrainingFieldsCard({
  trainingTitle,
  trainingCategory,
  trainingDescription,
  trainerName,
  trainerMobile,
  attendanceRequired,
  onTrainingTitleChange,
  onTrainingCategoryChange,
  onTrainingDescriptionChange,
  onTrainerNameChange,
  onTrainerMobileChange,
  onAttendanceRequiredChange,
}: Props) {
  const categoryOptions = useLookupOptions('TrainingCategory');
  return (
    <div className="glass-card" style={{ padding: 24, marginTop: 20 }}>
      <h3 className="form-section-header">Training Details</h3>
      <div className="form-grid">
        <div className="form-field">
          <label>Training Title</label>
          <InputText
            value={trainingTitle}
            onChange={(e) => onTrainingTitleChange(e.target.value)}
            placeholder="Enter training title"
          />
        </div>
        <div className="form-field">
          <label>Category</label>
          <FormSelect
            value={trainingCategory}
            options={categoryOptions}
            onChange={(val) => onTrainingCategoryChange(val)}
            placeholder="Select Category"
          />
        </div>
        <div className="form-field full-width">
          <label>Description</label>
          <InputTextarea
            value={trainingDescription}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onTrainingDescriptionChange(e.target.value)}
            rows={3}
          />
        </div>
        <div className="form-field">
          <label>Trainer Name</label>
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
            placeholder="Enter mobile number"
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
      </div>
    </div>
  );
}
