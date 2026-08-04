export {
  useStates,
  useDivisions,
  useDistricts,
  useBlocks,
  useGramPanchayats,
} from './useLocationQueries';

export {
  useCreateState,
  useCreateDivision,
  useCreateDistrict,
  useCreateBlock,
  useCreateGramPanchayat,
} from './useProjectQueries';

export {
  useProjects,
  useWorks,
  useCreateProject,
  useUpdateProject,
  useDeleteProject,
  useCreateWork,
  useGetWork,
  useUpdateWork,
} from './useWorkQueries';

export {
  useTrainingSchedules,
  useTrainingSchedule,
  useCreateTrainingSchedule,
  useUpdateTrainingSchedule,
} from './useTrainingScheduleQueries';
