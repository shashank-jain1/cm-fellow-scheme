import { RatingScaleInput } from '../../../shared/components/ui';

interface ExitInterviewRatingInputProps {
  label: string;
  value: number | null;
  onChange: (value: number | null) => void;
}

export default function ExitInterviewRatingInput({ label, value, onChange }: ExitInterviewRatingInputProps) {
  return (
    <div
      className="form-group"
      style={{
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between',
        gap: 20,
        padding: '12px 16px',
        borderRadius: 8,
        background: 'var(--surface-ground, #f8fafc)',
        border: '1px solid var(--border-color, #e2e8f0)',
        marginBottom: 12,
      }}
    >
      <label className="form-label" style={{ minWidth: 180, marginBottom: 0, fontWeight: 600, color: 'var(--text-heading, #1e293b)' }}>
        {label}
      </label>
      <RatingScaleInput
        value={value}
        onChange={onChange}
        min={1}
        max={5}
      />
    </div>
  );
}
