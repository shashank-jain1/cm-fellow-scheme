const helpDeskUrls = {
  base: 'tickets',
  tickets: () => 'tickets/list',
  ticketDetail: (id: number) => `tickets/${id}`,
  raiseTicket: () => 'tickets',
  resolve: () => 'tickets/resolve',
  escalate: () => 'tickets/escalate',
  close: () => 'tickets/close',
  exportTickets: (format: string) => `tickets/export?format=${format}`,
  surveys: () => 'help-desk/surveys',
  surveyByTicket: (ticketId: number) => `help-desk/surveys?ticketId=${ticketId}`,
  knowledgeBaseSearch: () => 'help-desk/knowledge-base/search',
  knowledgeBaseArticle: (id: number) => `help-desk/knowledge-base/${id}`,
  knowledgeBaseCreate: () => 'help-desk/knowledge-base',
  slaCheck: () => 'tickets/sla/overdue',
};

export default helpDeskUrls;
