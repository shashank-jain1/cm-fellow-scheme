export const roleOptions = [
  { label: 'Admin', value: 'Admin' },
  { label: 'Coordinator', value: 'Coordinator' },
  { label: 'CM Fellow', value: 'CM Fellow' },
  { label: 'Intern', value: 'Intern' },
];

export const filterRoleOptions = [
  { label: 'All Roles', value: '' },
  ...roleOptions,
];

export const statusFilterOptions = [
  { label: 'All', value: '' },
  { label: 'Active', value: 'true' },
  { label: 'Inactive', value: 'false' },
];

export function getRoleSeverity(role: string): 'success' | 'info' | 'warning' | 'danger' | 'secondary' | 'contrast' {
  switch (role) {
    case 'Admin': return 'danger';
    case 'Coordinator': return 'warning';
    case 'CM Fellow': return 'info';
    case 'Intern': return 'success';
    default: return 'secondary';
  }
}
