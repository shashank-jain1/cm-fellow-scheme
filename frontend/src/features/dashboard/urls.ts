const dashboardUrls = {
  admin: () => 'dashboards/admin',
  coordinator: (id: number) => `dashboards/coordinator/${id}`,
  exportPdf: () => 'dashboard/export/pdf',
  exportExcel: () => 'dashboard/export/excel',
};

export default dashboardUrls;
