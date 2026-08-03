import { useParams } from 'react-router-dom';
import { usePerformanceDetail, useReviewHistory } from '../queries';
import ReviewChainProgress from '../components/ReviewChainProgress';
import ReviewHistoryList from '../components/ReviewHistoryList';
import { usePerformanceForm } from '../components/form.hook';
import { PageHeader, EmptyState } from '../../../shared/components/ui';
import PerformanceScoreCard from './PerformanceScoreCard';
import PerformanceActions from './PerformanceActions';

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

      <PerformanceScoreCard
        performanceScore={record.performanceScore}
        performanceGrade={record.performanceGrade}
        completionPercentage={record.completionPercentage}
        surveysCompleted={record.surveysCompleted}
        totalSurveysAssigned={record.totalSurveysAssigned}
      />

      <ReviewChainProgress
        reviewLevel={record.reviewLevel ?? 'Draft'}
        reviewStatus={record.reviewStatus ?? 'Draft'}
      />

      <PerformanceActions
        reviewLevel={record.reviewLevel ?? 'Draft'}
        reviewStatus={record.reviewStatus ?? 'Draft'}
        evaluationId={evaluationId}
        supervisorRating={supervisorRating}
        setSupervisorRating={setSupervisorRating}
        evaluationRemarks={evaluationRemarks}
        setEvaluationRemarks={setEvaluationRemarks}
        submitRating={submitRating}
        submitRemarks={submitRemarks}
        isRatingSubmitting={isRatingSubmitting}
        isRemarksSubmitting={isRemarksSubmitting}
        initialRemarks={record.evaluationRemarks}
      />

      <ReviewHistoryList history={history} />
    </div>
  );
}
