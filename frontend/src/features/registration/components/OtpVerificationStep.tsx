import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { AppInput } from '../../../shared/components/forms';
import { useVerifyMobileOtpMutation, useSendOtpMutation } from '../queries';
import type { StepProps } from '../components/form.hook';
import OtpInput from './OtpInput';

export default function OtpVerificationStep({ formData, update }: StepProps) {
  const [otp, setOtp] = useState('');
  const [verified, setVerified] = useState(false);
  const [otpSent, setOtpSent] = useState(false);
  const toast = useRef<Toast>(null);
  const verifyMutation = useVerifyMobileOtpMutation();
  const sendOtpMutation = useSendOtpMutation();

  const handleSendOtp = async () => {
    if (!formData.mobileNumber || formData.mobileNumber.length !== 10) {
      toast.current?.show({ severity: 'warn', summary: 'Invalid', detail: 'Enter a valid 10-digit mobile number first' });
      return;
    }
    try {
      await sendOtpMutation.mutateAsync(formData.mobileNumber);
      setOtpSent(true);
      toast.current?.show({ severity: 'info', summary: 'OTP Sent', detail: `OTP sent to ${formData.mobileNumber}` });
    } catch (err) {
      toast.current?.show({
        severity: 'error',
        summary: 'Could not send OTP',
        detail: err instanceof Error ? err.message : 'Please try again.',
      });
    }
  };

  const handleVerify = async () => {
    if (otp.length !== 6) {
      toast.current?.show({ severity: 'warn', summary: 'Invalid', detail: 'OTP must be 6 digits' });
      return;
    }
    try {
      await verifyMutation.mutateAsync({ mobileNumber: formData.mobileNumber, otpCode: otp });
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
            onChange={(e) => { update('mobileNumber' as any, e.target.value); setVerified(false); setOtpSent(false); setOtp(''); }}
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
              disabled={!formData.mobileNumber || formData.mobileNumber.length !== 10 || sendOtpMutation.isPending}
            >
              {sendOtpMutation.isPending ? 'Sending...' : otpSent ? 'Resend OTP' : 'Send OTP'}
            </button>
          )}
          {verified && (
            <span style={{ display: 'flex', alignItems: 'center', gap: 8, color: '#059669', fontWeight: 600 }}>
              <i className="pi pi-check-circle" /> Verified
            </span>
          )}
        </div>
        {otpSent && !verified && (
          <OtpInput otp={otp} setOtp={setOtp} onVerify={handleVerify} isPending={verifyMutation.isPending} />
        )}
      </div>
    </div>
  );
}
