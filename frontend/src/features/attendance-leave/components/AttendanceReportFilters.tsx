interface AttendanceReportFiltersProps {
  month: number;
  setMonth: (m: number) => void;
  year: number;
  setYear: (y: number) => void;
}

const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

export default function AttendanceReportFilters({ month, setMonth, year, setYear }: AttendanceReportFiltersProps) {
  const now = new Date();
  const years = Array.from({ length: 5 }, (_, i) => now.getFullYear() - 2 + i);

  const selectStyle: React.CSSProperties = {
    padding: '8px 12px', border: '1px solid var(--border-color)', borderRadius: 6,
    fontSize: 14, background: 'var(--surface-card)', color: 'var(--text-primary)',
  };

  return (
    <div style={{ display: 'flex', gap: 12, marginBottom: 24 }}>
      <div>
        <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4, color: 'var(--text-secondary)' }}>Month</label>
        <select value={month} onChange={(e) => setMonth(Number(e.target.value))} style={selectStyle}>
          {months.map((m, i) => (<option key={i} value={i + 1}>{m}</option>))}
        </select>
      </div>
      <div>
        <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4, color: 'var(--text-secondary)' }}>Year</label>
        <select value={year} onChange={(e) => setYear(Number(e.target.value))} style={selectStyle}>
          {years.map((y) => (<option key={y} value={y}>{y}</option>))}
        </select>
      </div>
    </div>
  );
}
