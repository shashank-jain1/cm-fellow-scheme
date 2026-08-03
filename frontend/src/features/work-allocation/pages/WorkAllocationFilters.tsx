import { AppInput, AppSelect } from '../../../shared/components/forms';

const statusOptions = [
  { label: 'All Status', value: '' },
  { label: 'Active', value: 'active' },
  { label: 'Pending', value: 'pending' },
  { label: 'Completed', value: 'completed' },
];

interface Props {
  search: string;
  statusFilter: string;
  onSearchChange: (v: string) => void;
  onStatusChange: (v: string) => void;
}

export default function WorkAllocationFilters({ search, statusFilter, onSearchChange, onStatusChange }: Props) {
  return (
    <div style={{ display: 'flex', gap: 'var(--space-3)', marginBottom: 'var(--space-5)', alignItems: 'center' }}>
      <div className="search-input-wrapper" style={{ flex: '0 0 320px' }}>
        <i className="pi pi-search" />
        <AppInput value={search} onChange={(e: React.ChangeEvent<HTMLInputElement>) => onSearchChange(e.target.value)} placeholder="Search by description..." style={{ width: '100%' }} />
      </div>
      <AppSelect value={statusFilter} onChange={(val: string) => onStatusChange(val)} options={statusOptions} placeholder="Filter by status" showClear style={{ width: 180 }} />
    </div>
  );
}
