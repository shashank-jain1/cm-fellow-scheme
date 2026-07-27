import FormSelect from '../../../../shared/components/FormSelect';
import { useLookupOptions } from '../../../../shared/hooks/useMasters';
import { statusFilterOptions } from './constants';

interface UserFilterBarProps {
  roleFilter: string;
  statusFilter: string;
  onRoleChange: (value: string) => void;
  onStatusChange: (value: string) => void;
}

export default function UserFilterBar({ roleFilter, statusFilter, onRoleChange, onStatusChange }: UserFilterBarProps) {
  const roleLookupOptions = useLookupOptions('Role');
  const filterRoleOptions = [{ label: 'All Roles', value: '' }, ...roleLookupOptions];

  return (
    <div style={{ display: 'flex', gap: 12, marginBottom: 24, flexWrap: 'wrap' }}>
      <div className="form-group" style={{ marginBottom: 0 }}>
        <label className="form-label">Role</label>
        <FormSelect
          value={roleFilter}
          onChange={onRoleChange}
          options={filterRoleOptions}
          style={{ width: 180 }}
        />
      </div>
      <div className="form-group" style={{ marginBottom: 0 }}>
        <label className="form-label">Status</label>
        <FormSelect
          value={statusFilter}
          onChange={onStatusChange}
          options={statusFilterOptions}
          style={{ width: 150 }}
        />
      </div>
    </div>
  );
}
