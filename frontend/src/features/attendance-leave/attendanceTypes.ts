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

export interface DailyAttendanceRecordDto {
  attendanceDate: string;
  checkInTime: string;
  checkOutTime?: string;
  attendanceStatus: string;
  hoursWorked: number;
}

export interface WeeklyAttendanceReportDto {
  weekStartDate: string;
  weekEndDate: string;
  totalAttendanceDays: number;
  totalHours: number;
  dailyRecords: DailyAttendanceRecordDto[];
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
