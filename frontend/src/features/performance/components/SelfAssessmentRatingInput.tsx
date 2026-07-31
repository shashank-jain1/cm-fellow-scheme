import { InputNumber } from 'primereact/inputnumber';

interface SelfAssessmentRatingInputProps {
  value: number | null;
  onChange: (value: number | null) => void;
}

export default function SelfAssessmentRatingInput({ value, onChange }: SelfAssessmentRatingInputProps) {
  return (
    <div className="form-group">
      <label className="form-label" style={{ fontWeight: 600 }}>
        Overall Rating
      </label>
      <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
        <InputNumber
          value={value}
          onValueChange={(e) => onChange(e.value ?? null)}
          min={1}
          max={10}
          showButtons
          buttonLayout="horizontal"
          decrementButtonClassName="btn btn-secondary"
          incrementButtonClassName="btn btn-secondary"
          incrementButtonIcon="pi pi-plus"
          decrementButtonIcon="pi pi-minus"
          style={{ width: 160 }}
        />
        <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>(1-10 scale)</span>
      </div>
    </div>
  );
}
