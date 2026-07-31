import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { AppButton, PageHeader } from '../../../shared/components/ui';
import { useTrainingSessions, useTrainingCompletions, useCreateTrainingCompletion } from '../queries';
import TrainingCompletionForm from '../components/TrainingCompletionForm';
import TrainingCompletionTable from '../components/TrainingCompletionTable';

export default function TrainingCompletionPage() {
  const toast = useRef<Toast>(null);
  const { data: sessions = [], isLoading: sessionsLoading } = useTrainingSessions();
  const { data: completions = [], isLoading: completionsLoading } = useTrainingCompletions();
  const createCompletion = useCreateTrainingCompletion();

  const [selectedSessionId, setSelectedSessionId] = useState<number | null>(null);
  const [rating, setRating] = useState<number>(3);
  const [comments, setComments] = useState('');
  const [showForm, setShowForm] = useState(false);

  const isLoading = sessionsLoading || completionsLoading;

  const handleSubmitCompletion = async () => {
    if (!selectedSessionId) {
      toast.current?.show({ severity: 'warn', summary: 'Validation', detail: 'Select a training session' });
      return;
    }
    if (rating < 1 || rating > 5) {
      toast.current?.show({ severity: 'warn', summary: 'Validation', detail: 'Rating must be between 1 and 5' });
      return;
    }
    try {
      await createCompletion.mutateAsync({ trainingScheduleId: selectedSessionId, fellowId: 0, rating, comments });
      toast.current?.show({ severity: 'success', summary: 'Success', detail: 'Training completion recorded' });
      setShowForm(false);
      setSelectedSessionId(null);
      setRating(3);
      setComments('');
    } catch {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: 'Failed to record completion' });
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader
        title="Training Completions"
        subtitle="Track and record training completion status per fellow"
        action={<AppButton icon="pi pi-plus" onClick={() => setShowForm(!showForm)}>Mark Completion</AppButton>}
      />
      {showForm && (
        <TrainingCompletionForm
          toast={toast}
          sessions={sessions}
          selectedSessionId={selectedSessionId}
          setSelectedSessionId={setSelectedSessionId}
          rating={rating}
          setRating={setRating}
          comments={comments}
          setComments={setComments}
          onSubmit={handleSubmitCompletion}
          onCancel={() => setShowForm(false)}
          isPending={createCompletion.isPending}
        />
      )}
      {isLoading ? (
        <div style={{ padding: 20 }}>
          {[1, 2, 3, 4, 5].map((n) => (
            <div key={n} style={{ display: 'flex', gap: 16, padding: '12px 0', borderBottom: '1px solid var(--border)' }}>
              <div className="skeleton" style={{ width: '25%', height: 14 }} />
              <div className="skeleton" style={{ width: '20%', height: 14 }} />
              <div className="skeleton" style={{ width: '15%', height: 14 }} />
              <div className="skeleton" style={{ width: '10%', height: 14 }} />
              <div className="skeleton" style={{ width: '15%', height: 14 }} />
            </div>
          ))}
        </div>
      ) : (
        <TrainingCompletionTable
          sessions={sessions}
          completions={completions}
          onMark={(id) => { setSelectedSessionId(id); setShowForm(true); }}
        />
      )}
    </div>
  );
}
