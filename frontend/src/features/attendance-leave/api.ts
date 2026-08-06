import ApiService from '../../services/ApiService';
import { ATTENDANCE_LEAVE_URLS } from './urls';
import type {
  MarkAttendanceCommand,
  AttendanceDto,
  ApplyLeaveCommand,
  ApproveLeaveCommand,
  LeaveStatusDto,
  LeaveBalanceDto,
  HolidayDto,
  CreateHolidayCommand,
  UpdateHolidayCommand,
  PayrollSummaryDto,
  CheckOutAttendanceCommand,
  MonthlyAttendanceReportDto,
  WeeklyAttendanceReportDto,
} from './types';

export const attendanceLeaveApi = {
  markAttendance: (data: MarkAttendanceCommand) =>
    ApiService.post<number>(ATTENDANCE_LEAVE_URLS.MARK_ATTENDANCE, data),

  getAttendanceHistory: () =>
    ApiService.get<AttendanceDto[]>(ATTENDANCE_LEAVE_URLS.ATTENDANCE_HISTORY),

  applyLeave: (data: ApplyLeaveCommand) =>
    ApiService.post<number>(ATTENDANCE_LEAVE_URLS.APPLY_LEAVE, data),

  getLeaveStatus: (userAccountId: number) =>
    ApiService.get<LeaveStatusDto[]>(`${ATTENDANCE_LEAVE_URLS.LEAVE_STATUS}?UserAccountId=${userAccountId}`),

  getLeaveBalance: (userAccountId: number, year: number) =>
    ApiService.get<LeaveBalanceDto[]>(`${ATTENDANCE_LEAVE_URLS.LEAVE_BALANCE}?UserAccountId=${userAccountId}&Year=${year}`),

  approveLeave: (command: ApproveLeaveCommand) =>
    ApiService.put<boolean>(ATTENDANCE_LEAVE_URLS.APPROVE_LEAVE, command),

  getHolidays: (year?: number) =>
    ApiService.get<HolidayDto[]>(`${ATTENDANCE_LEAVE_URLS.HOLIDAYS}${year ? `?year=${year}` : ''}`),

  createHoliday: (data: CreateHolidayCommand) =>
    ApiService.post<number>(ATTENDANCE_LEAVE_URLS.HOLIDAYS, data),

  updateHoliday: (data: UpdateHolidayCommand) =>
    ApiService.put<void>(`${ATTENDANCE_LEAVE_URLS.HOLIDAYS}/${data.holidayId}`, data),

  deleteHoliday: (holidayId: number) =>
    ApiService.delete<void>(`${ATTENDANCE_LEAVE_URLS.HOLIDAYS}/${holidayId}`),

  checkOutAttendance: (data: CheckOutAttendanceCommand) =>
    ApiService.put<void>(ATTENDANCE_LEAVE_URLS.CHECKOUT, data),

  getMonthlyReport: (month: number, year: number) =>
    ApiService.get<MonthlyAttendanceReportDto>(
      `${ATTENDANCE_LEAVE_URLS.ATTENDANCE_REPORT_MONTHLY}?month=${month}&year=${year}`
    ),

  getWeeklyReport: (weekStart: string) =>
    ApiService.get<WeeklyAttendanceReportDto>(
      `${ATTENDANCE_LEAVE_URLS.ATTENDANCE_REPORT_WEEKLY}?weekStart=${weekStart}`
    ),

  getPayrollSummary: (payrollMonth?: string, applicantId?: number) => {
    const params = new URLSearchParams();
    if (payrollMonth) params.append('payrollMonth', payrollMonth);
    if (applicantId) params.append('applicantId', String(applicantId));
    const query = params.toString();
    return ApiService.get<PayrollSummaryDto[]>(`${ATTENDANCE_LEAVE_URLS.PAYROLL_SUMMARY}${query ? `?${query}` : ''}`);
  },
};
