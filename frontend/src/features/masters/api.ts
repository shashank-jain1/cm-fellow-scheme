import ApiService from '../../services/ApiService';
import type {
  StateDto,
  DivisionDto,
  DistrictDto,
  BlockDto,
  GramPanchayatDto,
  ProjectDto,
  WorkDto,
  TrainingScheduleDto,
  CreateStateCommand,
  CreateDivisionCommand,
  CreateDistrictCommand,
  CreateBlockCommand,
  CreateGramPanchayatCommand,
  CreateProjectCommand,
  CreateWorkCommand,
  CreateTrainingScheduleCommand,
  UpdateTrainingScheduleCommand,
  UpdateWorkCommand,
  DepartmentDto,
  CreateDepartmentCommand,
} from './types';
import { MASTER_URLS } from './urls';

export const mastersApi = {
  getStates: () => ApiService.get<StateDto[]>(MASTER_URLS.states),
  createState: (data: CreateStateCommand) => ApiService.post<number>(MASTER_URLS.states, data),

  getDivisions: (stateId?: number) =>
    ApiService.get<DivisionDto[]>(`${MASTER_URLS.divisions}${stateId ? `?stateId=${stateId}` : ''}`),
  createDivision: (data: CreateDivisionCommand) => ApiService.post<number>(MASTER_URLS.divisions, data),

  getDistricts: (divisionId?: number) =>
    ApiService.get<DistrictDto[]>(`${MASTER_URLS.districts}${divisionId ? `?divisionId=${divisionId}` : ''}`),
  createDistrict: (data: CreateDistrictCommand) => ApiService.post<number>(MASTER_URLS.districts, data),

  getBlocks: (districtId?: number) =>
    ApiService.get<BlockDto[]>(`${MASTER_URLS.blocks}${districtId ? `?districtId=${districtId}` : ''}`),
  createBlock: (data: CreateBlockCommand) => ApiService.post<number>(MASTER_URLS.blocks, data),

  getGramPanchayats: (blockId?: number) =>
    ApiService.get<GramPanchayatDto[]>(`${MASTER_URLS.gramPanchayats}${blockId ? `?blockId=${blockId}` : ''}`),
  createGramPanchayat: (data: CreateGramPanchayatCommand) => ApiService.post<number>(MASTER_URLS.gramPanchayats, data),

  getProjects: () => ApiService.get<ProjectDto[]>(MASTER_URLS.projects),
  createProject: (data: CreateProjectCommand) => ApiService.post<number>(MASTER_URLS.projects, data),

  getWorks: (projectId?: number) =>
    ApiService.get<WorkDto[]>(`${MASTER_URLS.works}${projectId ? `?projectId=${projectId}` : ''}`),
  getWork: (id: number) => ApiService.get<WorkDto>(`${MASTER_URLS.works}/${id}`),
  createWork: (data: CreateWorkCommand) => ApiService.post<number>(MASTER_URLS.works, data),
  updateWork: (id: number, data: UpdateWorkCommand) =>
    ApiService.put<void>(`${MASTER_URLS.works}/${id}`, { ...data, workId: id }),

  getTrainingSchedules: (filters?: { calendarYear?: string; projectId?: number; divisionId?: number }) => {
    const params = new URLSearchParams();
    if (filters?.calendarYear) params.append('calendarYear', filters.calendarYear);
    if (filters?.projectId) params.append('projectId', String(filters.projectId));
    if (filters?.divisionId) params.append('divisionId', String(filters.divisionId));
    const qs = params.toString();
    return ApiService.get<TrainingScheduleDto[]>(`${MASTER_URLS.trainingSchedules}${qs ? `?${qs}` : ''}`);
  },
  getTrainingSchedule: (id: number) =>
    ApiService.get<TrainingScheduleDto>(`${MASTER_URLS.trainingSchedules}/${id}`),
  createTrainingSchedule: (data: CreateTrainingScheduleCommand) =>
    ApiService.post<number>(MASTER_URLS.trainingSchedules, data),
  updateTrainingSchedule: (id: number, data: UpdateTrainingScheduleCommand) =>
    ApiService.put<void>(`${MASTER_URLS.trainingSchedules}/${id}`, { ...data, trainingScheduleId: id }),

  getDepartments: () => ApiService.get<DepartmentDto[]>(MASTER_URLS.departments),
  createDepartment: (data: CreateDepartmentCommand) =>
    ApiService.post<number>(MASTER_URLS.departments, data),
};
