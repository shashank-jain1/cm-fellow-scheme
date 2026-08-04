import { useState, useRef, useEffect } from 'react';
import { useTheme, type ThemeMode } from './ThemeContext';
import Section from './components/Section';
import ThemeGrid from './components/ThemeGrid';
import DesignOption from './components/DesignOption';
import RadiusOption from './components/RadiusOption';
import FontOption from './components/FontOption';
import SidebarStyleSection from './components/SidebarStyleSection';

interface CustomizerProps {
  sidebarCollapsed: boolean;
  onToggleSidebar: () => void;
}

interface CustomizerState {
  designStyle: 'boxy' | 'floating';
  borderRadius: 'sharp' | 'default' | 'round';
  fontSize: 'small' | 'medium' | 'large';
  sidebarStyle: 'default' | 'compact';
}

const STORAGE_KEY = 'cm_portal_customizer';
const defaults: CustomizerState = { designStyle: 'boxy', borderRadius: 'default', fontSize: 'medium', sidebarStyle: 'default' };

function loadSettings(): CustomizerState {
  try { return JSON.parse(localStorage.getItem(STORAGE_KEY) ?? '') || defaults; } catch { return defaults; }
}

export default function ThemeCustomizer({ sidebarCollapsed, onToggleSidebar }: CustomizerProps) {
  const { theme, setTheme } = useTheme();
  const [isOpen, setIsOpen] = useState(false);
  const [s, setS] = useState<CustomizerState>(loadSettings);
  const panelRef = useRef<HTMLDivElement>(null);
  const btnRef = useRef<HTMLButtonElement>(null);

  useEffect(() => { const r = document.documentElement; r.setAttribute('data-design', s.designStyle); r.setAttribute('data-radius', s.borderRadius); r.setAttribute('data-fontsize', s.fontSize); r.setAttribute('data-sidebar', s.sidebarStyle); }, [s]);
  useEffect(() => { localStorage.setItem(STORAGE_KEY, JSON.stringify(s)); }, [s]);
  useEffect(() => {
    const h = (e: MouseEvent) => { if (isOpen && panelRef.current && !panelRef.current.contains(e.target as Node) && btnRef.current && !btnRef.current.contains(e.target as Node)) setIsOpen(false); };
    document.addEventListener('mousedown', h); return () => document.removeEventListener('mousedown', h);
  }, [isOpen]);

  const update = <K extends keyof CustomizerState>(k: K, v: CustomizerState[k]) => setS(p => ({ ...p, [k]: v }));

  return (
    <>
      <button ref={btnRef} type="button" onClick={() => setIsOpen(!isOpen)} title="Theme Settings" className="header-icon-btn" style={{ background: isOpen ? 'var(--accent-light)' : undefined, color: isOpen ? 'var(--accent)' : undefined }}>
        <i className="pi pi-cog" />
      </button>
      {isOpen && <div style={{ position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.2)', zIndex: 199 }} onClick={() => setIsOpen(false)} />}
      {isOpen && (
        <div ref={panelRef} className="theme-customizer-panel fade-in" style={{ position: 'fixed', top: 0, right: 0, width: 320, height: '100vh', background: 'var(--bg-surface)', borderLeft: '1px solid var(--border)', boxShadow: '-4px 0 24px rgba(0,0,0,0.12)', zIndex: 200, display: 'flex', flexDirection: 'column' }}>
          <div style={{ padding: '20px 24px 16px', borderBottom: '1px solid var(--border)', display: 'flex', justifyContent: 'space-between', flexShrink: 0 }}>
            <div><h3 style={{ margin: 0, fontSize: 'var(--text-lg)', fontWeight: 700 }}>Theme Settings</h3><p style={{ margin: '2px 0 0', fontSize: 'var(--text-xs)', color: 'var(--text-muted)' }}>Customize your workspace</p></div>
            <button type="button" onClick={() => setIsOpen(false)} style={{ width: 32, height: 32, borderRadius: 'var(--radius-sm)', border: '1px solid var(--border)', background: 'transparent', cursor: 'pointer' }}><i className="pi pi-times" /></button>
          </div>
          <div style={{ flex: 1, overflowY: 'auto', padding: '20px 24px', display: 'flex', flexDirection: 'column', gap: 28 }}>
            <Section title="Color Theme"><ThemeGrid activeTheme={theme} onSelect={setTheme} /></Section>
            <Section title="Design Style"><div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 8 }}><DesignOption label="Boxy" icon="pi-stop" desc="Sharp edges" active={s.designStyle === 'boxy'} onClick={() => update('designStyle', 'boxy')} /><DesignOption label="Floating" icon="pi-external-link" desc="Rounded cards" active={s.designStyle === 'floating'} onClick={() => update('designStyle', 'floating')} /></div></Section>
            <Section title="Corner Style"><div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: 8 }}><RadiusOption label="Sharp" radius="4px" active={s.borderRadius === 'sharp'} onClick={() => update('borderRadius', 'sharp')} /><RadiusOption label="Default" radius="8px" active={s.borderRadius === 'default'} onClick={() => update('borderRadius', 'default')} /><RadiusOption label="Round" radius="16px" active={s.borderRadius === 'round'} onClick={() => update('borderRadius', 'round')} /></div></Section>
            <Section title="Font Size"><div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: 8 }}><FontOption label="Small" size={12} active={s.fontSize === 'small'} onClick={() => update('fontSize', 'small')} /><FontOption label="Medium" size={14} active={s.fontSize === 'medium'} onClick={() => update('fontSize', 'medium')} /><FontOption label="Large" size={16} active={s.fontSize === 'large'} onClick={() => update('fontSize', 'large')} /></div></Section>
            <Section title="Sidebar"><SidebarStyleSection sidebarStyle={s.sidebarStyle} sidebarCollapsed={sidebarCollapsed} onStyleChange={(v) => update('sidebarStyle', v as 'default' | 'compact')} onToggleCollapse={onToggleSidebar} /></Section>
          </div>
          <div style={{ padding: '16px 24px', borderTop: '1px solid var(--border)', flexShrink: 0 }}>
            <button type="button" onClick={() => { setTheme('slate'); setS(defaults); }} style={{ width: '100%', padding: 10, borderRadius: 'var(--radius-md)', border: '1px solid var(--border)', background: 'transparent', color: 'var(--text-muted)', cursor: 'pointer', fontSize: 'var(--text-sm)' }}>
              <i className="pi pi-replay" style={{ marginRight: 6 }} />Reset to Defaults
            </button>
          </div>
        </div>
      )}
    </>
  );
}
