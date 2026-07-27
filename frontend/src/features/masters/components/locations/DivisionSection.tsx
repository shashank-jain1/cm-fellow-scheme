import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import FormSelect from '../../../../shared/components/FormSelect';
import { useStates, useDivisions, useCreateDivision } from '../../queries';

export default function DivisionSection() {
  const { data: states } = useStates();
  const [selectedStateId, setSelectedStateId] = useState<number | null>(null);
  const { data: divisions, isLoading } = useDivisions(selectedStateId ?? undefined);
  const createMutation = useCreateDivision();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const stateOptions = (states ?? []).map((s) => ({ label: `${s.stateCode} - ${s.stateName}`, value: String(s.stateId) }));

  const handleCreate = async () => {
    if (!formName.trim() || !formCode.trim()) return;
    const stateId = selectedStateId ?? (states ?? [])[0]?.stateId;
    if (!stateId) return;
    await createMutation.mutateAsync({ stateId, divisionName: formName, divisionCode: formCode });
    setFormName('');
    setFormCode('');
    setShowForm(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>Divisions ({divisions?.length ?? 0})</h3>
        <div style={{ display: 'flex', gap: 12, alignItems: 'center' }}>
          <FormSelect
            value={selectedStateId ? String(selectedStateId) : ''}
            options={[{ label: 'All States', value: '' }, ...stateOptions]}
            onChange={(val) => setSelectedStateId(val ? Number(val) : null)}
            style={{ width: 240 }}
          />
          <Button label="Add Division" icon="pi pi-plus" className="btn btn-primary" size="small" onClick={() => setShowForm(!showForm)} />
        </div>
      </div>

      {showForm && (
        <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
          <div className="form-grid">
            <div className="form-field">
              <label>State *</label>
              <FormSelect
                value={selectedStateId ? String(selectedStateId) : ''}
                options={stateOptions}
                onChange={(val) => setSelectedStateId(val ? Number(val) : null)}
                placeholder="Select state"
              />
            </div>
            <div className="form-field">
              <label>Division Name *</label>
              <InputText value={formName} onChange={(e) => setFormName(e.target.value)} placeholder="e.g. Bhopal Division" />
            </div>
            <div className="form-field">
              <label>Division Code *</label>
              <InputText value={formCode} onChange={(e) => setFormCode(e.target.value)} placeholder="e.g. BPL" maxLength={10} />
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
        ) : (divisions ?? []).length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>ID</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Name</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Code</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>State</th>
              </tr>
            </thead>
            <tbody>
              {(divisions ?? []).map((d) => {
                const state = (states ?? []).find((s) => s.stateId === d.stateId);
                return (
                  <tr key={d.divisionId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                    <td style={{ padding: '12px 16px', fontFamily: 'monospace', fontSize: 13, color: 'var(--accent-primary)' }}>{d.divisionId}</td>
                    <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>{d.divisionName}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, fontFamily: 'monospace' }}>{d.divisionCode ?? '-'}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{state?.stateName ?? d.stateId}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-map" /><h3>No divisions yet</h3><p>{selectedStateId ? 'No divisions for this state' : 'Select a state or create your first division'}</p></div>
        )}
      </div>
    </div>
  );
}
