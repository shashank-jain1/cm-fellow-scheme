import TrainingModeSelect from './TrainingModeSelect';
import TrainingTopicFields from './TrainingTopicFields';

interface Props {
  trainingTitle: string;
  trainingCategory: string;
  trainingDescription: string;
  targetUserTypes: string[];
  trainerName: string;
  trainerMobile: string;
  attendanceRequired: boolean;
  certificateRequired: boolean;
  trainingMaterialFile: File | null;
  onTrainingTitleChange: (v: string) => void;
  onTrainingCategoryChange: (v: string) => void;
  onTrainingDescriptionChange: (v: string) => void;
  onTargetUserTypesChange: (v: string[]) => void;
  onTrainerNameChange: (v: string) => void;
  onTrainerMobileChange: (v: string) => void;
  onAttendanceRequiredChange: (v: boolean) => void;
  onCertificateRequiredChange: (v: boolean) => void;
  onTrainingMaterialChange: (v: File | null) => void;
}

export default function TrainingFieldsCard(props: Props) {
  return (
    <div className="glass-card" style={{ padding: 24, marginTop: 20 }}>
      <h3 className="form-section-header">Training Details</h3>
      <div className="form-grid">
        <TrainingModeSelect
          trainingTitle={props.trainingTitle} trainingCategory={props.trainingCategory}
          targetUserTypes={props.targetUserTypes} onTrainingTitleChange={props.onTrainingTitleChange}
          onTrainingCategoryChange={props.onTrainingCategoryChange}
          onTargetUserTypesChange={props.onTargetUserTypesChange} />
        <TrainingTopicFields
          trainingDescription={props.trainingDescription} trainerName={props.trainerName}
          trainerMobile={props.trainerMobile} attendanceRequired={props.attendanceRequired}
          certificateRequired={props.certificateRequired} trainingMaterialFile={props.trainingMaterialFile}
          onTrainingDescriptionChange={props.onTrainingDescriptionChange}
          onTrainerNameChange={props.onTrainerNameChange} onTrainerMobileChange={props.onTrainerMobileChange}
          onAttendanceRequiredChange={props.onAttendanceRequiredChange}
          onCertificateRequiredChange={props.onCertificateRequiredChange}
          onTrainingMaterialChange={props.onTrainingMaterialChange} />
      </div>
    </div>
  );
}
