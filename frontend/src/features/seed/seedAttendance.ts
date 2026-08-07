import ApiService from '../../services/ApiService';
import type { SeedLog } from './types';
import { log, dateOffset } from './seedHelpers';
import type { SeedFn } from './seedMasters';

const seedAttendance: SeedFn = async (appendLog) => {
  let count = 0;

  const usersRes = await ApiService.get<Array<{ applicantId: number; username: string; role: string }>>('user-accounts');
  const users = (usersRes.data ?? []).filter(u => u.role !== 'Admin');
  if (users.length === 0) {
    appendLog(log('Attendance', 'No non-admin users found. Seed users first.', 'error'));
    return 0;
  }

  const fellowApplicantId = users[0]?.applicantId ?? 1;
  const internApplicantId = users.find(u => u.role === 'Intern')?.applicantId ?? fellowApplicantId;

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

  for (let i = 5; i >= 1; i--) {
    const dateStr = dateOffset(-i);
    const day = new Date(dateStr).getDay();
    if (day === 0 || day === 6) continue;

    const payload = {
      applicantId: fellowApplicantId,
      faceImageBase64: 'c2VlZC1mYWNlLWRhdGE=', // placeholder base64
      latitude: 23.2599, longitude: 77.4126,
    };
    const res = await ApiService.post<number>('attendance', payload);
    if (res.data) { count++; appendLog(log('Attendance', `Marked attendance for fellow on ${dateStr}`, 'success')); }
  }

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

export default seedAttendance;
