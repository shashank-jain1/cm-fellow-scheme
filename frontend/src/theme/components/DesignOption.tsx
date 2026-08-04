interface DesignOptionProps {
  label: string;
  icon: string;
  desc: string;
  active: boolean;
  onClick: () => void;
}

export default function DesignOption({ label, icon, desc, active, onClick }: DesignOptionProps) {
  return (
    <button
      type="button"
      onClick={onClick}
      style={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        gap: '6px',
        padding: '12px 8px',
        borderRadius: 'var(--radius-md)',
        border: active ? '2px solid var(--accent)' : '2px solid var(--border)',
        background: active ? 'var(--accent-light)' : 'transparent',
        cursor: 'pointer',
        transition: 'all var(--transition-fast)',
      }}
    >
      <i
        className={`pi ${icon}`}
        style={{
          fontSize: 18,
          color: active ? 'var(--accent)' : 'var(--text-muted)',
        }}
      />
      <span style={{ fontSize: 12, fontWeight: 600, color: active ? 'var(--accent)' : 'var(--text-body)', fontFamily: 'var(--font-body)' }}>
        {label}
      </span>
      <span style={{ fontSize: 10, color: 'var(--text-muted)', fontFamily: 'var(--font-body)', textAlign: 'center', lineHeight: 1.3 }}>
        {desc}
      </span>
    </button>
  );
}
