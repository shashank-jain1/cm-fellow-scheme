import { useState } from 'react';
import { AppInput } from '../../../shared/components/forms';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { useVerifyMobileOtpMutation } from '../queries';
import type { StepProps } from '../components/form.hook';

export default function OtpVerificationStep({ formData, update }: StepProps) {
  const [otp, setOtp] = useState('');
  const [verified, setVerified] = useState(false);
  const [otpSent, setOtpSent] = useState(false);
  const toast = useRef<Toast>(null);
  const verifyMutation = useVerifyMobileOtpMutation();

  const handleSendOtp = () => {
    if (!formData.mobileNumber || formData.mobileNumber.length !== 10) {
      toast.current?.show({ severity: 'warn', summary: 'Invalid', detail: 'Enter a valid 10-digit mobile number first' });
      return;
    }
    setOtpSent(true);
    toast.current?.show({ severity: 'info', summary: 'OTP Sent', detail: `OTP sent to ${formData.mobileNumber}` });
  };

  const handleVerify = async () => {
    if (otp.length !== 6) {
      toast.current?.show({ severity: 'warn', summary: 'Invalid', detail: 'OTP must be 6 digits' });
      return;
    }

    try {
      await verifyMutation.mutateAsync({
        applicantId: 0,
        data: { mobileNumber: formData.mobileNumber, otpCode: otp },
      });
      setVerified(true);
      update('mobileNumber' as any, formData.mobileNumber);
      toast.current?.show({ severity: 'success', summary: 'Verified', detail: 'Mobile number verified successfully' });
    } catch {
      toast.current?.show({ severity: 'error', summary: 'Failed', detail: 'Invalid OTP. Please try again.' });
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <h2 style={{ marginTop: 0, marginBottom: 8, fontSize: 18, fontWeight: 700 }}>Mobile Verification</h2>
      <p style={{ color: 'var(--text-secondary)', marginBottom: 24, fontSize: 14 }}>
        Verify your mobile number to proceed with registration
      </p>

      <div className="form-grid">
        <div className="form-field">
          <label>Mobile Number *</label>
          <AppInput
            value={formData.mobileNumber}
            onChange={(e) => {
              update('mobileNumber' as any, e.target.value);
              setVerified(false);
              setOtpSent(false);
              setOtp('');
            }}
            placeholder="10-digit mobile number"
            maxLength={10}
            disabled={verified}
            style={{ width: '100%' }}
          />
        </div>

        <div className="form-field" style={{ display: 'flex', alignItems: 'flex-end' }}>
          {!verified && (
            <button
              type="button"
              className="btn btn-primary"
              onClick={handleSendOtp}
              disabled={!formData.mobileNumber || formData.mobileNumber.length !== 10 || verifyMutation.isPending}
            >
              {otpSent ? 'Resend OTP' : 'Send OTP'}
            </button>
          )}
          {verified && (
            <span style={{ display: 'flex', alignItems: 'center', gap: 8, color: '#059669', fontWeight: 600 }}>
              <i className="pi pi-check-circle" /> Verified
            </span>
          )}
        </div>

        {otpSent && !verified && (
          <div className="form-field full-width">
            <label>Enter OTP *</label>
            <div style={{ display: 'flex', gap: 12, alignItems: 'flex-end' }}>
              <AppInput
                value={otp}
                onChange={(e) => setOtp(e.target.value.replace(/\D/g, '').slice(0, 6))}
                placeholder="6-digit OTP"
                maxLength={6}
                style={{ width: 200 }}
              />
              <button
                type="button"
                className="btn btn-primary"
                onClick={handleVerify}
                disabled={otp.length !== 6 || verifyMutation.isPending}
              >
                {verifyMutation.isPending ? 'Verifying...' : 'Verify'}
              </button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
