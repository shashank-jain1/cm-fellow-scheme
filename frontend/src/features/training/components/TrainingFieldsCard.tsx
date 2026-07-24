import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { Dropdown } from 'primereact/dropdown';
import { InputSwitch } from 'primereact/inputswitch';

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

const categoryOptions = [
  { label: 'Technical', value: 'Technical' },
  { label: 'Soft Skills', value: 'Soft Skills' },
  { label: 'Domain', value: 'Domain' },
  { label: 'Leadership', value: 'Leadership' },
  { label: 'Other', value: 'Other' },
];

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
  return (
    <div className="card" style={{ padding: 20, marginTop: 16 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Training Details</h3>
      <div className="form-grid">
        <div className="form-field">
          <label>Training Title</label>
          <InputText
            value={trainingTitle}
            onChange={(e) => onTrainingTitleChange(e.target.value)}
            placeholder="Enter training title"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-field">
          <label>Category</label>
          <Dropdown
            value={trainingCategory}
            options={categoryOptions}
            onChange={(e) => onTrainingCategoryChange(e.value)}
            placeholder="Select Category"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-field full-width">
          <label>Description</label>
          <InputTextarea
            value={trainingDescription}
            onChange={(e) => onTrainingDescriptionChange(e.target.value)}
            rows={3}
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-field">
          <label>Trainer Name</label>
          <InputText
            value={trainerName}
            onChange={(e) => onTrainerNameChange(e.target.value)}
            placeholder="Enter trainer name"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-field">
          <label>Trainer Mobile</label>
          <InputText
            value={trainerMobile}
            onChange={(e) => onTrainerMobileChange(e.target.value)}
            placeholder="Enter mobile number"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-field">
          <label>Attendance Required</label>
          <InputSwitch
            checked={attendanceRequired}
            onChange={(e) => onAttendanceRequiredChange(e.value)}
          />
        </div>
      </div>
    </div>
  );
}
