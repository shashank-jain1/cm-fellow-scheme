import ApiService from '../../services/ApiService';
import type { WorkAllocationFormData, WorkAllocationDto, TaskProgressDto, SurveyDetailDto } from './types';

export interface RecordSurveyPayload {
  applicantId: number;
  surveyPersonName: string;
  mobileNumber: string;
  panchayatName: string;
  villageName: string;
  latitude: number;
  longitude: number;
}

export const workAllocationApi = {
  getAll: () => ApiService.get<WorkAllocationDto[]>('work-allocations/list'),
  getById: (id: number) => ApiService.get<WorkAllocationDto>(`work-allocations/${id}`),
  create: (data: WorkAllocationFormData) => ApiService.post<WorkAllocationDto>('work-allocations', data),
  update: (id: number, data: WorkAllocationFormData) =>
    ApiService.put<WorkAllocationDto>(`work-allocations/${id}`, data),
  delete: (id: number) => ApiService.delete(`work-allocations/${id}`),
  assign: (id: number, assignedToUserId: number) =>
    ApiService.put<void>(`work-allocations/${id}/assign`, { workAllocationId: id, assignedToUserId }),
  deactivate: (id: number) =>
    ApiService.put<void>(`work-allocations/${id}/deactivate`, {}),
};

export const taskProgressApi = {
  getAll: () => ApiService.get<TaskProgressDto[]>('task-progresses'),
  getByWorkAllocationId: (workAllocationId: number) =>
    ApiService.get<TaskProgressDto[]>(`task-progresses/by-work-allocation/${workAllocationId}`),
  recordSurveySubmission: (taskProgressId: number, data: RecordSurveyPayload) =>
    ApiService.post<void>(`task-progresses/${taskProgressId}/survey`, data),
};

export const surveyDetailApi = {
  getByTaskProgressId: (taskProgressId: number) =>
    ApiService.get<SurveyDetailDto[]>(`survey-records/by-task-progress/${taskProgressId}`),
};
