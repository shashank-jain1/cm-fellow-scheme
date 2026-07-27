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
