import { useState, useRef, useEffect } from 'react';
import { Outlet, useLocation, Link, useNavigate } from 'react-router-dom';
import Sidebar from './Sidebar';
import ThemeCustomizer from '../theme/ThemeCustomizer';
import { useAuth } from '../features/auth';

const breadcrumbMap: Record<string, string> = {
  dashboard: 'Dashboard',
  registration: 'Registration',
  admin: 'Admin',
  users: 'User Management',
  documents: 'Document Verification',
  training: 'Training & Meetings',
  new: 'Create Activity',
  'work-allocation': 'Work Allocation',
  attendance: 'Attendance & Leave',
  'apply-leave': 'Apply Leave',
  'leave-approval': 'Leave Approval',
  'leave-status': 'Leave Status',
  'leave-balance': 'Leave Balance',
  holidays: 'Holiday Calendar',
  'payroll-summary': 'Payroll Summary',
  performance: 'Performance Review',
  certificate: 'Exit & Certificates',
  apply: 'Apply Certificate',
  approvals: 'Certificate Approvals',
  exit: 'Exit Management',
  'help-desk': 'Help Desk & Tickets',
  masters: 'Master Settings',
  locations: 'Locations Master',
  projects: 'Projects Master',
  works: 'Works Master',
  'training-schedules': 'Training Schedules',
};

const searchIndex = [
  { title: 'Dashboard', category: 'Navigation', icon: 'pi pi-chart-bar', path: '/dashboard' },
  { title: 'Fellow Registration', category: 'Registration', icon: 'pi pi-user-plus', path: '/registration' },
  { title: 'User Management', category: 'Admin', icon: 'pi pi-users', path: '/admin/users' },
  { title: 'Document Verification', category: 'Admin', icon: 'pi pi-file-check', path: '/admin/documents' },
  { title: 'Activity Calendar', category: 'Training', icon: 'pi pi-calendar', path: '/training' },
  { title: 'Create Activity', category: 'Training', icon: 'pi pi-plus-circle', path: '/training/new' },
  { title: 'Work Allocation', category: 'Work', icon: 'pi pi-briefcase', path: '/work-allocation' },
  { title: 'Mark Attendance', category: 'Attendance', icon: 'pi pi-check-square', path: '/attendance' },
  { title: 'Apply Leave', category: 'Attendance', icon: 'pi pi-calendar-plus', path: '/attendance/apply-leave' },
  { title: 'Leave Approval', category: 'Attendance', icon: 'pi pi-check-circle', path: '/attendance/leave-approval' },
  { title: 'Leave Status', category: 'Attendance', icon: 'pi pi-info-circle', path: '/attendance/leave-status' },
  { title: 'Leave Balance', category: 'Attendance', icon: 'pi pi-wallet', path: '/attendance/leave-balance' },
  { title: 'Holiday Calendar', category: 'Attendance', icon: 'pi pi-calendar', path: '/attendance/holidays' },
  { title: 'Payroll Summary', category: 'Attendance', icon: 'pi pi-dollar', path: '/attendance/payroll-summary' },
  { title: 'Performance Review', category: 'Performance', icon: 'pi pi-star', path: '/performance' },
  { title: 'Apply Certificate', category: 'Certificates', icon: 'pi pi-file', path: '/certificate/apply' },
  { title: 'Certificate Approvals', category: 'Certificates', icon: 'pi pi-check-square', path: '/certificate/approvals' },
  { title: 'Exit Management', category: 'Certificates', icon: 'pi pi-sign-out', path: '/certificate/exit' },
  { title: 'Help Desk / Tickets', category: 'Support', icon: 'pi pi-ticket', path: '/help-desk' },
  { title: 'Raise New Ticket', category: 'Support', icon: 'pi pi-plus', path: '/help-desk/new' },
  { title: 'Locations Master', category: 'Masters', icon: 'pi pi-map-marker', path: '/masters/locations' },
  { title: 'Projects Master', category: 'Masters', icon: 'pi pi-folder', path: '/masters/projects' },
  { title: 'Works Master', category: 'Masters', icon: 'pi pi-cog', path: '/masters/works' },
  { title: 'Training Schedules Master', category: 'Masters', icon: 'pi pi-clock', path: '/masters/training-schedules' },
];

const mockNotifications = [
  { id: 1, title: 'New Leave Request', time: '10m ago', unread: true, icon: 'pi pi-calendar-plus', color: 'var(--accent)' },
  { id: 2, title: 'Activity Scheduled', time: '1h ago', unread: true, icon: 'pi pi-calendar', color: 'var(--info)' },
  { id: 3, title: 'Certificate Approved', time: '3h ago', unread: false, icon: 'pi pi-check-circle', color: 'var(--success)' },
];

export default function AppLayout() {
  const location = useLocation();
  const navigate = useNavigate();
  const { user, logout } = useAuth();
  const [sidebarCollapsed, setSidebarCollapsed] = useState(false);
  const [showNotifications, setShowNotifications] = useState(false);
  const [showUserMenu, setShowUserMenu] = useState(false);
  const [globalSearch, setGlobalSearch] = useState('');
  const [searchOpen, setSearchOpen] = useState(false);

  const searchInputRef = useRef<HTMLInputElement>(null);
  const searchContainerRef = useRef<HTMLDivElement>(null);
  const notifRef = useRef<HTMLDivElement>(null);
  const userRef = useRef<HTMLDivElement>(null);

  const segments = location.pathname.split('/').filter(Boolean);

  const filteredSearch = globalSearch.trim()
    ? searchIndex.filter(
        (item) =>
          item.title.toLowerCase().includes(globalSearch.toLowerCase()) ||
          item.category.toLowerCase().includes(globalSearch.toLowerCase())
      )
    : [];

  useEffect(() => {
    function handleKeyDown(e: KeyboardEvent) {
      if ((e.ctrlKey || e.metaKey) && e.key === 'k') {
        e.preventDefault();
        searchInputRef.current?.focus();
        setSearchOpen(true);
      }
      if (e.key === 'Escape') {
        setSearchOpen(false);
      }
    }

    function handleClickOutside(e: MouseEvent) {
      if (searchContainerRef.current && !searchContainerRef.current.contains(e.target as Node)) {
        setSearchOpen(false);
      }
      if (notifRef.current && !notifRef.current.contains(e.target as Node)) {
        setShowNotifications(false);
      }
      if (userRef.current && !userRef.current.contains(e.target as Node)) {
        setShowUserMenu(false);
      }
    }

    document.addEventListener('keydown', handleKeyDown);
    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('keydown', handleKeyDown);
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  const getInitials = (name?: string) => {
    if (!name) return 'U';
    return name.slice(0, 2).toUpperCase();
  };

  const handleSelectSearchResult = (path: string) => {
    navigate(path);
    setGlobalSearch('');
    setSearchOpen(false);
  };

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
          {/* Left: Sidebar Toggle & Breadcrumbs */}
          <div style={{ display: 'flex', alignItems: 'center', gap: 14 }}>
            <button
              className="header-icon-btn"
              onClick={() => setSidebarCollapsed(!sidebarCollapsed)}
              title={sidebarCollapsed ? 'Expand sidebar' : 'Collapse sidebar'}
            >
              <i className="pi pi-bars" />
            </button>

            <nav className="app-breadcrumb" aria-label="Breadcrumb">
              <Link to="/dashboard" className="breadcrumb-link" title="Dashboard">
                <i className="pi pi-home" style={{ fontSize: 13 }} />
              </Link>
              {segments.map((seg, i) => {
                const path = '/' + segments.slice(0, i + 1).join('/');
                const isLast = i === segments.length - 1;
                const label = breadcrumbMap[seg] || seg.replace(/-/g, ' ');

                return (
                  <span key={path} style={{ display: 'inline-flex', alignItems: 'center', gap: 6 }}>
                    <i className="pi pi-chevron-right breadcrumb-sep" style={{ fontSize: 10 }} />
                    {isLast ? (
                      <span className="breadcrumb-current">{label}</span>
                    ) : (
                      <Link to={path} className="breadcrumb-link">{label}</Link>
                    )}
                  </span>
                );
              })}
            </nav>
          </div>

          {/* Center: Global Search Bar */}
          <div ref={searchContainerRef} className="header-search-bar">
            <i className="pi pi-search header-search-icon" />
            <input
              ref={searchInputRef}
              type="text"
              placeholder="Search modules, fellows, activities..."
              value={globalSearch}
              onFocus={() => setSearchOpen(true)}
              onChange={(e) => {
                setGlobalSearch(e.target.value);
                setSearchOpen(true);
              }}
              className="header-search-input"
            />
            <kbd className="header-search-kbd">Ctrl K</kbd>

            {searchOpen && filteredSearch.length > 0 && (
              <div className="header-search-results">
                {filteredSearch.map((item) => (
                  <div
                    key={item.path}
                    className="search-result-item"
                    onClick={() => handleSelectSearchResult(item.path)}
                  >
                    <i className={item.icon} style={{ fontSize: 14, color: 'var(--accent)' }} />
                    <span style={{ fontWeight: 600 }}>{item.title}</span>
                    <span className="search-result-category">{item.category}</span>
                  </div>
                ))}
              </div>
            )}
          </div>

          {/* Right: Actions, Notifications & Profile */}
          <div className="app-header-actions">
            <ThemeCustomizer
              sidebarCollapsed={sidebarCollapsed}
              onToggleSidebar={() => setSidebarCollapsed(!sidebarCollapsed)}
            />

            {/* Notifications Popover */}
            <div ref={notifRef} style={{ position: 'relative' }}>
              <button
                className="header-icon-btn"
                title="Notifications"
                onClick={() => setShowNotifications(!showNotifications)}
              >
                <i className="pi pi-bell" />
                <span className="notification-dot" />
              </button>

              {showNotifications && (
                <div className="header-dropdown-menu notif-dropdown">
                  <div className="dropdown-header">
                    <span style={{ fontWeight: 700, fontSize: 14 }}>Notifications</span>
                    <span className="badge badge-info">2 New</span>
                  </div>
                  <div className="notif-list">
                    {mockNotifications.map((n) => (
                      <div key={n.id} className={`notif-item ${n.unread ? 'unread' : ''}`}>
                        <div className="notif-icon-box" style={{ background: 'var(--accent-light)', color: n.color }}>
                          <i className={n.icon} />
                        </div>
                        <div style={{ flex: 1 }}>
                          <div style={{ fontSize: 13, fontWeight: n.unread ? 600 : 400, color: 'var(--text-heading)' }}>
                            {n.title}
                          </div>
                          <div style={{ fontSize: 11, color: 'var(--text-muted)', marginTop: 2 }}>{n.time}</div>
                        </div>
                      </div>
                    ))}
                  </div>
                  <div className="dropdown-footer">
                    <button className="btn-link" onClick={() => setShowNotifications(false)}>View All Notifications</button>
                  </div>
                </div>
              )}
            </div>

            {/* User Profile Menu */}
            <div ref={userRef} style={{ position: 'relative' }}>
              <button
                className="user-profile-btn"
                onClick={() => setShowUserMenu(!showUserMenu)}
              >
                <div className="user-avatar-circle">
                  {getInitials(user?.username)}
                </div>
                <div className="user-profile-text">
                  <span className="user-profile-name">{user?.username ?? 'Admin User'}</span>
                  <span className="user-profile-role">{user?.role ?? 'Role'}</span>
                </div>
                <i className="pi pi-chevron-down" style={{ fontSize: 10, color: 'var(--text-muted)', marginLeft: 4 }} />
              </button>

              {showUserMenu && (
                <div className="header-dropdown-menu user-dropdown">
                  <div className="user-dropdown-header">
                    <div className="user-avatar-circle lg">
                      {getInitials(user?.username)}
                    </div>
                    <div>
                      <div style={{ fontWeight: 700, fontSize: 14, color: 'var(--text-heading)' }}>
                        {user?.username ?? 'Admin User'}
                      </div>
                      <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>{user?.role ?? 'System Administrator'}</div>
                    </div>
                  </div>
                  <div className="dropdown-divider" />
                  <div className="user-menu-items">
                    <button className="user-menu-item" onClick={() => { navigate('/admin/users'); setShowUserMenu(false); }}>
                      <i className="pi pi-users" />
                      <span>User Management</span>
                    </button>
                    <button className="user-menu-item" onClick={() => { navigate('/help-desk'); setShowUserMenu(false); }}>
                      <i className="pi pi-question-circle" />
                      <span>Help Desk & Support</span>
                    </button>
                  </div>
                  <div className="dropdown-divider" />
                  <button
                    className="user-menu-item danger"
                    onClick={() => {
                      logout();
                      navigate('/login');
                    }}
                  >
                    <i className="pi pi-sign-out" />
                    <span>Sign Out</span>
                  </button>
                </div>
              )}
            </div>
          </div>
        </header>

        <main className="app-content fade-in">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
