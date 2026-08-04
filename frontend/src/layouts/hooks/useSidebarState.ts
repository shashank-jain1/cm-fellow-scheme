import { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';

interface SidebarState {
  attendanceExpanded: boolean;
  certificateExpanded: boolean;
  workAllocationExpanded: boolean;
  performanceExpanded: boolean;
  helpDeskExpanded: boolean;
  mastersExpanded: boolean;
}

const expanders: (keyof SidebarState)[] = [
  'trainingExpanded',
  'attendanceExpanded',
  'certificateExpanded',
  'workAllocationExpanded',
  'performanceExpanded',
  'helpDeskExpanded',
  'mastersExpanded',
];

const pathMap: Record<string, keyof SidebarState> = {
  '/training': 'trainingExpanded',
  '/attendance': 'attendanceExpanded',
  '/certificate': 'certificateExpanded',
  '/work-allocation': 'workAllocationExpanded',
  '/performance': 'performanceExpanded',
  '/help-desk': 'helpDeskExpanded',
  '/masters': 'mastersExpanded',
};

export function useSidebarState() {
  const location = useLocation();
  const [state, setState] = useState<SidebarState>(() => {
    const initial: SidebarState = {
      trainingExpanded: false,
      attendanceExpanded: false,
      certificateExpanded: false,
      workAllocationExpanded: false,
      performanceExpanded: false,
      helpDeskExpanded: false,
      mastersExpanded: false,
    };
    for (const [path, key] of Object.entries(pathMap)) {
      if (location.pathname.startsWith(path)) initial[key] = true;
    }
    return initial;
  });

  useEffect(() => {
    for (const [path, key] of Object.entries(pathMap)) {
      if (location.pathname.startsWith(path)) {
        setState(prev => ({ ...prev, [key]: true }));
      }
    }
  }, [location.pathname]);

  const toggle = (key: keyof SidebarState) => {
    setState(prev => ({ ...prev, [key]: !prev[key] }));
  };

  return { state, toggle };
}
