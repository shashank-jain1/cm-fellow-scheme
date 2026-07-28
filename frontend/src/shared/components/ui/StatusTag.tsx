import { Tag } from 'primereact/tag';

type StatusSeverity = 'success' | 'info' | 'warning' | 'danger' | 'secondary' | 'contrast';

const severityMap: Record<string, StatusSeverity> = {
  active: 'success',
  completed: 'success',
  approved: 'success',
  pending: 'warning',
  'in-progress': 'info',
  inactive: 'secondary',
  rejected: 'danger',
  cancelled: 'danger',
  closed: 'secondary',
};

interface StatusTagProps {
  value: string;
  severity?: StatusSeverity;
}

export default function StatusTag({ value, severity }: StatusTagProps) {
  const resolvedSeverity = severity ?? severityMap[value.toLowerCase()] ?? 'secondary';
  return <Tag value={value} severity={resolvedSeverity} />;
}
