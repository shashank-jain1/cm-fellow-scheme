import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';

interface StateDto {
  stateId: number;
  stateName: string;
}

interface DivisionDto {
  divisionId: number;
  divisionName: string;
}

interface DistrictDto {
  districtId: number;
  districtName: string;
}

interface BlockDto {
  blockId: number;
  blockName: string;
}

interface WorkDto {
  workId: number;
  workName: string;
}

export function useStates() {
  return useQuery<StateDto[]>({
    queryKey: ['masters', 'states'],
    queryFn: async () => {
      const res = await ApiService.get<StateDto[]>('masters/locations/states');
      return res.data ?? [];
    },
  });
}

export function useDivisions(stateId: number | null) {
  return useQuery<DivisionDto[]>({
    queryKey: ['masters', 'divisions', stateId],
    queryFn: async () => {
      const res = await ApiService.get<DivisionDto[]>(`masters/locations/divisions?stateId=${stateId}`);
      return res.data ?? [];
    },
    enabled: !!stateId,
  });
}

export function useDistricts(divisionId: number | null) {
  return useQuery<DistrictDto[]>({
    queryKey: ['masters', 'districts', divisionId],
    queryFn: async () => {
      const res = await ApiService.get<DistrictDto[]>(`masters/locations/districts?divisionId=${divisionId}`);
      return res.data ?? [];
    },
    enabled: !!divisionId,
  });
}

export function useBlocks(districtId: number | null) {
  return useQuery<BlockDto[]>({
    queryKey: ['masters', 'blocks', districtId],
    queryFn: async () => {
      const res = await ApiService.get<BlockDto[]>(`masters/locations/blocks?districtId=${districtId}`);
      return res.data ?? [];
    },
    enabled: !!districtId,
  });
}

export function useWorks(projectId: number | null) {
  return useQuery<WorkDto[]>({
    queryKey: ['masters', 'works', projectId],
    queryFn: async () => {
      const res = await ApiService.get<WorkDto[]>(`masters/works?projectId=${projectId}`);
      return res.data ?? [];
    },
    enabled: !!projectId,
  });
}
