import { useTheme, THEME_OPTIONS, type ThemeMode } from '../ThemeContext';

interface ThemeDropdownProps {
  activeTheme: string;
  onSelect: (theme: ThemeMode) => void;
}

export default function ThemeDropdown({ activeTheme, onSelect }: ThemeDropdownProps) {
  return (
    <div className="fade-in" style={{ position: 'absolute', top: 'calc(100% + 6px)', right: 0, width: 220, maxHeight: 400, overflowY: 'auto', padding: 6, borderRadius: 'var(--radius-lg)', background: 'var(--bg-surface)', border: '1px solid var(--border)', boxShadow: 'var(--shadow-lg)', zIndex: 1000 }}>
      <div style={{ fontSize: 11, fontWeight: 600, textTransform: 'uppercase', letterSpacing: '0.06em', color: 'var(--text-muted)', padding: '4px 10px 6px' }}>Theme</div>
      {THEME_OPTIONS.map((opt) => {
        const isActive = opt.id === activeTheme;
        return (
          <button key={opt.id} type="button" onClick={() => onSelect(opt.id as ThemeMode)} style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', width: '100%', padding: '8px 10px', borderRadius: 'var(--radius-sm)', border: isActive ? '1px solid var(--accent-muted)' : '1px solid transparent', background: isActive ? 'var(--accent-light)' : 'transparent', color: isActive ? 'var(--accent)' : 'var(--text-body)', cursor: 'pointer', fontSize: 13, fontWeight: isActive ? 600 : 500, textAlign: 'left', transition: 'all var(--transition-fast)' }}>
            <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
              <span style={{ width: 12, height: 12, borderRadius: '50%', background: opt.accent, flexShrink: 0, border: '1px solid rgba(0,0,0,0.08)' }} />
              {opt.name}
            </div>
            {isActive && <i className="pi pi-check" style={{ fontSize: 11, color: 'var(--accent)' }} />}
          </button>
        );
      })}
    </div>
  );
}
