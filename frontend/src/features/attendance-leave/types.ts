export interface MarkAttendanceCommand {
  applicantId: number;
  latitude: number;
  longitude: number;
  faceImageBase64: string;
}

export interface AttendanceDto {
  attendanceId: number;
  applicantId: number;
  attendanceDate: string;
  checkInTime: string;
  checkOutTime?: string;
  attendanceStatus: string;
  latitude: number;
  longitude: number;
}

export interface ApplyLeaveCommand {
  userAccountId: number;
  leaveTypeId: number;
  fromDate: string;
  toDate: string;
  isHalfDay: boolean;
  reason: string;
  attachmentPath?: string;
}

export interface ApplyLeaveResult {
  leaveApplicationId: number;
  applicationNumber: string;
}

export interface ApproveLeaveCommand {
  leaveApplicationId: number;
  approvedBy: number;
  action: 'Approved' | 'Rejected';
  remarks?: string;
}

export interface LeaveStatusDto {
  leaveApplicationId: number;
  applicationNumber: string;
  leaveTypeName: string;
  fromDate: string;
  toDate: string;
  numberOfDays: number;
  isHalfDay: boolean;
  reason: string;
  status: string;
  approvalRemarks?: string;
  approvalDate?: string;
  createdOn: string;
}

export interface LeaveBalanceDto {
  leaveBalanceId: number;
  leaveTypeName: string;
  leaveTypeCode: string;
  totalDays: number;
  usedDays: number;
  remainingDays: number;
}

export interface HolidayDto {
  holidayId: number;
  holidayName: string;
  holidayDate: string;
  description?: string;
  isOptional: boolean;
  isActive: boolean;
}

export interface CreateHolidayCommand {
  holidayName: string;
  holidayDate: string;
  description?: string;
  isOptional: boolean;
}

export interface UpdateHolidayCommand {
  holidayId: number;
  holidayName: string;
  holidayDate: string;
  description?: string;
  isOptional: boolean;
}

export interface CheckOutAttendanceCommand {
  applicantId: number;
  attendanceDate: string;
}

export interface MonthlyAttendanceReportDto {
  attendanceDays: number;
  totalHours: number;
  absentDays: number;
  leaveDays: number;
}

export interface PayrollSummaryDto {
  payrollAttendanceSummaryId: number;
  applicantId: number;
  payrollMonth: string;
  totalWorkingDays: number;
  presentDays: number;
  approvedLeaveDays: number;
  absentDays: number;
  payableDays: number;
  createdOn: string;
}
