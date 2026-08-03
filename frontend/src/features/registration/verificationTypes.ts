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
