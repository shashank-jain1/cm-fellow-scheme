import { useEffect, useState, useCallback } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth, useRoleAccess } from '../features/auth';

const navItems = [
  { path: '/dashboard', label: 'Dashboard', icon: 'pi pi-home', moduleCode: 'DASHBOARD' },
  { path: '/registration', label: 'Registration', icon: 'pi pi-user-plus', moduleCode: 'REGISTRATION' },
  { path: '/training', label: 'Training', icon: 'pi pi-calendar', moduleCode: 'TRAINING' },
];

const attendanceSubItems = [
  { path: '/attendance', label: 'Mark Attendance', icon: 'pi pi-clock' },
  { path: '/attendance/report', label: 'Monthly Report', icon: 'pi pi-chart-line' },
  { path: '/attendance/weekly-report', label: 'Weekly Report', icon: 'pi pi-calendar' },
  { path: '/attendance/holidays', label: 'Holiday Calendar', icon: 'pi pi-calendar-plus' },
  { path: '/attendance/payroll-summary', label: 'Payroll Summary', icon: 'pi pi-money-bill' },
  { path: '/attendance/apply-leave', label: 'Apply Leave', icon: 'pi pi-send' },
  { path: '/attendance/leave-status', label: 'Leave Status', icon: 'pi pi-list' },
  { path: '/attendance/leave-balance', label: 'Leave Balance', icon: 'pi pi-wallet' },
  { path: '/attendance/leave-approval', label: 'Leave Approval', icon: 'pi pi-check-circle' },
];

const adminNavItems = [
  { path: '/admin/users', label: 'User Management', icon: 'pi pi-users', moduleCode: 'REGISTRATION' },
  { path: '/admin/access', label: 'User Access Management', icon: 'pi pi-key', moduleCode: 'ADMINISTRATION' },
  { path: '/admin/access/audit', label: 'Access Audit Log', icon: 'pi pi-history', moduleCode: 'ADMINISTRATION' },
  { path: '/admin/documents', label: 'Document Verification', icon: 'pi pi-file-check', moduleCode: 'REGISTRATION' },
  { path: '/admin/import', label: 'Bulk Import', icon: 'pi pi-upload', moduleCode: 'ADMINISTRATION' },
  { path: '/admin/backup', label: 'Database Backup', icon: 'pi pi-download', moduleCode: 'ADMINISTRATION' },
  { path: '/admin/seed', label: 'Seed Data', icon: 'pi pi-database', moduleCode: 'ADMINISTRATION' },
];

const certificateSubItems = [
  { path: '/certificate', label: 'Certificate Queue', icon: 'pi pi-list' },
  { path: '/certificate/apply', label: 'Apply for Certificate', icon: 'pi pi-send' },
  { path: '/certificate/verify', label: 'Verify Certificate', icon: 'pi pi-verified' },
  { path: '/certificate/exit', label: 'Exit Management', icon: 'pi pi-sign-out' },
  { path: '/certificate/exit-interview', label: 'Exit Interview', icon: 'pi pi-user-minus' },
];

const masterSubItems = [
  { path: '/masters/locations', label: 'Locations', icon: 'pi pi-map' },
  { path: '/masters/projects', label: 'Projects', icon: 'pi pi-briefcase' },
  { path: '/masters/works', label: 'Works', icon: 'pi pi-file-edit' },
  { path: '/masters/training-schedules', label: 'Training Schedule', icon: 'pi pi-calendar' },
  { path: '/masters/departments', label: 'Departments', icon: 'pi pi-building' },
];

const workAllocationSubItems = [
  { path: '/work-allocation', label: 'All Allocations', icon: 'pi pi-list' },
  { path: '/work-allocation/progress', label: 'Task Progress', icon: 'pi pi-chart-bar' },
];

const performanceSubItems = [
  { path: '/performance', label: 'Performance Reviews', icon: 'pi pi-chart-bar' },
  { path: '/performance/goals', label: 'Goals', icon: 'pi pi-star' },
  { path: '/performance/improvement-plans', label: 'Improvement Plans', icon: 'pi pi-arrow-up' },
  { path: '/performance/self-assessment', label: 'Self Assessment', icon: 'pi pi-user' },
  { path: '/performance/review-cycle', label: 'Review Cycles', icon: 'pi pi-sync' },
];

const helpDeskSubItems = [
  { path: '/help-desk', label: 'Ticket Queue', icon: 'pi pi-list' },
  { path: '/help-desk/new', label: 'Raise Ticket', icon: 'pi pi-plus-circle' },
  { path: '/help-desk/knowledge-base', label: 'Knowledge Base', icon: 'pi pi-book' },
];

interface SidebarProps {
  collapsed: boolean;
  onToggleCollapsed: (collapsed: boolean) => void;
  width?: number;
  onWidthChange?: (width: number) => void;
}

export default function Sidebar({ collapsed, onToggleCollapsed, width = 260, onWidthChange }: SidebarProps) {
  const location = useLocation();
  const navigate = useNavigate();
  const { user, logout } = useAuth();
  const [isResizing, setIsResizing] = useState(false);

  const [mastersExpanded, setMastersExpanded] = useState(() => location.pathname.startsWith('/masters'));
  const [attendanceExpanded, setAttendanceExpanded] = useState(() => location.pathname.startsWith('/attendance'));
  const [certificateExpanded, setCertificateExpanded] = useState(() => location.pathname.startsWith('/certificate'));
  const [workAllocationExpanded, setWorkAllocationExpanded] = useState(() => location.pathname.startsWith('/work-allocation'));
  const [performanceExpanded, setPerformanceExpanded] = useState(() => location.pathname.startsWith('/performance'));
  const [helpDeskExpanded, setHelpDeskExpanded] = useState(() => location.pathname.startsWith('/help-desk'));

  const isAttendanceActive = location.pathname.startsWith('/attendance');
  const isMastersActive = location.pathname.startsWith('/masters');
  const isCertificateActive = location.pathname.startsWith('/certificate');
  const isWorkAllocationActive = location.pathname.startsWith('/work-allocation');
  const isPerformanceActive = location.pathname.startsWith('/performance');
  const isHelpDeskActive = location.pathname.startsWith('/help-desk');

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

  useEffect(() => {
    if (isWorkAllocationActive) {
      setWorkAllocationExpanded(true);
    }
  }, [isWorkAllocationActive]);

  useEffect(() => {
    if (isPerformanceActive) {
      setPerformanceExpanded(true);
    }
  }, [isPerformanceActive]);

  useEffect(() => {
    if (isHelpDeskActive) {
      setHelpDeskExpanded(true);
    }
  }, [isHelpDeskActive]);

  const handleMouseDown = useCallback((e: React.MouseEvent) => {
    e.preventDefault();
    setIsResizing(true);
  }, []);

  useEffect(() => {
    if (!isResizing || !onWidthChange) return;

    const handleMouseMove = (e: MouseEvent) => {
      const newWidth = Math.min(Math.max(e.clientX, 210), 320);
      onWidthChange(newWidth);
    };

    const handleMouseUp = () => {
      setIsResizing(false);
    };

    document.addEventListener('mousemove', handleMouseMove);
    document.addEventListener('mouseup', handleMouseUp);
    document.body.style.userSelect = 'none';
    document.body.style.cursor = 'col-resize';

    return () => {
      document.removeEventListener('mousemove', handleMouseMove);
      document.removeEventListener('mouseup', handleMouseUp);
      document.body.style.userSelect = '';
      document.body.style.cursor = '';
    };
  }, [isResizing, onWidthChange]);

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  /** Exact-match active check for sub-items */
  const isExactActive = (itemPath: string) => location.pathname === itemPath;

  /** Check if user can see a module (Admin sees all) */
  const canSeeModule = (moduleCode: string) => {
    if (user?.role === 'Admin') return true;
    const access = user?.modules?.[moduleCode];
    return access != null && access.canRead;
  };

  const visibleNavItems = navItems.filter(item => canSeeModule(item.moduleCode));

  return (
    <aside
      className={`sidebar ${collapsed ? 'sidebar--collapsed' : ''}`}
      style={{
        width: collapsed ? 'var(--sidebar-collapsed-width)' : `${width}px`,
        transition: isResizing ? 'none' : 'width var(--transition-slow)',
      }}
    >
      <div className="sidebar-brand">
        <div className="sidebar-logo">
          <img src="/logo.jpeg" alt="Logo" style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
        </div>
        {!collapsed && (
          <div className="sidebar-brand-text">
            <span className="sidebar-brand-title">CM Fellow</span>
            <span className="sidebar-brand-subtitle">Management System</span>
          </div>
        )}
        <button
          type="button"
          className="sidebar-collapse-btn"
          onClick={() => onToggleCollapsed(!collapsed)}
          title={collapsed ? 'Expand sidebar' : 'Collapse sidebar'}
          style={{
            background: 'transparent',
            border: 'none',
            color: 'rgba(255, 255, 255, 0.7)',
            cursor: 'pointer',
            marginLeft: 'auto',
            padding: 4,
            display: 'flex',
            alignItems: 'center',
            borderRadius: 4,
          }}
        >
          <i className={`pi ${collapsed ? 'pi-chevron-right' : 'pi-chevron-left'}`} style={{ fontSize: 14 }} />
        </button>
      </div>

      <nav className="sidebar-nav">
        {visibleNavItems.map((item) => {
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

        {canSeeModule('WORK_ALLOCATION') && (
          <button
            type="button"
            className={`sidebar-link sidebar-link--group ${workAllocationExpanded ? 'expanded' : ''}`}
            onClick={() => setWorkAllocationExpanded(!workAllocationExpanded)}
            title={collapsed ? 'Work Allocation' : undefined}
            aria-expanded={workAllocationExpanded}
            aria-controls="work-allocation-submenu"
            style={{ width: '100%', border: 'none', background: 'none', cursor: 'pointer', textAlign: 'left' }}
          >
            <div className="sidebar-link-icon">
              <i className="pi pi-briefcase" />
            </div>
            {!collapsed && (
              <>
                <span className="sidebar-link-label" style={{ flex: 1 }}>Work Allocation</span>
                <i
                  className={`pi ${workAllocationExpanded ? 'pi-chevron-down' : 'pi-chevron-right'}`}
                  style={{ fontSize: 10, color: 'var(--text-muted)', transition: 'transform 0.15s' }}
                />
              </>
            )}
          </button>
        )}
        {!collapsed && workAllocationExpanded && (
          <div id="work-allocation-submenu" style={{ paddingLeft: 18, flexShrink: 0 }}>
            {workAllocationSubItems.map((item) => {
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

        {canSeeModule('PERFORMANCE') && (
          <button
            type="button"
            className={`sidebar-link sidebar-link--group ${performanceExpanded ? 'expanded' : ''}`}
            onClick={() => setPerformanceExpanded(!performanceExpanded)}
            title={collapsed ? 'Performance' : undefined}
            aria-expanded={performanceExpanded}
            aria-controls="performance-submenu"
            style={{ width: '100%', border: 'none', background: 'none', cursor: 'pointer', textAlign: 'left' }}
          >
            <div className="sidebar-link-icon">
              <i className="pi pi-chart-bar" />
            </div>
            {!collapsed && (
              <>
                <span className="sidebar-link-label" style={{ flex: 1 }}>Performance</span>
                <i
                  className={`pi ${performanceExpanded ? 'pi-chevron-down' : 'pi-chevron-right'}`}
                  style={{ fontSize: 10, color: 'var(--text-muted)', transition: 'transform 0.15s' }}
                />
              </>
            )}
          </button>
        )}
        {!collapsed && performanceExpanded && (
          <div id="performance-submenu" style={{ paddingLeft: 18, flexShrink: 0 }}>
            {performanceSubItems.map((item) => {
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

        {canSeeModule('HELP_DESK') && (
          <button
            type="button"
            className={`sidebar-link sidebar-link--group ${helpDeskExpanded ? 'expanded' : ''}`}
            onClick={() => setHelpDeskExpanded(!helpDeskExpanded)}
            title={collapsed ? 'Help Desk' : undefined}
            aria-expanded={helpDeskExpanded}
            aria-controls="help-desk-submenu"
            style={{ width: '100%', border: 'none', background: 'none', cursor: 'pointer', textAlign: 'left' }}
          >
            <div className="sidebar-link-icon">
              <i className="pi pi-question-circle" />
            </div>
            {!collapsed && (
              <>
                <span className="sidebar-link-label" style={{ flex: 1 }}>Help Desk</span>
                <i
                  className={`pi ${helpDeskExpanded ? 'pi-chevron-down' : 'pi-chevron-right'}`}
                  style={{ fontSize: 10, color: 'var(--text-muted)', transition: 'transform 0.15s' }}
                />
              </>
            )}
          </button>
        )}
        {!collapsed && helpDeskExpanded && (
          <div id="help-desk-submenu" style={{ paddingLeft: 18, flexShrink: 0 }}>
            {helpDeskSubItems.map((item) => {
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

      {!collapsed && (
        <div
          className={`sidebar-resizer ${isResizing ? 'active' : ''}`}
          onMouseDown={handleMouseDown}
          title="Drag to adjust sidebar width"
        />
      )}
    </aside>
  );
}
