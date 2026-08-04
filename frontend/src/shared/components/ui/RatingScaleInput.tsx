import { useState } from 'react';

interface RatingScaleInputProps {
  value: number | null;
  onChange: (val: number | null) => void;
  min?: number;
  max?: number;
  showLabels?: boolean;
}

const getRatingMetadata = (num: number, max: number) => {
  const pct = (num - 1) / Math.max(max - 1, 1);
  if (pct <= 0.2) return { label: 'Poor', color: '#ef4444', bg: '#fef2f2' };
  if (pct <= 0.4) return { label: 'Fair', color: '#f97316', bg: '#fff7ed' };
  if (pct <= 0.6) return { label: 'Good', color: '#eab308', bg: '#fefce8' };
  if (pct <= 0.8) return { label: 'Very Good', color: '#3b82f6', bg: '#eff6ff' };
  return { label: 'Excellent', color: '#10b981', bg: '#ecfdf5' };
};

export default function RatingScaleInput({
  value,
  onChange,
  min = 1,
  max = 5,
  showLabels = true,
}: RatingScaleInputProps) {
  const [hoverVal, setHoverVal] = useState<number | null>(null);

  const options = Array.from({ length: max - min + 1 }, (_, i) => min + i);
  const activeVal = hoverVal ?? value;

  const currentMeta = activeVal ? getRatingMetadata(activeVal, max) : null;

  return (
    <div
      onMouseLeave={() => setHoverVal(null)}
      style={{ display: 'inline-flex', alignItems: 'center', gap: 12, flexWrap: 'nowrap' }}
    >
      <div style={{ display: 'inline-flex', alignItems: 'center', gap: 6, flexWrap: 'nowrap' }}>
        {options.map((num) => {
          const isSelected = value === num;
          const isHovered = hoverVal === num;
          const meta = getRatingMetadata(num, max);

          return (
            <button
              key={num}
              type="button"
              onClick={() => onChange(isSelected ? null : num)}
              onMouseEnter={() => setHoverVal(num)}
              style={{
                width: max > 5 ? 32 : 36,
                height: max > 5 ? 32 : 36,
                borderRadius: '50%',
                border: isSelected || isHovered ? `2px solid ${meta.color}` : '1.5px solid var(--border-color, #cbd5e1)',
                background: isSelected ? meta.color : isHovered ? meta.bg : 'var(--surface-card, #ffffff)',
                color: isSelected ? '#ffffff' : isHovered ? meta.color : 'var(--text-color, #334155)',
                fontWeight: isSelected ? 700 : 600,
                fontSize: max > 5 ? 12 : 13,
                cursor: 'pointer',
                transition: 'background-color 0.15s ease, border-color 0.15s ease, color 0.15s ease, box-shadow 0.15s ease',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                boxShadow: isSelected
                  ? `0 2px 8px ${meta.color}50`
                  : isHovered
                  ? `0 0 0 3px ${meta.color}25`
                  : 'none',
                outline: 'none',
                userSelect: 'none',
                WebkitTapHighlightColor: 'transparent',
                flexShrink: 0,
              }}
            >
              {num}
            </button>
          );
        })}
      </div>

      {showLabels && (
        <div style={{ display: 'inline-flex', alignItems: 'center', flexShrink: 0, minWidth: 150 }}>
          <span
            style={{
              padding: '4px 10px',
              borderRadius: 12,
              fontSize: 12,
              fontWeight: 600,
              color: currentMeta ? currentMeta.color : 'var(--text-muted, #94a3b8)',
              background: currentMeta ? currentMeta.bg : 'transparent',
              border: currentMeta ? `1px solid ${currentMeta.color}30` : '1px solid transparent',
              transition: 'all 0.15s ease-in-out',
              whiteSpace: 'nowrap',
              display: 'inline-block',
            }}
          >
            {currentMeta ? `${activeVal} / ${max} — ${currentMeta.label}` : `Select rating (${min}-${max})`}
          </span>
        </div>
      )}
    </div>
  );
}
