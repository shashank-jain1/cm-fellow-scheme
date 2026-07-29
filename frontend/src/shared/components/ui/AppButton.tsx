import type { ReactNode, ButtonHTMLAttributes } from 'react';

interface AppButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: 'primary' | 'secondary' | 'ghost' | 'danger' | 'accent';
  size?: 'sm' | 'md' | 'lg';
  icon?: string;
  loading?: boolean;
  children?: ReactNode;
}

const variantClass: Record<string, string> = {
  primary: 'btn-primary',
  secondary: 'btn-secondary',
  ghost: 'btn-ghost',
  danger: 'btn-danger',
  accent: 'btn-accent',
};

const sizeClass: Record<string, string> = {
  sm: 'btn-sm',
  md: '',
  lg: 'btn-lg',
};

export default function AppButton({
  variant = 'primary',
  size = 'md',
  icon,
  loading,
  children,
  className = '',
  disabled,
  ...rest
}: AppButtonProps) {
  const classes = `btn ${variantClass[variant] ?? ''} ${sizeClass[size] ?? ''} ${className}`.trim();

  return (
    <button
      className={classes}
      disabled={disabled || loading}
      {...rest}
    >
      {loading ? (
        <i className="pi pi-spin pi-spinner" />
      ) : icon ? (
        <i className={icon} />
      ) : null}
      {children}
    </button>
  );
}
