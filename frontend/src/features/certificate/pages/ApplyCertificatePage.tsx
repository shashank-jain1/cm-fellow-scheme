import ApplyForCertificateForm from '../components/ApplyForCertificateForm';

export default function ApplyCertificatePage() {
  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Apply for Certificate</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Submit your application for a completion certificate
          </p>
        </div>
      </div>
      <ApplyForCertificateForm />
    </div>
  );
}
