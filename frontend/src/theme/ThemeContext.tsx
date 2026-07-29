import { createContext, useContext, useEffect, useState } from 'react';

export type ThemeMode = 'slate' | 'sandstone' | 'ledger' | 'emerald' | 'rose' | 'ocean' | 'amber' | 'plum' | 'coral';

export interface ThemeOption {
  id: ThemeMode;
  name: string;
  accent: string;
}

export const THEME_OPTIONS: ThemeOption[] = [
  { id: 'slate', name: 'Slate Registry', accent: '#35507A' },
  { id: 'sandstone', name: 'Sandstone', accent: '#B08A50' },
  { id: 'ledger', name: 'Post & Ledger', accent: '#4A4570' },
  { id: 'emerald', name: 'Emerald', accent: '#2D6A4F' },
  { id: 'rose', name: 'Rose', accent: '#A4133C' },
  { id: 'ocean', name: 'Ocean', accent: '#0077B6' },
  { id: 'amber', name: 'Amber', accent: '#B8860B' },
  { id: 'plum', name: 'Plum', accent: '#6B2FA0' },
  { id: 'coral', name: 'Coral', accent: '#C45B28' },
];

interface ThemeContextType {
  theme: ThemeMode;
  setTheme: (theme: ThemeMode) => void;
  currentThemeOption: ThemeOption;
  toggleTheme: () => void;
}

const ThemeContext = createContext<ThemeContextType | undefined>(undefined);

const STORAGE_KEY = 'cm_portal_theme';

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [theme, setThemeState] = useState<ThemeMode>(() => {
    const saved = localStorage.getItem(STORAGE_KEY) as ThemeMode;
    return saved && THEME_OPTIONS.some((t) => t.id === saved) ? saved : 'slate';
  });

  const setTheme = (newTheme: ThemeMode) => {
    setThemeState(newTheme);
    localStorage.setItem(STORAGE_KEY, newTheme);
  };

  const toggleTheme = () => {
    const idx = THEME_OPTIONS.findIndex((t) => t.id === theme);
    const next = THEME_OPTIONS[(idx + 1) % THEME_OPTIONS.length];
    setTheme(next.id);
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
