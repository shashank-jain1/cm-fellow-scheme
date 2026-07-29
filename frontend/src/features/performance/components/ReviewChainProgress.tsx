import { SealChain } from '../../../shared/components/ui';
import type { SealChainStep } from '../../../shared/components/ui';

const REVIEW_LEVEL_ORDER = ['Draft', 'Fellow', 'Coordinator', 'Admin'];

interface Props {
  reviewLevel: string;
  reviewStatus: string;
}

export default function ReviewChainProgress({ reviewLevel, reviewStatus }: Props) {
  const currentIdx = REVIEW_LEVEL_ORDER.indexOf(reviewLevel ?? 'Draft');

  const steps: SealChainStep[] = REVIEW_LEVEL_ORDER.map((level, i) => {
    let status: SealChainStep['status'];
    if (i < currentIdx) status = 'completed';
    else if (i === currentIdx) {
      if (reviewStatus === 'Approved') status = 'completed';
      else if (reviewStatus === 'Rejected') status = 'failed';
      else status = 'current';
    } else {
      status = 'pending';
    }
    return { label: level, status };
  });

  return (
    <div className="card" style={{ padding: 'var(--space-5)', marginBottom: 'var(--space-4)' }}>
      <div style={{ fontFamily: 'var(--font-display)', fontSize: 'var(--text-sm)', fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.04em', marginBottom: 'var(--space-3)' }}>
        Review Chain
      </div>
      <SealChain steps={steps} />
      <div style={{ display: 'flex', gap: 'var(--space-3)', marginTop: 'var(--space-3)' }}>
        <div>
          <span style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-xs)', color: 'var(--text-muted)' }}>Level </span>
          <span style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-sm)', fontWeight: 600, color: 'var(--text-heading)' }}>{reviewLevel ?? 'Draft'}</span>
        </div>
        <div>
          <span style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-xs)', color: 'var(--text-muted)' }}>Status </span>
          <span style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-sm)', fontWeight: 600, color: 'var(--text-heading)' }}>{reviewStatus ?? 'Draft'}</span>
        </div>
      </div>
    </div>
  );
}
