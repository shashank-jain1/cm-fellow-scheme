import './layouts/layout.css';
import './index.css';

import { lazy, Suspense } from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import { ThemeProvider } from './theme/ThemeContext';
import { AuthProvider, ProtectedRoute, ModuleProtectedRoute, UnauthorizedPage } from './features/auth';
import AppLayout from './layouts/AppLayout';
import ErrorBoundary from './shared/components/ErrorBoundary';
import LoadingSkeleton from './shared/components/ui/LoadingSkeleton';

// Lazy-loaded Page Components
const LoginPage = lazy(() => import('./features/registration/pages/LoginPage'));
const ForgotPasswordPage = lazy(() => import('./features/registration/pages/ForgotPasswordPage'));
const ResetPasswordPage = lazy(() => import('./features/registration/pages/ResetPasswordPage'));
const RegistrationWizard = lazy(() => import('./features/registration/pages/RegistrationWizard'));
const UserManagementPage = lazy(() => import('./features/registration/pages/UserManagementPage'));
const DocumentVerificationPage = lazy(() => import('./features/registration/pages/DocumentVerificationPage'));
const ProfileEditPage = lazy(() => import('./features/registration/pages/ProfileEditPage'));

const DashboardRedirect = lazy(() => import('./features/dashboard/components/DashboardRedirect'));
const FellowDashboardPage = lazy(() => import('./features/dashboard/pages/FellowDashboardPage'));
const CoordinatorFellowDashboardPage = lazy(() => import('./features/dashboard/pages/CoordinatorFellowDashboardPage'));

const ActivityCalendar = lazy(() => import('./features/training/pages/ActivityCalendar'));
const CreateActivityForm = lazy(() => import('./features/training/pages/CreateActivityForm'));
const TrainingCompletionPage = lazy(() => import('./features/training/pages/TrainingCompletionPage'));
const MeetingListPage = lazy(() => import('./features/training/pages/MeetingListPage'));
const MeetingDetailPage = lazy(() => import('./features/training/pages/MeetingDetailPage'));

const WorkAllocationPage = lazy(() => import('./features/work-allocation/pages/WorkAllocationPage'));
const TaskProgressPage = lazy(() => import('./features/work-allocation/pages/TaskProgressPage'));

const MarkAttendancePage = lazy(() => import('./features/attendance-leave/pages/MarkAttendancePage'));
const AttendanceReportPage = lazy(() => import('./features/attendance-leave/pages/AttendanceReportPage'));
const WeeklyAttendanceReportPage = lazy(() => import('./features/attendance-leave/pages/WeeklyAttendanceReportPage'));
const ApplyLeavePage = lazy(() => import('./features/attendance-leave/pages/ApplyLeavePage'));
const LeaveApprovalPage = lazy(() => import('./features/attendance-leave/pages/LeaveApprovalPage'));
const LeaveStatusPage = lazy(() => import('./features/attendance-leave/pages/LeaveStatusPage'));
const LeaveBalancePage = lazy(() => import('./features/attendance-leave/pages/LeaveBalancePage'));
const HolidayCalendarPage = lazy(() => import('./features/attendance-leave/pages/HolidayCalendarPage'));
const PayrollSummaryPage = lazy(() => import('./features/attendance-leave/pages/PayrollSummaryPage'));

const PerformanceReviewGrid = lazy(() => import('./features/performance/pages/PerformanceReviewGrid'));
const PerformanceGoalsPage = lazy(() => import('./features/performance/pages/PerformanceGoalsPage'));
const ImprovementPlansPage = lazy(() => import('./features/performance/pages/ImprovementPlansPage'));
const SelfAssessmentForm = lazy(() => import('./features/performance/components/SelfAssessmentForm'));
const PeerFeedbackPage = lazy(() => import('./features/performance/pages/PeerFeedbackPage'));
const ReviewCyclePage = lazy(() => import('./features/performance/pages/ReviewCyclePage'));
const PerformanceDetailPage = lazy(() => import('./features/performance/pages/PerformanceDetailPage'));

const ApplyCertificatePage = lazy(() => import('./features/certificate/pages/ApplyCertificatePage'));
const CertificateApprovalPage = lazy(() => import('./features/certificate/pages/CertificateApprovalPage'));
const ExitManagementPage = lazy(() => import('./features/certificate/pages/ExitManagementPage'));
const ExitInterviewForm = lazy(() => import('./features/certificate/components/ExitInterviewForm'));
const CertificateVerifyPage = lazy(() => import('./features/certificate/pages/CertificateVerifyPage'));

const TicketQueuePage = lazy(() => import('./features/help-desk/pages/TicketQueuePage'));
const RaiseTicketPage = lazy(() => import('./features/help-desk/pages/RaiseTicketPage'));
const KnowledgeBasePage = lazy(() => import('./features/help-desk/pages/KnowledgeBasePage'));
const TicketDetailPage = lazy(() => import('./features/help-desk/pages/TicketDetailPage'));
const SatisfactionSurvey = lazy(() => import('./features/help-desk/components/SatisfactionSurvey'));

const LocationsPage = lazy(() => import('./features/masters/pages/LocationsPage'));
const ProjectsPage = lazy(() => import('./features/masters/pages/ProjectsPage'));
const WorksPage = lazy(() => import('./features/masters/pages/WorksPage'));
const TrainingSchedulePage = lazy(() => import('./features/masters/components/training-schedule/TrainingSchedulePage'));
const DepartmentPage = lazy(() => import('./features/masters/pages/DepartmentPage'));
const DesignationMasterPage = lazy(() => import('./features/masters/pages/DesignationMasterPage'));

const SeedDataPage = lazy(() => import('./features/seed/SeedDataPage'));
const UserAccessPage = lazy(() => import('./features/admin/pages/UserAccessPage'));
const AuditLogPage = lazy(() => import('./features/admin/pages/AuditLogPage'));
const BulkImportPage = lazy(() => import('./features/admin/pages/BulkImportPage'));
const BackupPage = lazy(() => import('./features/admin/pages/BackupPage'));

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
            <ErrorBoundary>
              <Suspense fallback={<LoadingSkeleton rows={8} />}>
                <Routes>
                  <Route path="/login" element={<LoginPage />} />
                  <Route path="/forgot-password" element={<ForgotPasswordPage />} />
                  <Route path="/reset-password" element={<ResetPasswordPage />} />
                  <Route path="/" element={<ProtectedRoute><AppLayout /></ProtectedRoute>}>
                    <Route index element={<Navigate to="/dashboard" replace />} />
                    <Route path="dashboard" element={<ModuleProtectedRoute moduleCode="DASHBOARD"><DashboardRedirect /></ModuleProtectedRoute>} />
                    <Route path="dashboard/fellow" element={<ModuleProtectedRoute moduleCode="DASHBOARD"><FellowDashboardPage /></ModuleProtectedRoute>} />
                    <Route path="dashboard/coordinator" element={<ModuleProtectedRoute moduleCode="DASHBOARD"><CoordinatorFellowDashboardPage /></ModuleProtectedRoute>} />
                    <Route path="registration" element={<ModuleProtectedRoute moduleCode="REGISTRATION" permission="Write"><RegistrationWizard /></ModuleProtectedRoute>} />
                    <Route path="admin/users" element={<ModuleProtectedRoute moduleCode="REGISTRATION" permission="Write"><UserManagementPage /></ModuleProtectedRoute>} />
                    <Route path="admin/documents" element={<ModuleProtectedRoute moduleCode="REGISTRATION" permission="Approve"><DocumentVerificationPage /></ModuleProtectedRoute>} />
                    <Route path="profile" element={<ModuleProtectedRoute moduleCode="REGISTRATION"><ProfileEditPage /></ModuleProtectedRoute>} />
                    <Route path="admin/seed" element={<ModuleProtectedRoute moduleCode="ADMINISTRATION" permission="Write"><SeedDataPage /></ModuleProtectedRoute>} />
                    <Route path="admin/access" element={<ModuleProtectedRoute moduleCode="ADMINISTRATION" permission="Write"><UserAccessPage /></ModuleProtectedRoute>} />
                    <Route path="admin/access/audit" element={<ModuleProtectedRoute moduleCode="ADMINISTRATION"><AuditLogPage /></ModuleProtectedRoute>} />
                    <Route path="admin/import" element={<ModuleProtectedRoute moduleCode="ADMINISTRATION" permission="Write"><BulkImportPage /></ModuleProtectedRoute>} />
                    <Route path="admin/backup" element={<ModuleProtectedRoute moduleCode="ADMINISTRATION" permission="Write"><BackupPage /></ModuleProtectedRoute>} />
                    <Route path="training" element={<ModuleProtectedRoute moduleCode="TRAINING"><ActivityCalendar /></ModuleProtectedRoute>} />
                    <Route path="training/new" element={<ModuleProtectedRoute moduleCode="TRAINING" permission="Write"><CreateActivityForm /></ModuleProtectedRoute>} />
                    <Route path="training/completions" element={<ModuleProtectedRoute moduleCode="TRAINING"><TrainingCompletionPage /></ModuleProtectedRoute>} />
                    <Route path="training/meetings" element={<ModuleProtectedRoute moduleCode="TRAINING"><MeetingListPage /></ModuleProtectedRoute>} />
                    <Route path="training/meetings/:id" element={<ModuleProtectedRoute moduleCode="TRAINING"><MeetingDetailPage /></ModuleProtectedRoute>} />
                    <Route path="work-allocation" element={<ModuleProtectedRoute moduleCode="WORK_ALLOCATION"><WorkAllocationPage /></ModuleProtectedRoute>} />
                    <Route path="work-allocation/progress" element={<ModuleProtectedRoute moduleCode="WORK_ALLOCATION"><TaskProgressPage /></ModuleProtectedRoute>} />
                    <Route path="attendance" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><MarkAttendancePage /></ModuleProtectedRoute>} />
                    <Route path="attendance/report" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><AttendanceReportPage /></ModuleProtectedRoute>} />
                    <Route path="attendance/weekly-report" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><WeeklyAttendanceReportPage /></ModuleProtectedRoute>} />
                    <Route path="attendance/apply-leave" element={<ModuleProtectedRoute moduleCode="ATTENDANCE" permission="Write"><ApplyLeavePage /></ModuleProtectedRoute>} />
                    <Route path="attendance/leave-approval" element={<ModuleProtectedRoute moduleCode="ATTENDANCE" permission="Approve"><LeaveApprovalPage /></ModuleProtectedRoute>} />
                    <Route path="attendance/leave-status" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><LeaveStatusPage /></ModuleProtectedRoute>} />
                    <Route path="attendance/leave-balance" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><LeaveBalancePage /></ModuleProtectedRoute>} />
                    <Route path="attendance/holidays" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><HolidayCalendarPage /></ModuleProtectedRoute>} />
                    <Route path="attendance/payroll-summary" element={<ModuleProtectedRoute moduleCode="ATTENDANCE"><PayrollSummaryPage /></ModuleProtectedRoute>} />
                    <Route path="performance" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><PerformanceReviewGrid /></ModuleProtectedRoute>} />
                    <Route path="performance/goals" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><PerformanceGoalsPage /></ModuleProtectedRoute>} />
                    <Route path="performance/improvement-plans" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><ImprovementPlansPage /></ModuleProtectedRoute>} />
                    <Route path="performance/self-assessment" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><SelfAssessmentForm /></ModuleProtectedRoute>} />
                    <Route path="performance/peer-feedback/:id" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><PeerFeedbackPage /></ModuleProtectedRoute>} />
                    <Route path="performance/review-cycle" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><ReviewCyclePage /></ModuleProtectedRoute>} />
                    <Route path="performance/:id" element={<ModuleProtectedRoute moduleCode="PERFORMANCE"><PerformanceDetailPage /></ModuleProtectedRoute>} />
                    <Route path="certificate/apply" element={<ModuleProtectedRoute moduleCode="CERTIFICATE" permission="Write"><ApplyCertificatePage /></ModuleProtectedRoute>} />
                    <Route path="certificate/approvals" element={<ModuleProtectedRoute moduleCode="CERTIFICATE" permission="Approve"><CertificateApprovalPage /></ModuleProtectedRoute>} />
                    <Route path="certificate/exit" element={<ModuleProtectedRoute moduleCode="CERTIFICATE" permission="Approve"><ExitManagementPage /></ModuleProtectedRoute>} />
                    <Route path="certificate/exit-interview" element={<ModuleProtectedRoute moduleCode="CERTIFICATE"><ExitInterviewForm /></ModuleProtectedRoute>} />
                    <Route path="certificate/verify" element={<ModuleProtectedRoute moduleCode="CERTIFICATE"><CertificateVerifyPage /></ModuleProtectedRoute>} />
                    <Route path="certificate" element={<ModuleProtectedRoute moduleCode="CERTIFICATE" permission="Approve"><CertificateApprovalPage /></ModuleProtectedRoute>} />
                    <Route path="help-desk" element={<ModuleProtectedRoute moduleCode="HELP_DESK"><TicketQueuePage /></ModuleProtectedRoute>} />
                    <Route path="help-desk/new" element={<ModuleProtectedRoute moduleCode="HELP_DESK" permission="Write"><RaiseTicketPage /></ModuleProtectedRoute>} />
                    <Route path="help-desk/knowledge-base" element={<ModuleProtectedRoute moduleCode="HELP_DESK"><KnowledgeBasePage /></ModuleProtectedRoute>} />
                    <Route path="help-desk/:id" element={<ModuleProtectedRoute moduleCode="HELP_DESK"><TicketDetailPage /></ModuleProtectedRoute>} />
                    <Route path="help-desk/:id/survey" element={<ModuleProtectedRoute moduleCode="HELP_DESK"><SatisfactionSurvey /></ModuleProtectedRoute>} />
                    <Route path="masters/locations" element={<ModuleProtectedRoute moduleCode="MASTERS"><LocationsPage /></ModuleProtectedRoute>} />
                    <Route path="masters/projects" element={<ModuleProtectedRoute moduleCode="MASTERS"><ProjectsPage /></ModuleProtectedRoute>} />
                    <Route path="masters/works" element={<ModuleProtectedRoute moduleCode="MASTERS"><WorksPage /></ModuleProtectedRoute>} />
                    <Route path="masters/training-schedules" element={<ModuleProtectedRoute moduleCode="MASTERS"><TrainingSchedulePage /></ModuleProtectedRoute>} />
                    <Route path="masters/departments" element={<ModuleProtectedRoute moduleCode="MASTERS"><DepartmentPage /></ModuleProtectedRoute>} />
                    <Route path="masters/designations" element={<ModuleProtectedRoute moduleCode="MASTERS"><DesignationMasterPage /></ModuleProtectedRoute>} />
                    <Route path="unauthorized" element={<UnauthorizedPage />} />
                  </Route>
                </Routes>
              </Suspense>
            </ErrorBoundary>
          </AuthProvider>
        </BrowserRouter>
        {import.meta.env.DEV && <ReactQueryDevtools initialIsOpen={false} />}
      </QueryClientProvider>
    </ThemeProvider>
  );
}
