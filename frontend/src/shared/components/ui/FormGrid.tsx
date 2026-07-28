import type { ReactNode } from 'react';

interface FormGridProps {
  children: ReactNode;
  columns?: 1 | 2 | 3;
  className?: string;
  style?: React.CSSProperties;
}

export default function FormGrid({ children, columns = 2, className = '', style }: FormGridProps) {
  const gridStyle: React.CSSProperties = {
    gridTemplateColumns: `repeat(${columns}, 1fr)`,
    ...style,
  };

  return (
    <div className={`form-grid ${className}`} style={gridStyle}>
      {children}
    </div>
  );
}
