import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { useCertificateForm } from './form.hook';

export default function ApplyForCertificateForm() {
  const { formData, updateField, submit, isSubmitting } = useCertificateForm();

  return (
    <div className="card" style={{ padding: 24, maxWidth: 640 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 20 }}>Apply for Certificate</h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
        <div className="form-group">
          <label className="form-label">Program Name</label>
          <InputText
            value={formData.programName}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateField('programName', e.target.value)}
            placeholder="e.g. CM Fellow Fellowship"
            style={{ width: '100%' }}
          />
        </div>
        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 16 }}>
          <div className="form-group">
            <label className="form-label">Start Date</label>
            <InputText
              type="date"
              value={formData.startDate}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateField('startDate', e.target.value)}
              style={{ width: '100%' }}
            />
          </div>
          <div className="form-group">
            <label className="form-label">End Date</label>
            <InputText
              type="date"
              value={formData.endDate}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateField('endDate', e.target.value)}
              style={{ width: '100%' }}
            />
          </div>
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
          <Button
            label="Submit Application"
            className="btn btn-primary"
            onClick={submit}
            disabled={!formData.programName || isSubmitting}
            loading={isSubmitting}
          />
        </div>
      </div>
    </div>
  );
}
