import { useState, useMemo } from 'react';
import { useWeeklyReport } from '../queries';
import { PageHeader } from '../../../shared/components/ui';
import type { WeeklyDayReportDto } from '../types';

function getMonday(date: Date): string {
  const d = new Date(date);
  const day = d.getDay();
  const diff = d.getDate() - day + (day === 0 ? -6 : 1);
  d.setDate(diff);
  return d.toISOString().split('T')[0];
}

const statusColor: Record<string, string> = {
  Present: 'var(--emerald-500)',
  Absent: 'var(--red-500)',
  Leave: 'var(--blue-500)',
  Holiday: 'var(--purple-500)',
};

function DayCard({ day }: { day: WeeklyDayReportDto }) {
  const color = statusColor[day.status] ?? 'var(--text-secondary)';
  return (
    <div style={{ border: '1px solid var(--surface-border)', borderRadius: 8, padding: 16, flex: 1, minWidth: 140, textAlign: 'center' }}>
      <div style={{ fontWeight: 600, marginBottom: 4 }}>{day.dayLabel}</div>
      <div style={{ fontSize: 12, color: 'var(--text-secondary)', marginBottom: 8 }}>{day.date}</div>
      <div style={{ fontWeight: 700, color, fontSize: 14, marginBottom: 4 }}>{day.status}</div>
      {day.checkInTime && <div style={{ fontSize: 12, color: 'var(--text-secondary)' }}>In: {day.checkInTime}</div>}
      {day.checkOutTime && <div style={{ fontSize: 12, color: 'var(--text-secondary)' }}>Out: {day.checkOutTime}</div>}
      <div style={{ fontSize: 12, color: 'var(--text-secondary)' }}>{day.hoursWorked.toFixed(1)}h</div>
    </div>
  );
}

export default function WeeklyAttendanceReportPage() {
  const [weekStart, setWeekStart] = useState(() => getMonday(new Date()));
  const { data: report, isLoading } = useWeeklyReport(weekStart);

  const weekLabel = useMemo(() => {
    const start = new Date(weekStart);
    const end = new Date(start);
    end.setDate(end.getDate() + 6);
    const fmt = (d: Date) => d.toLocaleDateString('en-IN', { day: 'numeric', month: 'short', year: 'numeric' });
    return `${fmt(start)} — ${fmt(end)}`;
  }, [weekStart]);

  const shiftWeek = (delta: number) => {
    const d = new Date(weekStart);
    d.setDate(d.getDate() + delta * 7);
    setWeekStart(getMonday(d));
  };

  return (
    <div>
      <PageHeader title="Weekly Attendance Report" subtitle={weekLabel} />
      <div style={{ display: 'flex', alignItems: 'center', gap: 12, marginBottom: 24 }}>
        <button className="p-button p-button-text" onClick={() => shiftWeek(-1)}>&#8592; Prev</button>
        <input type="date" value={weekStart} onChange={(e) => setWeekStart(getMonday(new Date(e.target.value)))} />
        <button className="p-button p-button-text" onClick={() => shiftWeek(1)}>Next &#8594;</button>
      </div>
      {isLoading ? (
        <div style={{ padding: 40, textAlign: 'center', color: 'var(--text-secondary)' }}>Loading report...</div>
      ) : report ? (
        <>
          <div style={{ display: 'flex', gap: 12, marginBottom: 24, flexWrap: 'wrap' }}>
            <div style={{ padding: '8px 16px', borderRadius: 6, background: 'var(--emerald-100)', color: 'var(--emerald-700)' }}>Present: {report.totalPresent}</div>
            <div style={{ padding: '8px 16px', borderRadius: 6, background: 'var(--red-100)', color: 'var(--red-700)' }}>Absent: {report.totalAbsent}</div>
            <div style={{ padding: '8px 16px', borderRadius: 6, background: 'var(--blue-100)', color: 'var(--blue-700)' }}>Leave: {report.totalLeave}</div>
            <div style={{ padding: '8px 16px', borderRadius: 6, background: 'var(--amber-100)', color: 'var(--amber-700)' }}>Hours: {report.totalHours.toFixed(1)}</div>
          </div>
          <div style={{ display: 'flex', gap: 12, flexWrap: 'wrap' }}>
            {report.days.map((d) => (
              <DayCard key={d.date} day={d} />
            ))}
          </div>
        </>
      ) : (
        <div style={{ padding: 40, textAlign: 'center', color: 'var(--text-secondary)' }}>No data for this week.</div>
      )}
    </div>
  );
}
