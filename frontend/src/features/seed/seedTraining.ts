import ApiService from '../../services/ApiService';
import { log, dateOffset, dateTime } from './seedHelpers';
import type { SeedFn } from './seedMasters';

const seedTraining: SeedFn = async (appendLog) => {
  let count = 0;

  const projRes = await ApiService.get<Array<{ projectId: number }>>('masters/projects');
  const projects = projRes.data ?? [];
  if (projects.length === 0) {
    appendLog(log('Training', 'No projects found. Seed masters first.', 'error'));
    return 0;
  }

  const firstProjectId = projects[0].projectId;

  const sessions = [
    { projectId: firstProjectId, workProjectId: 0, trainingTitle: 'Survey Methodology Training', trainingCategory: 'Technical', trainingDescription: 'Hands-on training on mobile-based survey tools', date: dateTime(dateOffset(3), 10, 0), startTime: dateTime(dateOffset(3), 10, 0), endTime: dateTime(dateOffset(3), 16, 0), mode: 'Offline', applicableDivisionIds: [], applicableDistrictIds: [], applicableBlockIds: [], trainerName: 'Dr. Rajesh Kumar', trainerMobile: '9999000001', attendanceRequired: true, certificateRequired: true, targetUserTypes: ['Intern', 'Fellow'], remarks: 'Mandatory for all new joiners' },
    { projectId: firstProjectId, workProjectId: 0, trainingTitle: 'Data Analysis Workshop', trainingCategory: 'Technical', trainingDescription: 'Excel and SPSS for data analysis', date: dateTime(dateOffset(14), 10, 0), startTime: dateTime(dateOffset(14), 10, 0), endTime: dateTime(dateOffset(14), 17, 0), mode: 'Hybrid', applicableDivisionIds: [], applicableDistrictIds: [], applicableBlockIds: [], trainerName: 'Prof. Meena Gupta', trainerMobile: '9999000002', attendanceRequired: true, certificateRequired: false, targetUserTypes: ['Fellow'], remarks: null },
    { projectId: firstProjectId, workProjectId: 0, trainingTitle: 'Policy Writing Skills', trainingCategory: 'Soft Skills', trainingDescription: 'Writing effective policy briefs and reports', date: dateTime(dateOffset(21), 14, 0), startTime: dateTime(dateOffset(21), 14, 0), endTime: dateTime(dateOffset(21), 17, 0), mode: 'Online', applicableDivisionIds: [], applicableDistrictIds: [], applicableBlockIds: [], trainerName: 'Dr. Amit Deshmukh', trainerMobile: '9999000003', attendanceRequired: false, certificateRequired: false, targetUserTypes: ['Intern', 'Fellow'], remarks: 'Optional but recommended' },
  ];

  for (const s of sessions) {
    const res = await ApiService.post<number>('training/sessions', s);
    if (res.data) { count++; appendLog(log('Training', `Created training: ${s.trainingTitle}`, 'success')); }
  }

  const meetings = [
    { projectId: firstProjectId, workProjectId: 0, meetingTitle: 'Weekly Progress Review', meetingAgenda: 'Review', meetingDescription: 'Weekly sync on project progress', conductPersonId: 1, coordinatorId: 1, participantIds: [], date: dateTime(dateOffset(2), 11, 0), startTime: dateTime(dateOffset(2), 11, 0), endTime: dateTime(dateOffset(2), 12, 0), mode: 'Offline', applicableDivisionIds: [], applicableDistrictIds: [], applicableBlockIds: [], momRequired: true, remarks: 'Bring progress reports' },
    { projectId: firstProjectId, workProjectId: 0, meetingTitle: 'Project Kickoff Meeting', meetingAgenda: 'Planning', meetingDescription: 'Initial planning meeting for DGA project', conductPersonId: 1, coordinatorId: 1, participantIds: [], date: dateTime(dateOffset(1), 10, 0), startTime: dateTime(dateOffset(1), 10, 0), endTime: dateTime(dateOffset(1), 11, 30), mode: 'Offline', applicableDivisionIds: [], applicableDistrictIds: [], applicableBlockIds: [], momRequired: true, remarks: null },
  ];

  for (const m of meetings) {
    const res = await ApiService.post<number>('training/meetings', m);
    if (res.data) { count++; appendLog(log('Training', `Created meeting: ${m.meetingTitle}`, 'success')); }
  }

  return count;
};

export default seedTraining;
