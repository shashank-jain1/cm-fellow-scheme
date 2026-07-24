import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import ExitReadinessChecklist from '../components/ExitReadinessChecklist';

export default function ExitManagementPage() {
  const [applicantId, setApplicantId] = useState<number | null>(null);

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
          <InputText
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
          onSubmit={(items) => console.log('Exit checklist completed:', items)}
        />
      )}
    </div>
  );
}
