import ApiService from '../../services/ApiService';
import type {
  StateDto,
  DivisionDto,
  DistrictDto,
  BlockDto,
  GramPanchayatDto,
  ProjectDto,
  WorkDto,
  CreateStateCommand,
  CreateDivisionCommand,
  CreateDistrictCommand,
  CreateBlockCommand,
  CreateGramPanchayatCommand,
  CreateProjectCommand,
  CreateWorkCommand,
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
  createWork: (data: CreateWorkCommand) => ApiService.post<number>(MASTER_URLS.works, data),
};
