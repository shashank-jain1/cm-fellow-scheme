import { useState, useMemo } from 'react';
import { Tag } from 'primereact/tag';
import { useWeeklyReport } from '../queries';
import { PageHeader, EmptyState, AppButton, SkeletonTable } from '../../../shared/components/ui';
import { AppInput } from '../../../shared/components/forms';
import type { DailyAttendanceRecordDto } from '../types';

function getMonday(date: Date): string {
  const d = new Date(date);
  const day = d.getDay();
  const diff = d.getDate() - day + (day === 0 ? -6 : 1);
  d.setDate(diff);
  return d.toISOString().split('T')[0];
}

const statusSeverity = (status: string) => {
  switch (status?.toLowerCase()) {
    case 'present': return 'success';
    case 'absent': return 'danger';
    case 'leave': return 'info';
    case 'holiday': return 'warning';
    default: return 'secondary';
  }
};

function DayCard({ record }: { record: DailyAttendanceRecordDto }) {
  const dateObj = new Date(record.attendanceDate);
  const dayName = dateObj.toLocaleDateString('en-IN', { weekday: 'short' });
  const dateLabel = dateObj.toLocaleDateString('en-IN', { day: 'numeric', month: 'short' });

  return (
    <div
      className="card"
      style={{
        padding: 16,
        flex: '1 1 140px',
        minWidth: 140,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        gap: 8,
        border: '1px solid var(--border-color, #e2e8f0)',
        borderRadius: 12,
        transition: 'all 0.2s ease',
      }}
    >
      <div style={{ textTransform: 'uppercase', fontSize: 11, fontWeight: 700, letterSpacing: '0.05em', color: 'var(--text-muted, #64748b)' }}>
        {dayName}
      </div>
      <div style={{ fontSize: 15, fontWeight: 700, color: 'var(--text-heading, #0f172a)' }}>
        {dateLabel}
      </div>

      <Tag value={record.attendanceStatus || 'N/A'} severity={statusSeverity(record.attendanceStatus)} style={{ fontSize: 12, padding: '4px 8px' }} />

      <div style={{ display: 'flex', flexDirection: 'column', gap: 4, width: '100%', marginTop: 4, fontSize: 12, color: 'var(--text-secondary, #475569)', textAlign: 'center' }}>
        {record.checkInTime ? (
          <div style={{ background: 'var(--surface-ground, #f8fafc)', padding: '3px 6px', borderRadius: 4 }}>
            <i className="pi pi-sign-in" style={{ fontSize: 10, marginRight: 4 }} />
            In: <strong>{record.checkInTime}</strong>
          </div>
        ) : null}
        {record.checkOutTime ? (
          <div style={{ background: 'var(--surface-ground, #f8fafc)', padding: '3px 6px', borderRadius: 4 }}>
            <i className="pi pi-sign-out" style={{ fontSize: 10, marginRight: 4 }} />
            Out: <strong>{record.checkOutTime}</strong>
          </div>
        ) : null}
        <div style={{ fontWeight: 600, color: 'var(--primary-color, #4f46e5)', marginTop: 2 }}>
          <i className="pi pi-clock" style={{ fontSize: 11, marginRight: 4 }} />
          {record.hoursWorked ? record.hoursWorked.toFixed(1) : '0.0'} hrs
        </div>
      </div>
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

  const handleResetCurrentWeek = () => {
    setWeekStart(getMonday(new Date()));
  };

  return (
    <div>
      <PageHeader
        title="Weekly Attendance Report"
        subtitle={weekLabel}
      />

      <div className="card" style={{ padding: '16px 20px', marginBottom: 24, display: 'flex', alignItems: 'center', justifyContent: 'space-between', flexWrap: 'wrap', gap: 16 }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 10, flexWrap: 'wrap' }}>
          <AppButton variant="secondary" icon="pi pi-chevron-left" onClick={() => shiftWeek(-1)}>
            Previous Week
          </AppButton>

          <div style={{ minWidth: 160 }}>
            <AppInput
              type="date"
              value={weekStart}
              onChange={(e) => {
                if (e.target.value) {
                  setWeekStart(getMonday(new Date(e.target.value)));
                }
              }}
              style={{ padding: '6px 12px', height: 38 }}
            />
          </div>

          <AppButton variant="secondary" onClick={() => shiftWeek(1)}>
            Next Week <i className="pi pi-chevron-right" style={{ marginLeft: 4 }} />
          </AppButton>
        </div>

        <AppButton variant="outline" icon="pi pi-calendar" onClick={handleResetCurrentWeek}>
          Current Week
        </AppButton>
      </div>

      {isLoading ? (
        <div className="card" style={{ padding: 24 }}>
          <SkeletonTable columns={7} rows={3} />
        </div>
      ) : report && report.dailyRecords && report.dailyRecords.length > 0 ? (
        <>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: 16, marginBottom: 24 }}>
            <div className="card" style={{ padding: 20, display: 'flex', alignItems: 'center', gap: 16 }}>
              <div style={{ width: 48, height: 48, borderRadius: 12, background: 'rgba(16, 185, 129, 0.1)', color: '#10b981', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: 22 }}>
                <i className="pi pi-check-circle" />
              </div>
              <div>
                <div style={{ fontSize: 12, fontWeight: 600, color: 'var(--text-muted, #64748b)', textTransform: 'uppercase' }}>Present Days</div>
                <div style={{ fontSize: 22, fontWeight: 700, color: 'var(--text-heading, #0f172a)' }}>{report.totalAttendanceDays} / 7</div>
              </div>
            </div>

            <div className="card" style={{ padding: 20, display: 'flex', alignItems: 'center', gap: 16 }}>
              <div style={{ width: 48, height: 48, borderRadius: 12, background: 'rgba(59, 130, 246, 0.1)', color: '#3b82f6', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: 22 }}>
                <i className="pi pi-clock" />
              </div>
              <div>
                <div style={{ fontSize: 12, fontWeight: 600, color: 'var(--text-muted, #64748b)', textTransform: 'uppercase' }}>Total Hours Worked</div>
                <div style={{ fontSize: 22, fontWeight: 700, color: 'var(--text-heading, #0f172a)' }}>{report.totalHours ? report.totalHours.toFixed(1) : '0.0'} hrs</div>
              </div>
            </div>

            <div className="card" style={{ padding: 20, display: 'flex', alignItems: 'center', gap: 16 }}>
              <div style={{ width: 48, height: 48, borderRadius: 12, background: 'rgba(245, 158, 11, 0.1)', color: '#f59e0b', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: 22 }}>
                <i className="pi pi-chart-line" />
              </div>
              <div>
                <div style={{ fontSize: 12, fontWeight: 600, color: 'var(--text-muted, #64748b)', textTransform: 'uppercase' }}>Daily Average</div>
                <div style={{ fontSize: 22, fontWeight: 700, color: 'var(--text-heading, #0f172a)' }}>
                  {report.totalAttendanceDays > 0 ? (report.totalHours / report.totalAttendanceDays).toFixed(1) : '0.0'} hrs
                </div>
              </div>
            </div>
          </div>

          <h3 style={{ fontSize: 16, fontWeight: 700, marginBottom: 16, color: 'var(--text-heading, #0f172a)' }}>Daily Breakdown</h3>
          <div style={{ display: 'flex', gap: 12, flexWrap: 'wrap' }}>
            {report.dailyRecords.map((rec) => (
              <DayCard key={rec.attendanceDate} record={rec} />
            ))}
          </div>
        </>
      ) : (
        <div className="card" style={{ padding: 40 }}>
          <EmptyState
            icon="pi pi-calendar-times"
            title="No attendance records found for this week"
            description="Try selecting a different date or navigate to another week using the controls above."
          />
        </div>
      )}
    </div>
  );
}
