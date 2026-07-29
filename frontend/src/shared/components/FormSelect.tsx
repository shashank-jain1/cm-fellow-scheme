import { useId, useState, useRef, useEffect, useCallback } from 'react';

interface FormSelectProps<T extends string | number = string | number> {
  value?: T | null;
  onChange: (value: T) => void;
  options: { label: string; value: T }[];
  placeholder?: string;
  disabled?: boolean;
  loading?: boolean;
  showClear?: boolean;
  style?: React.CSSProperties;
  className?: string;
}

export default function FormSelect<T extends string | number = string | number>({
  value,
  onChange,
  options,
  placeholder = 'Select...',
  disabled = false,
  loading = false,
  showClear = false,
  style,
  className,
}: FormSelectProps<T>) {
  const id = useId();
  const [open, setOpen] = useState(false);
  const ref = useRef<HTMLDivElement>(null);

  const selected = options.find((o) => o.value === value);
  const hasValue = value !== undefined && value !== null && value !== '';
  const isClearable = showClear && hasValue && selected;

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
      style={{ position: 'relative', display: 'block', width: '100%', ...style }}
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
          padding: '9px 12px',
          border: '1px solid var(--border)',
          borderRadius: 'var(--radius-md)',
          background: disabled ? 'var(--carbon-50)' : 'var(--bg-input)',
          fontFamily: 'var(--font-body)',
          fontSize: 'var(--text-base)',
          height: 40,
          color: hasValue && selected ? 'var(--text-heading)' : 'var(--text-muted)',
          cursor: disabled ? 'not-allowed' : 'pointer',
          outline: 'none',
          transition: 'all var(--transition-fast)',
          textAlign: 'left',
        }}
        onMouseEnter={(e) => {
          if (!disabled) e.currentTarget.style.borderColor = 'var(--carbon-300)';
        }}
        onMouseLeave={(e) => {
          e.currentTarget.style.borderColor = 'var(--border)';
        }}
        onFocus={(e) => {
          e.currentTarget.style.borderColor = 'var(--accent)';
          e.currentTarget.style.boxShadow = '0 0 0 3px var(--accent-light)';
        }}
        onBlur={(e) => {
          e.currentTarget.style.borderColor = 'var(--border)';
          e.currentTarget.style.boxShadow = 'none';
        }}
      >
        <span style={{ overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap', flex: 1, paddingRight: 8 }}>
          {loading ? 'Loading...' : (selected?.label ?? placeholder)}
        </span>
        
        <div style={{ display: 'flex', alignItems: 'center', gap: 6, flexShrink: 0 }}>
          {isClearable && (
            <span
              role="button"
              tabIndex={0}
              onClick={(e) => {
                e.stopPropagation();
                onChange('' as T);
              }}
              onKeyDown={(e) => {
                if (e.key === 'Enter' || e.key === ' ') {
                  e.stopPropagation();
                  onChange('' as T);
                }
              }}
              title="Clear selection"
              style={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                width: 18,
                height: 18,
                borderRadius: '50%',
                color: 'var(--text-muted)',
                fontSize: 10,
                cursor: 'pointer',
                transition: 'all var(--transition-fast)',
              }}
              onMouseEnter={(e) => {
                e.currentTarget.style.color = 'var(--text-heading)';
                e.currentTarget.style.background = 'var(--carbon-100)';
              }}
              onMouseLeave={(e) => {
                e.currentTarget.style.color = 'var(--text-muted)';
                e.currentTarget.style.background = 'transparent';
              }}
            >
              <i className="pi pi-times" />
            </span>
          )}
          <span style={{ fontSize: 11, color: 'var(--text-muted)', display: 'flex', alignItems: 'center' }}>
            <i className={`pi ${open ? 'pi-chevron-up' : 'pi-chevron-down'}`} />
          </span>
        </div>
      </button>

      {open && (
        <ul
          style={{
            position: 'absolute',
            top: 'calc(100% + 4px)',
            left: 0,
            right: 0,
            background: 'var(--bg-surface)',
            border: '1px solid var(--border)',
            borderRadius: 'var(--radius-md)',
            boxShadow: 'var(--shadow-lg)',
            listStyle: 'none',
            padding: '4px',
            margin: 0,
            zIndex: 100000,
            maxHeight: 240,
            overflowY: 'auto',
          }}
        >
          {options.length === 0 ? (
            <li style={{ padding: '10px 12px', color: 'var(--text-muted)', fontSize: 13, textAlign: 'center' }}>
              No options available
            </li>
          ) : (
            options.map((opt) => {
              const isSelected = opt.value === value;
              return (
                <li
                  key={String(opt.value)}
                  onClick={() => {
                    onChange(opt.value);
                    setOpen(false);
                  }}
                  style={{
                    padding: '8px 12px',
                    borderRadius: 'var(--radius-sm)',
                    fontSize: 13,
                    cursor: 'pointer',
                    background: isSelected ? 'var(--accent-light)' : 'transparent',
                    color: isSelected ? 'var(--accent)' : 'var(--text-body)',
                    fontWeight: isSelected ? 600 : 400,
                    transition: 'all var(--transition-fast)',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                  }}
                  onMouseEnter={(e) => {
                    if (!isSelected) e.currentTarget.style.background = 'var(--carbon-50)';
                  }}
                  onMouseLeave={(e) => {
                    if (!isSelected) e.currentTarget.style.background = 'transparent';
                  }}
                >
                  <span>{opt.label}</span>
                  {isSelected && <i className="pi pi-check" style={{ fontSize: 11, color: 'var(--accent)' }} />}
                </li>
              );
            })
          )}
        </ul>
      )}
    </div>
  );
}
