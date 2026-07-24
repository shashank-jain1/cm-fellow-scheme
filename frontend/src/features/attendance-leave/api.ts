import ApiService from '../../services/ApiService';
import { ATTENDANCE_LEAVE_URLS } from './urls';
import type { MarkAttendanceCommand, AttendanceDto, LeaveApplicationFormData, LeaveStatusDto, LeaveBalanceDto } from './types';

export const attendanceLeaveApi = {
  markAttendance: (data: MarkAttendanceCommand) =>
    ApiService.post<AttendanceDto>(ATTENDANCE_LEAVE_URLS.MARK_ATTENDANCE, data),

  getAttendanceByDate: (date: string) =>
    ApiService.get<AttendanceDto[]>(ATTENDANCE_LEAVE_URLS.ATTENDANCE_BY_DATE(date)),

  applyLeave: (data: LeaveApplicationFormData) => {
    const formData = new FormData();
    formData.append('leaveType', data.leaveType);
    formData.append('fromDate', data.fromDate);
    formData.append('toDate', data.toDate);
    formData.append('halfDayFullDay', String(data.halfDayFullDay));
    formData.append('leaveReason', data.leaveReason);
    if (data.attachmentFile) {
      formData.append('attachmentFile', data.attachmentFile);
    }
    return ApiService.post<void>(ATTENDANCE_LEAVE_URLS.APPLY_LEAVE, formData);
  },

  getLeaveStatus: () =>
    ApiService.get<LeaveStatusDto[]>(ATTENDANCE_LEAVE_URLS.LEAVE_STATUS),

  getLeaveBalance: () =>
    ApiService.get<LeaveBalanceDto[]>(ATTENDANCE_LEAVE_URLS.LEAVE_BALANCE),

  getLeaveApprovalQueue: () =>
    ApiService.get<LeaveStatusDto[]>(ATTENDANCE_LEAVE_URLS.LEAVE_APPROVAL_QUEUE),

  approveLeave: (applicationNo: string, remarks?: string) =>
    ApiService.post<void>(ATTENDANCE_LEAVE_URLS.APPROVE_LEAVE(applicationNo), { remarks }),

  rejectLeave: (applicationNo: string, remarks?: string) =>
    ApiService.post<void>(ATTENDANCE_LEAVE_URLS.REJECT_LEAVE(applicationNo), { remarks }),
};
