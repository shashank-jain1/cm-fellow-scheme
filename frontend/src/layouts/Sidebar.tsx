import { useState } from 'react';
import { NavLink, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../features/auth';

const navItems = [
  { path: '/dashboard', label: 'Dashboard', icon: 'pi pi-home' },
  { path: '/registration', label: 'Registration', icon: 'pi pi-user-plus' },
  { path: '/training', label: 'Training', icon: 'pi pi-calendar' },
  { path: '/work-allocation', label: 'Work Allocation', icon: 'pi pi-briefcase' },
  { path: '/attendance', label: 'Attendance', icon: 'pi pi-clock' },
  { path: '/performance', label: 'Performance', icon: 'pi pi-chart-bar' },
  { path: '/certificate', label: 'Certificate', icon: 'pi pi-verified' },
  { path: '/help-desk', label: 'Help Desk', icon: 'pi pi-question-circle' },
];

const adminNavItems = [
  { path: '/admin/users', label: 'User Management', icon: 'pi pi-users' },
];

export default function Sidebar() {
  const [collapsed, setCollapsed] = useState(false);
  const location = useLocation();
  const navigate = useNavigate();
  const { user, logout } = useAuth();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <aside
      className="sidebar"
      style={{
        width: collapsed ? 'var(--sidebar-collapsed-width)' : 'var(--sidebar-width)',
        transition: 'width var(--transition-slow)',
      }}
    >
      <div className="sidebar-brand">
        <div className="sidebar-logo">
          <i className="pi pi-shield" style={{ fontSize: 22, color: 'var(--accent-primary)' }} />
        </div>
        {!collapsed && (
          <div className="sidebar-brand-text">
            <span className="sidebar-brand-title">CM Fellow</span>
            <span className="sidebar-brand-subtitle">Management System</span>
          </div>
        )}
      </div>

      <button
        className="sidebar-toggle"
        onClick={() => setCollapsed(!collapsed)}
        aria-label="Toggle sidebar"
      >
        <i className={`pi ${collapsed ? 'pi-angle-right' : 'pi-angle-left'}`} />
      </button>

      <nav className="sidebar-nav">
        {navItems.map((item) => {
          const isActive = location.pathname.startsWith(item.path);
          return (
            <NavLink
              key={item.path}
              to={item.path}
              className={`sidebar-link ${isActive ? 'active' : ''}`}
              title={collapsed ? item.label : undefined}
            >
              <div className="sidebar-link-icon">
                <i className={item.icon} />
                {isActive && <div className="sidebar-active-dot" />}
              </div>
              {!collapsed && <span className="sidebar-link-label">{item.label}</span>}
              {isActive && <div className="sidebar-active-indicator" />}
            </NavLink>
          );
        })}
        {user?.role === 'Admin' && (
          <>
            <div style={{ height: 1, background: 'var(--border-color)', margin: '8px 14px' }} />
            {adminNavItems.map((item) => {
              const isActive = location.pathname.startsWith(item.path);
              return (
                <NavLink
                  key={item.path}
                  to={item.path}
                  className={`sidebar-link ${isActive ? 'active' : ''}`}
                  title={collapsed ? item.label : undefined}
                >
                  <div className="sidebar-link-icon">
                    <i className={item.icon} />
                    {isActive && <div className="sidebar-active-dot" />}
                  </div>
                  {!collapsed && <span className="sidebar-link-label">{item.label}</span>}
                  {isActive && <div className="sidebar-active-indicator" />}
                </NavLink>
              );
            })}
          </>
        )}
      </nav>

      <div className="sidebar-footer">
        <div className="sidebar-user">
          <div className="sidebar-avatar">
            <i className="pi pi-user" />
          </div>
          {!collapsed && (
            <div className="sidebar-user-info">
              <span className="sidebar-user-name">{user?.username ?? 'User'}</span>
              <span className="sidebar-user-role">{user?.role ?? 'Role'}</span>
            </div>
          )}
        </div>
        <button className="sidebar-logout" title="Logout" onClick={handleLogout}>
          <i className="pi pi-sign-out" />
        </button>
      </div>
    </aside>
  );
}
