import { useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useLookupMasters } from '../../../shared/hooks/useMasters';
import { mastersApi } from '../api';
import { PageHeader, StatusTag, EmptyState, SkeletonTable, AppButton, FormField } from '../../../shared/components/ui';
import { AppInput, AppDialog } from '../../../shared/components/forms';

export default function DesignationMasterPage() {
  const queryClient = useQueryClient();
  const [dialogVisible, setDialogVisible] = useState(false);
  const [label, setLabel] = useState('');
  const [value, setValue] = useState('');

  const { data: designations = [], isLoading } = useLookupMasters('Designation');

  const createMutation = useMutation({
    mutationFn: (data: { masterType: string; label: string; value: string; sortOrder: number }) =>
      mastersApi.createLookup(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['masters', 'lookups', 'Designation'] });
      setDialogVisible(false);
      setLabel('');
      setValue('');
    },
  });

  const handleSave = () => {
    if (!label.trim()) return;
    createMutation.mutate({
      masterType: 'Designation',
      label: label.trim(),
      value: value.trim() || label.trim().toLowerCase().replace(/\s+/g, '_'),
      sortOrder: designations.length + 1,
    });
  };

  return (
    <div>
      <PageHeader
        title="Designations Master"
        subtitle="Manage role designations used across the system"
        action={
          <AppButton icon="pi pi-plus" onClick={() => setDialogVisible(true)}>
            Add Designation
          </AppButton>
        }
      />

      <div className="card" style={{ padding: 'var(--space-6)' }}>
        {isLoading ? (
          <SkeletonTable columns={4} />
        ) : designations.length === 0 ? (
          <EmptyState icon="pi pi-id-card" title="No designations yet" description="Add your first designation to get started." />
        ) : (
          <DataTable value={designations} rows={10} stripedRows paginator emptyMessage="No designations found.">
            <Column field="lookupMasterId" header="ID" style={{ width: '80px' }} />
            <Column field="label" header="Designation Title" sortable />
            <Column field="value" header="System Code" sortable />
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
        header="Add Designation"
        visible={dialogVisible}
        style={{ width: 420 }}
        onHide={() => setDialogVisible(false)}
        footer={
          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8 }}>
            <AppButton variant="secondary" onClick={() => setDialogVisible(false)}>Cancel</AppButton>
            <AppButton
              icon="pi pi-check"
              disabled={!label.trim()}
              loading={createMutation.isPending}
              onClick={handleSave}
            >
              Save
            </AppButton>
          </div>
        }
      >
        <div style={{ display: 'flex', flexDirection: 'column', gap: 16, paddingTop: 8 }}>
          <FormField label="Designation Title" required>
            <AppInput
              value={label}
              onChange={(e) => setLabel(e.target.value)}
              placeholder="e.g. Senior CM Fellow"
            />
          </FormField>
          <FormField label="System Code">
            <AppInput
              value={value}
              onChange={(e) => setValue(e.target.value)}
              placeholder="e.g. senior_fellow"
            />
          </FormField>
        </div>
      </AppDialog>
    </div>
  );
}
