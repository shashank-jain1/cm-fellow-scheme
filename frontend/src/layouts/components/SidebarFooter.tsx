import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../features/auth';

interface SidebarFooterProps {
  collapsed: boolean;
}

export default function SidebarFooter({ collapsed }: SidebarFooterProps) {
  const navigate = useNavigate();
  const { user, logout } = useAuth();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
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
  );
}
