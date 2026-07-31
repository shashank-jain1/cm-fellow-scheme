import ApiService from '../../services/ApiService';
import helpDeskUrls from './urls';
import type { TicketDto, TicketFormData, SubmitSurveyCommand, SatisfactionSurveyDto, KnowledgeBaseArticleDto } from './types';

export async function fetchTickets(role?: string, applicantId?: number): Promise<TicketDto[]> {
  let url = helpDeskUrls.tickets();
  const params: string[] = [];
  if (role) params.push(`role=${encodeURIComponent(role)}`);
  if (applicantId) params.push(`applicantId=${applicantId}`);
  if (params.length > 0) url += `?${params.join('&')}`;
  const res = await ApiService.get<TicketDto[]>(url);
  return res.data ?? [];
}

export async function listTicketsByRole(role: string, applicantId?: number): Promise<TicketDto[]> {
  let url = helpDeskUrls.tickets();
  const params: string[] = [`role=${encodeURIComponent(role)}`];
  if (applicantId) params.push(`applicantId=${applicantId}`);
  url += `?${params.join('&')}`;
  const res = await ApiService.get<TicketDto[]>(url);
  return res.data ?? [];
}

export async function fetchTicketDetail(id: number): Promise<TicketDto> {
  const res = await ApiService.get<TicketDto>(helpDeskUrls.ticketDetail(id));
  return res.data!;
}

export async function raiseTicket(command: TicketFormData): Promise<TicketDto> {
  const res = await ApiService.post<TicketDto>(helpDeskUrls.raiseTicket(), command);
  return res.data!;
}

export async function resolveTicket(id: number, resolutionRemarks: string): Promise<void> {
  await ApiService.put(helpDeskUrls.resolve(), { ticketId: id, resolutionRemarks });
}

export async function escalateTicket(id: number): Promise<void> {
  await ApiService.put(helpDeskUrls.escalate(), { ticketId: id });
}

export async function closeTicket(id: number, resolutionRemarks: string): Promise<void> {
  await ApiService.put(helpDeskUrls.close(), { ticketId: id, resolutionRemarks });
}

export async function submitSurvey(command: SubmitSurveyCommand): Promise<SatisfactionSurveyDto> {
  const res = await ApiService.post<SatisfactionSurveyDto>(helpDeskUrls.surveys(), command);
  return res.data!;
}

export async function searchKnowledgeBase(query?: string, category?: string): Promise<KnowledgeBaseArticleDto[]> {
  const params: string[] = [];
  if (query) params.push(`query=${encodeURIComponent(query)}`);
  if (category) params.push(`category=${encodeURIComponent(category)}`);
  const qs = params.length > 0 ? `?${params.join('&')}` : '';
  const res = await ApiService.get<KnowledgeBaseArticleDto[]>(`${helpDeskUrls.knowledgeBaseSearch()}${qs}`);
  return res.data ?? [];
}

export async function fetchKnowledgeBaseArticle(id: number): Promise<KnowledgeBaseArticleDto> {
  const res = await ApiService.get<KnowledgeBaseArticleDto>(helpDeskUrls.knowledgeBaseArticle(id));
  return res.data!;
}
