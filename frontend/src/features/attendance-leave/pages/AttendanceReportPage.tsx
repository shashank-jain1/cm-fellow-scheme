import { useState } from 'react';
import { useMonthlyReport } from '../queries';
import { PageHeader } from '../../../shared/components/ui';
import AttendanceReportFilters from '../components/AttendanceReportFilters';
import AttendanceReportCards from '../components/AttendanceReportCards';

const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

export default function AttendanceReportPage() {
  const now = new Date();
  const [month, setMonth] = useState(now.getMonth() + 1);
  const [year, setYear] = useState(now.getFullYear());
  const { data: report, isLoading } = useMonthlyReport(month, year);

  const summaryCards = report
    ? [
        { label: 'Days Present', value: report.attendanceDays, icon: 'pi pi-check-circle', color: 'var(--emerald-500)' },
        { label: 'Days Absent', value: report.absentDays, icon: 'pi pi-times-circle', color: 'var(--red-500)' },
        { label: 'Days on Leave', value: report.leaveDays, icon: 'pi pi-calendar-minus', color: 'var(--blue-500)' },
        { label: 'Total Hours', value: report.totalHours, icon: 'pi pi-clock', color: 'var(--amber-500)' },
      ]
    : [];

  return (
    <div>
      <PageHeader title="Attendance Report" subtitle="Monthly attendance summary" />
      <AttendanceReportFilters month={month} setMonth={setMonth} year={year} setYear={setYear} />
      {isLoading ? (
        <div style={{ padding: 40, textAlign: 'center', color: 'var(--text-secondary)' }}>Loading report...</div>
      ) : (
        <AttendanceReportCards cards={summaryCards} monthLabel={months[month - 1]} year={year} />
      )}
    </div>
  );
}
