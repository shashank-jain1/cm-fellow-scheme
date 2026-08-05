import { useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { mastersApi } from '../api';
import type { CreateDepartmentCommand } from '../types';
import { PageHeader, StatusTag, EmptyState, SkeletonTable, AppButton, FormField } from '../../../shared/components/ui';
import { AppInput, AppDialog } from '../../../shared/components/forms';

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
      <PageHeader
        title="Department Masters"
        subtitle="Manage departments used across the system"
        action={
          <AppButton icon="pi pi-plus" onClick={() => setDialog(true)}>
            Add Department
          </AppButton>
        }
      />

      <div className="card" style={{ padding: 'var(--space-6)' }}>
        {isLoading ? (
          <SkeletonTable columns={3} />
        ) : departments.length === 0 ? (
          <EmptyState icon="pi pi-building" title="No departments yet" description="Add your first department to get started." />
        ) : (
          <DataTable value={departments} rows={10} stripedRows paginator emptyMessage="No departments found.">
            <Column field="departmentCode" header="Code" style={{ fontFamily: 'monospace', fontWeight: 600 }} />
            <Column field="departmentName" header="Name" />
            <Column
              field="isActive"
              header="Status"
              body={(row) => (
                <StatusTag value={row.isActive ? 'Active' : 'Inactive'} />
              )}
            />
          </DataTable>
        )}
      </div>

      <AppDialog
        header="Add Department"
        visible={dialog}
        style={{ width: 420 }}
        onHide={() => setDialog(false)}
        footer={
          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8 }}>
            <AppButton variant="secondary" onClick={() => setDialog(false)}>Cancel</AppButton>
            <AppButton
              icon="pi pi-check"
              disabled={!form.departmentName || !form.departmentCode}
              loading={mutation.isPending}
              onClick={handleCreate}
            >
              Save
            </AppButton>
          </div>
        }
      >
        <div style={{ display: 'flex', flexDirection: 'column', gap: 16, paddingTop: 8 }}>
          <FormField label="Department Name" required>
            <AppInput
              value={form.departmentName}
              onChange={(e) => setForm({ ...form, departmentName: e.target.value })}
              placeholder="e.g. Public Works"
            />
          </FormField>
          <FormField label="Department Code" required>
            <AppInput
              value={form.departmentCode}
              onChange={(e) => setForm({ ...form, departmentCode: e.target.value })}
              placeholder="e.g. PWD"
            />
          </FormField>
        </div>
      </AppDialog>
    </div>
  );
}
