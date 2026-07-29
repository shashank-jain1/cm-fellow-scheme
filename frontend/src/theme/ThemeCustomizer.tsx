import React, { useState, useRef, useEffect } from 'react';
import { useTheme, THEME_OPTIONS, type ThemeMode } from './ThemeContext';

type DesignStyle = 'boxy' | 'floating';
type BorderRadius = 'sharp' | 'default' | 'round';
type FontSize = 'small' | 'medium' | 'large';
type SidebarStyle = 'default' | 'compact';

interface CustomizerProps {
  sidebarCollapsed: boolean;
  onToggleSidebar: () => void;
}

interface CustomizerState {
  designStyle: DesignStyle;
  borderRadius: BorderRadius;
  fontSize: FontSize;
  sidebarStyle: SidebarStyle;
}

const STORAGE_KEY = 'cm_portal_customizer';

function loadCustomizer(): CustomizerState {
  try {
    const raw = localStorage.getItem(STORAGE_KEY);
    if (raw) return JSON.parse(raw);
  } catch {}
  return { designStyle: 'boxy', borderRadius: 'default', fontSize: 'medium', sidebarStyle: 'default' };
}

function saveCustomizer(state: CustomizerState) {
  localStorage.setItem(STORAGE_KEY, JSON.stringify(state));
}

export default function ThemeCustomizer({ sidebarCollapsed, onToggleSidebar }: CustomizerProps) {
  const { theme, setTheme } = useTheme();
  const [isOpen, setIsOpen] = useState(false);
  const [settings, setSettings] = useState<CustomizerState>(loadCustomizer);
  const panelRef = useRef<HTMLDivElement>(null);
  const btnRef = useRef<HTMLButtonElement>(null);

  useEffect(() => {
    const root = document.documentElement;
    root.setAttribute('data-design', settings.designStyle);
    root.setAttribute('data-radius', settings.borderRadius);
    root.setAttribute('data-fontsize', settings.fontSize);
    root.setAttribute('data-sidebar', settings.sidebarStyle);
  }, [settings]);

  useEffect(() => {
    saveCustomizer(settings);
  }, [settings]);

  useEffect(() => {
    const handleClick = (e: MouseEvent) => {
      if (
        isOpen &&
        panelRef.current &&
        !panelRef.current.contains(e.target as Node) &&
        btnRef.current &&
        !btnRef.current.contains(e.target as Node)
      ) {
        setIsOpen(false);
      }
    };
    document.addEventListener('mousedown', handleClick);
    return () => document.removeEventListener('mousedown', handleClick);
  }, [isOpen]);

  const update = <K extends keyof CustomizerState>(key: K, val: CustomizerState[K]) => {
    setSettings((prev) => ({ ...prev, [key]: val }));
  };

  return (
    <>
      <button
        ref={btnRef}
        type="button"
        onClick={() => setIsOpen(!isOpen)}
        title="Theme Settings"
        className="header-icon-btn"
        style={{
          background: isOpen ? 'var(--accent-light)' : undefined,
          color: isOpen ? 'var(--accent)' : undefined,
          borderColor: isOpen ? 'var(--accent-muted)' : undefined,
        }}
      >
        <i className="pi pi-cog" />
      </button>

      {isOpen && (
        <div
          ref={panelRef}
          className="theme-customizer-panel fade-in"
          style={{
            position: 'fixed',
            top: 0,
            right: 0,
            width: '320px',
            height: '100vh',
            background: 'var(--bg-surface)',
            borderLeft: '1px solid var(--border)',
            boxShadow: '-4px 0 24px rgba(0,0,0,0.12)',
            zIndex: 200,
            display: 'flex',
            flexDirection: 'column',
            overflow: 'hidden',
          }}
        >
          {/* Header */}
          <div
            style={{
              padding: '20px 24px 16px',
              borderBottom: '1px solid var(--border)',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'space-between',
              flexShrink: 0,
            }}
          >
            <div>
              <h3
                style={{
                  margin: 0,
                  fontFamily: 'var(--font-display)',
                  fontSize: 'var(--text-lg)',
                  fontWeight: 700,
                  color: 'var(--text-heading)',
                }}
              >
                Theme Settings
              </h3>
              <p
                style={{
                  margin: '2px 0 0',
                  fontSize: 'var(--text-xs)',
                  color: 'var(--text-muted)',
                }}
              >
                Customize your workspace look
              </p>
            </div>
            <button
              type="button"
              onClick={() => setIsOpen(false)}
              style={{
                width: 32,
                height: 32,
                borderRadius: 'var(--radius-sm)',
                border: '1px solid var(--border)',
                background: 'transparent',
                color: 'var(--text-muted)',
                cursor: 'pointer',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                fontSize: 14,
              }}
            >
              <i className="pi pi-times" />
            </button>
          </div>

          {/* Scrollable content */}
          <div
            style={{
              flex: 1,
              overflowY: 'auto',
              padding: '20px 24px',
              display: 'flex',
              flexDirection: 'column',
              gap: '28px',
            }}
          >
            {/* Color Theme */}
            <Section title="Color Theme">
              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '8px' }}>
                {THEME_OPTIONS.map((opt) => {
                  const isActive = opt.id === theme;
                  return (
                    <button
                      key={opt.id}
                      type="button"
                      onClick={() => setTheme(opt.id as ThemeMode)}
                      style={{
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        gap: '6px',
                        padding: '10px 6px',
                        borderRadius: 'var(--radius-md)',
                        border: isActive ? '2px solid var(--accent)' : '2px solid var(--border)',
                        background: isActive ? 'var(--accent-light)' : 'transparent',
                        cursor: 'pointer',
                        transition: 'all var(--transition-fast)',
                      }}
                    >
                      <div
                        style={{
                          width: 28,
                          height: 28,
                          borderRadius: '50%',
                          background: opt.accent,
                          border: '2px solid rgba(255,255,255,0.3)',
                          boxShadow: isActive ? `0 0 0 2px ${opt.accent}40` : 'none',
                        }}
                      />
                      <span
                        style={{
                          fontSize: 11,
                          fontWeight: isActive ? 600 : 500,
                          color: isActive ? 'var(--accent)' : 'var(--text-body)',
                          fontFamily: 'var(--font-body)',
                          lineHeight: 1.2,
                          textAlign: 'center',
                        }}
                      >
                        {opt.name}
                      </span>
                    </button>
                  );
                })}
              </div>
            </Section>

            {/* Design Style */}
            <Section title="Design Style">
              <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '8px' }}>
                <DesignOption
                  label="Boxy"
                  icon="pi-stop"
                  desc="Sharp edges, clean lines"
                  active={settings.designStyle === 'boxy'}
                  onClick={() => update('designStyle', 'boxy')}
                />
                <DesignOption
                  label="Floating"
                  icon="pi-external-link"
                  desc="Rounded, elevated cards"
                  active={settings.designStyle === 'floating'}
                  onClick={() => update('designStyle', 'floating')}
                />
              </div>
            </Section>

            {/* Border Radius */}
            <Section title="Corner Style">
              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '8px' }}>
                <RadiusOption label="Sharp" radius="4px" active={settings.borderRadius === 'sharp'} onClick={() => update('borderRadius', 'sharp')} />
                <RadiusOption label="Default" radius="8px" active={settings.borderRadius === 'default'} onClick={() => update('borderRadius', 'default')} />
                <RadiusOption label="Round" radius="16px" active={settings.borderRadius === 'round'} onClick={() => update('borderRadius', 'round')} />
              </div>
            </Section>

            {/* Font Size */}
            <Section title="Font Size">
              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '8px' }}>
                <FontOption label="Small" size={12} active={settings.fontSize === 'small'} onClick={() => update('fontSize', 'small')} />
                <FontOption label="Medium" size={14} active={settings.fontSize === 'medium'} onClick={() => update('fontSize', 'medium')} />
                <FontOption label="Large" size={16} active={settings.fontSize === 'large'} onClick={() => update('fontSize', 'large')} />
              </div>
            </Section>

            {/* Sidebar */}
            <Section title="Sidebar">
              <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '8px', marginBottom: '12px' }}>
                <DesignOption
                  label="Default"
                  icon="pi-bars"
                  desc="Standard width"
                  active={settings.sidebarStyle === 'default'}
                  onClick={() => update('sidebarStyle', 'default')}
                />
                <DesignOption
                  label="Compact"
                  icon="pi-minus"
                  desc="Narrower padding"
                  active={settings.sidebarStyle === 'compact'}
                  onClick={() => update('sidebarStyle', 'compact')}
                />
              </div>
              <button
                type="button"
                onClick={onToggleSidebar}
                style={{
                  width: '100%',
                  padding: '10px 12px',
                  borderRadius: 'var(--radius-md)',
                  border: '1px solid var(--border)',
                  background: sidebarCollapsed ? 'var(--accent-light)' : 'transparent',
                  color: sidebarCollapsed ? 'var(--accent)' : 'var(--text-body)',
                  cursor: 'pointer',
                  fontSize: 'var(--text-sm)',
                  fontWeight: 500,
                  fontFamily: 'var(--font-body)',
                  display: 'flex',
                  alignItems: 'center',
                  gap: '8px',
                  transition: 'all var(--transition-fast)',
                }}
              >
                <i className={`pi ${sidebarCollapsed ? 'pi-angle-right' : 'pi-angle-left'}`} style={{ fontSize: 12 }} />
                {sidebarCollapsed ? 'Expand Sidebar' : 'Collapse Sidebar'}
              </button>
            </Section>
          </div>

          {/* Reset footer */}
          <div
            style={{
              padding: '16px 24px',
              borderTop: '1px solid var(--border)',
              flexShrink: 0,
            }}
          >
            <button
              type="button"
              onClick={() => {
                setTheme('slate');
                setSettings({ designStyle: 'boxy', borderRadius: 'default', fontSize: 'medium', sidebarStyle: 'default' });
              }}
              style={{
                width: '100%',
                padding: '10px',
                borderRadius: 'var(--radius-md)',
                border: '1px solid var(--border)',
                background: 'transparent',
                color: 'var(--text-muted)',
                cursor: 'pointer',
                fontSize: 'var(--text-sm)',
                fontWeight: 500,
                fontFamily: 'var(--font-body)',
                transition: 'all var(--transition-fast)',
              }}
            >
              <i className="pi pi-replay" style={{ marginRight: 6, fontSize: 12 }} />
              Reset to Defaults
            </button>
          </div>
        </div>
      )}

      {isOpen && (
        <div
          style={{
            position: 'fixed',
            inset: 0,
            background: 'rgba(0,0,0,0.2)',
            zIndex: 199,
          }}
          onClick={() => setIsOpen(false)}
        />
      )}
    </>
  );
}

function Section({ title, children }: { title: string; children: React.ReactNode }) {
  return (
    <div>
      <label
        style={{
          display: 'block',
          fontSize: '11px',
          fontWeight: 600,
          textTransform: 'uppercase',
          letterSpacing: '0.06em',
          color: 'var(--text-muted)',
          marginBottom: '10px',
          fontFamily: 'var(--font-body)',
        }}
      >
        {title}
      </label>
      {children}
    </div>
  );
}

function DesignOption({
  label,
  icon,
  desc,
  active,
  onClick,
}: {
  label: string;
  icon: string;
  desc: string;
  active: boolean;
  onClick: () => void;
}) {
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

function RadiusOption({
  label,
  radius,
  active,
  onClick,
}: {
  label: string;
  radius: string;
  active: boolean;
  onClick: () => void;
}) {
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

function FontOption({
  label,
  size,
  active,
  onClick,
}: {
  label: string;
  size: number;
  active: boolean;
  onClick: () => void;
}) {
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
