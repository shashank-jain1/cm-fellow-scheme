import { useState } from 'react';
import { AppInput } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { ToastService } from '../../../shared/utils/toast';
import { useSubmitExitReadiness, useVerifyCompliance } from '../queries';
import { useAuth } from '../../auth';
import ExitReadinessChecklist from '../components/ExitReadinessChecklist';

export default function ExitManagementPage() {
  const [applicantId, setApplicantId] = useState<number | null>(null);
  const [exitRecordId, setExitRecordId] = useState<number | null>(null);
  const submitExit = useSubmitExitReadiness();
  const verifyCompliance = useVerifyCompliance();
  const { user } = useAuth();

  const handleSubmit = async (completionStatus: string, verificationFlags: string) => {
    if (!applicantId) return;
    try {
      await submitExit.mutateAsync({
        applicantId,
        completionStatus,
        verificationFlags,
        createdBy: user?.username ?? 'admin',
      });
      ToastService.success('Exit readiness submitted successfully!');
    } catch {
      ToastService.error('Failed to submit exit readiness');
    }
  };

  const handleVerifyCompliance = async () => {
    if (!exitRecordId) return;
    try {
      await verifyCompliance.mutateAsync({
        exitRecordId,
        status: 'Compliance Verified',
        verifiedBy: user?.username ?? 'admin',
      });
      ToastService.success('Exit record marked as compliance verified');
    } catch (err) {
      ToastService.error(err instanceof Error ? err.message : 'Failed to verify compliance');
    }
  };

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Exit Management</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Manage fellow exit readiness and clearances
          </p>
        </div>
      </div>

      <div className="card" style={{ padding: 24, marginBottom: 24, maxWidth: 400 }}>
        <label className="form-label">Applicant ID</label>
        <div style={{ display: 'flex', gap: 12 }}>
          <AppInput
            type="number"
            value={applicantId?.toString() ?? ''}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
              const val = e.target.value;
              setApplicantId(val ? Number(val) : null);
            }}
            placeholder="Enter applicant ID"
            style={{ flex: 1 }}
          />
        </div>
      </div>

      {applicantId && (
        <ExitReadinessChecklist
          applicantId={applicantId}
          onSubmit={handleSubmit}
          isSubmitting={submitExit.isPending}
        />
      )}

      <div className="card" style={{ padding: 24, marginTop: 24, maxWidth: 400 }}>
        <label className="form-label">Verify Compliance</label>
        <div style={{ display: 'flex', gap: 12 }}>
          <AppInput
            type="number"
            value={exitRecordId?.toString() ?? ''}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
              const val = e.target.value;
              setExitRecordId(val ? Number(val) : null);
            }}
            placeholder="Exit record ID"
            style={{ flex: 1 }}
          />
          <AppButton
            variant="primary"
            icon="pi pi-check"
            onClick={handleVerifyCompliance}
            loading={verifyCompliance.isPending}
            disabled={!exitRecordId || verifyCompliance.isPending}
          >
            Check
          </AppButton>
        </div>
      </div>
    </div>
  );
}
