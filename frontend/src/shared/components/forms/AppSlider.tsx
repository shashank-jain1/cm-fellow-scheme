import { useId } from 'react';

interface AppSliderProps {
  value: number;
  onChange: (val: number) => void;
  min?: number;
  max?: number;
  step?: number;
  className?: string;
}

export default function AppSlider({
  value,
  onChange,
  min = 0,
  max = 100,
  step = 1,
  className = '',
}: AppSliderProps) {
  const id = useId();
  const pct = max > min ? ((value - min) / (max - min)) * 100 : 0;

  return (
    <div className={className} style={{ width: '100%' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 6 }}>
        <label htmlFor={id} style={{ fontSize: 13, color: 'var(--text-secondary)' }}>
          Progress
        </label>
        <span style={{ fontSize: 13, fontWeight: 600, color: 'var(--accent)' }}>
          {value}%
        </span>
      </div>
      <input
        id={id}
        type="range"
        min={min}
        max={max}
        step={step}
        value={value}
        onChange={(e) => onChange(Number(e.target.value))}
        style={{
          width: '100%',
          height: 6,
          borderRadius: 3,
          appearance: 'none',
          background: `linear-gradient(to right, var(--accent) ${pct}%, var(--carbon-200) ${pct}%)`,
          cursor: 'pointer',
          outline: 'none',
        }}
      />
    </div>
  );
}
