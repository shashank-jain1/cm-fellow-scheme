export interface SearchItem {
  title: string;
  category: string;
  icon: string;
  path: string;
}

export const searchIndex: SearchItem[] = [
  { title: 'Dashboard', category: 'Navigation', icon: 'pi pi-chart-bar', path: '/dashboard' },
  { title: 'Fellow Registration', category: 'Registration', icon: 'pi pi-user-plus', path: '/registration' },
  { title: 'User Management', category: 'Admin', icon: 'pi pi-users', path: '/admin/users' },
  { title: 'Document Verification', category: 'Admin', icon: 'pi pi-file-check', path: '/admin/documents' },
  { title: 'Activity Calendar', category: 'Training', icon: 'pi pi-calendar', path: '/training' },
  { title: 'Create Activity', category: 'Training', icon: 'pi pi-plus-circle', path: '/training/new' },
  { title: 'Training Completions', category: 'Training', icon: 'pi pi-check-circle', path: '/training/completions' },
  { title: 'Work Allocation', category: 'Work', icon: 'pi pi-briefcase', path: '/work-allocation' },
  { title: 'Task Progress', category: 'Work', icon: 'pi pi-chart-bar', path: '/work-allocation/progress' },
  { title: 'Mark Attendance', category: 'Attendance', icon: 'pi pi-check-square', path: '/attendance' },
  { title: 'Monthly Report', category: 'Attendance', icon: 'pi pi-chart-line', path: '/attendance/report' },
  { title: 'Weekly Report', category: 'Attendance', icon: 'pi pi-calendar', path: '/attendance/weekly-report' },
  { title: 'Apply Leave', category: 'Attendance', icon: 'pi pi-calendar-plus', path: '/attendance/apply-leave' },
  { title: 'Leave Approval', category: 'Attendance', icon: 'pi pi-check-circle', path: '/attendance/leave-approval' },
  { title: 'Leave Status', category: 'Attendance', icon: 'pi pi-info-circle', path: '/attendance/leave-status' },
  { title: 'Leave Balance', category: 'Attendance', icon: 'pi pi-wallet', path: '/attendance/leave-balance' },
  { title: 'Holiday Calendar', category: 'Attendance', icon: 'pi pi-calendar', path: '/attendance/holidays' },
  { title: 'Payroll Summary', category: 'Attendance', icon: 'pi pi-dollar', path: '/attendance/payroll-summary' },
  { title: 'Performance Review', category: 'Performance', icon: 'pi pi-star', path: '/performance' },
  { title: 'Goals', category: 'Performance', icon: 'pi pi-bullseye', path: '/performance/goals' },
  { title: 'Improvement Plans', category: 'Performance', icon: 'pi pi-arrow-up', path: '/performance/improvement-plans' },
  { title: 'Self Assessment', category: 'Performance', icon: 'pi pi-user', path: '/performance/self-assessment' },
  { title: 'Review Cycles', category: 'Performance', icon: 'pi pi-sync', path: '/performance/review-cycle' },
  { title: 'Apply Certificate', category: 'Certificates', icon: 'pi pi-file', path: '/certificate/apply' },
  { title: 'Certificate Approvals', category: 'Certificates', icon: 'pi pi-check-square', path: '/certificate/approvals' },
  { title: 'Verify Certificate', category: 'Certificates', icon: 'pi pi-verified', path: '/certificate/verify' },
  { title: 'Exit Management', category: 'Certificates', icon: 'pi pi-sign-out', path: '/certificate/exit' },
  { title: 'Exit Interview', category: 'Certificates', icon: 'pi pi-user-minus', path: '/certificate/exit-interview' },
  { title: 'Help Desk / Tickets', category: 'Support', icon: 'pi pi-ticket', path: '/help-desk' },
  { title: 'Raise New Ticket', category: 'Support', icon: 'pi pi-plus', path: '/help-desk/new' },
  { title: 'Knowledge Base', category: 'Support', icon: 'pi pi-book', path: '/help-desk/knowledge-base' },
  { title: 'Locations Master', category: 'Masters', icon: 'pi pi-map-marker', path: '/masters/locations' },
  { title: 'Projects Master', category: 'Masters', icon: 'pi pi-folder', path: '/masters/projects' },
  { title: 'Works Master', category: 'Masters', icon: 'pi pi-cog', path: '/masters/works' },
  { title: 'Training Schedules Master', category: 'Masters', icon: 'pi pi-clock', path: '/masters/training-schedules' },
  { title: 'Departments', category: 'Masters', icon: 'pi pi-building', path: '/masters/departments' },
];
