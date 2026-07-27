import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import FormSelect from '../../../../shared/components/FormSelect';
import { useDivisions, useDistricts, useCreateDistrict } from '../../queries';

export default function DistrictSection() {
  const { data: divisions } = useDivisions();
  const [selectedDivisionId, setSelectedDivisionId] = useState<number | null>(null);
  const { data: districts, isLoading } = useDistricts(selectedDivisionId ?? undefined);
  const createMutation = useCreateDistrict();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');

  const divisionOptions = (divisions ?? []).map((d) => ({ label: `${d.divisionCode ?? ''} - ${d.divisionName}`, value: String(d.divisionId) }));

  const handleCreate = async () => {
    if (!formName.trim() || !formCode.trim()) return;
    const divId = selectedDivisionId ?? (divisions ?? [])[0]?.divisionId;
    if (!divId) return;
    await createMutation.mutateAsync({ divisionId: divId, districtName: formName, districtCode: formCode });
    setFormName('');
    setFormCode('');
    setShowForm(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>Districts ({districts?.length ?? 0})</h3>
        <div style={{ display: 'flex', gap: 12, alignItems: 'center' }}>
          <FormSelect
            value={selectedDivisionId ? String(selectedDivisionId) : ''}
            options={[{ label: 'All Divisions', value: '' }, ...divisionOptions]}
            onChange={(val) => setSelectedDivisionId(val ? Number(val) : null)}
            style={{ width: 260 }}
          />
          <Button label="Add District" icon="pi pi-plus" className="btn btn-primary" size="small" onClick={() => setShowForm(!showForm)} />
        </div>
      </div>

      {showForm && (
        <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
          <div className="form-grid">
            <div className="form-field">
              <label>Division *</label>
              <FormSelect
                value={selectedDivisionId ? String(selectedDivisionId) : ''}
                options={divisionOptions}
                onChange={(val) => setSelectedDivisionId(val ? Number(val) : null)}
                placeholder="Select division"
              />
            </div>
            <div className="form-field">
              <label>District Name *</label>
              <InputText value={formName} onChange={(e) => setFormName(e.target.value)} placeholder="e.g. Bhopal" />
            </div>
            <div className="form-field">
              <label>District Code *</label>
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
        ) : (districts ?? []).length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>ID</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Name</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Code</th>
                <th style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Division</th>
              </tr>
            </thead>
            <tbody>
              {(districts ?? []).map((d) => {
                const div = (divisions ?? []).find((dv) => dv.divisionId === d.divisionId);
                return (
                  <tr key={d.districtId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                    <td style={{ padding: '12px 16px', fontFamily: 'monospace', fontSize: 13, color: 'var(--accent-primary)' }}>{d.districtId}</td>
                    <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>{d.districtName}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, fontFamily: 'monospace' }}>{d.districtCode ?? '-'}</td>
                    <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{div?.divisionName ?? d.divisionId}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-map" /><h3>No districts yet</h3><p>{selectedDivisionId ? 'No districts for this division' : 'Select a division or create your first district'}</p></div>
        )}
      </div>
    </div>
  );
}
