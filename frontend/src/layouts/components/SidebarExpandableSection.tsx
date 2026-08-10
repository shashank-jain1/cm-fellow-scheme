import type { ReactNode } from 'react';

interface SidebarExpandableSectionProps {
  label: string;
  icon: string;
  expanded: boolean;
  onToggle: () => void;
  collapsed: boolean;
  isActive: boolean;
  children: ReactNode;
  submenuId: string;
}

export default function SidebarExpandableSection({
  label,
  icon,
  expanded,
  onToggle,
  collapsed,
  isActive: _isActive,
  children,
  submenuId,
}: SidebarExpandableSectionProps) {
  return (
    <>
      <button
        type="button"
        className={`sidebar-link sidebar-link--group ${expanded ? 'expanded' : ''}`}
        onClick={onToggle}
        title={collapsed ? label : undefined}
        aria-expanded={expanded}
        aria-controls={submenuId}
        style={{ width: '100%', border: 'none', background: 'none', cursor: 'pointer', textAlign: 'left' }}
      >
        <div className="sidebar-link-icon">
          <i className={icon} />
        </div>
        {!collapsed && (
          <>
            <span className="sidebar-link-label" style={{ flex: 1 }}>{label}</span>
            <i
              className={`pi ${expanded ? 'pi-chevron-down' : 'pi-chevron-right'}`}
              style={{ fontSize: 10, color: 'var(--text-muted)', transition: 'transform 0.15s' }}
            />
          </>
        )}
      </button>
      {!collapsed && expanded && (
        <div id={submenuId} style={{ paddingLeft: 18, flexShrink: 0 }}>
          {children}
        </div>
      )}
    </>
  );
}
