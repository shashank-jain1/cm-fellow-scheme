import { InputText } from 'primereact/inputtext';
import FormSelect from '../../../shared/components/FormSelect';
import { useLookupOptions } from '../../../shared/hooks/useMasters';

interface TicketFiltersProps {
  search: string;
  onSearchChange: (value: string) => void;
  statusFilter: string;
  onStatusFilterChange: (value: string) => void;
  priorityFilter: string;
  onPriorityFilterChange: (value: string) => void;
}

const statusOptions = [
  { label: 'All Status', value: '' },
  { label: 'Open', value: 'Open' },
  { label: 'In Progress', value: 'In Progress' },
  { label: 'Resolved', value: 'Resolved' },
  { label: 'Closed', value: 'Closed' },
];

export default function TicketFilters({ search, onSearchChange, statusFilter, onStatusFilterChange, priorityFilter, onPriorityFilterChange }: TicketFiltersProps) {
  const priorityLookupOptions = useLookupOptions('Priority');
  const priorityOptions = [{ label: 'All Priorities', value: '' }, ...priorityLookupOptions];

  return (
    <div style={{ display: 'flex', gap: 12, marginBottom: 20, alignItems: 'center' }}>
      <div className="search-input-wrapper" style={{ flex: '0 0 320px' }}>
        <i className="pi pi-search" />
        <InputText
          value={search}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => onSearchChange(e.target.value)}
          placeholder="Search tickets..."
          style={{ width: '100%' }}
        />
      </div>
      <FormSelect value={statusFilter} onChange={onStatusFilterChange} options={statusOptions} showClear style={{ width: 160 }} />
      <FormSelect value={priorityFilter} onChange={onPriorityFilterChange} options={priorityOptions} showClear style={{ width: 160 }} />
    </div>
  );
}
