import ApiService from '../../services/ApiService';
import type { SeedLog } from './types';

const log = (module: string, message: string, type: SeedLog['type'] = 'info'): SeedLog => ({
  module,
  message,
  type,
  timestamp: new Date(),
});

export type SeedFn = (appendLog: (entry: SeedLog) => void) => Promise<number>;

export interface SeedModule {
  key: string;
  label: string;
  icon: string;
  description: string;
  seed: SeedFn;
  dependsOn?: string[];
}

/* ── Helpers ── */

function today(): string {
  return new Date().toISOString().split('T')[0];
}

function dateOffset(days: number): string {
  const d = new Date();
  d.setDate(d.getDate() + days);
  return d.toISOString().split('T')[0];
}

function dateTime(dateStr: string, hours: number, minutes: number): string {
  return `${dateStr}T${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:00`;
}

function timeOnly(hours: number, minutes: number): string {
  return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:00`;
}

/* ── Module seed functions ── */

const seedMasters: SeedFn = async (appendLog) => {
  let count = 0;

  // Projects
  const projects = [
    { projectName: 'Digital Governance Assessment', projectCode: 'DGA-2026', projectDescription: 'Assessment of digital governance across MP districts', departmentName: 'Dept of IT', startDate: today(), endDate: dateOffset(180), projectIncharge: 'Dr. Rajesh Kumar', budgetAmount: 5000000 },
    { projectName: 'Rural Development Survey', projectCode: 'RDS-2026', projectDescription: 'Comprehensive rural development survey in tribal areas', departmentName: 'Dept of Rural Dev', startDate: today(), endDate: dateOffset(120), projectIncharge: 'Dr. Sunita Sharma', budgetAmount: 3000000 },
  ];

  for (const p of projects) {
    const res = await ApiService.post<number>('masters/projects', p);
    if (res.data) { count++; appendLog(log('Masters', `Created project: ${p.projectName} (ID: ${res.data})`, 'success')); }
  }

  // Get created projects
  const projRes = await ApiService.get<Array<{ projectId: number; projectName: string }>>('masters/projects');
  const projectsList = projRes.data ?? [];

  // Works
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

  // Get existing blocks
  const blockRes = await ApiService.get<Array<{ blockId: number; districtId: number }>>('masters/locations/blocks');
  const blocks = blockRes.data ?? [];

  // Gram Panchayats (5 in first block)
  const gpNames = ['Rampur', 'Sitapur', 'Lakshmipur', 'Gopalpura', 'Shivnagar'];
  const firstBlockId = blocks[0]?.blockId ?? 1;
  for (const gpName of gpNames) {
    const res = await ApiService.post<number>('masters/locations/gram-panchayats', {
      blockId: firstBlockId,
      gramPanchayatName: gpName,
      gpCode: `GP-${gpName.toUpperCase().slice(0, 3)}`,
    });
    if (res.data) { count++; appendLog(log('Masters', `Created Gram Panchayat: ${gpName}`, 'success')); }
  }

  // Training schedules (masters)
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

const seedUsers: SeedFn = async (appendLog) => {
  let count = 0;

  // Get existing user accounts to avoid duplicates
  const existingUsers = await ApiService.get<Array<{ username: string; applicantId: number }>>('user-accounts');
  const existingUsernames = new Set((existingUsers.data ?? []).map(u => u.username));
  const existingApplicantIds = new Set((existingUsers.data ?? []).map(u => u.applicantId));

  // Get locations
  const blockRes = await ApiService.get<Array<{ blockId: number; districtId: number; districtName: string }>>('masters/locations/blocks');
  const blocks = blockRes.data ?? [];
  const gpRes = await ApiService.get<Array<{ gramPanchayatId: number; blockId: number }>>('masters/locations/gram-panchayats');
  const gps = gpRes.data ?? [];

  const qualificationRes = await ApiService.get<Array<{ lookupMasterId: number }>>('masters/lookup?masterType=Qualification');
  const quals = qualificationRes.data ?? [];
  const qualId = quals[0]?.lookupMasterId ?? 1;

  const divisionRes = await ApiService.get<Array<{ divisionId: number }>>('masters/locations/divisions');
  const divisions = divisionRes.data ?? [];
  const firstDivId = divisions[0]?.divisionId ?? 1;

  const blockForDiv = blocks.find(b => {
    const dist = blocks.find(bl => bl.blockId === b.blockId);
    return dist != null;
  });
  const firstBlockId = blockForDiv?.blockId ?? blocks[0]?.blockId ?? 1;
  const firstDistrictId = blockForDiv?.districtId ?? blocks[0]?.districtId ?? 1;

  const firstGpId = gps[0]?.gramPanchayatId ?? 1;

  interface UserSeed {
    firstName: string;
    lastName: string;
    fatherName: string;
    email: string;
    mobile: string;
    username: string;
    password: string;
    role: string;
    dob: string;
  }

  const users: UserSeed[] = [
    { firstName: 'Priya', lastName: 'Verma', fatherName: 'Suresh Verma', email: 'priya.verma@cmfellow.gov.in', mobile: '9876543210', username: 'priya.fellow', password: 'Fellow@123', role: 'CM Fellow', dob: '1995-03-15' },
    { firstName: 'Amit', lastName: 'Patel', fatherName: 'Ramesh Patel', email: 'amit.patel@cmfellow.gov.in', mobile: '9876543211', username: 'amit.fellow', password: 'Fellow@123', role: 'CM Fellow', dob: '1994-07-22' },
    { firstName: 'Neha', lastName: 'Singh', fatherName: 'Vikram Singh', email: 'neha.singh@cmfellow.gov.in', mobile: '9876543212', username: 'neha.intern', password: 'Intern@123', role: 'Intern', dob: '1998-11-10' },
    { firstName: 'Ravi', lastName: 'Kumar', fatherName: 'Manoj Kumar', email: 'ravi.kumar@cmfellow.gov.in', mobile: '9876543213', username: 'ravi.intern', password: 'Intern@123', role: 'Intern', dob: '1999-01-25' },
    { firstName: 'Sneha', lastName: 'Reddy', fatherName: 'Anil Reddy', email: 'sneha.reddy@cmfellow.gov.in', mobile: '9876543214', username: 'sneha.guide', password: 'Guide@123', role: 'Guide', dob: '1990-06-08' },
  ];

  const createdUserIds: Array<{ applicantId: number; username: string; role: string }> = [];

  for (const u of users) {
    if (existingUsernames.has(u.username)) {
      appendLog(log('Users', `User ${u.username} already exists, skipping`, 'info'));
      const existing = (existingUsers.data ?? []).find(eu => eu.username === u.username);
      if (existing) createdUserIds.push({ applicantId: existing.applicantId, username: u.username, role: u.role });
      continue;
    }

    // Create applicant via registration
    const regPayload = {
      firstName: u.firstName,
      lastName: u.lastName,
      fatherName: u.fatherName,
      mobileNumber: u.mobile,
      emailId: u.email,
      dateOfBirth: u.dob,
      permanentAddress: `${u.firstName} Colony, Bhopal, MP`,
      divisionId: firstDivId,
      districtId: firstDistrictId,
      blockId: firstBlockId,
      gramPanchayatId: firstGpId,
      pinCode: '462001',
      appliedForTraining: 1,
      preferredTrainingLocationId: firstBlockId,
      qualificationId: qualId,
      boardUniversityName: 'Barkatullah University',
      passingYear: 2020,
      percentageCgpa: 7.5,
      declarationAccepted: true,
    };

    const regRes = await ApiService.post<number>('registrations', regPayload);
    if (!regRes.data) {
      appendLog(log('Users', `Failed to create applicant: ${u.firstName} ${u.lastName}`, 'error'));
      continue;
    }
    const applicantId = regRes.data;
    appendLog(log('Users', `Created applicant: ${u.firstName} ${u.lastName} (ID: ${applicantId})`, 'success'));
    count++;

    // Approve the registration
    await ApiService.put<void>(`registrations/${applicantId}/approve`, { approvedBy: 1 });
    appendLog(log('Users', `Approved registration for: ${u.firstName} ${u.lastName}`, 'success'));

    // Create user account
    const accountRes = await ApiService.post<number>('user-accounts', {
      applicantId,
      username: u.username,
      password: u.password,
      role: u.role,
      createdBy: 1,
    });
    if (accountRes.data) {
      count++;
      appendLog(log('Users', `Created account: ${u.username} (role: ${u.role})`, 'success'));
      createdUserIds.push({ applicantId, username: u.username, role: u.role });
    }
  }

  return count;
};

const seedAttendance: SeedFn = async (appendLog) => {
  let count = 0;

  // Get user accounts to find applicantIds
  const usersRes = await ApiService.get<Array<{ applicantId: number; username: string; role: string }>>('user-accounts');
  const users = (usersRes.data ?? []).filter(u => u.role !== 'Admin');
  if (users.length === 0) {
    appendLog(log('Attendance', 'No non-admin users found. Seed users first.', 'error'));
    return 0;
  }

  const fellowApplicantId = users[0]?.applicantId ?? 1;
  const internApplicantId = users.find(u => u.role === 'Intern')?.applicantId ?? fellowApplicantId;

  // Holidays
  const holidays = [
    { holidayName: 'Republic Day', holidayDate: '2026-01-26', description: 'National holiday', isOptional: false },
    { holidayName: 'Independence Day', holidayDate: '2026-08-15', description: 'National holiday', isOptional: false },
    { holidayName: 'Gandhi Jayanti', holidayDate: '2026-10-02', description: 'National holiday', isOptional: false },
    { holidayName: 'Diwali', holidayDate: '2026-10-20', description: 'Festival holiday', isOptional: true },
    { holidayName: 'Holi', holidayDate: '2026-03-10', description: 'Festival holiday', isOptional: true },
  ];

  for (const h of holidays) {
    const res = await ApiService.post<number>('holidays', h);
    if (res.data) { count++; appendLog(log('Attendance', `Created holiday: ${h.holidayName}`, 'success')); }
  }

  // Attendance records (last 5 working days)
  for (let i = 5; i >= 1; i--) {
    const dateStr = dateOffset(-i);
    const day = new Date(dateStr).getDay();
    if (day === 0 || day === 6) continue; // skip weekends

    const payload = {
      applicantId: fellowApplicantId,
      attendanceDate: dateStr,
      checkInTime: timeOnly(9, 30),
      checkOutTime: timeOnly(18, 0),
      captureFacePath: 'seed/face.jpg',
      faceVerificationStatus: 'Verified',
      latitude: 23.2599,
      longitude: 77.4126,
      attendanceStatus: 'Present',
    };
    const res = await ApiService.post<number>('attendance', payload);
    if (res.data) { count++; appendLog(log('Attendance', `Marked attendance for fellow on ${dateStr}`, 'success')); }
  }

  // Leave applications
  const leaveData = [
    { applicantId: fellowApplicantId, leaveType: 'Casual Leave', fromDate: dateOffset(10), toDate: dateOffset(10), numberOfDays: 1, halfDayFullDay: 'Full Day', leaveReason: 'Personal work', reportingManagerName: 'Dr. Rajesh Kumar' },
    { applicantId: internApplicantId, leaveType: 'Sick Leave', fromDate: dateOffset(5), toDate: dateOffset(5), numberOfDays: 1, halfDayFullDay: 'Full Day', leaveReason: 'Medical appointment', reportingManagerName: 'Dr. Rajesh Kumar' },
    { applicantId: fellowApplicantId, leaveType: 'Casual Leave', fromDate: dateOffset(15), toDate: dateOffset(16), numberOfDays: 2, halfDayFullDay: 'Full Day', leaveReason: 'Family function', reportingManagerName: 'Dr. Sunita Sharma' },
  ];

  for (const l of leaveData) {
    const res = await ApiService.post<number>('leave', { ...l, createdBy: 'admin' });
    if (res.data) { count++; appendLog(log('Attendance', `Created leave application: ${l.leaveType} for ${l.fromDate}`, 'success')); }
  }

  return count;
};

const seedTraining: SeedFn = async (appendLog) => {
  let count = 0;

  // Get projects
  const projRes = await ApiService.get<Array<{ projectId: number }>>('masters/projects');
  const projects = projRes.data ?? [];
  if (projects.length === 0) {
    appendLog(log('Training', 'No projects found. Seed masters first.', 'error'));
    return 0;
  }

  const firstProjectId = projects[0].projectId;

  // Training sessions
  const sessions = [
    { projectId: firstProjectId, workProjectId: 0, trainingTitle: 'Survey Methodology Training', trainingCategory: 'Technical', trainingDescription: 'Hands-on training on mobile-based survey tools', date: dateTime(dateOffset(3), 10, 0), startTime: dateTime(dateOffset(3), 10, 0), endTime: dateTime(dateOffset(3), 16, 0), mode: 'Offline', applicableDivisionIds: [], applicableDistrictIds: [], applicableBlockIds: [], trainerName: 'Dr. Rajesh Kumar', trainerMobile: '9999000001', attendanceRequired: true, certificateRequired: true, targetUserTypes: ['Intern', 'Fellow'], remarks: 'Mandatory for all new joiners' },
    { projectId: firstProjectId, workProjectId: 0, trainingTitle: 'Data Analysis Workshop', trainingCategory: 'Technical', trainingDescription: 'Excel and SPSS for data analysis', date: dateTime(dateOffset(14), 10, 0), startTime: dateTime(dateOffset(14), 10, 0), endTime: dateTime(dateOffset(14), 17, 0), mode: 'Hybrid', applicableDivisionIds: [], applicableDistrictIds: [], applicableBlockIds: [], trainerName: 'Prof. Meena Gupta', trainerMobile: '9999000002', attendanceRequired: true, certificateRequired: false, targetUserTypes: ['Fellow'], remarks: null },
    { projectId: firstProjectId, workProjectId: 0, trainingTitle: 'Policy Writing Skills', trainingCategory: 'Soft Skills', trainingDescription: 'Writing effective policy briefs and reports', date: dateTime(dateOffset(21), 14, 0), startTime: dateTime(dateOffset(21), 14, 0), endTime: dateTime(dateOffset(21), 17, 0), mode: 'Online', applicableDivisionIds: [], applicableDistrictIds: [], applicableBlockIds: [], trainerName: 'Dr. Amit Deshmukh', trainerMobile: '9999000003', attendanceRequired: false, certificateRequired: false, targetUserTypes: ['Intern', 'Fellow'], remarks: 'Optional but recommended' },
  ];

  for (const s of sessions) {
    const res = await ApiService.post<number>('training/sessions', s);
    if (res.data) { count++; appendLog(log('Training', `Created training: ${s.trainingTitle}`, 'success')); }
  }

  // Meetings
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

const seedWorkAllocation: SeedFn = async (appendLog) => {
  let count = 0;

  // Get dependencies
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

  // Create work allocations
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

  // Assign to users if we have fellows/interns
  const fellows = users.filter(u => u.role === 'CM Fellow');
  if (fellows.length > 0 && createdAllocationIds.length > 0) {
    const assignRes = await ApiService.put<void>(`work-allocations/${createdAllocationIds[0]}/assign`, {
      workAllocationId: createdAllocationIds[0],
      assignedToUserId: fellows[0].userAccountId,
    });
    if (!assignRes.error) {
      appendLog(log('WorkAllocation', `Assigned allocation to ${fellows[0].username}`, 'success'));
    }
  }

  // Create task progresses
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

  // Record survey submissions
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

const seedPerformance: SeedFn = async (appendLog) => {
  let count = 0;

  // Performance is auto-created from work allocation + survey data
  // We can only record ratings and remarks on existing evaluations
  const perfRes = await ApiService.get<Array<{ performanceEvaluationId: number }>>('performance/list');
  const evals = perfRes.data ?? [];

  if (evals.length === 0) {
    appendLog(log('Performance', 'No performance evaluations found. They are auto-created from work allocation data.', 'info'));
    return 0;
  }

  for (const e of evals) {
    // Record supervisor rating
    await ApiService.put<void>('performance/rating', {
      performanceEvaluationId: e.performanceEvaluationId,
      supervisorRating: 7.5,
      evaluatedBy: 'Admin',
    });
    count++;
    appendLog(log('Performance', `Recorded rating for evaluation ${e.performanceEvaluationId}`, 'success'));

    // Record remarks
    await ApiService.put<void>('performance/remarks', {
      performanceEvaluationId: e.performanceEvaluationId,
      evaluationRemarks: 'Good performance. Consistent attendance and quality work.',
      evaluatedBy: 'Admin',
    });
    appendLog(log('Performance', `Recorded remarks for evaluation ${e.performanceEvaluationId}`, 'success'));

    // Calculate score
    await ApiService.post<void>(`performance/${e.performanceEvaluationId}/calculate-score`, {});
    appendLog(log('Performance', `Calculated score for evaluation ${e.performanceEvaluationId}`, 'success'));
  }

  return count;
};

const seedCertificate: SeedFn = async (appendLog) => {
  let count = 0;

  const usersRes = await ApiService.get<Array<{ applicantId: number; firstName: string; lastName: string; role: string }>>('user-accounts');
  const users = (usersRes.data ?? []).filter(u => u.role !== 'Admin');
  if (users.length === 0) {
    appendLog(log('Certificate', 'No non-admin users found. Seed users first.', 'error'));
    return 0;
  }

  const fellow = users.find(u => u.role === 'CM Fellow') ?? users[0];
  const fellowName = `${fellow.firstName} ${fellow.lastName}`;

  // Certificate applications
  const certs = [
    { applicantId: fellow.applicantId, applicantName: fellowName, programName: 'CM Fellowship Program 2026', startDate: today(), endDate: dateOffset(30), durationDays: 30, createdBy: 'admin' },
  ];

  for (const c of certs) {
    const res = await ApiService.post<number>('certificates', c);
    if (res.data) { count++; appendLog(log('Certificate', `Created certificate application for: ${c.applicantName}`, 'success')); }
  }

  // Exit readiness
  const exitPayload = {
    applicantId: fellow.applicantId,
    completionStatus: 'Completed',
    verificationFlags: 'attendance_ok,surveys_completed',
    createdBy: 'admin',
  };
  const exitRes = await ApiService.post<number>('exit/readiness', exitPayload);
  if (exitRes.data) {
    count++;
    appendLog(log('Certificate', `Submitted exit readiness for: ${fellowName}`, 'success'));
  }

  return count;
};

const seedHelpDesk: SeedFn = async (appendLog) => {
  let count = 0;

  const usersRes = await ApiService.get<Array<{ applicantId: number; emailId: string; mobileNumber: string; role: string; firstName: string }>>('user-accounts');
  const users = (usersRes.data ?? []).filter(u => u.role !== 'Admin');
  if (users.length === 0) {
    appendLog(log('HelpDesk', 'No non-admin users found. Seed users first.', 'error'));
    return 0;
  }

  const tickets = [
    { applicantId: users[0].applicantId, email: users[0].emailId, mobile: users[0].mobileNumber, issueCategory: 'Technical Issue', issueDescription: 'Unable to access the survey application on mobile device. The app crashes when opening camera for face verification.', priority: 'High' },
    { applicantId: users[1]?.applicantId ?? users[0].applicantId, email: users[1]?.emailId ?? users[0].emailId, mobile: users[1]?.mobileNumber ?? users[0].mobileNumber, issueCategory: 'Account Access', issueDescription: 'Forgot password and unable to reset via email. Need urgent access for pending surveys.', priority: 'Medium' },
    { applicantId: users[0].applicantId, email: users[0].emailId, mobile: users[0].mobileNumber, issueCategory: 'Survey Problem', issueDescription: 'GPS coordinates are not being captured accurately in rural areas. Affecting survey data quality.', priority: 'High' },
    { applicantId: users[2]?.applicantId ?? users[0].applicantId, email: users[2]?.emailId ?? users[0].emailId, mobile: users[2]?.mobileNumber ?? users[0].mobileNumber, issueCategory: 'Attendance Issue', issueDescription: 'Attendance marked but not reflected in payroll summary for last week.', priority: 'Low' },
  ];

  for (const t of tickets) {
    const res = await ApiService.post<number>('tickets', t);
    if (res.data) { count++; appendLog(log('HelpDesk', `Created ticket: ${t.issueCategory} (${t.priority})`, 'success')); }
  }

  return count;
};

/* ── Module definitions ── */

export const SEED_MODULES: SeedModule[] = [
  {
    key: 'masters',
    label: 'Masters',
    icon: 'pi pi-database',
    description: 'Projects, Works, Gram Panchayats, Training Schedules',
    seed: seedMasters,
  },
  {
    key: 'users',
    label: 'Users',
    icon: 'pi pi-users',
    description: 'Fellows, Interns, Guides with Applicant + UserAccount records',
    seed: seedUsers,
  },
  {
    key: 'attendance',
    label: 'Attendance & Leave',
    icon: 'pi pi-clock',
    description: 'Holidays, attendance records, leave applications',
    seed: seedAttendance,
    dependsOn: ['users'],
  },
  {
    key: 'training',
    label: 'Training',
    icon: 'pi pi-calendar',
    description: 'Training sessions and meetings',
    seed: seedTraining,
    dependsOn: ['masters'],
  },
  {
    key: 'workAllocation',
    label: 'Work Allocation',
    icon: 'pi pi-briefcase',
    description: 'Work allocations, task progress, survey records',
    seed: seedWorkAllocation,
    dependsOn: ['masters', 'users'],
  },
  {
    key: 'performance',
    label: 'Performance',
    icon: 'pi pi-chart-bar',
    description: 'Supervisor ratings, remarks, score calculations',
    seed: seedPerformance,
    dependsOn: ['workAllocation'],
  },
  {
    key: 'certificate',
    label: 'Certificate & Exit',
    icon: 'pi pi-verified',
    description: 'Certificate applications and exit readiness',
    seed: seedCertificate,
    dependsOn: ['users'],
  },
  {
    key: 'helpDesk',
    label: 'Help Desk',
    icon: 'pi pi-question-circle',
    description: 'Support tickets with various priorities',
    seed: seedHelpDesk,
    dependsOn: ['users'],
  },
];
