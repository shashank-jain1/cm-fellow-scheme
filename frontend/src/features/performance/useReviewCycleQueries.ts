import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  createReviewCycle,
  getActiveReviewCycle,
  closeReviewCycle,
} from './api';
import type { CreateReviewCycleCommand } from './types';

export function useActiveReviewCycle() {
  return useQuery({
    queryKey: ['performance', 'review-cycle', 'active'],
    queryFn: getActiveReviewCycle,
  });
}

export function useCreateReviewCycle() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: CreateReviewCycleCommand) => createReviewCycle(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'review-cycle'] });
    },
  });
}

export function useCloseReviewCycle() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => closeReviewCycle(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'review-cycle'] });
    },
  });
}
