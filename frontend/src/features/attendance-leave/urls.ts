export const ATTENDANCE_LEAVE_URLS = {
  MARK_ATTENDANCE: 'attendance/mark',
  ATTENDANCE_BY_DATE: (date: string) => `attendance?date=${date}`,
  APPLY_LEAVE: 'leave/apply',
  LEAVE_STATUS: 'leave/status',
  LEAVE_BALANCE: 'leave/balance',
  LEAVE_APPROVAL_QUEUE: 'leave/approval/queue',
  APPROVE_LEAVE: (applicationNo: string) => `leave/approve/${applicationNo}`,
  REJECT_LEAVE: (applicationNo: string) => `leave/reject/${applicationNo}`,
};
