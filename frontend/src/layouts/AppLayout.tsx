import { useState } from 'react';
import { Outlet, useLocation } from 'react-router-dom';
import Sidebar from './Sidebar';
import ThemeCustomizer from '../theme/ThemeCustomizer';

const breadcrumbMap: Record<string, string> = {
  dashboard: 'Dashboard',
  registration: 'Registration',
  training: 'Training',
  'work-allocation': 'Work Allocation',
  attendance: 'Attendance',
  performance: 'Performance',
  certificate: 'Certificate',
  'help-desk': 'Help Desk',
};

export default function AppLayout() {
  const location = useLocation();
  const [sidebarCollapsed, setSidebarCollapsed] = useState(false);
  const segments = location.pathname.split('/').filter(Boolean);
  const crumbs = segments.map((seg) => breadcrumbMap[seg] || seg);

  return (
    <div
      className="app-layout"
      style={{
        '--sidebar-current-width': sidebarCollapsed ? 'var(--sidebar-collapsed-width)' : 'var(--sidebar-width)',
      } as React.CSSProperties}
    >
      <Sidebar collapsed={sidebarCollapsed} onToggleCollapsed={setSidebarCollapsed} />
      <div className="app-main">
        <header className="app-header">
          <div className="app-breadcrumb">
            <i className="pi pi-home" style={{ fontSize: 14, color: 'var(--text-muted)' }} />
            {crumbs.map((crumb, i) => (
              <span key={i} className="breadcrumb-item" style={{ display: 'inline-flex', alignItems: 'center' }}>
                <span className="breadcrumb-sep">/</span>
                <span className={i === crumbs.length - 1 ? 'breadcrumb-current' : ''}>
                  {crumb}
                </span>
              </span>
            ))}
          </div>

          <div className="app-header-actions">
            <ThemeCustomizer
              sidebarCollapsed={sidebarCollapsed}
              onToggleSidebar={() => setSidebarCollapsed(!sidebarCollapsed)}
            />
            <button className="header-icon-btn" title="Notifications">
              <i className="pi pi-bell" />
              <span className="notification-dot" />
            </button>
          </div>
        </header>
        <main className="app-content fade-in">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
