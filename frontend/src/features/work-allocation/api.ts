import ApiService from '../../services/ApiService';
import type {
  WorkAllocationFormData,
  WorkAllocationDto,
  TaskProgressDto,
  SurveyDetailDto,
  TaskDependency,
  TaskDependencyFormData,
  TaskAttachment,
  UpdateProgressCommand,
  VerifyTaskCommand,
  OverdueTaskDto,
} from './types';

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
  create: (data: WorkAllocationFormData) =>
    ApiService.post<WorkAllocationDto>('work-allocations', data),
  update: (id: number, data: WorkAllocationFormData) =>
    ApiService.put<WorkAllocationDto>(`work-allocations/${id}`, data),
  deactivate: (id: number) =>
    ApiService.put<void>(`work-allocations/${id}/deactivate`, {}),
  assign: (id: number, assignedToUserId: number) =>
    ApiService.put<void>(`work-allocations/${id}/assign`, {
      workAllocationId: id,
      assignedToUserId,
    }),
  updateProgress: (workAllocationId: number, data: UpdateProgressCommand) =>
    ApiService.put<void>(`work-allocations/${workAllocationId}/progress`, data),
  verifyTask: (workAllocationId: number, command: VerifyTaskCommand) =>
    ApiService.put<void>(`work-allocations/${workAllocationId}/verify`, command),
  checkOverdue: () =>
    ApiService.get<OverdueTaskDto[]>('work-allocations/overdue'),
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

export const taskDependencyApi = {
  getByWorkAllocationId: (workAllocationId: number) =>
    ApiService.get<TaskDependency[]>(`task-dependencies/by-work-allocation/${workAllocationId}`),
  create: (data: TaskDependencyFormData) =>
    ApiService.post<TaskDependency>('task-dependencies', data),
};

export const taskAttachmentApi = {
  getByTaskProgressId: (taskProgressId: number) =>
    ApiService.get<TaskAttachment[]>(`tasks/attachments/${taskProgressId}`),
  upload: (taskProgressId: number, file: File) => {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('taskProgressId', String(taskProgressId));
    return ApiService.postFormData<TaskAttachment>(`tasks/attachments`, formData);
  },
};
