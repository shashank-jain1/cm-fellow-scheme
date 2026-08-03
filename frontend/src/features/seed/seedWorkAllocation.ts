import ApiService from '../../services/ApiService';
import type { SeedLog } from './types';
import { log, today, dateOffset } from './seedHelpers';
import type { SeedFn } from './seedMasters';

const seedWorkAllocation: SeedFn = async (appendLog) => {
  let count = 0;

  const projRes = await ApiService.get<Array<{ projectId: number }>>('masters/projects');
  const projects = projRes.data ?? [];
  const workRes = await ApiService.get<Array<{ workId: number; projectId: number }>>('masters/works');
  const works = workRes.data ?? [];
  const blockRes = await ApiService.get<Array<{ blockId: number; districtId: number }>>('masters/locations/blocks');
  const blocks = blockRes.data ?? [];
  const usersRes = await ApiService.get<Array<{ applicantId: number; username: string; role: string; userAccountId: number }>>('user-accounts');
  const users = usersRes.data ?? [];

  if (projects.length === 0 || works.length === 0) {
    appendLog(log('WorkAllocation', 'No projects or works found. Seed masters first.', 'error'));
    return 0;
  }

  const firstBlockId = blocks[0]?.blockId ?? 1;
  const firstDistrictId = blocks[0]?.districtId ?? 1;
  const firstWorkId = works[0]?.workId ?? 1;

  const allocations = [
    { projectId: projects[0].projectId, workProjectId: String(firstWorkId), workDescription: 'Data collection in Bhopal district - household surveys', priority: 'High', startDate: today(), endDate: dateOffset(60), surveysPerIntern: 50, divisionId: 1, districtId: firstDistrictId, blockId: firstBlockId, status: 'Assigned', activeStatus: true, createdBy: 'admin', durationDays: 60 },
    { projectId: projects[0].projectId, workProjectId: String(works[1]?.workId ?? firstWorkId), workDescription: 'Data analysis and reporting for Phase 1', priority: 'Medium', startDate: dateOffset(30), endDate: dateOffset(90), surveysPerIntern: 0, divisionId: 1, districtId: firstDistrictId, blockId: firstBlockId, status: 'Assigned', activeStatus: true, createdBy: 'admin', durationDays: 60 },
  ];

  const createdAllocationIds: number[] = [];
  for (const a of allocations) {
    const res = await ApiService.post<number>('work-allocations', a);
    if (res.data) {
      count++;
      createdAllocationIds.push(res.data);
      appendLog(log('WorkAllocation', `Created work allocation (ID: ${res.data})`, 'success'));
    }
  }

  const fellows = users.filter(u => u.role === 'CM Fellow');
  if (fellows.length > 0 && createdAllocationIds.length > 0) {
    const assignRes = await ApiService.put<void>(`work-allocations/${createdAllocationIds[0]}/assign`, {
      workAllocationId: createdAllocationIds[0], assignedToUserId: fellows[0].userAccountId,
    });
    if (!assignRes.error) {
      appendLog(log('WorkAllocation', `Assigned allocation to ${fellows[0].username}`, 'success'));
    }
  }

  const taskPayloads = [
    { workAllocationId: createdAllocationIds[0] ?? 1, projectName: projects[0]?.projectName ?? 'Project', workProject: 'Data Collection', workDescription: 'Household survey data collection', priority: 'High', numberOfSurveys: 50, completedSurveys: 30, completionPercentage: 60, workStatus: 'In Progress' },
  ];

  const createdTaskIds: number[] = [];
  for (const t of taskPayloads) {
    const res = await ApiService.post<number>('task-progresses', t);
    if (res.data) {
      count++;
      createdTaskIds.push(res.data);
      appendLog(log('WorkAllocation', `Created task progress (ID: ${res.data})`, 'success'));
    }
  }

  if (createdTaskIds.length > 0) {
    const surveys = [
      { applicantId: users.find(u => u.role === 'Intern')?.applicantId ?? 1, surveyPersonName: 'Ramesh Tiwari', mobileNumber: '9800100001', panchayatName: 'Rampur', villageName: 'Rampur Kalan', latitude: 23.2599, longitude: 77.4126 },
      { applicantId: users.find(u => u.role === 'Intern')?.applicantId ?? 1, surveyPersonName: 'Suresh Yadav', mobileNumber: '9800100002', panchayatName: 'Sitapur', villageName: 'Sitapur Khurd', latitude: 23.2610, longitude: 77.4200 },
      { applicantId: users.find(u => u.role === 'Intern')?.applicantId ?? 1, surveyPersonName: 'Kamla Devi', mobileNumber: '9800100003', panchayatName: 'Lakshmipur', villageName: 'Lakshmipur', latitude: 23.2500, longitude: 77.4050 },
      { applicantId: users.find(u => u.role === 'Intern')?.applicantId ?? 1, surveyPersonName: 'Harish Patel', mobileNumber: '9800100004', panchayatName: 'Gopalpura', villageName: 'Gopalpura', latitude: 23.2700, longitude: 77.4300 },
    ];

    for (const s of surveys) {
      const res = await ApiService.post<void>(`task-progresses/${createdTaskIds[0]}/survey`, s);
      if (!res.error) { count++; appendLog(log('WorkAllocation', `Recorded survey: ${s.surveyPersonName}`, 'success')); }
    }
  }

  return count;
};

export default seedWorkAllocation;
