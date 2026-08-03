import ApiService from '../../services/ApiService';
import trainingUrls from './urls';
import type { ActivityFormData, TrainingScheduleDto, TrainingCompletion, TrainingCompletionFormData, TrainingMaterial } from './types';

export async function fetchTrainingSessions(): Promise<TrainingScheduleDto[]> {
  const res = await ApiService.get<TrainingScheduleDto[]>(trainingUrls.sessions());
  return res.data ?? [];
}

export async function fetchTrainingMeetings(): Promise<TrainingScheduleDto[]> {
  const res = await ApiService.get<TrainingScheduleDto[]>(trainingUrls.meetings());
  return res.data ?? [];
}

export async function createTrainingSession(command: ActivityFormData): Promise<TrainingScheduleDto> {
  const res = await ApiService.post<TrainingScheduleDto>(trainingUrls.createSession(), command);
  return res.data!;
}

export async function createTrainingMeeting(command: ActivityFormData): Promise<TrainingScheduleDto> {
  const res = await ApiService.post<TrainingScheduleDto>(trainingUrls.createMeeting(), command);
  return res.data!;
}

export async function uploadTrainingMaterial(trainingScheduleId: number, file: File): Promise<string> {
  const formData = new FormData();
  formData.append('file', file);
  const res = await ApiService.postFormData<string>(`training/sessions/${trainingScheduleId}/material`, formData);
  return res.data ?? '';
}

export async function uploadMeetingAttachment(trainingScheduleId: number, file: File): Promise<string> {
  const formData = new FormData();
  formData.append('file', file);
  const res = await ApiService.postFormData<string>(`training/meetings/${trainingScheduleId}/attachment`, formData);
  return res.data ?? '';
}

export async function uploadMom(trainingScheduleId: number, file: File): Promise<string> {
  const formData = new FormData();
  formData.append('file', file);
  const res = await ApiService.postFormData<string>(`training/meetings/${trainingScheduleId}/mom`, formData);
  return res.data ?? '';
}

export async function updateTrainingStatus(trainingScheduleId: number, newStatus: string): Promise<void> {
  await ApiService.put<void>(`training/sessions/${trainingScheduleId}/status`, { newStatus });
}

export async function fetchTrainingCompletions(): Promise<TrainingCompletion[]> {
  const res = await ApiService.get<TrainingCompletion[]>(trainingUrls.completions());
  return res.data ?? [];
}

export async function createTrainingCompletion(command: TrainingCompletionFormData): Promise<TrainingCompletion> {
  const res = await ApiService.post<TrainingCompletion>(trainingUrls.createCompletion(), command);
  return res.data!;
}

export async function fetchTrainingMaterial(materialId: number): Promise<TrainingMaterial> {
  const res = await ApiService.get<TrainingMaterial>(trainingUrls.material(materialId));
  return res.data!;
}

export async function uploadTrainingMaterialFile(trainingScheduleId: number, file: File): Promise<TrainingMaterial> {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('trainingScheduleId', String(trainingScheduleId));
  const res = await ApiService.postFormData<TrainingMaterial>(trainingUrls.uploadMaterial(), formData);
  return res.data!;
}

export async function getMeeting(id: number): Promise<TrainingScheduleDto> {
  const res = await ApiService.get<TrainingScheduleDto>(`training/meetings/${id}`);
  return res.data!;
}

export async function updateMeeting(id: number, data: Partial<ActivityFormData>): Promise<void> {
  await ApiService.put<void>(`training/meetings/${id}`, data);
}

export async function downloadMaterial(id: number): Promise<Blob> {
  const headers: Record<string, string> = {};
  const token = localStorage.getItem('token');
  if (token) headers['Authorization'] = `Bearer ${token}`;

  const response = await fetch(`${import.meta.env.VITE_API_URL || '/api/v1'}/training/materials/${id}/download`, { headers });
  if (!response.ok) throw new Error(`Download failed: ${response.statusText}`);
  return response.blob();
}

export async function getSessionMaterials(sessionId: number): Promise<TrainingMaterial[]> {
  const res = await ApiService.get<TrainingMaterial[]>(`training/sessions/${sessionId}/materials`);
  return res.data ?? [];
}
