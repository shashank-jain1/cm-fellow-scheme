import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { attendanceLeaveApi } from './api';
import type { MarkAttendanceCommand, LeaveApplicationFormData } from './types';

export function useMarkAttendance() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: MarkAttendanceCommand) => attendanceLeaveApi.markAttendance(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['attendance'] }),
  });
}

export function useAttendanceByDate(date: string) {
  return useQuery({
    queryKey: ['attendance', date],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getAttendanceByDate(date);
      return res.data ?? [];
    },
    enabled: !!date,
  });
}

export function useApplyLeave() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: LeaveApplicationFormData) => attendanceLeaveApi.applyLeave(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['leaveStatus'] }),
  });
}

export function useLeaveStatus() {
  return useQuery({
    queryKey: ['leaveStatus'],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getLeaveStatus();
      return res.data ?? [];
    },
  });
}

export function useLeaveBalance() {
  return useQuery({
    queryKey: ['leaveBalance'],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getLeaveBalance();
      return res.data ?? [];
    },
  });
}

export function useLeaveApprovalQueue() {
  return useQuery({
    queryKey: ['leaveApprovalQueue'],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getLeaveApprovalQueue();
      return res.data ?? [];
    },
  });
}

export function useApproveLeave() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ applicationNo, remarks }: { applicationNo: string; remarks?: string }) =>
      attendanceLeaveApi.approveLeave(applicationNo, remarks),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['leaveApprovalQueue'] }),
  });
}

export function useRejectLeave() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ applicationNo, remarks }: { applicationNo: string; remarks?: string }) =>
      attendanceLeaveApi.rejectLeave(applicationNo, remarks),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['leaveApprovalQueue'] }),
  });
}
