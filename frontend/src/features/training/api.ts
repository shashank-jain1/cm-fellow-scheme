import ApiService from '../../services/ApiService';
import trainingUrls from './urls';
import type { ActivityFormData, TrainingScheduleDto } from './types';

export async function fetchTrainingSchedules(): Promise<TrainingScheduleDto[]> {
  const res = await ApiService.get<TrainingScheduleDto[]>(trainingUrls.schedules());
  return res.data ?? [];
}

export async function fetchTrainingScheduleDetail(id: number): Promise<TrainingScheduleDto> {
  const res = await ApiService.get<TrainingScheduleDto>(trainingUrls.scheduleDetail(id));
  return res.data!;
}

export async function createTrainingSchedule(command: ActivityFormData): Promise<TrainingScheduleDto> {
  const res = await ApiService.post<TrainingScheduleDto>(trainingUrls.createSchedule(), command);
  return res.data!;
}

export async function updateTrainingSchedule(id: number, command: ActivityFormData): Promise<TrainingScheduleDto> {
  const res = await ApiService.put<TrainingScheduleDto>(trainingUrls.updateSchedule(id), command);
  return res.data!;
}

export async function deleteTrainingSchedule(id: number): Promise<void> {
  await ApiService.delete(trainingUrls.deleteSchedule(id));
}
