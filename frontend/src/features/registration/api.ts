import ApiService from '../../services/ApiService';
import { REGISTRATION_URLS } from './urls';
import type { Fellow, Division, District, Block, Project } from './types';

export const registrationApi = {
  getFellows: () => ApiService.get<Fellow[]>(REGISTRATION_URLS.FELLOWS),
  getFellow: (id: number) => ApiService.get<Fellow>(REGISTRATION_URLS.FELLOW_BY_ID(id)),
  createFellow: (data: Partial<Fellow>) => ApiService.post<Fellow>(REGISTRATION_URLS.FELLOWS, data),
  updateFellow: (id: number, data: Partial<Fellow>) => ApiService.put<Fellow>(REGISTRATION_URLS.FELLOW_BY_ID(id), data),
  deleteFellow: (id: number) => ApiService.delete(REGISTRATION_URLS.FELLOW_BY_ID(id)),

  getDivisions: () => ApiService.get<Division[]>(REGISTRATION_URLS.DIVISIONS),
  getDistricts: (divisionId?: number) =>
    ApiService.get<District[]>(divisionId ? `${REGISTRATION_URLS.DISTRICTS}?divisionId=${divisionId}` : REGISTRATION_URLS.DISTRICTS),
  getBlocks: (districtId?: number) =>
    ApiService.get<Block[]>(districtId ? `${REGISTRATION_URLS.BLOCKS}?districtId=${districtId}` : REGISTRATION_URLS.BLOCKS),
  getProjects: () => ApiService.get<Project[]>(REGISTRATION_URLS.PROJECTS),
};
