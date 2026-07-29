import { Tag } from 'primereact/tag';

type TagSeverity = 'success' | 'info' | 'warning' | 'danger' | 'secondary' | 'contrast';

const severityMap: Record<string, TagSeverity> = {
  active: 'success',
  completed: 'success',
  approved: 'success',
  issued: 'success',
  generated: 'success',
  closed: 'success',
  'close-archived': 'success',
  pending: 'warning',
  'in-progress': 'info',
  review: 'info',
  'under-review': 'info',
  applied: 'info',
  assigned: 'info',
  inactive: 'secondary',
  draft: 'secondary',
  submitted: 'info',
  rejected: 'danger',
  cancelled: 'danger',
  escalated: 'danger',
  overdue: 'danger',
  suspended: 'danger',
};

interface StatusTagProps {
  value: string;
  severity?: TagSeverity;
}

export default function StatusTag({ value, severity }: StatusTagProps) {
  const resolvedSeverity = severity ?? severityMap[value.toLowerCase()] ?? 'secondary';
  return <Tag value={value} severity={resolvedSeverity} />;
}
