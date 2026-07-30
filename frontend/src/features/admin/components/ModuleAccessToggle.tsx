import { InputSwitch } from 'primereact/inputswitch';
import type { ModuleAccessItem } from '../types';

interface ModuleAccessToggleProps {
  moduleCode: string;
  moduleName: string;
  canRead: boolean;
  canWrite: boolean;
  canApprove: boolean;
  canExport: boolean;
  onToggle: (field: keyof ModuleAccessItem, value: boolean) => void;
}

export default function ModuleAccessToggle({
  moduleCode,
  moduleName,
  canRead,
  canWrite,
  canApprove,
  canExport,
  onToggle,
}: ModuleAccessToggleProps) {
  return (
    <div
      style={{
        border: '1px solid var(--border-light)',
        borderRadius: 8,
        padding: '12px 16px',
        background: 'var(--surface-card)',
      }}
    >
      <div style={{ marginBottom: 8 }}>
        <strong>{moduleName}</strong>
        <span style={{ marginLeft: 8, color: 'var(--text-secondary)', fontSize: 12 }}>
          ({moduleCode})
        </span>
      </div>
      <div style={{ display: 'flex', gap: 24, flexWrap: 'wrap' }}>
        <label style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
          <InputSwitch checked={canRead} onChange={(e) => onToggle('canRead', e.value ?? false)} />
          Read
        </label>
        <label style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
          <InputSwitch checked={canWrite} onChange={(e) => onToggle('canWrite', e.value ?? false)} />
          Write
        </label>
        <label style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
          <InputSwitch checked={canApprove} onChange={(e) => onToggle('canApprove', e.value ?? false)} />
          Approve
        </label>
        <label style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
          <InputSwitch checked={canExport} onChange={(e) => onToggle('canExport', e.value ?? false)} />
          Export
        </label>
      </div>
    </div>
  );
}
