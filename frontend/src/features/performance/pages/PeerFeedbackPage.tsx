import { useParams } from 'react-router-dom';
import PeerFeedbackForm from '../components/PeerFeedbackForm';

export default function PeerFeedbackPage() {
  const { id } = useParams<{ id: string }>();
  const evaluationId = Number(id) || 0;

  return (
    <div style={{ padding: 24 }}>
      <PeerFeedbackForm performanceEvaluationId={evaluationId} />
    </div>
  );
}
