import './layouts/layout.css';
import './index.css';

import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import { ThemeProvider } from './theme/ThemeContext';
import { AuthProvider, ProtectedRoute, ModuleProtectedRoute } from './features/auth';
import AppLayout from './layouts/AppLayout';
import LoginPage from './features/registration/pages/LoginPage';
import { AdminDashboardPage } from './features/dashboard';
import RegistrationWizard from './features/registration/pages/RegistrationWizard';
import UserManagementPage from './features/registration/pages/UserManagementPage';
import { ActivityCalendar, CreateActivityForm } from './features/training';
import WorkAllocationPage from './features/work-allocation/pages/WorkAllocationPage';
import MarkAttendancePage from './features/attendance-leave/pages/MarkAttendancePage';
import ApplyLeavePage from './features/attendance-leave/pages/ApplyLeavePage';
import LeaveApprovalPage from './features/attendance-leave/pages/LeaveApprovalPage';
import LeaveStatusPage from './features/attendance-leave/pages/LeaveStatusPage';
import LeaveBalancePage from './features/attendance-leave/pages/LeaveBalancePage';
import HolidayCalendarPage from './features/attendance-leave/pages/HolidayCalendarPage';
import PayrollSummaryPage from './features/attendance-leave/pages/PayrollSummaryPage';
import { PerformanceReviewGrid, PerformanceDetailPage } from './features/performance';
import { ApplyCertificatePage, CertificateApprovalPage, ExitManagementPage } from './features/certificate';
import { RaiseTicketPage, TicketQueuePage, TicketDetailPage } from './features/help-desk';
import { LocationsPage, ProjectsPage, WorksPage, TrainingSchedulePage } from './features/masters';
import DocumentVerificationPage from './features/registration/pages/DocumentVerificationPage';
import ForgotPasswordPage from './features/registration/pages/ForgotPasswordPage';
import ResetPasswordPage from './features/registration/pages/ResetPasswordPage';
import { SeedDataPage } from './features/seed';
import { UserAccessPage, AuditLogPage } from './features/admin';

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
              <Route path="/forgot-password" element={<ForgotPasswordPage />} />
              <Route path="/reset-password" element={<ResetPasswordPage />} />
              <Route path="/" element={<ProtectedRoute><AppLayout /></ProtectedRoute>}>
                <Route index element={<Navigate to="/dashboard" replace />} />
                <Route path="dashboard" element={<ModuleProtectedRoute moduleCode="DASHBOARD"><AdminDashboardPage /></ModuleProtectedRoute>} />
                <Route path="registration" element={<ModuleProtectedRoute moduleCode="REGISTRATION" permission="Write"><RegistrationWizard /></ModuleProtectedRoute>} />
                <Route path="admin/users" element={<ModuleProtectedRoute moduleCode="REGISTRATION" permission="Write"><UserManagementPage /></ModuleProtectedRoute>} />
                <Route path="admin/documents" element={<ModuleProtectedRoute moduleCode="REGISTRATION" permission="Approve"><DocumentVerificationPage /></ModuleProtectedRoute>} />
                <Route path="admin/seed" element={<ModuleProtectedRoute moduleCode="ADMINISTRATION" permission="Write"><SeedDataPage /></ModuleProtectedRoute>} />
                <Route path="admin/access" element={<ModuleProtectedRoute moduleCode="ADMINISTRATION" permission="Write"><UserAccessPage /></ModuleProtectedRoute>} />
                <Route path="admin/access/audit" element={<ModuleProtectedRoute moduleCode="ADMINISTRATION"><AuditLogPage /></ModuleProtectedRoute>} />
                <Route path="training" element={<ModuleProtectedRoute moduleCode="TRAINING"><ActivityCalendar /></ModuleProtectedRoute>} />
                <Route path="training/new" element={<ModuleProtectedRoute moduleCode="TRAINING" permission="Write"><CreateActivityForm /></ModuleProtectedRoute>} />
                <Route path="work-allocation" element={<ModuleProtectedRoute moduleCode="WORK_ALLOCATION"><WorkAllocationPage /></ModuleProtectedRoute>} />
                <Route path="attendance" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><MarkAttendancePage /></ModuleProtectedRoute>} />
                <Route path="attendance/apply-leave" element={<ModuleProtectedRoute moduleCode="ATTENDANCE" permission="Write"><ApplyLeavePage /></ModuleProtectedRoute>} />
                <Route path="attendance/leave-approval" element={<ModuleProtectedRoute moduleCode="ATTENDANCE" permission="Approve"><LeaveApprovalPage /></ModuleProtectedRoute>} />
                <Route path="attendance/leave-status" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><LeaveStatusPage /></ModuleProtectedRoute>} />
                <Route path="attendance/leave-balance" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><LeaveBalancePage /></ModuleProtectedRoute>} />
                <Route path="attendance/holidays" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><HolidayCalendarPage /></ModuleProtectedRoute>} />
                <Route path="attendance/payroll-summary" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><PayrollSummaryPage /></ModuleProtectedRoute>} />
                <Route path="performance" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><PerformanceReviewGrid /></ModuleProtectedRoute>} />
                <Route path="performance/:id" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><PerformanceDetailPage /></ModuleProtectedRoute>} />
                <Route path="certificate/apply" element={<ModuleProtectedRoute moduleCode="CERTIFICATE" permission="Write"><ApplyCertificatePage /></ModuleProtectedRoute>} />
                <Route path="certificate/approvals" element={<ModuleProtectedRoute moduleCode="CERTIFICATE" permission="Approve"><CertificateApprovalPage /></ModuleProtectedRoute>} />
                <Route path="certificate/exit" element={<ModuleProtectedRoute moduleCode="CERTIFICATE" permission="Approve"><ExitManagementPage /></ModuleProtectedRoute>} />
                <Route path="certificate" element={<ModuleProtectedRoute moduleCode="CERTIFICATE" permission="Approve"><CertificateApprovalPage /></ModuleProtectedRoute>} />
                <Route path="help-desk" element={<ModuleProtectedRoute moduleCode="HELP_DESK"><TicketQueuePage /></ModuleProtectedRoute>} />
                <Route path="help-desk/new" element={<ModuleProtectedRoute moduleCode="HELP_DESK" permission="Write"><RaiseTicketPage /></ModuleProtectedRoute>} />
                <Route path="help-desk/:id" element={<ModuleProtectedRoute moduleCode="HELP_DESK"><TicketDetailPage /></ModuleProtectedRoute>} />
                <Route path="masters/locations" element={<ModuleProtectedRoute moduleCode="MASTERS"><LocationsPage /></ModuleProtectedRoute>} />
                <Route path="masters/projects" element={<ModuleProtectedRoute moduleCode="MASTERS"><ProjectsPage /></ModuleProtectedRoute>} />
                <Route path="masters/works" element={<ModuleProtectedRoute moduleCode="MASTERS"><WorksPage /></ModuleProtectedRoute>} />
                <Route path="masters/training-schedules" element={<ModuleProtectedRoute moduleCode="MASTERS"><TrainingSchedulePage /></ModuleProtectedRoute>} />
                <Route path="unauthorized" element={<div style={{ padding: 40, textAlign: 'center' }}><h2>Unauthorized</h2><p>You don't have access to this page.</p></div>} />
              </Route>
            </Routes>
          </AuthProvider>
        </BrowserRouter>
        <ReactQueryDevtools initialIsOpen={false} />
      </QueryClientProvider>
    </ThemeProvider>
  );
}
