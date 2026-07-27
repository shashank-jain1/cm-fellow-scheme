import React, { useId, useState, useRef, useEffect, useCallback } from 'react';

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
          padding: showClear && selected ? '9px 36px 9px 13px' : '9px 13px',
          border: '1px solid var(--border-color)',
          borderRadius: 'var(--radius-md)',
          background: disabled ? 'var(--bg-card)' : 'var(--bg-input)',
          fontFamily: "'Plus Jakarta Sans', sans-serif",
          fontSize: 14,
          height: 40,
          color: selected ? 'var(--text-primary)' : 'var(--text-muted)',
          cursor: disabled ? 'not-allowed' : 'pointer',
          outline: 'none',
          transition: 'all var(--transition-fast)',
          textAlign: 'left',
          boxShadow: 'var(--shadow-sm)',
        }}
      >
        <span style={{ overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap', flex: 1 }}>
          {loading ? 'Loading...' : (selected?.label ?? placeholder)}
        </span>
        <span style={{ marginLeft: 8, fontSize: 11, color: 'var(--text-muted)', flexShrink: 0, display: 'flex', alignItems: 'center' }}>
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
          title="Clear selection"
          style={{
            position: 'absolute',
            right: 28,
            top: '50%',
            transform: 'translateY(-50%)',
            background: 'none',
            border: 'none',
            cursor: 'pointer',
            color: 'var(--text-muted)',
            fontSize: 11,
            padding: '2px 4px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            borderRadius: '50%',
          }}
        >
          <i className="pi pi-times" />
        </button>
      )}

      {open && (
        <ul
          style={{
            position: 'absolute',
            top: 'calc(100% + 4px)',
            left: 0,
            right: 0,
            background: 'var(--bg-secondary)',
            border: '1px solid var(--border-color)',
            borderRadius: 'var(--radius-md)',
            boxShadow: 'var(--shadow-lg)',
            listStyle: 'none',
            padding: '4px',
            margin: 0,
            zIndex: 1000,
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
                  key={opt.value}
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
                    color: isSelected ? 'var(--accent-primary-hover)' : 'var(--text-primary)',
                    fontWeight: isSelected ? 600 : 400,
                    transition: 'all var(--transition-fast)',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                  }}
                  onMouseEnter={(e) => {
                    if (!isSelected) e.currentTarget.style.background = 'var(--bg-card-hover)';
                  }}
                  onMouseLeave={(e) => {
                    if (!isSelected) e.currentTarget.style.background = 'transparent';
                  }}
                >
                  <span>{opt.label}</span>
                  {isSelected && <i className="pi pi-check" style={{ fontSize: 11, color: 'var(--accent-primary)' }} />}
                </li>
              );
            })
          )}
        </ul>
      )}
    </div>
  );
}
