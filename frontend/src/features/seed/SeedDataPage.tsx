import { useState, useCallback, useRef, useEffect } from 'react';
import PageHeader from '../../shared/components/ui/PageHeader';
import Card from '../../shared/components/ui/Card';
import { SEED_MODULES, type SeedModule } from './seedHelpers';
import type { ModuleSeedState, SeedLog } from './types';
import SeedButton from './SeedButton';
import SeedStatusCard from './SeedStatusCard';

const initialStates: Record<string, ModuleSeedState> = Object.fromEntries(
  SEED_MODULES.map(m => [m.key, { status: 'idle', message: '', count: 0 }]),
);

export default function SeedDataPage() {
  const [states, setStates] = useState<Record<string, ModuleSeedState>>(initialStates);
  const [logs, setLogs] = useState<SeedLog[]>([]);
  const [seedingAll, setSeedingAll] = useState(false);
  const logEndRef = useRef<HTMLDivElement>(null);

  useEffect(() => { logEndRef.current?.scrollIntoView({ behavior: 'smooth' }); }, [logs]);

  const appendLog = useCallback((entry: SeedLog) => { setLogs(prev => [...prev, entry]); }, []);
  const updateState = useCallback((key: string, patch: Partial<ModuleSeedState>) => { setStates(prev => ({ ...prev, [key]: { ...prev[key], ...patch } })); }, []);

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
    setSeedingAll(true); setLogs([]); setStates(initialStates);
    for (const mod of SEED_MODULES) {
      const deps = mod.dependsOn ?? [];
      for (const dep of deps) { const depState = states[dep]; if (depState?.status !== 'success') { const depMod = SEED_MODULES.find(m => m.key === dep); if (depMod) await seedModule(depMod); } }
      await seedModule(mod);
    }
    setSeedingAll(false);
  }, [seedModule, states]);

  const resetAll = () => { setStates(initialStates); setLogs([]); };

  const logColor = (type: SeedLog['type']) => { switch (type) { case 'success': return '#16a34a'; case 'error': return '#dc2626'; default: return 'var(--text-body)'; } };
  const totalCreated = Object.values(states).reduce((sum, s) => sum + s.count, 0);
  const completedCount = Object.values(states).filter(s => s.status === 'success').length;

  return (
    <div>
      <PageHeader title="Seed Test Data" subtitle={`Populate modules with sample data for smoke testing (${completedCount}/${SEED_MODULES.length} modules seeded)`}
        action={<div style={{ display: 'flex', gap: 8 }}><SeedButton label={seedingAll ? 'Seeding All...' : 'Seed All Modules'} icon={seedingAll ? 'pi-spin pi-spinner' : 'pi-play'} onClick={seedAll} disabled={seedingAll} /><SeedButton label="Reset" icon="pi-refresh" onClick={resetAll} disabled={seedingAll} variant="outline" /></div>} />
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: 16, marginBottom: 24 }}>
        {SEED_MODULES.map(mod => <SeedStatusCard key={mod.key} mod={mod} state={states[mod.key]} seedingAll={seedingAll} onSeed={seedModule} />)}
      </div>
      {totalCreated > 0 && (
        <Card style={{ marginBottom: 16 }}><div style={{ display: 'flex', alignItems: 'center', gap: 12 }}><i className="pi pi-info-circle" style={{ color: 'var(--accent)', fontSize: 18 }} /><span style={{ fontWeight: 600 }}>{totalCreated} total items seeded across {completedCount} modules</span></div></Card>
      )}
      {logs.length > 0 && (
        <Card>
          <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 12 }}><i className="pi pi-terminal" style={{ color: 'var(--text-muted)' }} /><span style={{ fontWeight: 600, fontSize: 14 }}>Seed Log</span><span style={{ fontSize: 12, color: 'var(--text-muted)' }}>({logs.length} entries)</span></div>
          <div style={{ background: 'var(--carbon-900, #1a1a2e)', borderRadius: 'var(--radius-md)', padding: 16, maxHeight: 400, overflowY: 'auto', fontFamily: 'var(--font-mono, monospace)', fontSize: 12, lineHeight: 1.8 }}>
            {logs.map((l, i) => (<div key={i} style={{ color: logColor(l.type) }}><span style={{ color: 'var(--text-muted)', marginRight: 8 }}>{l.timestamp.toLocaleTimeString()}</span><span style={{ color: 'var(--accent)', marginRight: 8, fontWeight: 600 }}>[{l.module}]</span><span>{l.message}</span></div>))}
            <div ref={logEndRef} />
          </div>
        </Card>
      )}
    </div>
  );
}
