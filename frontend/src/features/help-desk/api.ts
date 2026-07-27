import ApiService from '../../services/ApiService';
import helpDeskUrls from './urls';
import type { TicketDto, TicketFormData } from './types';

export async function fetchTickets(): Promise<TicketDto[]> {
  const res = await ApiService.get<TicketDto[]>(helpDeskUrls.tickets());
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
