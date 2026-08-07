import { PERMISSION_FIELDS, PERMISSION_LABELS, PERMISSION_DESCRIPTIONS } from '../hooks/useModuleAccess';

interface ModuleAccessHeaderProps {
  sticky?: boolean;
}

export default function ModuleAccessHeader({ sticky = true }: ModuleAccessHeaderProps) {
  return (
    <div
      style={{
        display: 'grid',
        gridTemplateColumns: '1fr 72px 72px 76px 72px',
        alignItems: 'center',
        padding: '12px 20px 12px 32px',
        borderBottom: '2px solid var(--border-color, #E2E8F0)',
        background: 'var(--surface-card, #FFFFFF)',
        ...(sticky ? { position: 'sticky' as const, top: 0, zIndex: 1 } : {}),
      }}
    >
      <span style={labelStyle}>Module</span>
      {PERMISSION_FIELDS.map((f) => (
        <span key={f} style={{ ...labelStyle, textAlign: 'center' }} title={PERMISSION_DESCRIPTIONS[f]}>
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
