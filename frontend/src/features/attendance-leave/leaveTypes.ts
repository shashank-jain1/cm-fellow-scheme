export interface ApplyLeaveCommand {
  applicantId: number;
  leaveType: string;
  fromDate: string;
  toDate: string;
  numberOfDays: number;
  halfDayFullDay: string;
  leaveReason: string;
  attachmentPath?: string;
  reportingManagerName: string;
  createdBy: string;
}

export interface ApproveLeaveCommand {
  leaveApplicationId: number;
  status: string;
  remarks: string;
}

export interface LeaveStatusDto {
  leaveApplicationId: number;
  leaveType: string;
  fromDate: string;
  toDate: string;
  numberOfDays: number;
  status: string;
  createdOn: string;
}

export interface LeaveBalanceDto {
  leaveBalanceId: number;
  applicantId: number;
  leaveType: string;
  openingBalance: number;
  availedLeave: number;
  pendingApprovalLeave: number;
  availableBalance: number;
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
