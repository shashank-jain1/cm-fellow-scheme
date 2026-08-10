import ApiService from '../../services/ApiService';
import { log, today, dateOffset } from './seedHelpers';
import type { SeedFn } from './seedMasters';

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

  const certs = [
    { applicantId: fellow.applicantId, applicantName: fellowName, programName: 'CM Fellowship Program 2026', startDate: today(), endDate: dateOffset(30), durationDays: 30, createdBy: 'admin' },
  ];

  for (const c of certs) {
    const res = await ApiService.post<number>('certificates', c);
    if (res.data) { count++; appendLog(log('Certificate', `Created certificate application for: ${c.applicantName}`, 'success')); }
  }

  const exitPayload = {
    applicantId: fellow.applicantId, completionStatus: 'Completed',
    verificationFlags: 'attendance_ok,surveys_completed', createdBy: 'admin',
  };
  const exitRes = await ApiService.post<number>('exit/readiness', exitPayload);
  if (exitRes.data) {
    count++;
    appendLog(log('Certificate', `Submitted exit readiness for: ${fellowName}`, 'success'));
  }

  return count;
};

export default seedCertificate;
