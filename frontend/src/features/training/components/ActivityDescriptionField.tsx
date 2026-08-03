import { AppTextarea } from '../../../shared/components/forms';

interface Props {
  remarks: string;
  onRemarksChange: (v: string) => void;
}

export default function ActivityDescriptionField({ remarks, onRemarksChange }: Props) {
  return (
    <div className="form-field full-width">
      <label>Remarks</label>
      <AppTextarea value={remarks} onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onRemarksChange(e.target.value)} rows={3} placeholder="Enter remarks" />
    </div>
  );
}
