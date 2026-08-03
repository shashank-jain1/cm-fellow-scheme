import type { SeedLog } from './types';
import seedMasters from './seedMasters';
import seedUsers from './seedUsers';
import seedAttendance from './seedAttendance';
import seedTraining from './seedTraining';
import seedWorkAllocation from './seedWorkAllocation';
import seedPerformance from './seedPerformance';
import seedCertificate from './seedCertificate';
import seedHelpDesk from './seedHelpDesk';

export type { SeedFn } from './seedMasters';

export interface SeedModule {
  key: string;
  label: string;
  icon: string;
  description: string;
  seed: SeedFn;
  dependsOn?: string[];
}

export const log = (module: string, message: string, type: SeedLog['type'] = 'info'): SeedLog => ({
  module, message, type, timestamp: new Date(),
});

export function today(): string {
  return new Date().toISOString().split('T')[0];
}

export function dateOffset(days: number): string {
  const d = new Date();
  d.setDate(d.getDate() + days);
  return d.toISOString().split('T')[0];
}

export function dateTime(dateStr: string, hours: number, minutes: number): string {
  return `${dateStr}T${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:00`;
}

export function timeOnly(hours: number, minutes: number): string {
  return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:00`;
}

export const SEED_MODULES: SeedModule[] = [
  { key: 'masters', label: 'Masters', icon: 'pi pi-database', description: 'Projects, Works, Gram Panchayats, Training Schedules', seed: seedMasters },
  { key: 'users', label: 'Users', icon: 'pi pi-users', description: 'Fellows, Interns, Guides with Applicant + UserAccount records', seed: seedUsers },
  { key: 'attendance', label: 'Attendance & Leave', icon: 'pi pi-clock', description: 'Holidays, attendance records, leave applications', seed: seedAttendance, dependsOn: ['users'] },
  { key: 'training', label: 'Training', icon: 'pi pi-calendar', description: 'Training sessions and meetings', seed: seedTraining, dependsOn: ['masters'] },
  { key: 'workAllocation', label: 'Work Allocation', icon: 'pi pi-briefcase', description: 'Work allocations, task progress, survey records', seed: seedWorkAllocation, dependsOn: ['masters', 'users'] },
  { key: 'performance', label: 'Performance', icon: 'pi pi-chart-bar', description: 'Supervisor ratings, remarks, score calculations', seed: seedPerformance, dependsOn: ['workAllocation'] },
  { key: 'certificate', label: 'Certificate & Exit', icon: 'pi pi-verified', description: 'Certificate applications and exit readiness', seed: seedCertificate, dependsOn: ['users'] },
  { key: 'helpDesk', label: 'Help Desk', icon: 'pi pi-question-circle', description: 'Support tickets with various priorities', seed: seedHelpDesk, dependsOn: ['users'] },
];

export async function runAllSeeds(appendLog: (entry: SeedLog) => void): Promise<number> {
  let total = 0;
  for (const mod of SEED_MODULES) {
    total += await mod.seed(appendLog);
  }
  return total;
}

export { seedMasters, seedUsers, seedAttendance, seedTraining, seedWorkAllocation, seedPerformance, seedCertificate, seedHelpDesk };
