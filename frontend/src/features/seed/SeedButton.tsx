interface SeedButtonProps {
  label: string;
  icon: string;
  onClick: () => void;
  disabled: boolean;
  variant?: 'primary' | 'outline';
}

export default function SeedButton({ label, icon, onClick, disabled, variant = 'primary' }: SeedButtonProps) {
  return (
    <button
      type="button"
      className={variant === 'outline' ? 'btn btn-outline' : 'btn btn-primary'}
      onClick={onClick}
      disabled={disabled}
    >
      <i className={`pi ${disabled && variant === 'primary' ? 'pi-spin pi-spinner' : icon}`} style={{ marginRight: 6 }} />
      {label}
    </button>
  );
}
