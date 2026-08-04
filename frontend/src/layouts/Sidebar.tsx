import { useAuth } from '../features/auth';
import SidebarBrand from './components/SidebarBrand';
import SidebarNavItem from './components/SidebarNavItem';
import SidebarExpandableSection from './components/SidebarExpandableSection';
import SidebarSubItem from './components/SidebarSubItem';
import SidebarFooter from './components/SidebarFooter';
import { useSidebarState } from './hooks/useSidebarState';
import { useSidebarResizer } from './hooks/useSidebarResizer';
import {
  navItems,
  trainingSubItems,
  attendanceSubItems,
  workAllocationSubItems,
  performanceSubItems,
  certificateSubItems,
  helpDeskSubItems,
  adminNavItems,
  masterSubItems,
} from './navigation';

interface SidebarProps {
  collapsed: boolean;
  onToggleCollapsed: (collapsed: boolean) => void;
  width?: number;
  onWidthChange?: (width: number) => void;
}

export default function Sidebar({ collapsed, onToggleCollapsed, width = 260, onWidthChange }: SidebarProps) {
  const { user } = useAuth();
  const { state, toggle } = useSidebarState();
  const { isResizing, handleMouseDown } = useSidebarResizer(onWidthChange);

  const canSeeModule = (code: string) => {
    if (user?.role === 'Admin') return true;
    return user?.modules?.[code]?.canRead === true;
  };

  const visibleNavItems = navItems.filter(i => i.moduleCode ? canSeeModule(i.moduleCode) : true);
  const isActive = (p: string) => location.pathname === p;

  return (
    <aside
      className={`sidebar ${collapsed ? 'sidebar--collapsed' : ''}`}
      style={{ width: collapsed ? 'var(--sidebar-collapsed-width)' : `${width}px`, transition: isResizing ? 'none' : 'width var(--transition-slow)' }}
    >
      <SidebarBrand collapsed={collapsed} onToggleCollapsed={onToggleCollapsed} />

      <div className="sidebar-nav">
        {visibleNavItems.map(i => (
          <SidebarNavItem key={i.path} path={i.path} label={i.label} icon={i.icon} collapsed={collapsed} />
        ))}

        {canSeeModule('TRAINING') && (
          <SidebarExpandableSection label="Training & Meetings" icon="pi pi-calendar" expanded={state.trainingExpanded} onToggle={() => toggle('trainingExpanded')} collapsed={collapsed} isActive={location.pathname.startsWith('/training')} submenuId="training-submenu">
            {trainingSubItems.map(i => <SidebarSubItem key={i.path} path={i.path} label={i.label} icon={i.icon} isActive={isActive(i.path)} />)}
          </SidebarExpandableSection>
        )}

        {canSeeModule('WORK_ALLOCATION') && (
          <SidebarExpandableSection label="Work Allocation" icon="pi pi-briefcase" expanded={state.workAllocationExpanded} onToggle={() => toggle('workAllocationExpanded')} collapsed={collapsed} isActive={location.pathname.startsWith('/work-allocation')} submenuId="work-allocation-submenu">
            {workAllocationSubItems.map(i => <SidebarSubItem key={i.path} path={i.path} label={i.label} icon={i.icon} isActive={isActive(i.path)} />)}
          </SidebarExpandableSection>
        )}

        {canSeeModule('PERFORMANCE') && (
          <SidebarExpandableSection label="Performance" icon="pi pi-chart-bar" expanded={state.performanceExpanded} onToggle={() => toggle('performanceExpanded')} collapsed={collapsed} isActive={location.pathname.startsWith('/performance')} submenuId="performance-submenu">
            {performanceSubItems.map(i => <SidebarSubItem key={i.path} path={i.path} label={i.label} icon={i.icon} isActive={isActive(i.path)} />)}
          </SidebarExpandableSection>
        )}

        {canSeeModule('HELP_DESK') && (
          <SidebarExpandableSection label="Help Desk" icon="pi pi-question-circle" expanded={state.helpDeskExpanded} onToggle={() => toggle('helpDeskExpanded')} collapsed={collapsed} isActive={location.pathname.startsWith('/help-desk')} submenuId="help-desk-submenu">
            {helpDeskSubItems.map(i => <SidebarSubItem key={i.path} path={i.path} label={i.label} icon={i.icon} isActive={isActive(i.path)} />)}
          </SidebarExpandableSection>
        )}

        {canSeeModule('ATTENDANCE') && (
          <SidebarExpandableSection label="Attendance" icon="pi pi-clock" expanded={state.attendanceExpanded} onToggle={() => toggle('attendanceExpanded')} collapsed={collapsed} isActive={location.pathname.startsWith('/attendance')} submenuId="attendance-submenu">
            {attendanceSubItems.map(i => <SidebarSubItem key={i.path} path={i.path} label={i.label} icon={i.icon} isActive={isActive(i.path)} />)}
          </SidebarExpandableSection>
        )}

        {canSeeModule('CERTIFICATE') && (
          <SidebarExpandableSection label="Certificate" icon="pi pi-verified" expanded={state.certificateExpanded} onToggle={() => toggle('certificateExpanded')} collapsed={collapsed} isActive={location.pathname.startsWith('/certificate')} submenuId="certificate-submenu">
            {certificateSubItems.map(i => <SidebarSubItem key={i.path} path={i.path} label={i.label} icon={i.icon} isActive={isActive(i.path)} />)}
          </SidebarExpandableSection>
        )}

        {user?.role === 'Admin' && (
          <>
            <div style={{ height: 1, background: 'var(--sidebar-border)', margin: '8px 0', flexShrink: 0 }} />
            {adminNavItems.map(i => (
              <SidebarNavItem key={i.path} path={i.path} label={i.label} icon={i.icon} collapsed={collapsed} />
            ))}
            <SidebarExpandableSection label="Masters" icon="pi pi-database" expanded={state.mastersExpanded} onToggle={() => toggle('mastersExpanded')} collapsed={collapsed} isActive={location.pathname.startsWith('/masters')} submenuId="masters-submenu">
              {masterSubItems.map(i => <SidebarSubItem key={i.path} path={i.path} label={i.label} icon={i.icon} isActive={isActive(i.path)} />)}
            </SidebarExpandableSection>
          </>
        )}
      </div>

      <SidebarFooter collapsed={collapsed} />
      {!collapsed && <div className={`sidebar-resizer ${isResizing ? 'active' : ''}`} onMouseDown={handleMouseDown} title="Drag to adjust sidebar width" />}
    </aside>
  );
}
