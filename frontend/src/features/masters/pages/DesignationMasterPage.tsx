import { useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { Dialog } from 'primereact/dialog';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import ApiService from '../../../services/ApiService';
import { AppButton, SkeletonTable } from '../../../shared/components/ui';
import { AppInput } from '../../../shared/components/forms';

interface LookupMasterItem {
  lookupMasterId: number;
  masterType: string;
  label: string;
  value: string;
  sortOrder: number;
  isActive: boolean;
}

export default function DesignationMasterPage() {
  const queryClient = useQueryClient();
  const [dialogVisible, setDialogVisible] = useState(false);
  const [label, setLabel] = useState('');
  const [value, setValue] = useState('');

  const { data: lookups, isLoading } = useQuery<LookupMasterItem[]>({
    queryKey: ['lookup-masters'],
    queryFn: async () => {
      const res = await ApiService.get<LookupMasterItem[]>('masters/lookup');
      return res ?? [];
    },
  });

  const createMutation = useMutation({
    mutationFn: async (data: { masterType: string; label: string; value: string; sortOrder: number }) => {
      return ApiService.post('masters/lookup', data);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['lookup-masters'] });
      setDialogVisible(false);
      setLabel('');
      setValue('');
    },
  });

  const designations = (lookups ?? []).filter(
    (l) => l.masterType?.toLowerCase() === 'designation'
  );

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
      <div className="page-header">
        <div>
          <h1>Designations Master</h1>
          <p style={{ color: 'var(--text-muted)', marginTop: 4 }}>
            Manage role designations and designations lookup
          </p>
        </div>
        <AppButton icon="pi pi-plus" onClick={() => setDialogVisible(true)}>
          Add Designation
        </AppButton>
      </div>

      <div className="card" style={{ padding: 24 }}>
        {isLoading ? (
          <SkeletonTable columns={4} />
        ) : (
          <DataTable
            value={designations}
            paginator
            rows={10}
            responsiveLayout="scroll"
            emptyMessage="No designations found."
          >
            <Column field="lookupMasterId" header="ID" style={{ width: '80px' }} />
            <Column field="label" header="Designation Title" sortable />
            <Column field="value" header="System Code" sortable />
            <Column
              field="isActive"
              header="Status"
              body={(r: LookupMasterItem) => (
                <Tag value={r.isActive ? 'Active' : 'Inactive'} severity={r.isActive ? 'success' : 'danger'} />
              )}
            />
          </DataTable>
        )}
      </div>

      <Dialog
        header="Add New Designation"
        visible={dialogVisible}
        onHide={() => setDialogVisible(false)}
        style={{ width: '400px' }}
      >
        <div style={{ display: 'flex', flexDirection: 'column', gap: 16, paddingTop: 8 }}>
          <div>
            <label style={{ fontSize: 13, fontWeight: 500, marginBottom: 4, display: 'block' }}>Designation Title *</label>
            <AppInput
              value={label}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => setLabel(e.target.value)}
              placeholder="e.g. Senior CM Fellow"
            />
          </div>
          <div>
            <label style={{ fontSize: 13, fontWeight: 500, marginBottom: 4, display: 'block' }}>System Code (Optional)</label>
            <AppInput
              value={value}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => setValue(e.target.value)}
              placeholder="e.g. senior_fellow"
            />
          </div>
          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 16 }}>
            <AppButton variant="secondary" onClick={() => setDialogVisible(false)}>
              Cancel
            </AppButton>
            <AppButton
              icon="pi pi-check"
              disabled={!label.trim()}
              loading={createMutation.isPending}
              onClick={handleSave}
            >
              Save
            </AppButton>
          </div>
        </div>
      </Dialog>
    </div>
  );
}
