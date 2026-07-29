import { AppInput } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { useCertificateForm } from './form.hook';

export default function ApplyForCertificateForm() {
  const { formData, updateField, submit, isSubmitting } = useCertificateForm();

  return (
    <div className="card" style={{ padding: 24, maxWidth: 640 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 20 }}>Apply for Certificate</h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
        <div className="form-group">
          <label className="form-label">Applicant ID *</label>
          <AppInput
            type="number"
            value={formData.applicantId ? formData.applicantId.toString() : ''}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateField('applicantId', e.target.value ? Number(e.target.value) : 0)}
            placeholder="Enter applicant ID"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Program Name *</label>
          <AppInput
            value={formData.programName}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateField('programName', e.target.value)}
            placeholder="e.g. CM Fellow Fellowship"
            style={{ width: '100%' }}
          />
        </div>
        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 16 }}>
          <div className="form-group">
            <label className="form-label">Start Date *</label>
            <AppInput
              type="date"
              value={formData.startDate}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateField('startDate', e.target.value)}
              style={{ width: '100%' }}
            />
          </div>
          <div className="form-group">
            <label className="form-label">End Date *</label>
            <AppInput
              type="date"
              value={formData.endDate}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateField('endDate', e.target.value)}
              style={{ width: '100%' }}
            />
          </div>
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
          <AppButton
            icon="pi pi-send"
            onClick={submit}
            disabled={!formData.programName || !formData.applicantId || isSubmitting}
            loading={isSubmitting}
          >
            Submit Application
          </AppButton>
        </div>
      </div>
    </div>
  );
}
