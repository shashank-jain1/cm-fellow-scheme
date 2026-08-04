import { THEME_OPTIONS, type ThemeMode } from '../ThemeContext';

interface ThemeGridProps {
  activeTheme: string;
  onSelect: (theme: ThemeMode) => void;
}

export default function ThemeGrid({ activeTheme, onSelect }: ThemeGridProps) {
  return (
    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '8px' }}>
      {THEME_OPTIONS.map((opt) => {
        const isActive = opt.id === activeTheme;
        return (
          <button
            key={opt.id}
            type="button"
            onClick={() => onSelect(opt.id as ThemeMode)}
            style={{
              display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '6px',
              padding: '10px 6px', borderRadius: 'var(--radius-md)',
              border: isActive ? '2px solid var(--accent)' : '2px solid var(--border)',
              background: isActive ? 'var(--accent-light)' : 'transparent',
              cursor: 'pointer', transition: 'all var(--transition-fast)',
            }}
          >
            <div style={{ width: 28, height: 28, borderRadius: '50%', background: opt.accent, border: '2px solid rgba(255,255,255,0.3)', boxShadow: isActive ? `0 0 0 2px ${opt.accent}40` : 'none' }} />
            <span style={{ fontSize: 11, fontWeight: isActive ? 600 : 500, color: isActive ? 'var(--accent)' : 'var(--text-body)', fontFamily: 'var(--font-body)', lineHeight: 1.2, textAlign: 'center' }}>
              {opt.name}
            </span>
          </button>
        );
      })}
    </div>
  );
}
