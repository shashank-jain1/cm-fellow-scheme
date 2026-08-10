import Card from '../../shared/components/ui/Card';
import type { SeedModule } from './seedHelpers';
import type { ModuleSeedState } from './types';

interface Props {
  mod: SeedModule;
  state: ModuleSeedState;
  seedingAll: boolean;
  onSeed: (mod: SeedModule) => void;
}

export default function SeedStatusCard({ mod, state, seedingAll, onSeed }: Props) {
  const statusIcon = (status: ModuleSeedState['status']) => {
    switch (status) {
      case 'running': return 'pi pi-spin pi-spinner';
      case 'success': return 'pi pi-check-circle';
      case 'error': return 'pi pi-times-circle';
      default: return 'pi pi-circle';
    }
  };

  const statusColor = (status: ModuleSeedState['status']) => {
    switch (status) {
      case 'running': return 'var(--accent)';
      case 'success': return '#16a34a';
      case 'error': return '#dc2626';
      default: return 'var(--text-muted)';
    }
  };

  return (
    <Card className="seed-module-card">
      <div style={{ display: 'flex', alignItems: 'flex-start', gap: 12 }}>
        <div style={{ width: 40, height: 40, borderRadius: 'var(--radius-md)', background: state.status === 'success' ? '#dcfce7' : state.status === 'error' ? '#fef2f2' : state.status === 'running' ? '#eff6ff' : 'var(--carbon-50)', display: 'flex', alignItems: 'center', justifyContent: 'center', flexShrink: 0 }}>
          <i className={mod.icon} style={{ fontSize: 18, color: statusColor(state.status) }} />
        </div>
        <div style={{ flex: 1, minWidth: 0 }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 4 }}>
            <span style={{ fontWeight: 600, fontSize: 14 }}>{mod.label}</span>
            <i className={statusIcon(state.status)} style={{ fontSize: 14, color: statusColor(state.status) }} />
          </div>
          <p style={{ fontSize: 12, color: 'var(--text-muted)', margin: '0 0 10px', lineHeight: 1.4 }}>{mod.description}</p>
          {state.message && (
            <p style={{ fontSize: 11, color: state.status === 'error' ? '#dc2626' : state.status === 'success' ? '#16a34a' : 'var(--text-muted)', margin: 0, lineHeight: 1.3 }}>{state.message}</p>
          )}
          {mod.dependsOn && mod.dependsOn.length > 0 && (
            <p style={{ fontSize: 10, color: 'var(--text-muted)', margin: '6px 0 0', fontStyle: 'italic' }}>Depends on: {mod.dependsOn.join(', ')}</p>
          )}
        </div>
        <button type="button" className="btn btn-outline" style={{ padding: '6px 12px', fontSize: 12, flexShrink: 0 }} onClick={() => onSeed(mod)} disabled={seedingAll || state.status === 'running'}>
          {state.status === 'running' ? '...' : 'Seed'}
        </button>
      </div>
    </Card>
  );
}
