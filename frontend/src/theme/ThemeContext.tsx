import React, { createContext, useContext, useEffect, useState } from 'react';

export type ThemeMode = 'light' | 'dark' | 'corporate-light' | 'emerald-dark';

export interface ThemeOption {
  id: ThemeMode;
  name: string;
  primaryColor: string;
  previewGradient: string;
  isDark: boolean;
}

export const THEME_OPTIONS: ThemeOption[] = [
  {
    id: 'light',
    name: 'Clean Light',
    primaryColor: '#4f46e5',
    previewGradient: 'linear-gradient(135deg, #ffffff 0%, #4f46e5 100%)',
    isDark: false,
  },
  {
    id: 'corporate-light',
    name: 'Emerald Light',
    primaryColor: '#059669',
    previewGradient: 'linear-gradient(135deg, #ffffff 0%, #059669 100%)',
    isDark: false,
  },
  {
    id: 'dark',
    name: 'Pro Dark',
    primaryColor: '#6366f1',
    previewGradient: 'linear-gradient(135deg, #0f172a 0%, #6366f1 100%)',
    isDark: true,
  },
  {
    id: 'emerald-dark',
    name: 'Emerald Dark',
    primaryColor: '#10b981',
    previewGradient: 'linear-gradient(135deg, #0b0f19 0%, #10b981 100%)',
    isDark: true,
  },
];

interface ThemeContextType {
  theme: ThemeMode;
  setTheme: (theme: ThemeMode) => void;
  currentThemeOption: ThemeOption;
  toggleTheme: () => void;
}

const ThemeContext = createContext<ThemeContextType | undefined>(undefined);

const STORAGE_KEY = 'cm_fellow_theme';

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [theme, setThemeState] = useState<ThemeMode>(() => {
    const saved = localStorage.getItem(STORAGE_KEY) as ThemeMode;
    return saved && THEME_OPTIONS.some((t) => t.id === saved) ? saved : 'light';
  });

  const setTheme = (newTheme: ThemeMode) => {
    setThemeState(newTheme);
    localStorage.setItem(STORAGE_KEY, newTheme);
  };

  const toggleTheme = () => {
    const next = currentThemeOption.isDark ? 'light' : 'dark';
    setTheme(next);
  };

  useEffect(() => {
    document.documentElement.setAttribute('data-theme', theme);
  }, [theme]);

  const currentThemeOption = THEME_OPTIONS.find((t) => t.id === theme) || THEME_OPTIONS[0];

  return (
    <ThemeContext.Provider value={{ theme, setTheme, currentThemeOption, toggleTheme }}>
      {children}
    </ThemeContext.Provider>
  );
};

export const useTheme = (): ThemeContextType => {
  const context = useContext(ThemeContext);
  if (!context) {
    throw new Error('useTheme must be used within a ThemeProvider');
  }
  return context;
};
