interface AttendanceLeaveChartProps {
  attendancePercentage: number;
  isLoading?: boolean;
}

export default function AttendanceLeaveChart({ attendancePercentage, isLoading }: AttendanceLeaveChartProps) {
  if (isLoading) {
    return (
      <div className="card" style={{ padding: 24 }}>
        <div className="skeleton" style={{ width: '60%', height: 16, marginBottom: 20 }} />
        <div className="skeleton" style={{ width: '100%', height: 180 }} />
      </div>
    );
  }

  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 20 }}>Attendance Overview</h3>
      <div style={{ display: 'flex', alignItems: 'center', gap: 20 }}>
        <div
          style={{
            width: 120,
            height: 120,
            borderRadius: '50%',
            background: `conic-gradient(var(--emerald-500) ${attendancePercentage * 3.6}deg, var(--border-color) 0deg)`,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            flexShrink: 0,
          }}
        >
          <div
            style={{
              width: 90,
              height: 90,
              borderRadius: '50%',
              background: 'var(--white)',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
            }}
          >
            <span style={{ fontSize: 22, fontWeight: 700, color: 'var(--emerald-500)' }}>{attendancePercentage}%</span>
          </div>
        </div>
        <div style={{ flex: 1 }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 12 }}>
            <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Present</span>
            <span style={{ fontSize: 13, fontWeight: 600 }}>{attendancePercentage}%</span>
          </div>
          <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 12 }}>
            <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>Absent</span>
            <span style={{ fontSize: 13, fontWeight: 600 }}>{100 - attendancePercentage}%</span>
          </div>
          <div style={{ height: 8, background: 'var(--border-color)', borderRadius: 4, overflow: 'hidden' }}>
            <div
              style={{
                height: '100%',
                width: `${attendancePercentage}%`,
                background: 'var(--emerald-500)',
                borderRadius: 4,
                transition: 'width 0.5s ease',
              }}
            />
          </div>
        </div>
      </div>
    </div>
  );
}
