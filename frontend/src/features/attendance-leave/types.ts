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

export interface LeaveApplicationFormData {
  leaveType: string;
  fromDate: string;
  toDate: string;
  halfDayFullDay: boolean;
  leaveReason: string;
  attachmentFile?: File;
}

export interface LeaveStatusDto {
  leaveApplicationNo: string;
  employeeName: string;
  leaveType: string;
  leavePeriod: string;
  numberOfDays: number;
  approvalStatus: string;
  approvedBy?: string;
  approvalDate?: string;
  remarks?: string;
}

export interface LeaveBalanceDto {
  employeeName: string;
  leaveType: string;
  openingBalance: number;
  availedLeave: number;
  pendingApprovalLeave: number;
  availableBalance: number;
}
