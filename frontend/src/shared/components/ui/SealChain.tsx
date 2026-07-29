export interface SealChainStep {
  label: string;
  status: 'completed' | 'current' | 'pending' | 'failed';
}

interface SealChainProps {
  steps: SealChainStep[];
}

export default function SealChain({ steps }: SealChainProps) {
  return (
    <div style={{ display: 'flex', alignItems: 'center', gap: 0 }}>
      {steps.map((step, i) => {
        const isLast = i === steps.length - 1;

        let bg = 'var(--carbon-50)';
        let border = 'var(--border)';
        let text = 'var(--text-muted)';
        let dot = 'var(--carbon-300)';

        if (step.status === 'completed') {
          bg = 'var(--success-light)';
          border = 'var(--success)';
          text = 'var(--success)';
          dot = 'var(--success)';
        } else if (step.status === 'current') {
          bg = 'var(--accent-light)';
          border = 'var(--accent)';
          text = 'var(--accent)';
          dot = 'var(--accent)';
        } else if (step.status === 'failed') {
          bg = 'var(--danger-light)';
          border = 'var(--danger)';
          text = 'var(--danger)';
          dot = 'var(--danger)';
        }

        return (
          <div key={i} style={{ display: 'flex', alignItems: 'center' }}>
            <div
              style={{
                display: 'flex',
                alignItems: 'center',
                gap: 'var(--space-2)',
                padding: '5px 12px',
                borderRadius: 'var(--radius-full)',
                background: bg,
                border: `1px solid ${border}`,
              }}
            >
              <span
                style={{
                  width: 7,
                  height: 7,
                  borderRadius: '50%',
                  background: dot,
                  flexShrink: 0,
                }}
              />
              <span
                style={{
                  fontFamily: 'var(--font-body)',
                  fontSize: 'var(--text-sm)',
                  fontWeight: 600,
                  color: text,
                  whiteSpace: 'nowrap',
                  lineHeight: 1,
                }}
              >
                {step.label}
              </span>
            </div>

            {!isLast && (
              <div
                style={{
                  width: 24,
                  height: 1.5,
                  background: step.status === 'completed' ? 'var(--success)' : 'var(--border)',
                  flexShrink: 0,
                }}
              />
            )}
          </div>
        );
      })}
    </div>
  );
}
