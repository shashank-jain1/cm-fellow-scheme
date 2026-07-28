interface SkeletonTableProps {
  columns: number;
  rows?: number;
}

export default function SkeletonTable({ columns, rows = 5 }: SkeletonTableProps) {
  return (
    <div style={{ padding: '0 16px' }}>
      {Array.from({ length: rows }).map((_, rowIdx) => (
        <div
          key={rowIdx}
          style={{
            display: 'flex',
            gap: 24,
            padding: '16px 0',
            borderBottom: rowIdx < rows - 1 ? '1px solid var(--border-light)' : 'none',
          }}
        >
          {Array.from({ length: columns }).map((_, colIdx) => (
            <div
              key={colIdx}
              className="skeleton"
              style={{
                flex: colIdx === 0 ? 2 : 1,
                height: 16,
              }}
            />
          ))}
        </div>
      ))}
    </div>
  );
}
