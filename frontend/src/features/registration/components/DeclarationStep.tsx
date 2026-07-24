import type { ChangeEvent } from 'react';
import type { StepProps } from './form.hook';

export default function DeclarationStep({ formData, update }: StepProps) {
  return (
    <div className="fade-in">
      <h3 style={{ marginBottom: 24, fontSize: 18 }}>Declaration</h3>
      <div
        style={{
          padding: 24,
          background: 'var(--navy-50)',
          borderRadius: 'var(--radius-md)',
          marginBottom: 24,
          fontSize: 14,
          lineHeight: 1.7,
          color: 'var(--text-secondary)',
        }}
      >
        <p style={{ marginBottom: 12 }}>
          I hereby declare that all the information provided above is true and correct to the best of my
          knowledge. I understand that providing false or misleading information may lead to disqualification
          from the CM Fellow Program.
        </p>
        <p>
          I agree to abide by all the rules and regulations of the program as prescribed by the competent
          authority from time to time.
        </p>
      </div>
      <label className="login-checkbox" style={{ fontSize: 14, cursor: 'pointer' }}>
        <input
          type="checkbox"
          checked={formData.declaration}
          onChange={(e: ChangeEvent<HTMLInputElement>) => update('declaration', e.target.checked)}
        />
        <span>I have read and agree to the above declaration</span>
      </label>
    </div>
  );
}
