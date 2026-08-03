const helpDeskUrls = {
  base: 'tickets',
  tickets: () => 'tickets/list',
  ticketDetail: (id: number) => `tickets/${id}`,
  raiseTicket: () => 'tickets',
  resolve: () => 'tickets/resolve',
  escalate: () => 'tickets/escalate',
  close: () => 'tickets/close',
  exportTickets: (format: string) => `tickets/export?format=${format}`,
  surveys: () => 'surveys',
  surveyByTicket: (ticketId: number) => `surveys/ticket/${ticketId}`,
  knowledgeBaseSearch: () => 'knowledge-base/search',
  knowledgeBaseArticle: (id: number) => `knowledge-base/${id}`,
  knowledgeBaseCreate: () => 'knowledge-base',
  slaCheck: () => 'sla/check-overdue',
};

export default helpDeskUrls;
