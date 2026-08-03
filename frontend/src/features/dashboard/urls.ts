const dashboardUrls = {
  admin: () => 'dashboards/admin',
  coordinator: (id: number) => `dashboards/coordinator/${id}`,
  fellow: (userId: number) => `dashboards/fellow/${userId}`,
  byRole: (role: string) => `dashboards/by-role/${role}`,
  projectProgress: () => 'dashboards/project-progress',
  exportPdf: () => 'dashboards/export/pdf',
  exportExcel: () => 'dashboards/export/excel',
};

export default dashboardUrls;
