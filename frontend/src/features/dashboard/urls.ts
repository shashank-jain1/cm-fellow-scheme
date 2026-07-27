const dashboardUrls = {
  admin: () => 'dashboards/admin',
  coordinator: (id: number) => `dashboards/coordinator/${id}`,
};

export default dashboardUrls;
