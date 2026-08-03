import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { AppButton, EmptyState, SkeletonTable } from '../../../shared/components/ui';
import type { HolidayDto } from '../types';

interface Props {
  holidays: HolidayDto[];
  isLoading: boolean;
  onEdit: (holiday: HolidayDto) => void;
  onDelete: (holiday: HolidayDto) => void;
}

export default function HolidayTable({ holidays, isLoading, onEdit, onDelete }: Props) {
  const dateBody = (row: HolidayDto) =>
    new Date(row.holidayDate).toLocaleDateString('en-IN', { day: '2-digit', month: 'short', year: 'numeric' });

  const typeBody = (row: HolidayDto) => (
    <Tag value={row.isOptional ? 'Optional' : 'Compulsory'} severity={row.isOptional ? 'info' : 'success'} />
  );

  const actionsBody = (row: HolidayDto) => (
    <div style={{ display: 'flex', gap: 4 }}>
      <AppButton variant="ghost" size="sm" icon="pi pi-pencil" onClick={() => onEdit(row)} title="Edit" />
      <AppButton variant="ghost" size="sm" icon="pi pi-trash" onClick={() => onDelete(row)} title="Delete" />
    </div>
  );

  return (
    <div className="table-wrapper">
      {isLoading ? (
        <SkeletonTable columns={5} />
      ) : holidays.length > 0 ? (
        <DataTable value={holidays} rows={10} paginator emptyMessage=" ">
          <Column field="holidayName" header="Holiday Name" />
          <Column field="holidayDate" header="Date" body={dateBody} />
          <Column field="description" header="Description" />
          <Column field="isOptional" header="Type" body={typeBody} />
          <Column header="Actions" body={actionsBody} />
        </DataTable>
      ) : (
        <EmptyState icon="pi pi-calendar" title="No holidays for this year" description="Click 'Add Holiday' to add holidays for this year" />
      )}
    </div>
  );
}
