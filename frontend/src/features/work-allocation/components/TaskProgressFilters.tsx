
const getProgressColor = (percentage: number) => {
  if (percentage >= 80) return 'var(--emerald-500)';
  if (percentage >= 50) return 'var(--amber-500)';
  return 'var(--navy-400)';
};

const getStatusSeverity = (status: string) => {
  switch (status.toLowerCase()) {
    case 'active':
    case 'completed':
      return 'success';
    case 'pending':
    case 'in progress':
      return 'warning';
    case 'not started':
      return 'secondary';
    default:
      return 'info';
  }
};

export { getProgressColor, getStatusSeverity };
