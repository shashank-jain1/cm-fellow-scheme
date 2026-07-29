import React, { useState, useRef, useEffect } from 'react';
import { useTheme, THEME_OPTIONS, type ThemeMode } from './ThemeContext';

export const ThemeSwitcher: React.FC = () => {
  const { theme, setTheme, currentThemeOption, toggleTheme } = useTheme();
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  return (
    <div style={{ display: 'inline-flex', alignItems: 'center', gap: '6px' }}>
      <button
        type="button"
        onClick={toggleTheme}
        title="Switch theme"
        style={{
          width: '36px',
          height: '36px',
          borderRadius: 'var(--radius-md)',
          border: '1px solid var(--border)',
          background: 'var(--bg-surface)',
          color: 'var(--text-muted)',
          cursor: 'pointer',
          display: 'inline-flex',
          alignItems: 'center',
          justifyContent: 'center',
          transition: 'all var(--transition-fast)',
          flexShrink: 0,
          fontSize: '15px',
        }}
      >
        <i className="pi pi-palette" />
      </button>

      <div ref={dropdownRef} style={{ position: 'relative', display: 'inline-block' }}>
        <button
          type="button"
          onClick={() => setIsOpen(!isOpen)}
          title="Select theme"
          style={{
            height: '36px',
            padding: '0 12px',
            borderRadius: 'var(--radius-md)',
            border: '1px solid var(--border)',
            background: 'var(--bg-surface)',
            cursor: 'pointer',
            color: 'var(--text-body)',
            fontSize: '13px',
            fontWeight: 600,
            fontFamily: 'var(--font-body)',
            transition: 'all var(--transition-fast)',
            display: 'inline-flex',
            alignItems: 'center',
            gap: '8px',
            whiteSpace: 'nowrap',
          }}
        >
          <span
            style={{
              width: '10px',
              height: '10px',
              borderRadius: '50%',
              background: currentThemeOption.accent,
              flexShrink: 0,
              border: '1px solid rgba(0,0,0,0.08)',
            }}
          />
          <span>{currentThemeOption.name}</span>
          <i className="pi pi-chevron-down" style={{ fontSize: '9px', color: 'var(--text-muted)' }} />
        </button>

        {isOpen && (
          <div
            className="fade-in"
            style={{
              position: 'absolute',
              top: 'calc(100% + 6px)',
              right: 0,
              width: '220px',
              maxHeight: '400px',
              overflowY: 'auto',
              padding: '6px',
              borderRadius: 'var(--radius-lg)',
              background: 'var(--bg-surface)',
              border: '1px solid var(--border)',
              boxShadow: 'var(--shadow-lg)',
              zIndex: 1000,
            }}
          >
            <div
              style={{
                fontSize: '11px',
                fontWeight: 600,
                textTransform: 'uppercase',
                letterSpacing: '0.06em',
                color: 'var(--text-muted)',
                padding: '4px 10px 6px',
                fontFamily: 'var(--font-body)',
              }}
            >
              Theme
            </div>
            {THEME_OPTIONS.map((opt) => {
              const isActive = opt.id === theme;
              return (
                <button
                  key={opt.id}
                  type="button"
                  onClick={() => {
                    setTheme(opt.id as ThemeMode);
                    setIsOpen(false);
                  }}
                  style={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                    width: '100%',
                    padding: '8px 10px',
                    borderRadius: 'var(--radius-sm)',
                    border: isActive ? '1px solid var(--accent-muted)' : '1px solid transparent',
                    background: isActive ? 'var(--accent-light)' : 'transparent',
                    color: isActive ? 'var(--accent)' : 'var(--text-body)',
                    cursor: 'pointer',
                    fontSize: '13px',
                    fontFamily: 'var(--font-body)',
                    fontWeight: isActive ? 600 : 500,
                    textAlign: 'left',
                    transition: 'all var(--transition-fast)',
                  }}
                >
                  <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
                    <span
                      style={{
                        width: '12px',
                        height: '12px',
                        borderRadius: '50%',
                        background: opt.accent,
                        flexShrink: 0,
                        border: '1px solid rgba(0,0,0,0.08)',
                      }}
                    />
                    {opt.name}
                  </div>
                  {isActive && <i className="pi pi-check" style={{ fontSize: '11px', color: 'var(--accent)' }} />}
                </button>
              );
            })}
          </div>
        )}
      </div>
    </div>
  );
};
