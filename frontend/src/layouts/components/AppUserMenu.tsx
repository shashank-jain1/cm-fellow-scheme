import { useState, useRef, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../features/auth';

export default function AppUserMenu() {
  const navigate = useNavigate();
  const { user, logout } = useAuth();
  const [showUserMenu, setShowUserMenu] = useState(false);
  const userRef = useRef<HTMLDivElement>(null);

  const getInitials = (name?: string) => {
    if (!name) return 'U';
    return name.slice(0, 2).toUpperCase();
  };

  useEffect(() => {
    function handleClickOutside(e: MouseEvent) {
      if (userRef.current && !userRef.current.contains(e.target as Node)) {
        setShowUserMenu(false);
      }
    }
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  return (
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
  );
}
