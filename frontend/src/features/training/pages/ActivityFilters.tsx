import { AppInput } from '../../../shared/components/forms';

interface Props {
  searchTerm: string;
  onSearchChange: (v: string) => void;
}

export default function ActivityFilters({ searchTerm, onSearchChange }: Props) {
  return (
    <div style={{ marginBottom: 24 }}>
      <AppInput
        value={searchTerm}
        onChange={(e: React.ChangeEvent<HTMLInputElement>) => onSearchChange(e.target.value)}
        placeholder="Search activities..."
        style={{ width: 320 }}
      />
    </div>
  );
}
