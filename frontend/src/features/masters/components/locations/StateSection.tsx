import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { useStates, useCreateState } from '../../queries';

export default function StateSection() {
  const { data: states, isLoading } = useStates();
  const createMutation = useCreateState();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const handleCreate = async () => {
    if (!formName.trim() || !formCode.trim()) return;
    await createMutation.mutateAsync({ stateName: formName, stateCode: formCode });
    setFormName('');
    setFormCode('');
    setShowForm(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>States ({states?.length ?? 0})</h3>
        <Button label="Add State" icon="pi pi-plus" className="btn btn-primary" size="small" onClick={() => setShowForm(!showForm)} />
      </div>

      {showForm && (
        <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
          <div className="form-grid">
            <div className="form-field">
              <label>State Name *</label>
              <InputText value={formName} onChange={(e) => setFormName(e.target.value)} placeholder="e.g. Madhya Pradesh" />
            </div>
            <div className="form-field">
              <label>State Code *</label>
              <InputText value={formCode} onChange={(e) => setFormCode(e.target.value)} placeholder="e.g. MP" maxLength={10} />
            </div>
          </div>
          <div style={{ display: 'flex', gap: 8, justifyContent: 'flex-end', marginTop: 12 }}>
            <Button label="Cancel" className="btn btn-secondary" size="small" onClick={() => { setShowForm(false); setFormName(''); setFormCode(''); }} />
            <Button label="Create" className="btn btn-primary" size="small" onClick={handleCreate} loading={createMutation.isPending} />
          </div>
        </div>
      )}

      <div className="table-wrapper">
        {isLoading ? (
          <div style={{ padding: 20 }}>{[1, 2, 3].map((n) => <div key={n} className="skeleton" style={{ height: 40, marginBottom: 8 }} />)}</div>
        ) : (states ?? []).length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>ID</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Name</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Code</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Status</th>
              </tr>
            </thead>
            <tbody>
              {(states ?? []).map((s) => (
                <tr key={s.stateId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                  <td style={{ padding: '12px 16px', fontFamily: 'monospace', fontSize: 13, color: 'var(--accent-primary)' }}>{s.stateId}</td>
                  <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>{s.stateName}</td>
                  <td style={{ padding: '12px 16px', fontSize: 13, fontFamily: 'monospace' }}>{s.stateCode}</td>
                  <td style={{ padding: '12px 16px' }}>
                    <span className="badge" style={{ background: s.isActive ? 'var(--badge-emerald-bg)' : 'var(--badge-red-bg)', color: s.isActive ? 'var(--badge-emerald-text)' : 'var(--badge-red-text)', padding: '4px 10px', borderRadius: 12, fontSize: 12, fontWeight: 600 }}>
                      {s.isActive ? 'Active' : 'Inactive'}
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-map" /><h3>No states yet</h3><p>Create your first state to get started</p></div>
        )}
      </div>
    </div>
  );
}
