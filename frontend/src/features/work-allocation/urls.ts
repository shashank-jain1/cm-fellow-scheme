export const WORK_ALLOCATION_URLS = {
  LIST: '/work-allocation',
  CREATE: '/work-allocation/create',
  EDIT: (id: number) => `/work-allocation/edit/${id}`,
  TASK_PROGRESS: '/work-allocation/task-progress',
  TASK_PROGRESS_BY_PROJECT: (projectId: number) => `/work-allocation/task-progress/${projectId}`,
};
