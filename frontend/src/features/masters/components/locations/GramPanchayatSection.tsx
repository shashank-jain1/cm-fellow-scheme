import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import FormSelect from '../../../../shared/components/FormSelect';
import { useBlocks, useGramPanchayats, useCreateGramPanchayat } from '../../queries';

export default function GramPanchayatSection() {
  const { data: blocks } = useBlocks();
  const [selectedBlockId, setSelectedBlockId] = useState<number | null>(null);
  const { data: gps, isLoading } = useGramPanchayats(selectedBlockId ?? undefined);
  const createMutation = useCreateGramPanchayat();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const blockOptions = (blocks ?? []).map((b) => ({ label: `${b.blockCode ?? ''} - ${b.blockName}`, value: String(b.blockId) }));

  const handleCreate = async () => {
    if (!formName.trim()) return;
    const blockId = selectedBlockId ?? (blocks ?? [])[0]?.blockId;
    if (!blockId) return;
    await createMutation.mutateAsync({ blockId, gramPanchayatName: formName, gpCode: formCode || undefined });
    setFormName('');
    setFormCode('');
    setShowForm(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>Gram Panchayats ({gps?.length ?? 0})</h3>
        <div style={{ display: 'flex', gap: 12, alignItems: 'center' }}>
          <FormSelect
            value={selectedBlockId ? String(selectedBlockId) : ''}
            options={[{ label: 'All Blocks', value: '' }, ...blockOptions]}
            onChange={(val) => setSelectedBlockId(val ? Number(val) : null)}
            style={{ width: 260 }}
          />
          <Button label="Add Gram Panchayat" icon="pi pi-plus" className="btn btn-primary" size="small" onClick={() => setShowForm(!showForm)} />
        </div>
      </div>

      {showForm && (
        <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
          <div className="form-grid">
            <div className="form-field">
              <label>Block *</label>
              <FormSelect
                value={selectedBlockId ? String(selectedBlockId) : ''}
                options={blockOptions}
                onChange={(val) => setSelectedBlockId(val ? Number(val) : null)}
                placeholder="Select block"
              />
            </div>
            <div className="form-field">
              <label>GP Name *</label>
              <InputText value={formName} onChange={(e) => setFormName(e.target.value)} placeholder="e.g. Gram Panchayat name" />
            </div>
            <div className="form-field">
              <label>GP Code</label>
              <InputText value={formCode} onChange={(e) => setFormCode(e.target.value)} placeholder="Optional code" maxLength={20} />
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
        ) : (gps ?? []).length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>ID</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Name</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Code</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Block</th>
              </tr>
            </thead>
            <tbody>
              {(gps ?? []).map((g) => {
                const block = (blocks ?? []).find((b) => b.blockId === g.blockId);
                return (
                  <tr key={g.gramPanchayatId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                    <td style={{ padding: '12px 16px', fontFamily: 'monospace', fontSize: 13, color: 'var(--accent-primary)' }}>{g.gramPanchayatId}</td>
                    <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>{g.gramPanchayatName}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, fontFamily: 'monospace' }}>{g.gpCode ?? '-'}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{block?.blockName ?? g.blockId}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-map" /><h3>No gram panchayats yet</h3><p>{selectedBlockId ? 'No GPs for this block' : 'Select a block or create your first gram panchayat'}</p></div>
        )}
      </div>
    </div>
  );
}
