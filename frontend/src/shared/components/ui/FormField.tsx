import type { ReactNode } from 'react';

interface FormFieldProps {
  label?: string;
  required?: boolean;
  error?: string;
  children: ReactNode;
  className?: string;
  style?: React.CSSProperties;
  fullWidth?: boolean;
}

export default function FormField({ label, required, error, children, className = '', style, fullWidth }: FormFieldProps) {
  return (
    <div
      className={`form-field ${fullWidth ? 'full-width' : ''} ${className}`}
      style={style}
    >
      {label && (
        <label>
          {label}
          {required && <span style={{ color: '#ef4444', marginLeft: 4 }}>*</span>}
        </label>
      )}
      {children}
      {error && (
        <small style={{ color: '#ef4444', fontSize: 12, marginTop: 4 }}>{error}</small>
      )}
    </div>
  );
}
