import ApiService from '../../services/ApiService';
import trainingUrls from './urls';
import type { ActivityFormData, TrainingScheduleDto } from './types';

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
