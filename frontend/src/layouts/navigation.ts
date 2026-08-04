export interface NavItem {
  path: string;
  label: string;
  icon: string;
  moduleCode?: string;
}

export const navItems: NavItem[] = [
  { path: '/dashboard', label: 'Dashboard', icon: 'pi pi-home', moduleCode: 'DASHBOARD' },
  { path: '/registration', label: 'Registration', icon: 'pi pi-user-plus', moduleCode: 'REGISTRATION' },
];

export const trainingSubItems: NavItem[] = [
  { path: '/training', label: 'Activity Calendar', icon: 'pi pi-calendar' },
  { path: '/training/new', label: 'New Activity', icon: 'pi pi-plus' },
  { path: '/training/meetings', label: 'Meetings Queue', icon: 'pi pi-users' },
  { path: '/training/completions', label: 'Completions', icon: 'pi pi-check-circle' },
];

export const attendanceSubItems: NavItem[] = [
  { path: '/attendance', label: 'Mark Attendance', icon: 'pi pi-clock' },
  { path: '/attendance/report', label: 'Monthly Report', icon: 'pi pi-chart-line' },
  { path: '/attendance/weekly-report', label: 'Weekly Report', icon: 'pi pi-calendar' },
  { path: '/attendance/holidays', label: 'Holiday Calendar', icon: 'pi pi-calendar-plus' },
  { path: '/attendance/payroll-summary', label: 'Payroll Summary', icon: 'pi pi-money-bill' },
  { path: '/attendance/apply-leave', label: 'Apply Leave', icon: 'pi pi-send' },
  { path: '/attendance/leave-status', label: 'Leave Status', icon: 'pi pi-list' },
  { path: '/attendance/leave-balance', label: 'Leave Balance', icon: 'pi pi-wallet' },
  { path: '/attendance/leave-approval', label: 'Leave Approval', icon: 'pi pi-check-circle' },
];

export const workAllocationSubItems: NavItem[] = [
  { path: '/work-allocation', label: 'All Allocations', icon: 'pi pi-list' },
  { path: '/work-allocation/progress', label: 'Task Progress', icon: 'pi pi-chart-bar' },
];

export const performanceSubItems: NavItem[] = [
  { path: '/performance', label: 'Performance Reviews', icon: 'pi pi-chart-bar' },
  { path: '/performance/goals', label: 'Goals', icon: 'pi pi-star' },
  { path: '/performance/improvement-plans', label: 'Improvement Plans', icon: 'pi pi-arrow-up' },
  { path: '/performance/self-assessment', label: 'Self Assessment', icon: 'pi pi-user' },
  { path: '/performance/review-cycle', label: 'Review Cycles', icon: 'pi pi-sync' },
];

export const certificateSubItems: NavItem[] = [
  { path: '/certificate', label: 'Certificate Queue', icon: 'pi pi-list' },
  { path: '/certificate/apply', label: 'Apply for Certificate', icon: 'pi pi-send' },
  { path: '/certificate/verify', label: 'Verify Certificate', icon: 'pi pi-verified' },
  { path: '/certificate/exit', label: 'Exit Management', icon: 'pi pi-sign-out' },
  { path: '/certificate/exit-interview', label: 'Exit Interview', icon: 'pi pi-user-minus' },
];

export const helpDeskSubItems: NavItem[] = [
  { path: '/help-desk', label: 'Ticket Queue', icon: 'pi pi-list' },
  { path: '/help-desk/new', label: 'Raise Ticket', icon: 'pi pi-plus-circle' },
  { path: '/help-desk/knowledge-base', label: 'Knowledge Base', icon: 'pi pi-book' },
];

export const adminNavItems: NavItem[] = [
  { path: '/admin/users', label: 'User Management', icon: 'pi pi-users', moduleCode: 'REGISTRATION' },
  { path: '/admin/access', label: 'User Access Management', icon: 'pi pi-key', moduleCode: 'ADMINISTRATION' },
  { path: '/admin/access/audit', label: 'Access Audit Log', icon: 'pi pi-history', moduleCode: 'ADMINISTRATION' },
  { path: '/admin/documents', label: 'Document Verification', icon: 'pi pi-file-check', moduleCode: 'REGISTRATION' },
  { path: '/admin/import', label: 'Bulk Import', icon: 'pi pi-upload', moduleCode: 'ADMINISTRATION' },
  { path: '/admin/backup', label: 'Database Backup', icon: 'pi pi-download', moduleCode: 'ADMINISTRATION' },
  { path: '/admin/seed', label: 'Seed Data', icon: 'pi pi-database', moduleCode: 'ADMINISTRATION' },
];

export const masterSubItems: NavItem[] = [
  { path: '/masters/locations', label: 'Locations', icon: 'pi pi-map' },
  { path: '/masters/projects', label: 'Projects', icon: 'pi pi-briefcase' },
  { path: '/masters/works', label: 'Works', icon: 'pi pi-file-edit' },
  { path: '/masters/training-schedules', label: 'Training Schedule', icon: 'pi pi-calendar' },
  { path: '/masters/departments', label: 'Departments', icon: 'pi pi-building' },
  { path: '/masters/designations', label: 'Designations', icon: 'pi pi-id-card' },
];
