import { useMutation } from '@tanstack/react-query';
import { loginApi } from './api';

export function useLoginMutation() {
  return useMutation({
    mutationFn: ({ username, password }: { username: string; password: string }) =>
      loginApi(username, password),
  });
}
