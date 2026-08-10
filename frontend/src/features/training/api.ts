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

/** The API answers with the new schedule id, not the full record. */
export async function createTrainingSession(command: ActivityFormData): Promise<number> {
  const res = await ApiService.post<number>(trainingUrls.createSession(), command);
  return res.data!;
}

export async function createTrainingMeeting(command: ActivityFormData): Promise<number> {
  const res = await ApiService.post<number>(trainingUrls.createMeeting(), command);
  return res.data!;
}

export async function uploadTrainingMaterial(trainingScheduleId: number, file: File): Promise<string> {
  const formData = new FormData();
  formData.append('file', file);
  const res = await ApiService.postFormData<string>(trainingUrls.sessionMaterials(trainingScheduleId), formData);
  return res.data ?? '';
}

export async function uploadMeetingAttachment(trainingScheduleId: number, file: File): Promise<string> {
  const formData = new FormData();
  formData.append('file', file);
  const res = await ApiService.postFormData<string>(trainingUrls.meetingAttachment(trainingScheduleId), formData);
  return res.data ?? '';
}

export async function uploadMom(trainingScheduleId: number, file: File): Promise<string> {
  const formData = new FormData();
  formData.append('file', file);
  const res = await ApiService.postFormData<string>(trainingUrls.meetingMom(trainingScheduleId), formData);
  return res.data ?? '';
}

export async function updateTrainingStatus(trainingScheduleId: number, newStatus: string): Promise<void> {
  await ApiService.put<void>(trainingUrls.sessionStatus(trainingScheduleId), { newStatus });
}

export async function fetchTrainingCompletions(): Promise<TrainingCompletion[]> {
  const res = await ApiService.get<TrainingCompletion[]>(trainingUrls.completions());
  return res.data ?? [];
}

export async function createTrainingCompletion(command: TrainingCompletionFormData): Promise<TrainingCompletion> {
  const res = await ApiService.post<TrainingCompletion>(trainingUrls.createCompletion(), command);
  return res.data!;
}

export async function fetchSessionMaterials(trainingScheduleId: number): Promise<TrainingMaterial[]> {
  const res = await ApiService.get<TrainingMaterial[]>(trainingUrls.sessionMaterials(trainingScheduleId));
  return res.data ?? [];
}

export async function getMeeting(id: number): Promise<TrainingScheduleDto> {
  const res = await ApiService.get<TrainingScheduleDto>(trainingUrls.meetingDetail(id));
  return res.data!;
}

export async function updateMeeting(id: number, data: Partial<ActivityFormData>): Promise<void> {
  await ApiService.put<void>(trainingUrls.meetingDetail(id), data);
}

/** Streams the stored file through the API so the bearer token is applied. */
export async function downloadMaterial(materialId: number, fileName: string): Promise<void> {
  await ApiService.getBlob(trainingUrls.downloadMaterial(materialId), fileName);
}
