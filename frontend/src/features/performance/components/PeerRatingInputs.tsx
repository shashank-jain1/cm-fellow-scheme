import { InputNumber } from 'primereact/inputnumber';

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
          style={{ display: 'flex', alignItems: 'center', gap: 16 }}
        >
          <label
            className="form-label"
            style={{ minWidth: 160, marginBottom: 0 }}
          >
            {field.label}
          </label>
          <InputNumber
            value={field.value}
            onValueChange={(e) => updateRating(field.key, e.value ?? null)}
            min={1}
            max={5}
            showButtons
            buttonLayout="horizontal"
            decrementButtonClassName="btn btn-secondary"
            incrementButtonClassName="btn btn-secondary"
            incrementButtonIcon="pi pi-plus"
            decrementButtonIcon="pi pi-minus"
            style={{ width: 140 }}
          />
          <span style={{ fontSize: 12, color: 'var(--text-muted)' }}>
            (1-5 scale)
          </span>
        </div>
      ))}
    </div>
  );
}
