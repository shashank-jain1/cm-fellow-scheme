import ApiService from '../../services/ApiService';
import type { Fellow, Division, District, Block, Project } from './types';

export const registrationApi = {
  getFellows: () => ApiService.get<Fellow[]>('fellows'),
  getFellow: (id: number) => ApiService.get<Fellow>(`fellows/${id}`),
  createFellow: (data: Partial<Fellow>) => ApiService.post<Fellow>('fellows', data),
  updateFellow: (id: number, data: Partial<Fellow>) => ApiService.put<Fellow>(`fellows/${id}`, data),
  deleteFellow: (id: number) => ApiService.delete(`fellows/${id}`),

  getDivisions: () => ApiService.get<Division[]>('divisions'),
  getDistricts: (divisionId?: number) =>
    ApiService.get<District[]>(divisionId ? `districts?divisionId=${divisionId}` : 'districts'),
  getBlocks: (districtId?: number) =>
    ApiService.get<Block[]>(districtId ? `blocks?districtId=${districtId}` : 'blocks'),
  getProjects: () => ApiService.get<Project[]>('projects'),
};
