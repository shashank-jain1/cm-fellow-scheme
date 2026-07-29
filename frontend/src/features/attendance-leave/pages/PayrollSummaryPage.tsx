import { useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { AppInput } from '../../../shared/components/forms';
import { usePayrollSummary } from '../queries';
import { PageHeader, EmptyState, AppButton, SkeletonTable } from '../../../shared/components/ui';
import type { PayrollSummaryDto } from '../types';

export default function PayrollSummaryPage() {
  const [payrollMonth, setPayrollMonth] = useState('');
  const [applicantId, setApplicantId] = useState('');
  const [searchApplied, setSearchApplied] = useState(false);

  const { data: summaries = [], isLoading } = usePayrollSummary(
    searchApplied ? payrollMonth : undefined,
    searchApplied && applicantId ? Number(applicantId) : undefined
  );

  const handleSearch = () => setSearchApplied(true);

  const handleClear = () => {
    setPayrollMonth('');
    setApplicantId('');
    setSearchApplied(false);
  };

  const absentDaysBody = (row: PayrollSummaryDto) =>
    row.absentDays > 0
      ? <Tag value={row.absentDays.toString()} severity="danger" />
      : <Tag value="0" severity="success" />;

  return (
    <div>
      <PageHeader
        title="Payroll Attendance Summary"
        subtitle="View monthly attendance summaries for payroll processing"
      />

      <div className="card" style={{ padding: 20, marginBottom: 24 }}>
        <div style={{ display: 'flex', gap: 12, alignItems: 'flex-end', flexWrap: 'wrap' }}>
          <div className="form-field" style={{ minWidth: 180 }}>
            <label>Payroll Month</label>
            <AppInput
              value={payrollMonth}
              onChange={(e) => setPayrollMonth(e.target.value)}
              placeholder="e.g. 2026-07"
              style={{ width: '100%' }}
            />
          </div>
          <div className="form-field" style={{ minWidth: 150 }}>
            <label>Applicant ID</label>
            <AppInput
              value={applicantId}
              onChange={(e) => setApplicantId(e.target.value)}
              placeholder="Applicant ID"
              style={{ width: '100%' }}
            />
          </div>
          <div style={{ display: 'flex', gap: 8 }}>
            <AppButton icon="pi pi-search" onClick={handleSearch}>Search</AppButton>
            <AppButton variant="secondary" onClick={handleClear}>Clear</AppButton>
          </div>
        </div>
      </div>

      <div className="table-wrapper">
        {isLoading ? (
          <SkeletonTable columns={8} />
        ) : summaries.length > 0 ? (
          <DataTable value={summaries} rows={10} paginator emptyMessage=" ">
            <Column field="payrollMonth" header="Month" />
            <Column field="applicantId" header="Applicant ID" />
            <Column field="totalWorkingDays" header="Working Days" />
            <Column field="presentDays" header="Present" />
            <Column field="approvedLeaveDays" header="Approved Leave" />
            <Column field="absentDays" header="Absent" body={absentDaysBody} />
            <Column field="payableDays" header="Payable Days" />
            <Column
              field="createdOn"
              header="Generated"
              body={(row: PayrollSummaryDto) => new Date(row.createdOn).toLocaleDateString('en-IN')}
            />
          </DataTable>
        ) : (
          <EmptyState icon="pi pi-money-bill" title="No payroll records found" description="Try adjusting your search filters" />
        )}
      </div>
    </div>
  );
}
