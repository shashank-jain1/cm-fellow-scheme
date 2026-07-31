export interface Fellow {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  gender: string;
  dateOfBirth: string;
  status: string;
  divisionId: number;
  districtId: number;
  blockId: number;
  createdAt: string;
}

export interface Division {
  id: number;
  name: string;
  code: string;
}

export interface District {
  id: number;
  name: string;
  divisionId: number;
}

export interface Block {
  id: number;
  name: string;
  districtId: number;
}

export interface Project {
  id: number;
  name: string;
  code: string;
}

export interface TrainingSession {
  id: number;
  title: string;
  trainer: string;
  date: string;
  startTime: string;
  endTime: string;
  location: string;
  status: string;
  enrolledCount: number;
  maxCapacity: number;
}

export interface WorkAllocation {
  id: number;
  fellowName: string;
  projectName: string;
  role: string;
  startDate: string;
  endDate: string;
  status: string;
  priority: string;
  progress: number;
}

export interface AttendanceRecord {
  id: number;
  fellowName: string;
  date: string;
  checkIn: string;
  checkOut: string;
  status: string;
  hoursWorked: number;
  location: string;
}

export interface PerformanceRecord {
  id: number;
  fellowName: string;
  project: string;
  metrics: {
    taskCompletion: number;
    qualityScore: number;
    punctuality: number;
    teamwork: number;
  };
  overallScore: number;
  reviewPeriod: string;
  status: string;
}

export interface Certificate {
  id: number;
  fellowName: string;
  certificateType: string;
  issueDate: string;
  certificateNumber: string;
  status: string;
  project: string;
}

export interface Ticket {
  id: number;
  ticketNumber: string;
  subject: string;
  description: string;
  raisedBy: string;
  createdAt: string;
  status: string;
  priority: string;
  category: string;
}

export interface ProfileUpdatePayload {
  name: string;
  phone: string;
  email: string;
  address: string;
  qualification: string;
  experience: string;
}

export interface BulkApprovalPayload {
  applicantIds: number[];
  approvedBy: number;
  action: 'Approved' | 'Rejected';
  reason?: string;
}
