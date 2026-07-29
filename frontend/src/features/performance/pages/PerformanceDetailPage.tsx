import { useParams } from 'react-router-dom';
import { usePerformanceDetail, useReviewHistory } from '../queries';
import ReviewChainProgress from '../components/ReviewChainProgress';
import ReviewActionsForm from '../components/ReviewActionsForm';
import ReviewHistoryList from '../components/ReviewHistoryList';
import SupervisorRatingInput from '../components/SupervisorRatingInput';
import EvaluationRemarksForm from '../components/EvaluationRemarksForm';
import { usePerformanceForm } from '../components/form.hook';
import { PageHeader, EmptyState } from '../../../shared/components/ui';

export default function PerformanceDetailPage() {
  const { id } = useParams<{ id: string }>();
  const evaluationId = Number(id);
  const { data: record, isLoading } = usePerformanceDetail(evaluationId);
  const { data: history = [] } = useReviewHistory(evaluationId);

  const {
    supervisorRating,
    setSupervisorRating,
    evaluationRemarks,
    setEvaluationRemarks,
    submitRating,
    submitRemarks,
    isRatingSubmitting,
    isRemarksSubmitting,
  } = usePerformanceForm(evaluationId);

  if (isLoading) {
    return (
      <div style={{ padding: 24 }}>
        <div className="skeleton" style={{ width: 200, height: 24, marginBottom: 16 }} />
        <div className="skeleton" style={{ width: '100%', height: 200 }} />
      </div>
    );
  }

  if (!record) {
    return <EmptyState icon="pi pi-exclamation-circle" title="Evaluation not found" />;
  }

  return (
    <div>
      <PageHeader
        title="Performance Detail"
        subtitle={`${record.applicantName} — ${record.projectName}`}
      />

      <div className="metric-bar" style={{ marginBottom: 'var(--space-4)' }}>
        <div className="metric-item">
          <span className="metric-label">Score</span>
          <span className="metric-value">{record.performanceScore}</span>
        </div>
        <div className="metric-item">
          <span className="metric-label">Grade</span>
          <span className="metric-value">{record.performanceGrade}</span>
        </div>
        <div className="metric-item">
          <span className="metric-label">Completion</span>
          <span className="metric-value">{record.completionPercentage}%</span>
        </div>
        <div className="metric-item">
          <span className="metric-label">Surveys</span>
          <span className="metric-value">{record.surveysCompleted}/{record.totalSurveysAssigned}</span>
        </div>
      </div>

      <ReviewChainProgress
        reviewLevel={record.reviewLevel ?? 'Draft'}
        reviewStatus={record.reviewStatus ?? 'Draft'}
      />

      <div className="card" style={{ padding: 'var(--space-5)', marginBottom: 'var(--space-4)' }}>
        <ReviewActionsForm
          reviewLevel={record.reviewLevel ?? 'Draft'}
          reviewStatus={record.reviewStatus ?? 'Draft'}
          evaluationId={evaluationId}
        />
      </div>

      <ReviewHistoryList history={history} />

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 'var(--space-4)' }}>
        <SupervisorRatingInput
          value={supervisorRating}
          onChange={setSupervisorRating}
          onSubmit={submitRating}
          isSubmitting={isRatingSubmitting}
        />
        <EvaluationRemarksForm
          value={evaluationRemarks}
          onChange={setEvaluationRemarks}
          onSubmit={submitRemarks}
          isSubmitting={isRemarksSubmitting}
          initialRemarks={record.evaluationRemarks}
        />
      </div>
    </div>
  );
}
