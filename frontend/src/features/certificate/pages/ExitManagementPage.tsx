import { useState } from 'react';
import { AppInput } from '../../../shared/components/forms';
import { ToastService } from '../../../shared/utils/toast';
import { useSubmitExitReadiness } from '../queries';
import { useAuth } from '../../auth';
import ExitReadinessChecklist from '../components/ExitReadinessChecklist';

export default function ExitManagementPage() {
  const [applicantId, setApplicantId] = useState<number | null>(null);
  const submitExit = useSubmitExitReadiness();
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
    </div>
  );
}
