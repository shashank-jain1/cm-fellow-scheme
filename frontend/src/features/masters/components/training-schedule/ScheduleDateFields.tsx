import { AppCalendar, AppInput, AppTextarea } from '../../../../shared/components/forms';

interface Props {
  trainingDate: Date | null;
  venueName: string;
  trainingDescription: string;
  onTrainingDateChange: (v: Date) => void;
  onVenueNameChange: (v: string) => void;
  onTrainingDescriptionChange: (v: string) => void;
}

export default function ScheduleDateFields({
  trainingDate,
  venueName,
  trainingDescription,
  onTrainingDateChange,
  onVenueNameChange,
  onTrainingDescriptionChange,
}: Props) {
  return (
    <>
      <div className="form-field">
        <label>Training Date *</label>
        <AppCalendar
          value={trainingDate}
          onChange={(e) => onTrainingDateChange(e.value as Date)}
          dateFormat="dd/mm/yy"
          placeholder="Select Date"
          className="w-full"
          showIcon
        />
      </div>
      <div className="form-field">
        <label>Venue Name</label>
        <AppInput
          value={venueName}
          onChange={(e) => onVenueNameChange(e.target.value)}
          placeholder="Enter venue name"
          className="w-full"
        />
      </div>
      <div className="form-field col-span-full">
        <label>Training Description</label>
        <AppTextarea
          value={trainingDescription}
          onChange={(e) => onTrainingDescriptionChange(e.target.value)}
          rows={3}
          placeholder="Enter training description"
          className="w-full"
        />
      </div>
    </>
  );
}
