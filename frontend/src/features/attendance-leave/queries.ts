import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { attendanceLeaveApi } from './api';
import type {
  MarkAttendanceCommand,
  ApplyLeaveCommand,
  ApproveLeaveCommand,
  CreateHolidayCommand,
  UpdateHolidayCommand,
} from './types';

export function useMarkAttendance() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: MarkAttendanceCommand) => attendanceLeaveApi.markAttendance(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['attendance'] }),
  });
}

export function useAttendanceHistory() {
  return useQuery({
    queryKey: ['attendance'],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getAttendanceHistory();
      return res.data ?? [];
    },
  });
}

export function useApplyLeave() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: ApplyLeaveCommand) => attendanceLeaveApi.applyLeave(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['leaveStatus'] }),
  });
}

export function useLeaveStatus(userAccountId: number) {
  return useQuery({
    queryKey: ['leaveStatus', userAccountId],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getLeaveStatus(userAccountId);
      return res.data ?? [];
    },
    enabled: !!userAccountId,
  });
}

export function useLeaveBalance(userAccountId: number, year: number) {
  return useQuery({
    queryKey: ['leaveBalance', userAccountId, year],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getLeaveBalance(userAccountId, year);
      return res.data ?? [];
    },
    enabled: !!userAccountId && !!year,
  });
}

export function useApproveLeave() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (command: ApproveLeaveCommand) => attendanceLeaveApi.approveLeave(command),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['leaveStatus'] }),
  });
}

export function useHolidays(year?: number) {
  return useQuery({
    queryKey: ['holidays', year],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getHolidays(year);
      return res.data ?? [];
    },
  });
}

export function useCreateHoliday() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateHolidayCommand) => attendanceLeaveApi.createHoliday(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['holidays'] }),
  });
}

export function useUpdateHoliday() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: UpdateHolidayCommand) => attendanceLeaveApi.updateHoliday(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['holidays'] }),
  });
}

export function useDeleteHoliday() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (holidayId: number) => attendanceLeaveApi.deleteHoliday(holidayId),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['holidays'] }),
  });
}

export function usePayrollSummary(payrollMonth?: string, applicantId?: number) {
  return useQuery({
    queryKey: ['payrollSummary', payrollMonth, applicantId],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getPayrollSummary(payrollMonth, applicantId);
      return res.data ?? [];
    },
  });
}
