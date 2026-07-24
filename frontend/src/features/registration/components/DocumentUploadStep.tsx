import { Button } from 'primereact/button';

const documents = ['Photograph', 'ID Proof (Aadhaar/PAN)', 'Educational Certificates', 'Experience Letter'];

export default function DocumentUploadStep() {
  return (
    <div className="fade-in">
      <h3 style={{ marginBottom: 24, fontSize: 18 }}>Upload Documents</h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
        {documents.map((doc, i) => (
          <div
            key={i}
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
                <div style={{ fontWeight: 500, fontSize: 14 }}>{doc}</div>
                <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>PDF, JPG or PNG (Max 5MB)</div>
              </div>
            </div>
            <Button
              label="Upload"
              icon="pi pi-upload"
              className="btn btn-secondary btn-sm"
              size="small"
            />
          </div>
        ))}
      </div>
    </div>
  );
}
