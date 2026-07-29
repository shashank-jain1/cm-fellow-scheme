import { useEffect, useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../features/auth';

const navItems = [
  { path: '/dashboard', label: 'Dashboard', icon: 'pi pi-home' },
  { path: '/registration', label: 'Registration', icon: 'pi pi-user-plus' },
  { path: '/training', label: 'Training', icon: 'pi pi-calendar' },
  { path: '/work-allocation', label: 'Work Allocation', icon: 'pi pi-briefcase' },
  { path: '/performance', label: 'Performance', icon: 'pi pi-chart-bar' },
  { path: '/help-desk', label: 'Help Desk', icon: 'pi pi-question-circle' },
];

const attendanceSubItems = [
  { path: '/attendance', label: 'Mark Attendance', icon: 'pi pi-clock' },
  { path: '/attendance/holidays', label: 'Holiday Calendar', icon: 'pi pi-calendar-plus' },
  { path: '/attendance/payroll-summary', label: 'Payroll Summary', icon: 'pi pi-money-bill' },
  { path: '/attendance/apply-leave', label: 'Apply Leave', icon: 'pi pi-send' },
  { path: '/attendance/leave-status', label: 'Leave Status', icon: 'pi pi-list' },
  { path: '/attendance/leave-balance', label: 'Leave Balance', icon: 'pi pi-wallet' },
];

const adminNavItems = [
  { path: '/admin/users', label: 'User Management', icon: 'pi pi-users' },
  { path: '/admin/documents', label: 'Document Verification', icon: 'pi pi-file-check' },
];

const certificateSubItems = [
  { path: '/certificate', label: 'Certificate Queue', icon: 'pi pi-list' },
  { path: '/certificate/apply', label: 'Apply for Certificate', icon: 'pi pi-send' },
  { path: '/certificate/exit', label: 'Exit Management', icon: 'pi pi-sign-out' },
];

const masterSubItems = [
  { path: '/masters/locations', label: 'Locations', icon: 'pi pi-map' },
  { path: '/masters/projects', label: 'Projects', icon: 'pi pi-briefcase' },
  { path: '/masters/works', label: 'Works', icon: 'pi pi-file-edit' },
  { path: '/masters/training-schedules', label: 'Training Schedule', icon: 'pi pi-calendar' },
];

interface SidebarProps {
  collapsed: boolean;
  onToggleCollapsed: (collapsed: boolean) => void;
}

export default function Sidebar({ collapsed, onToggleCollapsed }: SidebarProps) {
  const location = useLocation();
  const navigate = useNavigate();
  const { user, logout } = useAuth();

  const [mastersExpanded, setMastersExpanded] = useState(() => location.pathname.startsWith('/masters'));
  const [attendanceExpanded, setAttendanceExpanded] = useState(() => location.pathname.startsWith('/attendance'));
  const [certificateExpanded, setCertificateExpanded] = useState(() => location.pathname.startsWith('/certificate'));

  const isAttendanceActive = location.pathname.startsWith('/attendance');
  const isMastersActive = location.pathname.startsWith('/masters');
  const isCertificateActive = location.pathname.startsWith('/certificate');

  useEffect(() => {
    if (isAttendanceActive) {
      setAttendanceExpanded(true);
    }
  }, [isAttendanceActive]);

  useEffect(() => {
    if (isMastersActive) {
      setMastersExpanded(true);
    }
  }, [isMastersActive]);

  useEffect(() => {
    if (isCertificateActive) {
      setCertificateExpanded(true);
    }
  }, [isCertificateActive]);

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  /** Exact-match active check for sub-items */
  const isExactActive = (itemPath: string) => location.pathname === itemPath;

  return (
    <aside
      className={`sidebar ${collapsed ? 'sidebar--collapsed' : ''}`}
      style={{
        width: collapsed ? 'var(--sidebar-collapsed-width)' : 'var(--sidebar-width)',
        transition: 'width var(--transition-slow)',
      }}
    >
      <div className="sidebar-brand">
        <div className="sidebar-logo">
          <img src="/logo.jpeg" alt="Logo" style={{ width: collapsed ? 28 : 36, height: collapsed ? 28 : 36, borderRadius: '50%', objectFit: 'cover' }} />
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
        type="button"
        onClick={() => onToggleCollapsed(!collapsed)}
        aria-label="Toggle sidebar"
      >
        <i className={`pi ${collapsed ? 'pi-angle-right' : 'pi-angle-left'}`} />
      </button>

      <nav className="sidebar-nav">
        {navItems.map((item) => {
          const isActive = location.pathname === item.path || (item.path !== '/dashboard' && location.pathname.startsWith(item.path));
          return (
            <Link
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
            </Link>
          );
        })}

        <button
          type="button"
          className={`sidebar-link sidebar-link--group ${attendanceExpanded ? 'expanded' : ''}`}
          onClick={() => setAttendanceExpanded(!attendanceExpanded)}
          title={collapsed ? 'Attendance' : undefined}
          aria-expanded={attendanceExpanded}
          aria-controls="attendance-submenu"
          style={{ width: '100%', border: 'none', background: 'none', cursor: 'pointer', textAlign: 'left' }}
        >
          <div className="sidebar-link-icon">
            <i className="pi pi-clock" />
          </div>
          {!collapsed && (
            <>
              <span className="sidebar-link-label" style={{ flex: 1 }}>Attendance</span>
              <i
                className={`pi ${attendanceExpanded ? 'pi-chevron-down' : 'pi-chevron-right'}`}
                style={{ fontSize: 10, color: 'var(--text-muted)', transition: 'transform 0.15s' }}
              />
            </>
          )}
        </button>
        {!collapsed && attendanceExpanded && (
          <div id="attendance-submenu" style={{ paddingLeft: 18, flexShrink: 0 }}>
            {attendanceSubItems.map((item) => {
              const isActive = isExactActive(item.path);
              return (
                <Link
                  key={item.path}
                  to={item.path}
                  className={`sidebar-link ${isActive ? 'active' : ''}`}
                  style={{ paddingLeft: 12, fontSize: 13 }}
                >
                  <div className="sidebar-link-icon" style={{ width: 20, height: 20 }}>
                    <i className={item.icon} style={{ fontSize: 12 }} />
                    {isActive && <div className="sidebar-active-dot" />}
                  </div>
                  <span className="sidebar-link-label">{item.label}</span>
                  {isActive && <div className="sidebar-active-indicator" />}
                </Link>
              );
            })}
          </div>
        )}

        <button
          type="button"
          className={`sidebar-link sidebar-link--group ${certificateExpanded ? 'expanded' : ''}`}
          onClick={() => setCertificateExpanded(!certificateExpanded)}
          title={collapsed ? 'Certificate' : undefined}
          aria-expanded={certificateExpanded}
          aria-controls="certificate-submenu"
          style={{ width: '100%', border: 'none', background: 'none', cursor: 'pointer', textAlign: 'left' }}
        >
          <div className="sidebar-link-icon">
            <i className="pi pi-verified" />
          </div>
          {!collapsed && (
            <>
              <span className="sidebar-link-label" style={{ flex: 1 }}>Certificate</span>
              <i
                className={`pi ${certificateExpanded ? 'pi-chevron-down' : 'pi-chevron-right'}`}
                style={{ fontSize: 10, color: 'var(--text-muted)', transition: 'transform 0.15s' }}
              />
            </>
          )}
        </button>
        {!collapsed && certificateExpanded && (
          <div id="certificate-submenu" style={{ paddingLeft: 18, flexShrink: 0 }}>
            {certificateSubItems.map((item) => {
              const isActive = isExactActive(item.path);
              return (
                <Link
                  key={item.path}
                  to={item.path}
                  className={`sidebar-link ${isActive ? 'active' : ''}`}
                  style={{ paddingLeft: 12, fontSize: 13 }}
                >
                  <div className="sidebar-link-icon" style={{ width: 20, height: 20 }}>
                    <i className={item.icon} style={{ fontSize: 12 }} />
                    {isActive && <div className="sidebar-active-dot" />}
                  </div>
                  <span className="sidebar-link-label">{item.label}</span>
                  {isActive && <div className="sidebar-active-indicator" />}
                </Link>
              );
            })}
          </div>
        )}

        {user?.role === 'Admin' && (
          <>
            <div style={{ height: 1, background: 'var(--border-color)', margin: '8px 14px', flexShrink: 0 }} />
            {adminNavItems.map((item) => {
              const isActive = location.pathname.startsWith(item.path);
              return (
                <Link
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
                </Link>
              );
            })}
            <button
              type="button"
              className={`sidebar-link sidebar-link--group ${mastersExpanded ? 'expanded' : ''}`}
              onClick={() => setMastersExpanded(!mastersExpanded)}
              title={collapsed ? 'Masters' : undefined}
              aria-expanded={mastersExpanded}
              aria-controls="masters-submenu"
              style={{ width: '100%', border: 'none', background: 'none', cursor: 'pointer', textAlign: 'left' }}
            >
              <div className="sidebar-link-icon">
                <i className="pi pi-database" />
              </div>
              {!collapsed && (
                <>
                  <span className="sidebar-link-label" style={{ flex: 1 }}>Masters</span>
                  <i
                    className={`pi ${mastersExpanded ? 'pi-chevron-down' : 'pi-chevron-right'}`}
                    style={{ fontSize: 10, color: 'var(--text-muted)', transition: 'transform 0.15s' }}
                  />
                </>
              )}
            </button>
            {!collapsed && mastersExpanded && (
              <div id="masters-submenu" style={{ paddingLeft: 18, flexShrink: 0 }}>
                {masterSubItems.map((item) => {
                  const isActive = isExactActive(item.path);
                  return (
                    <Link
                      key={item.path}
                      to={item.path}
                      className={`sidebar-link ${isActive ? 'active' : ''}`}
                      style={{ paddingLeft: 12, fontSize: 13 }}
                    >
                      <div className="sidebar-link-icon" style={{ width: 20, height: 20 }}>
                        <i className={item.icon} style={{ fontSize: 12 }} />
                        {isActive && <div className="sidebar-active-dot" />}
                      </div>
                      <span className="sidebar-link-label">{item.label}</span>
                      {isActive && <div className="sidebar-active-indicator" />}
                    </Link>
                  );
                })}
              </div>
            )}
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
