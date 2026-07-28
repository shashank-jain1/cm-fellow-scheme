import { useState } from 'react';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Card } from 'primereact/card';
import { Tag } from 'primereact/tag';
import { usePayrollSummary } from '../queries';

export default function PayrollSummaryPage() {
  const [payrollMonth, setPayrollMonth] = useState('');
  const [applicantId, setApplicantId] = useState<string>('');
  const [searchApplied, setSearchApplied] = useState(false);

  const { data: summaries = [], isLoading } = usePayrollSummary(
    searchApplied ? payrollMonth : undefined,
    searchApplied && applicantId ? Number(applicantId) : undefined
  );

  const handleSearch = () => {
    setSearchApplied(true);
  };

  const handleClear = () => {
    setPayrollMonth('');
    setApplicantId('');
    setSearchApplied(false);
  };

  const absentDaysBody = (row: { absentDays: number }) =>
    row.absentDays > 0
      ? <Tag value={row.absentDays.toString()} severity="danger" />
      : <Tag value="0" severity="success" />;

  return (
    <div className="form-grid">
      <div className="form-field" style={{ gridColumn: '1 / -1' }}>
        <h2>Payroll Attendance Summary</h2>
      </div>

      <div className="form-field">
        <label>Payroll Month</label>
        <InputText
          value={payrollMonth}
          onChange={(e) => setPayrollMonth(e.target.value)}
          placeholder="e.g. 2026-07"
          style={{ width: 200 }}
        />
      </div>
      <div className="form-field">
        <label>Applicant ID</label>
        <InputText
          value={applicantId}
          onChange={(e) => setApplicantId(e.target.value)}
          placeholder="Applicant ID"
          style={{ width: 150 }}
        />
      </div>
      <div className="form-field" style={{ display: 'flex', alignItems: 'flex-end', gap: 8 }}>
        <Button label="Search" icon="pi pi-search" onClick={handleSearch} />
        <Button label="Clear" severity="secondary" onClick={handleClear} />
      </div>

      <div className="form-field" style={{ gridColumn: '1 / -1' }}>
        <Card>
          <DataTable value={summaries} loading={isLoading} emptyMessage="No payroll records found." rows={10} paginator>
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
              body={(row: { createdOn: string }) => new Date(row.createdOn).toLocaleDateString('en-IN')}
            />
          </DataTable>
        </Card>
      </div>
    </div>
  );
}
