import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { attendanceLeaveApi } from './api';
import type {
  MarkAttendanceCommand,
  CheckOutAttendanceCommand,
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

export function useCheckOutMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: CheckOutAttendanceCommand) => attendanceLeaveApi.checkOutAttendance(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['attendance'] }),
  });
}

export function useMonthlyReport(month: number, year: number) {
  return useQuery({
    queryKey: ['attendance', 'monthlyReport', month, year],
    queryFn: async () => {
      const res = await attendanceLeaveApi.getMonthlyReport(month, year);
      return res.data ?? null;
    },
    enabled: !!month && !!year,
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
