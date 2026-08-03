import { SearchInput } from '../../../shared/components/ui';

interface Props {
  search: string;
  onSearchChange: (v: string) => void;
}

export default function DocumentVerificationFilters({ search, onSearchChange }: Props) {
  return (
    <div style={{ marginBottom: 16 }}>
      <SearchInput value={search} onChange={onSearchChange} placeholder="Search by name or mobile..."
        style={{ width: 320 }} />
    </div>
  );
}
