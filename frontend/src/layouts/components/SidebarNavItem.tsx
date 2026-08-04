import { Link, useLocation } from 'react-router-dom';

interface SidebarNavItemProps {
  path: string;
  label: string;
  icon: string;
  collapsed: boolean;
}

export default function SidebarNavItem({ path, label, icon, collapsed }: SidebarNavItemProps) {
  const location = useLocation();
  const isActive = location.pathname === path || (path !== '/dashboard' && location.pathname.startsWith(path));

  return (
    <Link
      to={path}
      className={`sidebar-link ${isActive ? 'active' : ''}`}
      title={collapsed ? label : undefined}
    >
      <div className="sidebar-link-icon">
        <i className={icon} />
        {isActive && <div className="sidebar-active-dot" />}
      </div>
      {!collapsed && <span className="sidebar-link-label">{label}</span>}
      {isActive && <div className="sidebar-active-indicator" />}
    </Link>
  );
}
