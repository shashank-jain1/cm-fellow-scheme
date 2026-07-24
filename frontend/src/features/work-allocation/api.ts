import ApiService from '../../services/ApiService';
import type { WorkAllocationFormData, WorkAllocationDto, TaskProgressDto, SurveyDetailDto } from './types';

const BASE_URL = 'work-allocation';

export const workAllocationApi = {
  getAll: () => ApiService.get<WorkAllocationDto[]>(BASE_URL),
  getById: (id: number) => ApiService.get<WorkAllocationDto>(`${BASE_URL}/${id}`),
  create: (data: WorkAllocationFormData) => ApiService.post<WorkAllocationDto>(BASE_URL, data),
  update: (id: number, data: WorkAllocationFormData) => 
    ApiService.put<WorkAllocationDto>(`${BASE_URL}/${id}`, data),
  delete: (id: number) => ApiService.delete(`${BASE_URL}/${id}`),
};

export const taskProgressApi = {
  getAll: () => ApiService.get<TaskProgressDto[]>('task-progress'),
  getByProjectId: (projectId: number) => 
    ApiService.get<TaskProgressDto[]>(`task-progress/project/${projectId}`),
};

export const surveyDetailApi = {
  getByWorkProjectId: (workProjectId: number) => 
    ApiService.get<SurveyDetailDto[]>(`survey-details/work-project/${workProjectId}`),
};
