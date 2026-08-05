import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import type { SurveyDetailDto } from '../types';

const getStatusSeverity = (status: string) => {
  switch (status.toLowerCase()) {
    case 'completed':
    case 'done':
      return 'success';
    case 'in progress':
    case 'pending':
      return 'warning';
    case 'not started':
    case 'failed':
      return 'danger';
    default:
      return 'info';
  }
};

export default function SurveyTable({ data }: { data: SurveyDetailDto[] }) {
  return (
    <div className="table-wrapper">
      <DataTable
        value={data}
        responsiveLayout="scroll"
        emptyMessage="No surveys found"
        rowKey="surveyRecordId"
      >
        <Column field="internName" header="Intern" bodyStyle={{ fontWeight: 500 }} />
        <Column field="surveyPersonName" header="Survey Person" bodyStyle={{ color: 'var(--text-secondary)' }} />
        <Column field="mobileNumber" header="Mobile" />
        <Column field="panchayatName" header="Panchayat" />
        <Column field="villageName" header="Village" />
        <Column field="surveyDate" header="Date" bodyStyle={{ fontSize: 12, color: 'var(--text-muted)' }} />
        <Column
          field="surveyStatus"
          header="Status"
          body={(row: SurveyDetailDto) => (
            <Tag value={row.surveyStatus} severity={getStatusSeverity(row.surveyStatus)} />
          )}
        />
      </DataTable>
    </div>
  );
}
