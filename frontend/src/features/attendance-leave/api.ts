import ApiService from '../../services/ApiService';
import { ATTENDANCE_LEAVE_URLS } from './urls';
import type {
  MarkAttendanceCommand,
  AttendanceDto,
  LeaveApplicationFormData,
  LeaveStatusDto,
  LeaveBalanceDto,
  HolidayDto,
  CreateHolidayCommand,
  UpdateHolidayCommand,
  PayrollSummaryDto,
} from './types';

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

  getHolidays: (year?: number) =>
    ApiService.get<HolidayDto[]>(ATTENDANCE_LEAVE_URLS.HOLIDAYS, { params: year ? { year } : {} }),

  createHoliday: (data: CreateHolidayCommand) =>
    ApiService.post<number>(ATTENDANCE_LEAVE_URLS.HOLIDAYS, data),

  updateHoliday: (data: UpdateHolidayCommand) =>
    ApiService.put<void>(`${ATTENDANCE_LEAVE_URLS.HOLIDAYS}/${data.holidayId}`, data),

  deleteHoliday: (holidayId: number) =>
    ApiService.delete<void>(`${ATTENDANCE_LEAVE_URLS.HOLIDAYS}/${holidayId}`),

  getPayrollSummary: (payrollMonth?: string, applicantId?: number) =>
    ApiService.get<PayrollSummaryDto[]>(ATTENDANCE_LEAVE_URLS.PAYROLL_SUMMARY, {
      params: {
        ...(payrollMonth ? { payrollMonth } : {}),
        ...(applicantId ? { applicantId } : {}),
      },
    }),
};
