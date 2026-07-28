import { Tag } from 'primereact/tag';

type TagSeverity = 'success' | 'info' | 'warning' | 'danger' | 'secondary' | 'contrast';

const severityMap: Record<string, TagSeverity> = {
  high: 'danger',
  medium: 'warning',
  low: 'info',
};

interface PriorityTagProps {
  value: string;
}

export default function PriorityTag({ value }: PriorityTagProps) {
  const severity = severityMap[value.toLowerCase()] ?? 'secondary';
  return <Tag value={value} severity={severity} />;
}
