const helpDeskUrls = {
  base: 'helpdesk',
  tickets: () => `helpdesk/tickets`,
  ticketDetail: (id: number) => `helpdesk/tickets/${id}`,
  raiseTicket: () => `helpdesk/tickets`,
  resolve: (id: number) => `helpdesk/tickets/${id}/resolve`,
  escalate: (id: number) => `helpdesk/tickets/${id}/escalate`,
};

export default helpDeskUrls;
