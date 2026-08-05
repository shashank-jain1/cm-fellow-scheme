import { useState } from 'react';
import { Button } from 'primereact/button';
import { AppInput } from '../../../../shared/components/forms';
import { ConfirmDialog } from '../../../../shared/components/ui';
import { useStates, useCreateState, useUpdateState, useDeleteState } from '../../queries';

export default function StateSection() {
  const { data: states, isLoading } = useStates();
  const createMutation = useCreateState();
  const updateMutation = useUpdateState();
  const deleteMutation = useDeleteState();
  const [showForm, setShowForm] = useState(false);
  const [formName, setFormName] = useState('');
  const [formCode, setFormCode] = useState('');
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editName, setEditName] = useState('');
  const [editCode, setEditCode] = useState('');
  const [deleteTargetId, setDeleteTargetId] = useState<number | null>(null);

  const handleCreate = async () => {
    if (!formName.trim() || !formCode.trim()) return;
    await createMutation.mutateAsync({ stateName: formName, stateCode: formCode });
    setFormName('');
    setFormCode('');
    setShowForm(false);
  };

  const handleEdit = (s: { stateId: number; stateName: string; stateCode: string }) => {
    setEditingId(s.stateId);
    setEditName(s.stateName);
    setEditCode(s.stateCode);
  };

  const handleEditSave = async () => {
    if (!editingId || !editName.trim() || !editCode.trim()) return;
    await updateMutation.mutateAsync({ id: editingId, data: { stateId: editingId, stateName: editName, stateCode: editCode } });
    setEditingId(null);
  };

  const handleDeleteConfirm = async () => {
    if (!deleteTargetId) return;
    await deleteMutation.mutateAsync(deleteTargetId);
    setDeleteTargetId(null);
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
              <AppInput value={formName} onChange={(e) => setFormName(e.target.value)} placeholder="e.g. Madhya Pradesh" />
            </div>
            <div className="form-field">
              <label>State Code *</label>
              <AppInput value={formCode} onChange={(e) => setFormCode(e.target.value)} placeholder="e.g. MP" maxLength={10} />
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
                <th style={{ padding: '12px 16px', textAlign: 'center', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' }}>Actions</th>
              </tr>
            </thead>
            <tbody>
              {(states ?? []).map((s) => (
                <tr key={s.stateId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                  <td style={{ padding: '12px 16px', fontFamily: 'monospace', fontSize: 13, color: 'var(--accent-primary)' }}>{s.stateId}</td>
                  <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>
                    {editingId === s.stateId ? <AppInput value={editName} onChange={(e) => setEditName(e.target.value)} /> : s.stateName}
                  </td>
                  <td style={{ padding: '12px 16px', fontSize: 13, fontFamily: 'monospace' }}>
                    {editingId === s.stateId ? <AppInput value={editCode} onChange={(e) => setEditCode(e.target.value)} maxLength={10} /> : s.stateCode}
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    <span className="badge" style={{ background: s.isActive ? 'var(--badge-emerald-bg)' : 'var(--badge-red-bg)', color: s.isActive ? 'var(--badge-emerald-text)' : 'var(--badge-red-text)', padding: '4px 10px', borderRadius: 12, fontSize: 12, fontWeight: 600 }}>
                      {s.isActive ? 'Active' : 'Inactive'}
                    </span>
                  </td>
                  <td style={{ padding: '12px 16px', textAlign: 'center' }}>
                    {editingId === s.stateId ? (
                      <div style={{ display: 'flex', gap: 4, justifyContent: 'center' }}>
                        <Button icon="pi pi-check" className="p-button-success p-button-text" size="small" onClick={handleEditSave} loading={updateMutation.isPending} />
                        <Button icon="pi pi-times" className="p-button-secondary p-button-text" size="small" onClick={() => setEditingId(null)} />
                      </div>
                    ) : (
                      <div style={{ display: 'flex', gap: 4, justifyContent: 'center' }}>
                        <Button icon="pi pi-pencil" className="p-button-text p-button-info" size="small" onClick={() => handleEdit(s)} />
                        <Button icon="pi pi-trash" className="p-button-text p-button-danger" size="small" onClick={() => setDeleteTargetId(s.stateId)} />
                      </div>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-map" /><h3>No states yet</h3><p>Create your first state to get started</p></div>
        )}
      </div>

      <ConfirmDialog
        visible={deleteTargetId !== null}
        header="Delete State"
        message="Are you sure you want to delete this state? This action cannot be undone."
        onConfirm={handleDeleteConfirm}
        onCancel={() => setDeleteTargetId(null)}
        loading={deleteMutation.isPending}
      />
    </div>
  );
}
