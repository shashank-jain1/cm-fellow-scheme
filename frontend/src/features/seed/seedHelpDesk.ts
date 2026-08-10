import ApiService from '../../services/ApiService';
import { log } from './seedHelpers';
import type { SeedFn } from './seedMasters';

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

export default seedHelpDesk;
