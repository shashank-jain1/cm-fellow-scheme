import { useParams } from 'react-router-dom';
import { usePerformanceDetail } from '../queries';
import SupervisorRatingInput from '../components/SupervisorRatingInput';
import EvaluationRemarksForm from '../components/EvaluationRemarksForm';
import { usePerformanceForm } from '../components/form.hook';

export default function PerformanceDetailPage() {
  const { id } = useParams<{ id: string }>();
  const evaluationId = Number(id);
  const { data: record, isLoading } = usePerformanceDetail(evaluationId);

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
    return (
      <div className="empty-state">
        <i className="pi pi-exclamation-circle" />
        <h3>Evaluation not found</h3>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Performance Detail</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            {record.applicantName} &mdash; {record.projectName}
          </p>
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: 16, marginBottom: 24 }}>
        {[
          { label: 'Score', value: record.performanceScore, icon: 'pi pi-chart-line', color: 'var(--emerald-500)' },
          { label: 'Grade', value: record.performanceGrade, icon: 'pi pi-star', color: 'var(--amber-500)' },
          { label: 'Surveys Done', value: `${record.surveysCompleted}/${record.totalSurveysAssigned}`, icon: 'pi pi-check-square', color: 'var(--navy-600)' },
          { label: 'Completion', value: `${record.completionPercentage}%`, icon: 'pi pi-percentage', color: 'var(--emerald-600)' },
        ].map((stat, i) => (
          <div key={i} className="kpi-card" style={{ textAlign: 'center', padding: 20 }}>
            <i className={stat.icon} style={{ fontSize: 22, color: stat.color, marginBottom: 10 }} />
            <div style={{ fontSize: 24, fontWeight: 700, color: 'var(--text-primary)' }}>{stat.value}</div>
            <div style={{ fontSize: 13, color: 'var(--text-secondary)', marginTop: 4 }}>{stat.label}</div>
          </div>
        ))}
      </div>

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
