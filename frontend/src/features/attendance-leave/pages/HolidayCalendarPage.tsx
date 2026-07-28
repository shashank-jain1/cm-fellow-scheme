import { useState, useRef } from 'react';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { InputSwitch } from 'primereact/inputswitch';
import { Calendar } from 'primereact/calendar';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { Toast } from 'primereact/toast';
import { useHolidays, useCreateHoliday, useUpdateHoliday, useDeleteHoliday } from '../queries';
import { PageHeader, EmptyState, ConfirmDialog, AppButton } from '../../../shared/components/ui';
import type { HolidayDto } from '../types';

export default function HolidayCalendarPage() {
  const currentYear = new Date().getFullYear();
  const [selectedYear, setSelectedYear] = useState<number>(currentYear);
  const [dialogVisible, setDialogVisible] = useState(false);
  const [editingHoliday, setEditingHoliday] = useState<HolidayDto | null>(null);
  const [holidayName, setHolidayName] = useState('');
  const [holidayDate, setHolidayDate] = useState<Date | null>(null);
  const [description, setDescription] = useState('');
  const [isOptional, setIsOptional] = useState(false);
  const [deleteTarget, setDeleteTarget] = useState<HolidayDto | null>(null);
  const toast = useRef<Toast>(null);

  const { data: holidays = [], isLoading } = useHolidays(selectedYear);
  const createHoliday = useCreateHoliday();
  const updateHoliday = useUpdateHoliday();
  const deleteHoliday = useDeleteHoliday();

  const openNew = () => {
    setEditingHoliday(null);
    setHolidayName('');
    setHolidayDate(null);
    setDescription('');
    setIsOptional(false);
    setDialogVisible(true);
  };

  const openEdit = (holiday: HolidayDto) => {
    setEditingHoliday(holiday);
    setHolidayName(holiday.holidayName);
    setHolidayDate(new Date(holiday.holidayDate));
    setDescription(holiday.description ?? '');
    setIsOptional(holiday.isOptional);
    setDialogVisible(true);
  };

  const saveHoliday = async () => {
    if (!holidayName || !holidayDate) return;

    if (editingHoliday) {
      await updateHoliday.mutateAsync({
        holidayId: editingHoliday.holidayId,
        holidayName,
        holidayDate: holidayDate.toISOString(),
        description: description || undefined,
        isOptional,
      });
      toast.current?.show({ severity: 'success', summary: 'Updated', detail: 'Holiday updated' });
    } else {
      await createHoliday.mutateAsync({
        holidayName,
        holidayDate: holidayDate.toISOString(),
        description: description || undefined,
        isOptional,
      });
      toast.current?.show({ severity: 'success', summary: 'Created', detail: 'Holiday created' });
    }

    setDialogVisible(false);
  };

  const handleDelete = async () => {
    if (!deleteTarget) return;
    await deleteHoliday.mutateAsync(deleteTarget.holidayId);
    toast.current?.show({ severity: 'success', summary: 'Deleted', detail: 'Holiday deleted' });
    setDeleteTarget(null);
  };

  const yearOptions = Array.from({ length: 5 }, (_, i) => currentYear - 2 + i);

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader
        title="Holiday Calendar"
        subtitle="Manage company holidays for the year"
        action={
          <div style={{ display: 'flex', gap: 12, alignItems: 'center' }}>
            <select
              value={selectedYear}
              onChange={(e) => setSelectedYear(Number(e.target.value))}
              className="form-input"
              style={{ width: 120 }}
            >
              {yearOptions.map((y) => (
                <option key={y} value={y}>{y}</option>
              ))}
            </select>
            <AppButton icon="pi pi-plus" onClick={openNew}>
              Add Holiday
            </AppButton>
          </div>
        }
      />

      <div className="table-wrapper">
        <DataTable value={holidays} loading={isLoading} rows={10} paginator emptyMessage=" ">
          <Column field="holidayName" header="Holiday Name" />
          <Column
            field="holidayDate"
            header="Date"
            body={(row: HolidayDto) => new Date(row.holidayDate).toLocaleDateString('en-IN', { day: '2-digit', month: 'short', year: 'numeric' })}
          />
          <Column field="description" header="Description" />
          <Column
            field="isOptional"
            header="Type"
            body={(row: HolidayDto) => (
              <Tag value={row.isOptional ? 'Optional' : 'Compulsory'} severity={row.isOptional ? 'info' : 'success'} />
            )}
          />
          <Column
            header="Actions"
            body={(row: HolidayDto) => (
              <div style={{ display: 'flex', gap: 4 }}>
                <AppButton variant="ghost" size="sm" icon="pi pi-pencil" onClick={() => openEdit(row)} title="Edit" />
                <AppButton variant="ghost" size="sm" icon="pi pi-trash" onClick={() => setDeleteTarget(row)} title="Delete" />
              </div>
            )}
          />
        </DataTable>
        {holidays.length === 0 && !isLoading && (
          <EmptyState icon="pi pi-calendar" title="No holidays for this year" description="Click 'Add Holiday' to add holidays for this year" />
        )}
      </div>

      <Dialog
        header={editingHoliday ? 'Edit Holiday' : 'Add Holiday'}
        visible={dialogVisible}
        style={{ width: '480px' }}
        modal
        onHide={() => setDialogVisible(false)}
      >
        <div className="form-grid" style={{ marginTop: 16 }}>
          <div className="form-field full-width">
            <label>Holiday Name *</label>
            <InputText value={holidayName} onChange={(e) => setHolidayName(e.target.value)} style={{ width: '100%' }} />
          </div>
          <div className="form-field">
            <label>Date *</label>
            <Calendar value={holidayDate} onChange={(e) => setHolidayDate(e.value as Date)} dateFormat="dd/mm/yy" style={{ width: '100%' }} showIcon />
          </div>
          <div className="form-field">
            <label>Optional Holiday</label>
            <InputSwitch checked={isOptional} onChange={(e) => setIsOptional(Boolean(e.value))} />
          </div>
          <div className="form-field full-width">
            <label>Description</label>
            <InputText value={description} onChange={(e) => setDescription(e.target.value)} style={{ width: '100%' }} />
          </div>
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 16 }}>
          <AppButton variant="secondary" onClick={() => setDialogVisible(false)}>Cancel</AppButton>
          <AppButton onClick={saveHoliday} loading={createHoliday.isPending || updateHoliday.isPending}>Save</AppButton>
        </div>
      </Dialog>

      <ConfirmDialog
        visible={!!deleteTarget}
        header="Delete Holiday"
        message={`Are you sure you want to delete "${deleteTarget?.holidayName}"?`}
        onConfirm={handleDelete}
        onCancel={() => setDeleteTarget(null)}
        loading={deleteHoliday.isPending}
      />
    </div>
  );
}
