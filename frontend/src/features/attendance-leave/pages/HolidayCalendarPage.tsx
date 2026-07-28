import { useState, useRef } from 'react';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { InputSwitch } from 'primereact/inputswitch';
import { Calendar } from 'primereact/calendar';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { Toast } from 'primereact/toast';
import { useHolidays, useCreateHoliday, useUpdateHoliday, useDeleteHoliday } from '../queries';
import type { HolidayDto, CreateHolidayCommand } from '../types';

export default function HolidayCalendarPage() {
  const currentYear = new Date().getFullYear();
  const [selectedYear, setSelectedYear] = useState<number>(currentYear);
  const [dialogVisible, setDialogVisible] = useState(false);
  const [editingHoliday, setEditingHoliday] = useState<HolidayDto | null>(null);
  const [holidayName, setHolidayName] = useState('');
  const [holidayDate, setHolidayDate] = useState<Date | null>(null);
  const [description, setDescription] = useState('');
  const [isOptional, setIsOptional] = useState(false);
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

  const handleDelete = async (holiday: HolidayDto) => {
    await deleteHoliday.mutateAsync(holiday.holidayId);
    toast.current?.show({ severity: 'success', summary: 'Deleted', detail: 'Holiday deleted' });
  };

  const yearOptions = Array.from({ length: 5 }, (_, i) => currentYear - 2 + i);

  return (
    <div className="form-grid">
      <Toast ref={toast} />
      <div className="form-field" style={{ gridColumn: '1 / -1', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h2>Holiday Calendar</h2>
        <div className="flex gap-2 align-items-center">
          <select
            value={selectedYear}
            onChange={(e) => setSelectedYear(Number(e.target.value))}
            style={{ padding: '0.5rem', borderRadius: '4px', border: '1px solid #ccc' }}
          >
            {yearOptions.map((y) => (
              <option key={y} value={y}>{y}</option>
            ))}
          </select>
          <Button label="Add Holiday" icon="pi pi-plus" onClick={openNew} />
        </div>
      </div>

      <div className="form-field" style={{ gridColumn: '1 / -1' }}>
        <DataTable value={holidays} loading={isLoading} emptyMessage="No holidays for this year." rows={10} paginator>
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
              <div className="flex gap-1">
                <Button icon="pi pi-pencil" size="small" text onClick={() => openEdit(row)} />
                <Button icon="pi pi-trash" size="small" text severity="danger" onClick={() => handleDelete(row)} />
              </div>
            )}
          />
        </DataTable>
      </div>

      <Dialog
        header={editingHoliday ? 'Edit Holiday' : 'Add Holiday'}
        visible={dialogVisible}
        style={{ width: '450px' }}
        modal
        onHide={() => setDialogVisible(false)}
      >
        <div className="form-grid">
          <div className="form-field">
            <label>Holiday Name *</label>
            <InputText value={holidayName} onChange={(e) => setHolidayName(e.target.value)} style={{ width: '100%' }} />
          </div>
          <div className="form-field">
            <label>Date *</label>
            <Calendar value={holidayDate} onChange={(e) => setHolidayDate(e.value as Date)} dateFormat="dd/mm/yy" style={{ width: '100%' }} showIcon />
          </div>
          <div className="form-field">
            <label>Description</label>
            <InputText value={description} onChange={(e) => setDescription(e.target.value)} style={{ width: '100%' }} />
          </div>
          <div className="form-field">
            <label>Optional Holiday</label>
            <InputSwitch checked={isOptional} onChange={(e) => setIsOptional(Boolean(e.value))} />
          </div>
        </div>
        <div className="flex justify-content-end gap-2" style={{ marginTop: '1rem' }}>
          <Button label="Cancel" severity="secondary" onClick={() => setDialogVisible(false)} />
          <Button label="Save" onClick={saveHoliday} loading={createHoliday.isPending || updateHoliday.isPending} />
        </div>
      </Dialog>
    </div>
  );
}
