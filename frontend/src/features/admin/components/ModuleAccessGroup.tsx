import ModuleAccessRow from './ModuleAccessRow';
import type { PermissionField } from '../hooks/useModuleAccess';
import type { ModuleMasterDto, ModuleAccessItem } from '../types';

interface ModuleAccessGroupProps {
  parent: ModuleMasterDto;
  expanded: boolean;
  accessMap: Map<number, ModuleAccessItem>;
  onToggleExpand: (id: number) => void;
  onToggle: (id: number, field: PermissionField, value: boolean) => void;
}

export default function ModuleAccessGroup({ parent, expanded, accessMap, onToggleExpand, onToggle }: ModuleAccessGroupProps) {
  const hasChildren = parent.children && parent.children.length > 0;

  return (
    <div>
      <div
        style={{ cursor: hasChildren ? 'pointer' : 'default', paddingLeft: 16 }}
        onClick={() => hasChildren && onToggleExpand(parent.moduleMasterId)}
      >
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <div style={{ width: 20, display: 'flex', alignItems: 'center', justifyContent: 'center', flexShrink: 0 }}>
            {hasChildren && (
              <i
                className={`pi ${expanded ? 'pi-chevron-down' : 'pi-chevron-right'}`}
                style={{ fontSize: 11, color: 'var(--text-secondary, #64748B)' }}
              />
            )}
          </div>
          <div style={{ flex: 1 }}>
            <ModuleAccessRow
              id={parent.moduleMasterId}
              name={parent.moduleName}
              level="parent"
              item={accessMap.get(parent.moduleMasterId)}
              onToggle={(id, field, value) => onToggle(id, field, value)}
            />
          </div>
        </div>
      </div>

      {expanded && hasChildren && parent.children!.map((child) => (
        <div key={child.moduleMasterId} style={{ paddingLeft: 16 }}>
          <ModuleAccessRow
            id={child.moduleMasterId}
            name={child.moduleName}
            level="child"
            item={accessMap.get(child.moduleMasterId)}
            onToggle={onToggle}
          />
        </div>
      ))}
    </div>
  );
}
