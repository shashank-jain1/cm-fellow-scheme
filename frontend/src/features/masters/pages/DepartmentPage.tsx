import { useState } from 'react';
import { Button } from 'primereact/button';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { AppInput } from '../../../shared/components/forms';
import AppDialog from '../../../shared/components/forms/AppDialog';
import { mastersApi } from '../api';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import type { CreateDepartmentCommand } from '../types';

const emptyForm: CreateDepartmentCommand = { departmentName: '', departmentCode: '' };

export default function DepartmentPage() {
  const queryClient = useQueryClient();
  const [dialog, setDialog] = useState(false);
  const [form, setForm] = useState<CreateDepartmentCommand>(emptyForm);

  const { data: departments = [], isLoading } = useQuery({
    queryKey: ['departments'],
    queryFn: () => mastersApi.getDepartments().then((r) => r.data ?? []),
  });

  const mutation = useMutation({
    mutationFn: (data: CreateDepartmentCommand) => mastersApi.createDepartment(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['departments'] });
      setDialog(false);
      setForm(emptyForm);
    },
  });

  const handleCreate = () => {
    if (!form.departmentName || !form.departmentCode) return;
    mutation.mutate(form);
  };

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Department Masters</h1>
          <p>Manage departments used across the system</p>
        </div>
        <Button label="Add Department" icon="pi pi-plus" size="small" onClick={() => setDialog(true)} />
      </div>

      <div className="table-wrapper">
        <DataTable value={departments} loading={isLoading} rows={10} stripedRows>
          <Column field="departmentCode" header="Code" bodyStyle={{ fontFamily: 'monospace', fontWeight: 600 }} />
          <Column field="departmentName" header="Name" />
          <Column
            field="isActive"
            header="Status"
            body={(row) => (
              <span className="badge" style={{ background: row.isActive ? 'var(--badge-emerald-bg)' : 'var(--badge-red-bg)', color: row.isActive ? 'var(--badge-emerald-text)' : 'var(--badge-red-text)', padding: '4px 10px', borderRadius: 12, fontSize: 12, fontWeight: 600 }}>
                {row.isActive ? 'Active' : 'Inactive'}
              </span>
            )}
          />
        </DataTable>
      </div>

      <AppDialog header="Add Department" visible={dialog} style={{ width: 420 }} onHide={() => setDialog(false)} footer={
        <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8 }}>
          <Button label="Cancel" text size="small" onClick={() => setDialog(false)} />
          <Button label="Save" size="small" loading={mutation.isPending} onClick={handleCreate} />
        </div>
      }>
        <div style={{ display: 'flex', flexDirection: 'column', gap: 16, padding: '8px 0' }}>
          <div>
            <label style={{ display: 'block', fontSize: 13, fontWeight: 600, marginBottom: 6 }}>Department Name</label>
            <AppInput value={form.departmentName} onChange={(e) => setForm({ ...form, departmentName: e.target.value })} placeholder="e.g. Public Works" />
          </div>
          <div>
            <label style={{ display: 'block', fontSize: 13, fontWeight: 600, marginBottom: 6 }}>Department Code</label>
            <AppInput value={form.departmentCode} onChange={(e) => setForm({ ...form, departmentCode: e.target.value })} placeholder="e.g. PWD" />
          </div>
        </div>
      </AppDialog>
    </div>
  );
}
