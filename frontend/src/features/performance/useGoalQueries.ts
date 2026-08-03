import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fetchGoalsByUser, createGoal, updateGoalStatus } from './api';
import type { CreateGoalCommand, UpdateGoalStatusCommand } from './types';

export function useGoalsByUser(userId: number) {
  return useQuery({
    queryKey: ['performance', 'goals', userId],
    queryFn: () => fetchGoalsByUser(userId),
    enabled: !!userId,
  });
}

export function useCreateGoal() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: CreateGoalCommand) => createGoal(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'goals'] });
    },
  });
}

export function useUpdateGoalStatus() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ goalId, command }: { goalId: number; command: UpdateGoalStatusCommand }) =>
      updateGoalStatus(goalId, command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'goals'] });
    },
  });
}
