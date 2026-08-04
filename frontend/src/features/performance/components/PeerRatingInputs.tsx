import { RatingScaleInput } from '../../../shared/components/ui';

interface RatingField {
  key: string;
  label: string;
  value: number | null;
}

interface PeerRatingInputsProps {
  ratings: RatingField[];
  updateRating: (key: string, value: number | null) => void;
}

export default function PeerRatingInputs({
  ratings,
  updateRating,
}: PeerRatingInputsProps) {
  return (
    <div style={{ display: 'grid', gap: 20 }}>
      {ratings.map((field) => (
        <div
          key={field.key}
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
          }}
        >
          <label
            className="form-label"
            style={{ minWidth: 180, marginBottom: 0, fontWeight: 600, color: 'var(--text-heading, #1e293b)' }}
          >
            {field.label}
          </label>
          <RatingScaleInput
            value={field.value}
            onChange={(val) => updateRating(field.key, val)}
            min={1}
            max={5}
          />
        </div>
      ))}
    </div>
  );
}
