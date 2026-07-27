import DocumentItem from './fields/DocumentItem';

const documents = [
  { name: 'Photograph', description: 'JPG or PNG (Max 2MB)' },
  { name: 'ID Proof (Aadhaar/PAN)', description: 'PDF, JPG or PNG (Max 5MB)' },
  { name: 'Educational Certificates', description: 'PDF, JPG or PNG (Max 5MB)' },
  { name: 'Experience Letter', description: 'PDF (Max 5MB)' },
];

export default function DocumentUploadStep() {
  return (
    <div className="fade-in">
      <h3 style={{ marginBottom: 24, fontSize: 18 }}>Upload Documents</h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
        {documents.map((doc) => (
          <DocumentItem key={doc.name} name={doc.name} description={doc.description} />
        ))}
      </div>
    </div>
  );
}
