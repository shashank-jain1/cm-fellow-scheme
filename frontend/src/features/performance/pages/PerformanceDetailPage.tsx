import { useParams } from 'react-router-dom';
import { usePerformanceDetail, useReviewHistory } from '../queries';
import ReviewChainProgress from '../components/ReviewChainProgress';
import ReviewActionsForm from '../components/ReviewActionsForm';
import ReviewHistoryList from '../components/ReviewHistoryList';
import SupervisorRatingInput from '../components/SupervisorRatingInput';
import EvaluationRemarksForm from '../components/EvaluationRemarksForm';
import { usePerformanceForm } from '../components/form.hook';
import { PageHeader, EmptyState } from '../../../shared/components/ui';

const STATS = [
  { label: 'Score', key: 'performanceScore' as const, icon: 'pi pi-chart-line', color: 'var(--emerald-500)' },
  { label: 'Grade', key: 'performanceGrade' as const, icon: 'pi pi-star', color: 'var(--amber-500)' },
  { label: 'Completion', key: 'completionPercentage' as const, icon: 'pi pi-percentage', color: 'var(--emerald-600)' },
];

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

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: 16, marginBottom: 24 }}>
        {STATS.map((stat, i) => (
          <div key={i} className="kpi-card" style={{ textAlign: 'center', padding: 20 }}>
            <i className={stat.icon} style={{ fontSize: 22, color: stat.color, marginBottom: 10 }} />
            <div style={{ fontSize: 24, fontWeight: 700, color: 'var(--text-primary)' }}>
              {stat.key === 'completionPercentage' ? `${record[stat.key]}%` : record[stat.key]}
            </div>
            <div style={{ fontSize: 13, color: 'var(--text-secondary)', marginTop: 4 }}>{stat.label}</div>
          </div>
        ))}
        <div className="kpi-card" style={{ textAlign: 'center', padding: 20 }}>
          <i className="pi pi-check-square" style={{ fontSize: 22, color: 'var(--navy-600)', marginBottom: 10 }} />
          <div style={{ fontSize: 24, fontWeight: 700, color: 'var(--text-primary)' }}>
            {record.surveysCompleted}/{record.totalSurveysAssigned}
          </div>
          <div style={{ fontSize: 13, color: 'var(--text-secondary)', marginTop: 4 }}>Surveys Done</div>
        </div>
      </div>

      <ReviewChainProgress
        reviewLevel={record.reviewLevel ?? 'Draft'}
        reviewStatus={record.reviewStatus ?? 'Draft'}
      />

      <div className="card" style={{ padding: 24, marginBottom: 20 }}>
        <ReviewActionsForm
          reviewLevel={record.reviewLevel ?? 'Draft'}
          reviewStatus={record.reviewStatus ?? 'Draft'}
          evaluationId={evaluationId}
        />
      </div>

      <ReviewHistoryList history={history} />

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20 }}>
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
