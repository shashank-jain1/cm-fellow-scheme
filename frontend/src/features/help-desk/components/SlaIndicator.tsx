import { Tag } from 'primereact/tag';
import type { SlaStatus } from '../types';

interface SlaIndicatorProps {
  status: SlaStatus;
}

const config: Record<SlaStatus, { label: string; severity: 'success' | 'warning' | 'danger'; icon: string }> = {
  'On Track': { label: 'On Track', severity: 'success', icon: 'pi pi-check-circle' },
  'At Risk': { label: 'At Risk', severity: 'warning', icon: 'pi pi-exclamation-triangle' },
  'Breached': { label: 'Breached', severity: 'danger', icon: 'pi pi-times-circle' },
};

export default function SlaIndicator({ status }: SlaIndicatorProps) {
  const c = config[status] ?? config['On Track'];
  return (
    <div style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
      <i className={c.icon} style={{ color: c.severity === 'success' ? 'var(--badge-emerald-text)' : c.severity === 'warning' ? 'var(--badge-amber-text)' : 'var(--badge-red-text)', fontSize: 14 }} />
      <Tag value={c.label} severity={c.severity} />
    </div>
  );
}
