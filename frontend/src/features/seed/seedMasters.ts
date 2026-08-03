import ApiService from '../../services/ApiService';
import type { SeedLog } from './types';
import { log, today, dateOffset } from './seedHelpers';

export type SeedFn = (appendLog: (entry: SeedLog) => void) => Promise<number>;

const seedMasters: SeedFn = async (appendLog) => {
  let count = 0;

  const projects = [
    { projectName: 'Digital Governance Assessment', projectCode: 'DGA-2026', projectDescription: 'Assessment of digital governance across MP districts', departmentName: 'Dept of IT', startDate: today(), endDate: dateOffset(180), projectIncharge: 'Dr. Rajesh Kumar', budgetAmount: 5000000 },
    { projectName: 'Rural Development Survey', projectCode: 'RDS-2026', projectDescription: 'Comprehensive rural development survey in tribal areas', departmentName: 'Dept of Rural Dev', startDate: today(), endDate: dateOffset(120), projectIncharge: 'Dr. Sunita Sharma', budgetAmount: 3000000 },
  ];

  for (const p of projects) {
    const res = await ApiService.post<number>('masters/projects', p);
    if (res.data) { count++; appendLog(log('Masters', `Created project: ${p.projectName} (ID: ${res.data})`, 'success')); }
  }

  const projRes = await ApiService.get<Array<{ projectId: number; projectName: string }>>('masters/projects');
  const projectsList = projRes.data ?? [];

  const worksData = [
    { projectId: projectsList[0]?.projectId ?? 1, workName: 'Data Collection - Phase 1', workDescription: 'Primary data collection from 5 districts', priority: 'High', startDate: today(), endDate: dateOffset(60), assignedTo: 'CM Fellows', remarks: 'Priority assignment' },
    { projectId: projectsList[0]?.projectId ?? 1, workName: 'Data Analysis - Phase 1', workDescription: 'Statistical analysis of collected data', priority: 'Medium', startDate: dateOffset(30), endDate: dateOffset(90), assignedTo: 'CM Fellows', remarks: null },
    { projectId: projectsList[1]?.projectId ?? (projectsList[0]?.projectId ?? 1) + 1, workName: 'Village Survey', workDescription: 'Ground-level village survey for development index', priority: 'High', startDate: today(), endDate: dateOffset(45), assignedTo: 'Interns', remarks: 'Field work' },
    { projectId: projectsList[1]?.projectId ?? (projectsList[0]?.projectId ?? 1) + 1, workName: 'Report Compilation', workDescription: 'Compile survey findings into report', priority: 'Low', startDate: dateOffset(45), endDate: dateOffset(90), assignedTo: 'CM Fellows', remarks: null },
  ];

  for (const w of worksData) {
    const res = await ApiService.post<number>('masters/works', w);
    if (res.data) { count++; appendLog(log('Masters', `Created work: ${w.workName} (ID: ${res.data})`, 'success')); }
  }

  const blockRes = await ApiService.get<Array<{ blockId: number; districtId: number }>>('masters/locations/blocks');
  const blocks = blockRes.data ?? [];

  const gpNames = ['Rampur', 'Sitapur', 'Lakshmipur', 'Gopalpura', 'Shivnagar'];
  const firstBlockId = blocks[0]?.blockId ?? 1;
  for (const gpName of gpNames) {
    const res = await ApiService.post<number>('masters/locations/gram-panchayats', {
      blockId: firstBlockId, gramPanchayatName: gpName, gpCode: `GP-${gpName.toUpperCase().slice(0, 3)}`,
    });
    if (res.data) { count++; appendLog(log('Masters', `Created Gram Panchayat: ${gpName}`, 'success')); }
  }

  if (projectsList.length > 0) {
    const schedData = [
      { calendarYear: '2026', projectId: projectsList[0].projectId, trainingDate: dateOffset(7), venueName: 'Bhopal Training Center', trainingDescription: 'Orientation for new data collection methodology' },
      { calendarYear: '2026', projectId: projectsList[0].projectId, trainingDate: dateOffset(21), venueName: 'Online', trainingDescription: 'Advanced survey techniques workshop' },
    ];
    for (const s of schedData) {
      const res = await ApiService.post<number>('masters/training-schedules', s);
      if (res.data) { count++; appendLog(log('Masters', `Created training schedule: ${s.trainingDescription}`, 'success')); }
    }
  }

  return count;
};

export default seedMasters;
