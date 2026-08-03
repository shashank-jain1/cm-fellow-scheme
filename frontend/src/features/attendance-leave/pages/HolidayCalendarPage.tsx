import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { useHolidays, useCreateHoliday, useUpdateHoliday, useDeleteHoliday } from '../queries';
import { PageHeader, ConfirmDialog, AppButton } from '../../../shared/components/ui';
import HolidayTable from './HolidayTable';
import HolidayForm from './HolidayForm';
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
  const yearOptions = Array.from({ length: 5 }, (_, i) => currentYear - 2 + i);

  const resetForm = () => {
    setEditingHoliday(null); setHolidayName(''); setHolidayDate(null);
    setDescription(''); setIsOptional(false);
  };

  const openNew = () => { resetForm(); setDialogVisible(true); };
  const openEdit = (h: HolidayDto) => {
    setEditingHoliday(h); setHolidayName(h.holidayName); setHolidayDate(new Date(h.holidayDate));
    setDescription(h.description ?? ''); setIsOptional(h.isOptional); setDialogVisible(true);
  };

  const saveHoliday = async () => {
    if (!holidayName || !holidayDate) return;
    if (editingHoliday) {
      await updateHoliday.mutateAsync({ holidayId: editingHoliday.holidayId, holidayName,
        holidayDate: holidayDate.toISOString(), description: description || undefined, isOptional });
      toast.current?.show({ severity: 'success', summary: 'Updated', detail: 'Holiday updated' });
    } else {
      await createHoliday.mutateAsync({ holidayName, holidayDate: holidayDate.toISOString(),
        description: description || undefined, isOptional });
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

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader title="Holiday Calendar" subtitle="Manage company holidays for the year"
        action={
          <div style={{ display: 'flex', gap: 12, alignItems: 'center' }}>
            <select value={selectedYear} onChange={(e) => setSelectedYear(Number(e.target.value))}
              className="form-input" style={{ width: 120 }}>
              {yearOptions.map((y) => <option key={y} value={y}>{y}</option>)}
            </select>
            <AppButton icon="pi pi-plus" onClick={openNew}>Add Holiday</AppButton>
          </div>
        } />
      <HolidayTable holidays={holidays} isLoading={isLoading} onEdit={openEdit} onDelete={setDeleteTarget} />
      <HolidayForm visible={dialogVisible} isEditing={!!editingHoliday} holidayName={holidayName}
        holidayDate={holidayDate} description={description} isOptional={isOptional}
        isSaving={createHoliday.isPending || updateHoliday.isPending}
        onHolidayNameChange={setHolidayName} onHolidayDateChange={setHolidayDate}
        onDescriptionChange={setDescription} onIsOptionalChange={setIsOptional}
        onSave={saveHoliday} onHide={() => setDialogVisible(false)} />
      <ConfirmDialog visible={!!deleteTarget} header="Delete Holiday"
        message={`Are you sure you want to delete "${deleteTarget?.holidayName}"?`}
        onConfirm={handleDelete} onCancel={() => setDeleteTarget(null)} loading={deleteHoliday.isPending} />
    </div>
  );
}
