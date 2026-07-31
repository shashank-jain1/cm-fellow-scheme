import { InputNumber } from 'primereact/inputnumber';

interface ExitInterviewRatingInputProps {
  label: string;
  value: number | null;
  onChange: (value: number | null) => void;
}

export default function ExitInterviewRatingInput({ label, value, onChange }: ExitInterviewRatingInputProps) {
  return (
    <div className="form-group" style={{ display: 'flex', alignItems: 'center', gap: 16 }}>
      <label className="form-label" style={{ minWidth: 180, marginBottom: 0, fontWeight: 600 }}>
        {label}
      </label>
      <InputNumber
        value={value}
        onValueChange={(e) => onChange(e.value ?? null)}
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
      <span style={{ fontSize: 12, color: 'var(--text-secondary)' }}>(1-5 scale)</span>
    </div>
  );
}
