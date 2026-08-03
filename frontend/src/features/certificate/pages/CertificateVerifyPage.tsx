import { useState } from 'react';
import { AppInput } from '../../../shared/components/forms';
import { AppButton, PageHeader } from '../../../shared/components/ui';
import { useVerifyCertificate } from '../queries';
import type { CertificateVerifyResult } from '../types';

export default function CertificateVerifyPage() {
  const [certNumber, setCertNumber] = useState('');
  const verifyMutation = useVerifyCertificate();
  const [result, setResult] = useState<CertificateVerifyResult | null>(null);

  const handleVerify = async () => {
    if (!certNumber.trim()) return;
    try {
      const res = await verifyMutation.mutateAsync(certNumber.trim());
      setResult(res);
    } catch {
      setResult(null);
    }
  };

  return (
    <div>
      <PageHeader title="Verify Certificate" subtitle="Enter a certificate number to verify its authenticity" />
      <div className="card" style={{ padding: 24, maxWidth: 500, marginBottom: 24 }}>
        <label className="form-label">Certificate Number</label>
        <div style={{ display: 'flex', gap: 12 }}>
          <AppInput
            value={certNumber}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setCertNumber(e.target.value)}
            placeholder="e.g. CERT-2026-00123"
            style={{ flex: 1 }}
          />
          <AppButton
            variant="primary"
            icon="pi pi-search"
            onClick={handleVerify}
            loading={verifyMutation.isPending}
            disabled={!certNumber.trim() || verifyMutation.isPending}
          >
            Verify
          </AppButton>
        </div>
      </div>

      {verifyMutation.isError && (
        <div className="card" style={{ padding: 24, maxWidth: 500, border: '1px solid var(--badge-red-border)' }}>
          <p style={{ color: 'var(--badge-red-text)', fontWeight: 500 }}>
            Verification failed. Please check the certificate number and try again.
          </p>
        </div>
      )}

      {result && (
        <div
          className="card"
          style={{
            padding: 24,
            maxWidth: 500,
            border: result.isValid ? '1px solid var(--badge-emerald-border)' : '1px solid var(--badge-red-border)',
          }}
        >
          <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 12 }}>
            <i
              className={result.isValid ? 'pi pi-check-circle' : 'pi pi-times-circle'}
              style={{ fontSize: 20, color: result.isValid ? 'var(--badge-emerald-text)' : 'var(--badge-red-text)' }}
            />
            <span style={{ fontWeight: 600, fontSize: 16 }}>
              {result.isValid ? 'Certificate Valid' : 'Certificate Invalid'}
            </span>
          </div>
          {result.isValid && (
            <div style={{ fontSize: 14, lineHeight: 1.8 }}>
              <p><strong>Applicant:</strong> {result.applicantName}</p>
              <p><strong>Program:</strong> {result.programName}</p>
              <p><strong>Issued:</strong> {result.issuedOn}</p>
            </div>
          )}
          <p style={{ fontSize: 13, color: 'var(--text-muted)', marginTop: 8 }}>{result.message}</p>
        </div>
      )}
    </div>
  );
}
