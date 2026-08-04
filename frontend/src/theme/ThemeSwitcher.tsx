import { useState, useRef, useEffect } from 'react';
import { useTheme } from './ThemeContext';
import ThemeDropdown from './components/ThemeDropdown';

export default function ThemeSwitcher() {
  const { theme, setTheme, currentThemeOption, toggleTheme } = useTheme();
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const h = (e: MouseEvent) => { if (dropdownRef.current && !dropdownRef.current.contains(e.target as Node)) setIsOpen(false); };
    document.addEventListener('mousedown', h); return () => document.removeEventListener('mousedown', h);
  }, []);

  return (
    <div style={{ display: 'inline-flex', alignItems: 'center', gap: 6 }}>
      <button type="button" onClick={toggleTheme} title="Switch theme" style={{ width: 36, height: 36, borderRadius: 'var(--radius-md)', border: '1px solid var(--border)', background: 'var(--bg-surface)', color: 'var(--text-muted)', cursor: 'pointer', display: 'inline-flex', alignItems: 'center', justifyContent: 'center', fontSize: 15 }}>
        <i className="pi pi-palette" />
      </button>
      <div ref={dropdownRef} style={{ position: 'relative', display: 'inline-block' }}>
        <button type="button" onClick={() => setIsOpen(!isOpen)} title="Select theme" style={{ height: 36, padding: '0 12px', borderRadius: 'var(--radius-md)', border: '1px solid var(--border)', background: 'var(--bg-surface)', cursor: 'pointer', fontSize: 13, fontWeight: 600, display: 'inline-flex', alignItems: 'center', gap: 8 }}>
          <span style={{ width: 10, height: 10, borderRadius: '50%', background: currentThemeOption.accent, border: '1px solid rgba(0,0,0,0.08)' }} />
          <span>{currentThemeOption.name}</span>
          <i className="pi pi-chevron-down" style={{ fontSize: 9, color: 'var(--text-muted)' }} />
        </button>
        {isOpen && <ThemeDropdown activeTheme={theme} onSelect={(t) => { setTheme(t); setIsOpen(false); }} />}
      </div>
    </div>
  );
}
