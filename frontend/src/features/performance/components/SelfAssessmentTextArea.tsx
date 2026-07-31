import { AppTextarea } from '../../../shared/components/forms';

interface SelfAssessmentTextAreaProps {
  label: string;
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  rows?: number;
}

export default function SelfAssessmentTextArea({
  label,
  value,
  onChange,
  placeholder = 'Enter your response...',
  rows = 4,
}: SelfAssessmentTextAreaProps) {
  return (
    <div className="form-group">
      <label className="form-label" style={{ fontWeight: 600 }}>{label}</label>
      <AppTextarea
        value={value}
        onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onChange(e.target.value)}
        placeholder={placeholder}
        rows={rows}
      />
    </div>
  );
}
