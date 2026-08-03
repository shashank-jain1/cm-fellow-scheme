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
