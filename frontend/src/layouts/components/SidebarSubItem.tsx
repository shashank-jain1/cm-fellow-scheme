import { Link } from 'react-router-dom';

interface SidebarSubItemProps {
  path: string;
  label: string;
  icon: string;
  isActive: boolean;
}

export default function SidebarSubItem({ path, label, icon, isActive }: SidebarSubItemProps) {
  return (
    <Link
      to={path}
      className={`sidebar-link ${isActive ? 'active' : ''}`}
      style={{ paddingLeft: 12, fontSize: 13 }}
    >
      <div className="sidebar-link-icon" style={{ width: 20, height: 20 }}>
        <i className={icon} style={{ fontSize: 12 }} />
        {isActive && <div className="sidebar-active-dot" />}
      </div>
      <span className="sidebar-link-label">{label}</span>
      {isActive && <div className="sidebar-active-indicator" />}
    </Link>
  );
}
