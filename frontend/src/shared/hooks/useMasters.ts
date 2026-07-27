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
      const url = stateId
        ? `masters/locations/divisions?stateId=${stateId}`
        : 'masters/locations/divisions';
      const res = await ApiService.get<DivisionDto[]>(url);
      return res.data ?? [];
    },
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

interface ProjectDto {
  projectId: number;
  projectName: string;
}

export function useProjects() {
  return useQuery<ProjectDto[]>({
    queryKey: ['masters', 'projects'],
    queryFn: async () => {
      const res = await ApiService.get<ProjectDto[]>('masters/projects');
      return res.data ?? [];
    },
  });
}

interface LookupMasterDto {
  lookupMasterId: number;
  masterType: string;
  label: string;
  value: string;
  sortOrder: number;
}

export function useLookupMasters(masterType: string) {
  return useQuery<LookupMasterDto[]>({
    queryKey: ['masters', 'lookups', masterType],
    queryFn: async () => {
      const res = await ApiService.get<LookupMasterDto[]>(`masters/lookup?masterType=${masterType}`);
      return res.data ?? [];
    },
    enabled: !!masterType,
  });
}

export interface SelectOption {
  label: string;
  value: string;
}

export function useLookupOptions(masterType: string): SelectOption[] {
  const { data } = useLookupMasters(masterType);
  return (data ?? []).map((m) => ({ label: m.label, value: m.value }));
}
