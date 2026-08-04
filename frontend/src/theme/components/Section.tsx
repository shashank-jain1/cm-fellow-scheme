import type { ReactNode } from 'react';

interface SectionProps {
  title: string;
  children: ReactNode;
}

export default function Section({ title, children }: SectionProps) {
  return (
    <div>
      <label
        style={{
          display: 'block',
          fontSize: '11px',
          fontWeight: 600,
          textTransform: 'uppercase',
          letterSpacing: '0.06em',
          color: 'var(--text-muted)',
          marginBottom: '10px',
          fontFamily: 'var(--font-body)',
        }}
      >
        {title}
      </label>
      {children}
    </div>
  );
}
