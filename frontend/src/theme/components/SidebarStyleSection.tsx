import DesignOption from './DesignOption';

interface SidebarStyleSectionProps {
  sidebarStyle: string;
  sidebarCollapsed: boolean;
  onStyleChange: (style: string) => void;
  onToggleCollapse: () => void;
}

export default function SidebarStyleSection({ sidebarStyle, sidebarCollapsed, onStyleChange, onToggleCollapse }: SidebarStyleSectionProps) {
  return (
    <>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '8px', marginBottom: '12px' }}>
        <DesignOption label="Default" icon="pi-bars" desc="Standard width" active={sidebarStyle === 'default'} onClick={() => onStyleChange('default')} />
        <DesignOption label="Compact" icon="pi-minus" desc="Narrower padding" active={sidebarStyle === 'compact'} onClick={() => onStyleChange('compact')} />
      </div>
      <button type="button" onClick={onToggleCollapse} style={{ width: '100%', padding: '10px 12px', borderRadius: 'var(--radius-md)', border: '1px solid var(--border)', background: sidebarCollapsed ? 'var(--accent-light)' : 'transparent', color: sidebarCollapsed ? 'var(--accent)' : 'var(--text-body)', cursor: 'pointer', fontSize: 'var(--text-sm)', fontWeight: 500, fontFamily: 'var(--font-body)', display: 'flex', alignItems: 'center', gap: '8px', transition: 'all var(--transition-fast)' }}>
        <i className={`pi ${sidebarCollapsed ? 'pi-angle-right' : 'pi-angle-left'}`} style={{ fontSize: 12 }} />
        {sidebarCollapsed ? 'Expand Sidebar' : 'Collapse Sidebar'}
      </button>
    </>
  );
}
