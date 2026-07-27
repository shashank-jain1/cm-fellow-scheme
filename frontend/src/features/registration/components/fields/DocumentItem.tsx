import { Button } from 'primereact/button';

interface DocumentItemProps {
  name: string;
  description: string;
}

export default function DocumentItem({ name, description }: DocumentItemProps) {
  return (
    <div
      style={{
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between',
        padding: '16px 20px',
        border: '1px dashed var(--border-color)',
        borderRadius: 'var(--radius-md)',
        background: 'var(--navy-50)',
      }}
    >
      <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
        <i className="pi pi-file" style={{ color: 'var(--emerald-500)', fontSize: 20 }} />
        <div>
          <div style={{ fontWeight: 500, fontSize: 14 }}>{name}</div>
          <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>{description}</div>
        </div>
      </div>
      <Button
        label="Upload"
        icon="pi pi-upload"
        className="btn btn-secondary btn-sm"
        size="small"
      />
    </div>
  );
}
