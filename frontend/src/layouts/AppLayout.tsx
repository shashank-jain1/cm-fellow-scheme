import { useState } from 'react';
import { Outlet } from 'react-router-dom';
import Sidebar from './Sidebar';
import ThemeCustomizer from '../theme/ThemeCustomizer';
import AppBreadcrumb from './components/AppBreadcrumb';
import AppSearch from './components/AppSearch';
import AppNotifications from './components/AppNotifications';
import AppUserMenu from './components/AppUserMenu';

export default function AppLayout() {
  const [sidebarCollapsed, setSidebarCollapsed] = useState(false);
  const [sidebarWidth, setSidebarWidth] = useState<number>(() => {
    const saved = localStorage.getItem('sidebar_custom_width');
    const parsed = saved ? parseInt(saved, 10) : 260;
    if (isNaN(parsed) || parsed < 200 || parsed > 320) {
      localStorage.removeItem('sidebar_custom_width');
      return 260;
    }
    return parsed;
  });

  const handleWidthChange = (w: number) => {
    const clamped = Math.min(Math.max(w, 210), 320);
    setSidebarWidth(clamped);
    localStorage.setItem('sidebar_custom_width', clamped.toString());
  };

  return (
    <div
      className="app-layout"
      style={{
        '--sidebar-width': `${sidebarWidth}px`,
        '--sidebar-current-width': sidebarCollapsed ? 'var(--sidebar-collapsed-width)' : `${sidebarWidth}px`,
      } as React.CSSProperties}
    >
      <Sidebar
        collapsed={sidebarCollapsed}
        onToggleCollapsed={setSidebarCollapsed}
        width={sidebarWidth}
        onWidthChange={handleWidthChange}
      />
      <div className="app-main">
        <header className="app-header">
          <div style={{ display: 'flex', alignItems: 'center', gap: 14 }}>
            <button
              className="header-icon-btn"
              onClick={() => setSidebarCollapsed(!sidebarCollapsed)}
              title={sidebarCollapsed ? 'Expand sidebar' : 'Collapse sidebar'}
            >
              <i className="pi pi-bars" />
            </button>
            <AppBreadcrumb />
          </div>

          <AppSearch />

          <div className="app-header-actions">
            <ThemeCustomizer
              sidebarCollapsed={sidebarCollapsed}
              onToggleSidebar={() => setSidebarCollapsed(!sidebarCollapsed)}
            />
            <AppNotifications />
            <AppUserMenu />
          </div>
        </header>

        <main className="app-content fade-in">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
