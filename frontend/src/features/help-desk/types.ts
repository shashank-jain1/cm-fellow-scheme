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

export type SlaStatus = 'On Track' | 'At Risk' | 'Breached';

export interface SatisfactionSurveyDto {
  surveyId: number;
  ticketId: number;
  rating: number;
  comments: string;
  createdOn: string;
}

export interface SubmitSurveyCommand {
  ticketId: number;
  rating: number;
  comments: string;
}

export interface KnowledgeBaseArticleDto {
  articleId: number;
  title: string;
  category: string;
  content: string;
  published: boolean;
  authorName?: string;
  createdOn: string;
}

export interface CreateKBArticleCommand {
  title: string;
  category: string;
  content: string;
  published?: boolean;
}

export interface SlaOverdueResult {
  overdueCount: number;
  tickets: TicketDto[];
}
