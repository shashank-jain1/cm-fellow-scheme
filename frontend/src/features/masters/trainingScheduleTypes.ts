export interface TrainingScheduleDto {
  trainingScheduleId: number;
  calendarYear: string;
  projectId: number;
  projectName: string;
  workId: number | null;
  workName: string | null;
  divisionId: number | null;
  divisionName: string | null;
  districtId: number | null;
  districtName: string | null;
  blockId: number | null;
  blockName: string | null;
  trainingDate: string;
  venueName: string | null;
  trainingDescription: string | null;
  isActive: boolean;
}

export interface CreateTrainingScheduleCommand {
  calendarYear: string;
  projectId: number;
  workId?: number;
  divisionId?: number;
  districtId?: number;
  blockId?: number;
  trainingDate: string;
  venueName?: string;
  trainingDescription?: string;
}

export interface UpdateTrainingScheduleCommand {
  trainingScheduleId: number;
  calendarYear: string;
  projectId: number;
  workId?: number;
  divisionId?: number;
  districtId?: number;
  blockId?: number;
  trainingDate: string;
  venueName?: string;
  trainingDescription?: string;
}
