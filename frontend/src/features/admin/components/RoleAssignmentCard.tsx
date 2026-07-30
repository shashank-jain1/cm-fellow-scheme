import { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import AppButton from '../../../shared/components/ui/AppButton';
import { ToastService } from '../../../shared/utils/toast';
import ApiService from '../../../services/ApiService';

const ROLES = [
  { label: 'Admin', value: 'Admin' },
  { label: 'Fellow', value: 'Fellow' },
  { label: 'Intern', value: 'Intern' },
  { label: 'Guide', value: 'Guide' },
  { label: 'Coordinator', value: 'Coordinator' },
];

interface RoleAssignmentCardProps {
  userAccountId: number;
  currentRole: string;
  onUpdate: () => void;
}

export default function RoleAssignmentCard({ userAccountId, currentRole, onUpdate }: RoleAssignmentCardProps) {
  const [selectedRole, setSelectedRole] = useState(currentRole);
  const [loading, setLoading] = useState(false);

  const handleAssign = async () => {
    if (selectedRole === currentRole) return;
    setLoading(true);
    try {
      await ApiService.put(`/user-accounts/${userAccountId}/assign-role`, { role: selectedRole });
      ToastService.success('Role updated successfully');
      onUpdate();
    } catch {
      ToastService.error('Failed to update role');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div
      style={{
        border: '1px solid var(--border-light)',
        borderRadius: 8,
        padding: '16px 20px',
        background: 'var(--surface-card)',
      }}
    >
      <div style={{ marginBottom: 12, fontWeight: 600, color: 'var(--text-heading)' }}>Assign Role</div>
      <div style={{ display: 'flex', gap: 12, alignItems: 'flex-end' }}>
        <div style={{ flex: 1 }}>
          <label style={{ display: 'block', marginBottom: 4, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
            Role
          </label>
          <Dropdown
            value={selectedRole}
            options={ROLES}
            onChange={(e) => setSelectedRole(e.value)}
            style={{ width: '100%' }}
            disabled={loading}
          />
        </div>
        <AppButton
          variant="primary"
          loading={loading}
          disabled={selectedRole === currentRole}
          onClick={handleAssign}
        >
          Update
        </AppButton>
      </div>
    </div>
  );
}
