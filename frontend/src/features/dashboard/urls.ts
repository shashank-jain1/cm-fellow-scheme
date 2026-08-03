const dashboardUrls = {
  admin: () => 'dashboards/admin',
  coordinator: (id: number) => `dashboards/coordinator/${id}`,
  fellow: (userId: number) => `dashboards/fellow/${userId}`,
  byRole: (role: string) => `dashboards/role/${role}`,
  projectProgress: () => 'dashboards/project-progress',
  exportPdf: () => 'dashboard/export/pdf',
  exportExcel: () => 'dashboard/export/excel',
};

export default dashboardUrls;
