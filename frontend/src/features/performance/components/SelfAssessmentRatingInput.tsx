import { RatingScaleInput } from '../../../shared/components/ui';

interface SelfAssessmentRatingInputProps {
  value: number | null;
  onChange: (value: number | null) => void;
}

export default function SelfAssessmentRatingInput({ value, onChange }: SelfAssessmentRatingInputProps) {
  return (
    <div
      className="form-group"
      style={{
        padding: '12px 16px',
        borderRadius: 8,
        background: 'var(--surface-ground, #f8fafc)',
        border: '1px solid var(--border-color, #e2e8f0)',
      }}
    >
      <label className="form-label" style={{ fontWeight: 600, color: 'var(--text-heading, #1e293b)', marginBottom: 8, display: 'block' }}>
        Overall Rating
      </label>
      <RatingScaleInput
        value={value}
        onChange={onChange}
        min={1}
        max={10}
      />
    </div>
  );
}
