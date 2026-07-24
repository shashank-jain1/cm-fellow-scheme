import { useId, useState, useRef, useEffect, useCallback } from 'react';

interface SelectOption {
  label: string;
  value: string;
}

interface FormSelectProps {
  value: string;
  onChange: (value: string) => void;
  options: SelectOption[];
  placeholder?: string;
  disabled?: boolean;
  loading?: boolean;
  showClear?: boolean;
  style?: React.CSSProperties;
  className?: string;
}

export default function FormSelect({
  value,
  onChange,
  options,
  placeholder = 'Select...',
  disabled = false,
  loading = false,
  showClear = false,
  style,
  className,
}: FormSelectProps) {
  const id = useId();
  const [open, setOpen] = useState(false);
  const ref = useRef<HTMLDivElement>(null);

  const selected = options.find((o) => o.value === value);

  const handleClickOutside = useCallback((e: MouseEvent) => {
    if (ref.current && !ref.current.contains(e.target as Node)) {
      setOpen(false);
    }
  }, []);

  useEffect(() => {
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, [handleClickOutside]);

  return (
    <div
      ref={ref}
      className={`p-select ${className ?? ''}`}
      style={{ position: 'relative', ...style }}
      id={id}
    >
      <button
        type="button"
        className="p-select-trigger"
        onClick={() => !disabled && setOpen(!open)}
        disabled={disabled}
        style={{
          width: '100%',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between',
          padding: '10px 14px',
          border: '1px solid var(--border-color)',
          borderRadius: 'var(--radius-md)',
          background: disabled ? 'var(--navy-50)' : 'var(--white)',
          fontFamily: "'Inter', sans-serif",
          fontSize: 14,
          color: selected ? 'var(--text-primary)' : 'var(--text-muted)',
          cursor: disabled ? 'not-allowed' : 'pointer',
          outline: 'none',
          transition: 'border-color 150ms, box-shadow 150ms',
          textAlign: 'left',
        }}
      >
        <span style={{ overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>
          {loading ? 'Loading...' : (selected?.label ?? placeholder)}
        </span>
        <span style={{ marginLeft: 8, fontSize: 12, color: 'var(--text-muted)', flexShrink: 0 }}>
          <i className={`pi ${open ? 'pi-chevron-up' : 'pi-chevron-down'}`} />
        </span>
      </button>

      {showClear && selected && (
        <button
          type="button"
          onClick={(e) => {
            e.stopPropagation();
            onChange('');
          }}
          style={{
            position: 'absolute',
            right: 32,
            top: '50%',
            transform: 'translateY(-50%)',
            background: 'none',
            border: 'none',
            cursor: 'pointer',
            color: 'var(--text-muted)',
            fontSize: 12,
            padding: 4,
          }}
        >
          <i className="pi pi-times" />
        </button>
      )}

      {open && (
        <ul
          style={{
            position: 'absolute',
            top: '100%',
            left: 0,
            right: 0,
            marginTop: 4,
            background: 'var(--white)',
            border: '1px solid var(--border-color)',
            borderRadius: 'var(--radius-md)',
            boxShadow: 'var(--shadow-lg)',
            listStyle: 'none',
            padding: '4px 0',
            margin: '4px 0 0 0',
            zIndex: 1000,
            maxHeight: 240,
            overflow: 'auto',
          }}
        >
          {options.length === 0 ? (
            <li style={{ padding: '12px 14px', color: 'var(--text-muted)', fontSize: 13, textAlign: 'center' }}>
              No options available
            </li>
          ) : (
            options.map((opt) => (
              <li
                key={opt.value}
                onClick={() => {
                  onChange(opt.value);
                  setOpen(false);
                }}
                style={{
                  padding: '10px 14px',
                  fontSize: 14,
                  cursor: 'pointer',
                  background: opt.value === value ? 'var(--emerald-50)' : 'transparent',
                  color: opt.value === value ? 'var(--emerald-600)' : 'var(--text-primary)',
                  fontWeight: opt.value === value ? 500 : 400,
                  transition: 'background 100ms',
                }}
                onMouseEnter={(e) => {
                  if (opt.value !== value) e.currentTarget.style.background = 'var(--navy-50)';
                }}
                onMouseLeave={(e) => {
                  if (opt.value !== value) e.currentTarget.style.background = 'transparent';
                }}
              >
                {opt.label}
              </li>
            ))
          )}
        </ul>
      )}
    </div>
  );
}
