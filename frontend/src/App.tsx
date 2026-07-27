import './layouts/layout.css';
import './index.css';

import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import { ThemeProvider } from './theme/ThemeContext';
import { AuthProvider, ProtectedRoute } from './features/auth';
import AppLayout from './layouts/AppLayout';
import LoginPage from './features/registration/pages/LoginPage';
import { AdminDashboardPage } from './features/dashboard';
import RegistrationWizard from './features/registration/pages/RegistrationWizard';
import { ActivityCalendar, CreateActivityForm } from './features/training';
import WorkAllocationPage from './features/work-allocation/pages/WorkAllocationPage';
import MarkAttendancePage from './features/attendance-leave/pages/MarkAttendancePage';
import ApplyLeavePage from './features/attendance-leave/pages/ApplyLeavePage';
import LeaveApprovalPage from './features/attendance-leave/pages/LeaveApprovalPage';
import LeaveStatusPage from './features/attendance-leave/pages/LeaveStatusPage';
import LeaveBalancePage from './features/attendance-leave/pages/LeaveBalancePage';
import { PerformanceReviewGrid, PerformanceDetailPage } from './features/performance';
import { ApplyCertificatePage, CertificateApprovalPage, ExitManagementPage } from './features/certificate';
import { RaiseTicketPage, TicketQueuePage, TicketDetailPage } from './features/help-desk';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
      staleTime: 5 * 60 * 1000,
    },
  },
});

export default function App() {
  return (
    <ThemeProvider>
      <QueryClientProvider client={queryClient}>
        <BrowserRouter>
          <AuthProvider>
            <Routes>
              <Route path="/login" element={<LoginPage />} />
              <Route path="/" element={<ProtectedRoute><AppLayout /></ProtectedRoute>}>
                <Route index element={<Navigate to="/dashboard" replace />} />
                <Route path="dashboard" element={<AdminDashboardPage />} />
                <Route path="registration" element={<RegistrationWizard />} />
                <Route path="training" element={<ActivityCalendar />} />
                <Route path="training/new" element={<CreateActivityForm />} />
                <Route path="work-allocation" element={<WorkAllocationPage />} />
                <Route path="attendance" element={<MarkAttendancePage />} />
                <Route path="attendance/apply-leave" element={<ApplyLeavePage />} />
                <Route path="attendance/leave-approval" element={<LeaveApprovalPage />} />
                <Route path="attendance/leave-status" element={<LeaveStatusPage />} />
                <Route path="attendance/leave-balance" element={<LeaveBalancePage />} />
                <Route path="performance" element={<PerformanceReviewGrid />} />
                <Route path="performance/:id" element={<PerformanceDetailPage />} />
                <Route path="certificate/apply" element={<ApplyCertificatePage />} />
                <Route path="certificate/approvals" element={<CertificateApprovalPage />} />
                <Route path="certificate/exit" element={<ExitManagementPage />} />
                <Route path="certificate" element={<CertificateApprovalPage />} />
                <Route path="help-desk" element={<TicketQueuePage />} />
                <Route path="help-desk/new" element={<RaiseTicketPage />} />
                <Route path="help-desk/:id" element={<TicketDetailPage />} />
              </Route>
            </Routes>
          </AuthProvider>
        </BrowserRouter>
        <ReactQueryDevtools initialIsOpen={false} />
      </QueryClientProvider>
    </ThemeProvider>
  );
}
