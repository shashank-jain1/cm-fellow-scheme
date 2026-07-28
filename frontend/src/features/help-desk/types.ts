export interface TicketFormData {
  issueCategory: string;
  issueDescription: string;
  priority: 'High' | 'Medium' | 'Low';
}

export interface TicketDto {
  ticketId: number;
  applicantId: number;
  email: string;
  mobile: string;
  issueCategory: string;
  issueDescription: string;
  priority: string;
  status: string;
  resolutionRemarks?: string;
  slaDeadline?: string;
  slaBreached: boolean;
  createdOn: string;
  closedOn?: string;
}
