interface RadiusOptionProps {
  label: string;
  radius: string;
  active: boolean;
  onClick: () => void;
}

export default function RadiusOption({ label, radius, active, onClick }: RadiusOptionProps) {
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
      <div
        style={{
          width: 28,
          height: 28,
          borderRadius: radius,
          border: '2px solid',
          borderColor: active ? 'var(--accent)' : 'var(--text-muted)',
          transition: 'all var(--transition-fast)',
        }}
      />
      <span style={{ fontSize: 11, fontWeight: 600, color: active ? 'var(--accent)' : 'var(--text-body)', fontFamily: 'var(--font-body)' }}>
        {label}
      </span>
    </button>
  );
}
