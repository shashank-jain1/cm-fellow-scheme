const helpDeskUrls = {
  base: 'tickets',
  tickets: () => 'tickets/list',
  ticketDetail: (id: number) => `tickets/${id}`,
  raiseTicket: () => 'tickets',
  resolve: () => 'tickets/resolve',
  escalate: () => 'tickets/escalate',
};

export default helpDeskUrls;
