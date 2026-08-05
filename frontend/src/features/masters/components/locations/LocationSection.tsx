import { useState } from 'react';
import { Button } from 'primereact/button';
import { AppInput, AppSelect } from '../../../../shared/components/forms';
import { ConfirmDialog } from '../../../../shared/components/ui';

interface LocationSectionProps {
  title: string;
  parentOptions: { label: string; value: string }[];
  selectedParentId: number | null;
  onParentChange: (id: number | null) => void;
  items: any[];
  isLoading: boolean;
  formName: string;
  formCode: string;
  onFormNameChange: (v: string) => void;
  onFormCodeChange: (v: string) => void;
  onCreate: () => void;
  showForm: boolean;
  onToggleForm: () => void;
  parentLabel: string;
  idKey: string;
  nameKey: string;
  codeKey: string;
  parentIdKey: string;
  getParentName: (parentId: number) => string;
  nameLabel: string;
  codeLabel: string;
  codeRequired?: boolean;
  addButtonLabel?: string;
  namePlaceholder?: string;
  codePlaceholder?: string;
  onUpdate?: (id: number, name: string, code: string, parentId: number) => void;
  onDelete?: (id: number) => void;
  onUpdateLoading?: boolean;
  onDeleteLoading?: boolean;
}

const thStyle: React.CSSProperties = { padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', borderBottom: '1px solid var(--border-color)' };
const tdStyle: React.CSSProperties = { padding: '12px 16px', fontSize: 13 };

export default function LocationSection({
  title,
  parentOptions,
  selectedParentId,
  onParentChange,
  items,
  isLoading,
  formName,
  formCode,
  onFormNameChange,
  onFormCodeChange,
  onCreate,
  showForm,
  onToggleForm,
  parentLabel,
  idKey,
  nameKey,
  codeKey,
  parentIdKey,
  getParentName,
  nameLabel,
  codeLabel,
  codeRequired: _codeRequired = true,
  addButtonLabel,
  namePlaceholder,
  codePlaceholder,
  onUpdate,
  onDelete,
  onUpdateLoading,
  onDeleteLoading,
}: LocationSectionProps) {
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editName, setEditName] = useState('');
  const [editCode, setEditCode] = useState('');
  const [editParentId, setEditParentId] = useState<number | null>(null);
  const [deleteTargetId, setDeleteTargetId] = useState<number | null>(null);

  const allOptions = [{ label: `All ${parentLabel}s`, value: '' }, ...parentOptions];

  const handleEdit = (item: any) => {
    setEditingId(item[idKey] as number);
    setEditName(item[nameKey] as string);
    setEditCode((item[codeKey] as string) ?? '');
    setEditParentId(item[parentIdKey] as number);
  };

  const handleEditSave = () => {
    if (!editingId || !onUpdate) return;
    onUpdate(editingId, editName, editCode, editParentId ?? 0);
    setEditingId(null);
  };

  const handleDeleteConfirm = () => {
    if (!deleteTargetId || !onDelete) return;
    onDelete(deleteTargetId);
    setDeleteTargetId(null);
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 700, margin: 0 }}>{title} ({items?.length ?? 0})</h3>
        <div style={{ display: 'flex', gap: 12, alignItems: 'center' }}>
          <AppSelect
            value={selectedParentId ? String(selectedParentId) : ''}
            options={allOptions}
            onChange={(val) => onParentChange(val ? Number(val) : null)}
            style={{ width: 260 }}
          />
          <Button label={addButtonLabel ?? `Add ${parentLabel}`} icon="pi pi-plus" className="btn btn-primary" size="small" onClick={onToggleForm} />
        </div>
      </div>

      {showForm && (
        <div className="glass-card" style={{ padding: 20, marginBottom: 20 }}>
          <div className="form-grid">
            <div className="form-field">
              <label>{parentLabel} *</label>
              <AppSelect
                value={selectedParentId ? String(selectedParentId) : ''}
                options={parentOptions}
                onChange={(val) => onParentChange(val ? Number(val) : null)}
                placeholder={`Select ${parentLabel.toLowerCase()}`}
              />
            </div>
            <div className="form-field">
              <label>{nameLabel}</label>
              <AppInput value={formName} onChange={(e) => onFormNameChange(e.target.value)} placeholder={namePlaceholder} />
            </div>
            <div className="form-field">
              <label>{codeLabel}</label>
              <AppInput value={formCode} onChange={(e) => onFormCodeChange(e.target.value)} placeholder={codePlaceholder} maxLength={10} />
            </div>
          </div>
          <div style={{ display: 'flex', gap: 8, justifyContent: 'flex-end', marginTop: 12 }}>
            <Button label="Cancel" className="btn btn-secondary" size="small" onClick={onToggleForm} />
            <Button label="Create" className="btn btn-primary" size="small" onClick={onCreate} />
          </div>
        </div>
      )}

      <div className="table-wrapper">
        {isLoading ? (
          <div style={{ padding: 20 }}>{[1, 2, 3].map((n) => <div key={n} className="skeleton" style={{ height: 40, marginBottom: 8 }} />)}</div>
        ) : (items ?? []).length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                <th style={thStyle}>ID</th>
                <th style={thStyle}>Name</th>
                <th style={thStyle}>Code</th>
                <th style={thStyle}>{parentLabel}</th>
                {(onUpdate || onDelete) && <th style={{ ...thStyle, textAlign: 'center' }}>Actions</th>}
              </tr>
            </thead>
            <tbody>
              {(items ?? []).map((item: any) => (
                <tr key={item[idKey] as number} style={{ borderBottom: '1px solid var(--border-light)' }}>
                  <td style={{ ...tdStyle, fontFamily: 'monospace', color: 'var(--accent-primary)' }}>{item[idKey] as number}</td>
                  <td style={{ ...tdStyle, fontWeight: 600, fontSize: 14 }}>
                    {editingId === (item[idKey] as number) ? (
                      <AppInput value={editName} onChange={(e) => setEditName(e.target.value)} />
                    ) : (
                      item[nameKey] as string
                    )}
                  </td>
                  <td style={{ ...tdStyle, fontFamily: 'monospace' }}>
                    {editingId === (item[idKey] as number) ? (
                      <AppInput value={editCode} onChange={(e) => setEditCode(e.target.value)} maxLength={10} />
                    ) : (
                      (item[codeKey] as string) ?? '-'
                    )}
                  </td>
                  <td style={{ ...tdStyle, color: 'var(--text-secondary)' }}>
                    {editingId === (item[idKey] as number) ? (
                      <AppSelect
                        value={String(editParentId ?? '')}
                        options={parentOptions}
                        onChange={(val) => setEditParentId(val ? Number(val) : null)}
                      />
                    ) : (
                      getParentName(item[parentIdKey] as number)
                    )}
                  </td>
                  {(onUpdate || onDelete) && (
                    <td style={{ ...tdStyle, textAlign: 'center' }}>
                      {editingId === (item[idKey] as number) ? (
                        <div style={{ display: 'flex', gap: 4, justifyContent: 'center' }}>
                          <Button icon="pi pi-check" className="p-button-success p-button-text" size="small" onClick={handleEditSave} loading={onUpdateLoading} />
                          <Button icon="pi pi-times" className="p-button-secondary p-button-text" size="small" onClick={() => setEditingId(null)} />
                        </div>
                      ) : (
                        <div style={{ display: 'flex', gap: 4, justifyContent: 'center' }}>
                          {onUpdate && <Button icon="pi pi-pencil" className="p-button-text p-button-info" size="small" onClick={() => handleEdit(item)} />}
                          {onDelete && <Button icon="pi pi-trash" className="p-button-text p-button-danger" size="small" onClick={() => setDeleteTargetId(item[idKey] as number)} />}
                        </div>
                      )}
                    </td>
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <div className="empty-state"><i className="pi pi-map" /><h3>No {title.toLowerCase()} yet</h3><p>Select a {parentLabel.toLowerCase()} or create your first {title.toLowerCase().slice(0, -1)}</p></div>
        )}
      </div>

      <ConfirmDialog
        visible={deleteTargetId !== null}
        header={`Delete ${title.slice(0, -1)}`}
        message={`Are you sure you want to delete this ${title.slice(0, -1).toLowerCase()}? This action cannot be undone.`}
        onConfirm={handleDeleteConfirm}
        onCancel={() => setDeleteTargetId(null)}
        loading={onDeleteLoading}
      />
    </div>
  );
}
