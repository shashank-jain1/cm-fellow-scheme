import { useNavigate } from 'react-router-dom';
import ApplyLeaveForm from '../components/ApplyLeaveForm';
import PageHeader from '../../../shared/components/ui/PageHeader';
import Card from '../../../shared/components/ui/Card';

export default function ApplyLeavePage() {
  const navigate = useNavigate();

  return (
    <div>
      <PageHeader
        title="Apply for Leave"
        subtitle="Submit a new leave application"
      />

      <Card style={{ maxWidth: 640, padding: 24 }}>
        <ApplyLeaveForm onSuccess={() => navigate('/leave/status')} />
      </Card>
    </div>
  );
}
