const helpDeskUrls = {
  base: 'tickets',
  tickets: () => 'tickets/list',
  ticketDetail: (id: number) => `tickets/${id}`,
  raiseTicket: () => 'tickets',
  resolve: () => 'tickets/resolve',
  escalate: () => 'tickets/escalate',
  close: () => 'tickets/close',
  surveys: () => 'help-desk/surveys',
  knowledgeBaseSearch: () => 'help-desk/knowledge-base/search',
  knowledgeBaseArticle: (id: number) => `help-desk/knowledge-base/${id}`,
};

export default helpDeskUrls;
