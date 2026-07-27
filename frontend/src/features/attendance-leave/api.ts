import ApiService from '../../services/ApiService';
import { ATTENDANCE_LEAVE_URLS } from './urls';
import type { MarkAttendanceCommand, AttendanceDto, LeaveApplicationFormData, LeaveStatusDto, LeaveBalanceDto } from './types';

export const attendanceLeaveApi = {
  markAttendance: (data: MarkAttendanceCommand) =>
    ApiService.post<AttendanceDto>(ATTENDANCE_LEAVE_URLS.MARK_ATTENDANCE, data),

  getAttendanceHistory: () =>
    ApiService.get<AttendanceDto[]>(ATTENDANCE_LEAVE_URLS.ATTENDANCE_HISTORY),

  applyLeave: (data: LeaveApplicationFormData) =>
    ApiService.post<void>(ATTENDANCE_LEAVE_URLS.APPLY_LEAVE, data),

  getLeaveStatus: () =>
    ApiService.get<LeaveStatusDto[]>(ATTENDANCE_LEAVE_URLS.LEAVE_STATUS),

  getLeaveBalance: () =>
    ApiService.get<LeaveBalanceDto[]>(ATTENDANCE_LEAVE_URLS.LEAVE_BALANCE),

  approveLeave: (command: { LeaveApplicationNo: string; ApprovalStatus: string; Remarks?: string }) =>
    ApiService.put<void>(ATTENDANCE_LEAVE_URLS.APPROVE_LEAVE, command),
};
