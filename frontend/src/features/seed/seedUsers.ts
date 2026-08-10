import ApiService from '../../services/ApiService';
import { log } from './seedHelpers';
import type { SeedFn } from './seedMasters';

const seedUsers: SeedFn = async (appendLog) => {
  let count = 0;

  const existingUsers = await ApiService.get<Array<{ username: string; applicantId: number }>>('user-accounts');
  const existingUsernames = new Set((existingUsers.data ?? []).map(u => u.username));

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

  const firstBlockId = blocks[0]?.blockId ?? 1;
  const firstDistrictId = blocks[0]?.districtId ?? 1;
  const firstGpId = gps[0]?.gramPanchayatId ?? 1;

  interface UserSeed {
    firstName: string; lastName: string; fatherName: string; email: string;
    mobile: string; username: string; password: string; role: string; dob: string;
  }

  const users: UserSeed[] = [
    { firstName: 'Priya', lastName: 'Verma', fatherName: 'Suresh Verma', email: 'priya.verma@cmfellow.gov.in', mobile: '9876543210', username: 'priya.fellow', password: 'Fellow@123', role: 'CM Fellow', dob: '1995-03-15' },
    { firstName: 'Amit', lastName: 'Patel', fatherName: 'Ramesh Patel', email: 'amit.patel@cmfellow.gov.in', mobile: '9876543211', username: 'amit.fellow', password: 'Fellow@123', role: 'CM Fellow', dob: '1994-07-22' },
    { firstName: 'Neha', lastName: 'Singh', fatherName: 'Vikram Singh', email: 'neha.singh@cmfellow.gov.in', mobile: '9876543212', username: 'neha.intern', password: 'Intern@123', role: 'Intern', dob: '1998-11-10' },
    { firstName: 'Ravi', lastName: 'Kumar', fatherName: 'Manoj Kumar', email: 'ravi.kumar@cmfellow.gov.in', mobile: '9876543213', username: 'ravi.intern', password: 'Intern@123', role: 'Intern', dob: '1999-01-25' },
    { firstName: 'Sneha', lastName: 'Reddy', fatherName: 'Anil Reddy', email: 'sneha.reddy@cmfellow.gov.in', mobile: '9876543214', username: 'sneha.guide', password: 'Guide@123', role: 'Guide', dob: '1990-06-08' },
  ];

  for (const u of users) {
    if (existingUsernames.has(u.username)) {
      appendLog(log('Users', `User ${u.username} already exists, skipping`, 'info'));
      continue;
    }

    const regPayload = {
      firstName: u.firstName, lastName: u.lastName, fatherName: u.fatherName,
      mobileNumber: u.mobile, emailId: u.email, dateOfBirth: u.dob,
      permanentAddress: `${u.firstName} Colony, Bhopal, MP`,
      divisionId: firstDivId, districtId: firstDistrictId, blockId: firstBlockId,
      gramPanchayatId: firstGpId, pinCode: '462001', appliedForTraining: 1,
      preferredTrainingLocationId: firstBlockId, qualificationId: qualId,
      boardUniversityName: 'Barkatullah University', passingYear: 2020,
      percentageCgpa: 7.5, declarationAccepted: true,
    };

    const regRes = await ApiService.post<number>('registrations', regPayload);
    if (!regRes.data) {
      appendLog(log('Users', `Failed to create applicant: ${u.firstName} ${u.lastName}`, 'error'));
      continue;
    }
    const applicantId = regRes.data;
    appendLog(log('Users', `Created applicant: ${u.firstName} ${u.lastName} (ID: ${applicantId})`, 'success'));
    count++;

    await ApiService.put<void>(`registrations/${applicantId}/approve`, { approvedBy: 1 });
    appendLog(log('Users', `Approved registration for: ${u.firstName} ${u.lastName}`, 'success'));

    const accountRes = await ApiService.post<number>('user-accounts', {
      applicantId, username: u.username, password: u.password, role: u.role, createdBy: 1,
    });
    if (accountRes.data) {
      count++;
      appendLog(log('Users', `Created account: ${u.username} (role: ${u.role})`, 'success'));
    }
  }

  return count;
};

export default seedUsers;
