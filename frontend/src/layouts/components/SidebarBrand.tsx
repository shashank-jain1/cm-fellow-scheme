interface SidebarBrandProps {
  collapsed: boolean;
  onToggleCollapsed: (collapsed: boolean) => void;
}

export default function SidebarBrand({ collapsed, onToggleCollapsed }: SidebarBrandProps) {
  return (
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
  );
}
