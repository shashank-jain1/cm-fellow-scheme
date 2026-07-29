import { useState, useCallback, useRef, useEffect } from 'react';
import PageHeader from '../../shared/components/ui/PageHeader';
import Card from '../../shared/components/ui/Card';
import { SEED_MODULES, type SeedModule } from './seedHelpers';
import type { ModuleSeedState, SeedLog } from './types';

const initialStates: Record<string, ModuleSeedState> = Object.fromEntries(
  SEED_MODULES.map(m => [m.key, { status: 'idle', message: '', count: 0 }]),
);

export default function SeedDataPage() {
  const [states, setStates] = useState<Record<string, ModuleSeedState>>(initialStates);
  const [logs, setLogs] = useState<SeedLog[]>([]);
  const [seedingAll, setSeedingAll] = useState(false);
  const logEndRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    logEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [logs]);

  const appendLog = useCallback((entry: SeedLog) => {
    setLogs(prev => [...prev, entry]);
  }, []);

  const updateState = useCallback((key: string, patch: Partial<ModuleSeedState>) => {
    setStates(prev => ({ ...prev, [key]: { ...prev[key], ...patch } }));
  }, []);

  const seedModule = useCallback(async (mod: SeedModule) => {
    updateState(mod.key, { status: 'running', message: 'Seeding...', count: 0 });
    appendLog({ module: mod.label, message: `Starting ${mod.label} seed...`, type: 'info', timestamp: new Date() });

    try {
      const count = await mod.seed(appendLog);
      updateState(mod.key, { status: 'success', message: `Done - ${count} items created`, count });
      appendLog({ module: mod.label, message: `Completed ${mod.label}: ${count} items`, type: 'success', timestamp: new Date() });
    } catch (err) {
      const msg = err instanceof Error ? err.message : 'Unknown error';
      updateState(mod.key, { status: 'error', message: msg, count: 0 });
      appendLog({ module: mod.label, message: `Failed: ${msg}`, type: 'error', timestamp: new Date() });
    }
  }, [appendLog, updateState]);

  const seedAll = useCallback(async () => {
    setSeedingAll(true);
    setLogs([]);
    setStates(initialStates);

    for (const mod of SEED_MODULES) {
      const deps = mod.dependsOn ?? [];
      for (const dep of deps) {
        const depState = states[dep];
        if (depState?.status !== 'success') {
          const depMod = SEED_MODULES.find(m => m.key === dep);
          if (depMod) await seedModule(depMod);
        }
      }
      await seedModule(mod);
    }

    setSeedingAll(false);
  }, [seedModule, states]);

  const resetAll = () => {
    setStates(initialStates);
    setLogs([]);
  };

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

  const logColor = (type: SeedLog['type']) => {
    switch (type) {
      case 'success': return '#16a34a';
      case 'error': return '#dc2626';
      default: return 'var(--text-body)';
    }
  };

  const totalCreated = Object.values(states).reduce((sum, s) => sum + s.count, 0);
  const completedCount = Object.values(states).filter(s => s.status === 'success').length;

  return (
    <div>
      <PageHeader
        title="Seed Test Data"
        subtitle={`Populate modules with sample data for smoke testing (${completedCount}/${SEED_MODULES.length} modules seeded)`}
        action={
          <div style={{ display: 'flex', gap: 8 }}>
            <button
              type="button"
              className="btn btn-primary"
              onClick={seedAll}
              disabled={seedingAll}
            >
              <i className={`pi ${seedingAll ? 'pi-spin pi-spinner' : 'pi-play'}`} style={{ marginRight: 6 }} />
              {seedingAll ? 'Seeding All...' : 'Seed All Modules'}
            </button>
            <button
              type="button"
              className="btn btn-outline"
              onClick={resetAll}
              disabled={seedingAll}
            >
              <i className="pi pi-refresh" style={{ marginRight: 6 }} />
              Reset
            </button>
          </div>
        }
      />

      {/* Module Cards */}
      <div style={{
        display: 'grid',
        gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))',
        gap: 16,
        marginBottom: 24,
      }}>
        {SEED_MODULES.map(mod => {
          const st = states[mod.key];
          return (
            <Card key={mod.key} className="seed-module-card">
              <div style={{ display: 'flex', alignItems: 'flex-start', gap: 12 }}>
                <div style={{
                  width: 40,
                  height: 40,
                  borderRadius: 'var(--radius-md)',
                  background: st.status === 'success' ? '#dcfce7' : st.status === 'error' ? '#fef2f2' : st.status === 'running' ? '#eff6ff' : 'var(--carbon-50)',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  flexShrink: 0,
                }}>
                  <i className={mod.icon} style={{ fontSize: 18, color: statusColor(st.status) }} />
                </div>
                <div style={{ flex: 1, minWidth: 0 }}>
                  <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 4 }}>
                    <span style={{ fontWeight: 600, fontSize: 14 }}>{mod.label}</span>
                    <i className={statusIcon(st.status)} style={{ fontSize: 14, color: statusColor(st.status) }} />
                  </div>
                  <p style={{ fontSize: 12, color: 'var(--text-muted)', margin: '0 0 10px', lineHeight: 1.4 }}>
                    {mod.description}
                  </p>
                  {st.message && (
                    <p style={{
                      fontSize: 11,
                      color: st.status === 'error' ? '#dc2626' : st.status === 'success' ? '#16a34a' : 'var(--text-muted)',
                      margin: 0,
                      lineHeight: 1.3,
                    }}>
                      {st.message}
                    </p>
                  )}
                  {mod.dependsOn && mod.dependsOn.length > 0 && (
                    <p style={{ fontSize: 10, color: 'var(--text-muted)', margin: '6px 0 0', fontStyle: 'italic' }}>
                      Depends on: {mod.dependsOn.join(', ')}
                    </p>
                  )}
                </div>
                <button
                  type="button"
                  className="btn btn-outline"
                  style={{ padding: '6px 12px', fontSize: 12, flexShrink: 0 }}
                  onClick={() => seedModule(mod)}
                  disabled={seedingAll || st.status === 'running'}
                >
                  {st.status === 'running' ? '...' : 'Seed'}
                </button>
              </div>
            </Card>
          );
        })}
      </div>

      {/* Summary */}
      {totalCreated > 0 && (
        <Card style={{ marginBottom: 16 }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
            <i className="pi pi-info-circle" style={{ color: 'var(--accent)', fontSize: 18 }} />
            <span style={{ fontWeight: 600 }}>
              {totalCreated} total items seeded across {completedCount} modules
            </span>
          </div>
        </Card>
      )}

      {/* Log Console */}
      {logs.length > 0 && (
        <Card>
          <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 12 }}>
            <i className="pi pi-terminal" style={{ color: 'var(--text-muted)' }} />
            <span style={{ fontWeight: 600, fontSize: 14 }}>Seed Log</span>
            <span style={{ fontSize: 12, color: 'var(--text-muted)' }}>({logs.length} entries)</span>
          </div>
          <div style={{
            background: 'var(--carbon-900, #1a1a2e)',
            borderRadius: 'var(--radius-md)',
            padding: 16,
            maxHeight: 400,
            overflowY: 'auto',
            fontFamily: 'var(--font-mono, monospace)',
            fontSize: 12,
            lineHeight: 1.8,
          }}>
            {logs.map((l, i) => (
              <div key={i} style={{ color: logColor(l.type) }}>
                <span style={{ color: 'var(--text-muted)', marginRight: 8 }}>
                  {l.timestamp.toLocaleTimeString()}
                </span>
                <span style={{ color: 'var(--accent)', marginRight: 8, fontWeight: 600 }}>
                  [{l.module}]
                </span>
                <span>{l.message}</span>
              </div>
            ))}
            <div ref={logEndRef} />
          </div>
        </Card>
      )}
    </div>
  );
}
