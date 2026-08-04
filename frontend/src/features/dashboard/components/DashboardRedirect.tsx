import { useAuth } from '../../auth/useAuth';
import AdminDashboardPage from '../pages/AdminDashboardPage';
import FellowDashboardPage from '../pages/FellowDashboardPage';
import CoordinatorFellowDashboardPage from '../pages/CoordinatorFellowDashboardPage';

export default function DashboardRedirect() {
  const { user } = useAuth();
  const role = user?.role?.toLowerCase() || '';

  if (role === 'coordinator') {
    return <CoordinatorFellowDashboardPage />;
  }

  if (role === 'fellow' || role === 'intern') {
    return <FellowDashboardPage />;
  }

  return <AdminDashboardPage />;
}
