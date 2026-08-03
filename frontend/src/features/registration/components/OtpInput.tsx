import { AppInput } from '../../../shared/components/forms';

interface OtpInputProps {
  otp: string;
  setOtp: (value: string) => void;
  onVerify: () => void;
  isPending: boolean;
}

export default function OtpInput({ otp, setOtp, onVerify, isPending }: OtpInputProps) {
  return (
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
          onClick={onVerify}
          disabled={otp.length !== 6 || isPending}
        >
          {isPending ? 'Verifying...' : 'Verify'}
        </button>
      </div>
    </div>
  );
}
