interface LoadingSkeletonProps {
  rows?: number;
}

export default function LoadingSkeleton({ rows = 5 }: LoadingSkeletonProps) {
  return (
    <div style={{ padding: 20 }}>
      {Array.from({ length: rows }, (_, n) => (
        <div
          key={n}
          style={{
            display: 'flex',
            gap: 16,
            padding: '14px 0',
            borderBottom: '1px solid var(--border-light)',
          }}
        >
          <div className="skeleton" style={{ width: '15%', height: 14 }} />
          <div className="skeleton" style={{ width: '20%', height: 14 }} />
          <div className="skeleton" style={{ width: '15%', height: 14 }} />
          <div className="skeleton" style={{ width: '12%', height: 14 }} />
        </div>
      ))}
    </div>
  );
}
