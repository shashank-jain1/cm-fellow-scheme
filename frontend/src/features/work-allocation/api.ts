import ApiService from '../../services/ApiService';
import type { WorkAllocationFormData, WorkAllocationDto, TaskProgressDto, SurveyDetailDto } from './types';

export const workAllocationApi = {
  getAll: () => ApiService.get<WorkAllocationDto[]>('work-allocations/list'),
  getById: (id: number) => ApiService.get<WorkAllocationDto>(`work-allocations/${id}`),
  create: (data: WorkAllocationFormData) => ApiService.post<WorkAllocationDto>('work-allocations', data),
  update: (id: number, data: WorkAllocationFormData) =>
    ApiService.put<WorkAllocationDto>(`work-allocations/${id}`, data),
  delete: (id: number) => ApiService.delete(`work-allocations/${id}`),
};

export const taskProgressApi = {
  getAll: () => ApiService.get<TaskProgressDto[]>('task-progresses'),
  getByWorkAllocationId: (workAllocationId: number) =>
    ApiService.get<TaskProgressDto[]>(`task-progresses/by-work-allocation/${workAllocationId}`),
};

export const surveyDetailApi = {
  getByTaskProgressId: (taskProgressId: number) =>
    ApiService.get<SurveyDetailDto[]>(`survey-records/by-task-progress/${taskProgressId}`),
};
