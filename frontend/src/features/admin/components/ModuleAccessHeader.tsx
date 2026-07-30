import { PERMISSION_FIELDS, PERMISSION_LABELS } from '../hooks/useModuleAccess';

interface ModuleAccessHeaderProps {
  sticky?: boolean;
}

export default function ModuleAccessHeader({ sticky = true }: ModuleAccessHeaderProps) {
  return (
    <div style={{
      display: 'grid',
      gridTemplateColumns: '1fr 52px 52px 52px 52px',
      alignItems: 'center',
      padding: '10px 24px',
      borderBottom: '2px solid var(--border)',
      background: 'var(--surface-card)',
      ...(sticky ? { position: 'sticky' as const, top: 0, zIndex: 1 } : {}),
    }}>
      <span style={labelStyle}>Module</span>
      {PERMISSION_FIELDS.map((f) => (
        <span key={f} style={{ ...labelStyle, textAlign: 'center' }}>
          {PERMISSION_LABELS[f]}
        </span>
      ))}
    </div>
  );
}

const labelStyle: React.CSSProperties = {
  fontSize: 11,
  fontWeight: 700,
  textTransform: 'uppercase',
  letterSpacing: '0.06em',
  color: 'var(--text-muted)',
};
