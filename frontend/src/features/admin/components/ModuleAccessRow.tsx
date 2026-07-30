import { Checkbox } from 'primereact/checkbox';
import { PERMISSION_FIELDS, type PermissionField } from '../hooks/useModuleAccess';
import type { ModuleAccessItem } from '../types';

interface ModuleAccessRowProps {
  id: number;
  name: string;
  level: 'parent' | 'child';
  item: ModuleAccessItem | undefined;
  onToggle: (id: number, field: PermissionField, value: boolean) => void;
}

export default function ModuleAccessRow({ id, name, level, item, onToggle }: ModuleAccessRowProps) {
  const isChild = level === 'child';
  const anyActive = item && (item.canRead || item.canWrite || item.canApprove || item.canExport);

  return (
    <div style={{
      display: 'grid',
      gridTemplateColumns: '1fr 52px 52px 52px 52px',
      alignItems: 'center',
      padding: isChild ? '9px 24px 9px 24px' : '11px 24px',
      paddingLeft: isChild ? 56 : 24,
      borderBottom: '1px solid var(--border-light)',
      background: anyActive ? 'var(--accent-light)' : isChild ? 'var(--surface-section, var(--bg-page))' : 'var(--surface-card)',
    }}>
      <span style={{
        fontSize: 13,
        fontWeight: isChild ? 400 : 600,
        color: isChild ? 'var(--text-body)' : 'var(--text-heading)',
        whiteSpace: 'nowrap',
        overflow: 'hidden',
        textOverflow: 'ellipsis',
      }}>
        {name}
      </span>
      {PERMISSION_FIELDS.map((field) => (
        <div key={field} style={{ textAlign: 'center', display: 'flex', justifyContent: 'center' }}>
          <Checkbox
            checked={item?.[field] ?? false}
            onChange={(e) => onToggle(id, field, e.checked ?? false)}
            style={{ width: 18, height: 18 }}
          />
        </div>
      ))}
    </div>
  );
}
