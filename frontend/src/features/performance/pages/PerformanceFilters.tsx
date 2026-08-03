import { AppInput } from '../../../shared/components/forms';

interface Props {
  search: string;
  onSearchChange: (v: string) => void;
}

export default function PerformanceFilters({ search, onSearchChange }: Props) {
  return (
    <div className="search-input-wrapper" style={{ width: '100%', maxWidth: 360, marginBottom: 'var(--space-4)' }}>
      <i className="pi pi-search" />
      <AppInput
        value={search}
        onChange={(e: React.ChangeEvent<HTMLInputElement>) => onSearchChange(e.target.value)}
        placeholder="Search by fellow name..."
        style={{ width: '100%' }}
      />
    </div>
  );
}
