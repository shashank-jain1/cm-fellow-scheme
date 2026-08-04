interface FontOptionProps {
  label: string;
  size: number;
  active: boolean;
  onClick: () => void;
}

export default function FontOption({ label, size, active, onClick }: FontOptionProps) {
  return (
    <button
      type="button"
      onClick={onClick}
      style={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        gap: '6px',
        padding: '10px 6px',
        borderRadius: 'var(--radius-md)',
        border: active ? '2px solid var(--accent)' : '2px solid var(--border)',
        background: active ? 'var(--accent-light)' : 'transparent',
        cursor: 'pointer',
        transition: 'all var(--transition-fast)',
      }}
    >
      <span
        style={{
          fontSize: size,
          fontWeight: 700,
          color: active ? 'var(--accent)' : 'var(--text-heading)',
          fontFamily: 'var(--font-display)',
          lineHeight: 1,
        }}
      >
        Aa
      </span>
      <span style={{ fontSize: 11, fontWeight: 600, color: active ? 'var(--accent)' : 'var(--text-body)', fontFamily: 'var(--font-body)' }}>
        {label}
      </span>
    </button>
  );
}
