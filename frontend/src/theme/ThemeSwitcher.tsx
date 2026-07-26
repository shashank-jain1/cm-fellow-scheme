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
    <div style={{ display: 'inline-flex', alignItems: 'center', gap: '8px' }}>
      {/* Quick Sun/Moon Toggle Button */}
      <button
        type="button"
        onClick={toggleTheme}
        title={`Switch to ${currentThemeOption.isDark ? 'Light' : 'Dark'} Mode`}
        style={{
          width: '40px',
          height: '40px',
          borderRadius: 'var(--radius-md)',
          border: '1px solid var(--border-color)',
          background: 'var(--bg-card)',
          color: 'var(--text-primary)',
          cursor: 'pointer',
          display: 'inline-flex',
          alignItems: 'center',
          justifyContent: 'center',
          transition: 'all var(--transition-fast)',
          flexShrink: 0,
          boxShadow: 'var(--shadow-sm)',
        }}
      >
        <i
          className={`pi ${currentThemeOption.isDark ? 'pi-sun' : 'pi-moon'}`}
          style={{ fontSize: '16px', color: currentThemeOption.isDark ? '#fbbf24' : '#4f46e5' }}
        />
      </button>

      {/* Theme Select Dropdown Button */}
      <div ref={dropdownRef} style={{ position: 'relative', display: 'inline-block' }}>
        <button
          type="button"
          onClick={() => setIsOpen(!isOpen)}
          title={`Active Theme: ${currentThemeOption.name}`}
          style={{
            height: '40px',
            padding: '0 14px',
            borderRadius: 'var(--radius-md)',
            border: '1px solid var(--border-color)',
            background: 'var(--bg-card)',
            cursor: 'pointer',
            color: 'var(--text-primary)',
            fontSize: '13px',
            fontWeight: 600,
            transition: 'all var(--transition-fast)',
            boxShadow: 'var(--shadow-sm)',
            display: 'inline-flex',
            alignItems: 'center',
            gap: '8px',
            whiteSpace: 'nowrap',
            width: 'auto',
          }}
        >
          <span
            style={{
              width: '10px',
              height: '10px',
              borderRadius: '50%',
              backgroundColor: currentThemeOption.primaryColor,
              boxShadow: `0 0 6px ${currentThemeOption.primaryColor}`,
              flexShrink: 0,
            }}
          />
          <span style={{ whiteSpace: 'nowrap' }}>{currentThemeOption.name}</span>
          <i className="pi pi-chevron-down" style={{ fontSize: '10px', color: 'var(--text-muted)', marginLeft: '2px' }} />
        </button>

        {isOpen && (
          <div
            className="fade-in"
            style={{
              position: 'absolute',
              top: 'calc(100% + 6px)',
              right: 0,
              width: '210px',
              padding: '6px',
              borderRadius: 'var(--radius-md)',
              background: 'var(--bg-secondary)',
              border: '1px solid var(--border-color)',
              boxShadow: 'var(--shadow-lg)',
              zIndex: 1000,
            }}
          >
            <div
              style={{
                fontSize: '11px',
                fontWeight: 700,
                textTransform: 'uppercase',
                letterSpacing: '0.8px',
                color: 'var(--text-muted)',
                marginBottom: '6px',
                padding: '4px 8px',
              }}
            >
              Theme Preset
            </div>
            <div style={{ display: 'flex', flexDirection: 'column', gap: '2px' }}>
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
                      padding: '8px 10px',
                      borderRadius: 'var(--radius-sm)',
                      border: isActive ? '1px solid var(--border-highlight)' : '1px solid transparent',
                      background: isActive ? 'var(--accent-light)' : 'transparent',
                      color: isActive ? 'var(--accent-primary-hover)' : 'var(--text-primary)',
                      cursor: 'pointer',
                      fontSize: '13px',
                      fontWeight: isActive ? 600 : 500,
                      textAlign: 'left',
                      transition: 'all var(--transition-fast)',
                    }}
                  >
                    <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                      <span
                        style={{
                          width: '12px',
                          height: '12px',
                          borderRadius: '50%',
                          background: opt.primaryColor,
                          border: '1px solid rgba(0, 0, 0, 0.1)',
                          flexShrink: 0,
                        }}
                      />
                      {opt.name}
                    </div>
                    {isActive && <i className="pi pi-check" style={{ fontSize: '12px' }} />}
                  </button>
                );
              })}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};
