import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import FormSelect from '../../../../shared/components/FormSelect';
import { useDistricts, useBlocks, useCreateBlock } from '../../queries';

export default function BlockSection() {
  const { data: districts } = useDistricts();
  const [selectedDistrictId, setSelectedDistrictId] = useState<number | null>(null);
  const { data: blocks, isLoading } = useBlocks(selectedDistrictId ?? undefined);
  const createMutation = useCreateBlock();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const districtOptions = (districts ?? []).map((d) => ({ label: `${d.districtCode ?? ''} - ${d.districtName}`, value: String(d.districtId) }));

  const handleCreate = async () => {
    if (!formName.trim() || !formCode.trim()) return;
    const distId = selectedDistrictId ?? (districts ?? [])[0]?.districtId;
    if (!distId) return;
    await createMutation.mutateAsync({ districtId: distId, blockName: formName, blockCode: formCode });
    setFormName('');
    setFormCode('');
    setShowForm(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>Blocks ({blocks?.length ?? 0})</h3>
        <div style={{ display: 'flex', gap: 12, alignItems: 'center' }}>
          <FormSelect
            value={selectedDistrictId ? String(selectedDistrictId) : ''}
            options={[{ label: 'All Districts', value: '' }, ...districtOptions]}
            onChange={(val) => setSelectedDistrictId(val ? Number(val) : null)}
            style={{ width: 260 }}
          />
          <Button label="Add Block" icon="pi pi-plus" className="btn btn-primary" size="small" onClick={() => setShowForm(!showForm)} />
        </div>
      </div>

      {showForm && (
        <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
          <div className="form-grid">
            <div className="form-field">
              <label>District *</label>
              <FormSelect
                value={selectedDistrictId ? String(selectedDistrictId) : ''}
                options={districtOptions}
                onChange={(val) => setSelectedDistrictId(val ? Number(val) : null)}
                placeholder="Select district"
              />
            </div>
            <div className="form-field">
              <label>Block Name *</label>
              <InputText value={formName} onChange={(e) => setFormName(e.target.value)} placeholder="e.g. Huzur" />
            </div>
            <div className="form-field">
              <label>Block Code *</label>
              <InputText value={formCode} onChange={(e) => setFormCode(e.target.value)} placeholder="e.g. HZ" maxLength={10} />
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
        ) : (blocks ?? []).length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>ID</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Name</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Code</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>District</th>
              </tr>
            </thead>
            <tbody>
              {(blocks ?? []).map((b) => {
                const dist = (districts ?? []).find((d) => d.districtId === b.districtId);
                return (
                  <tr key={b.blockId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                    <td style={{ padding: '12px 16px', fontFamily: 'monospace', fontSize: 13, color: 'var(--accent-primary)' }}>{b.blockId}</td>
                    <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>{b.blockName}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, fontFamily: 'monospace' }}>{b.blockCode ?? '-'}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{dist?.districtName ?? b.districtId}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-map" /><h3>No blocks yet</h3><p>{selectedDistrictId ? 'No blocks for this district' : 'Select a district or create your first block'}</p></div>
        )}
      </div>
    </div>
  );
}
