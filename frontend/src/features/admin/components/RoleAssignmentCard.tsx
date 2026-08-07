import { useState, useMemo } from 'react';
import { useQuery } from '@tanstack/react-query';
import { Dropdown } from 'primereact/dropdown';
import AppButton from '../../../shared/components/ui/AppButton';
import { ToastService } from '../../../shared/utils/toast';
import ApiService from '../../../services/ApiService';

interface LookupItem {
  lookupMasterId: number;
  masterType: string;
  label: string;
  value: string;
}

const BASE_ROLES = [
  { label: 'CM Fellow', value: 'CM Fellow' },
  { label: 'Fellow', value: 'Fellow' },
  { label: 'Intern', value: 'Intern' },
  { label: 'Guide', value: 'Guide' },
  { label: 'Coordinator', value: 'Coordinator' },
  { label: 'Admin', value: 'Admin' },
];

interface RoleAssignmentCardProps {
  userAccountId: number;
  currentRole: string;
  onUpdate: () => void;
}

export default function RoleAssignmentCard({ userAccountId, currentRole, onUpdate }: RoleAssignmentCardProps) {
  // Fetch role lookups dynamically from database
  const { data: dbRoleLookups, isLoading: isLoadingRoles } = useQuery({
    queryKey: ['lookup', 'Role'],
    queryFn: async () => {
      const res = await ApiService.get<LookupItem[]>('masters/lookup?masterType=Role');
      return res.data ?? [];
    },
  });

  // Merge database role lookups with fallback scheme roles
  const roleOptions = useMemo(() => {
    const optionsMap = new Map<string, { label: string; value: string }>();

    // Add base roles first
    for (const r of BASE_ROLES) {
      optionsMap.set(r.value.toLowerCase(), r);
    }

    // Dynamic roles from database
    if (dbRoleLookups && dbRoleLookups.length > 0) {
      for (const item of dbRoleLookups) {
        const key = item.value.toLowerCase();
        optionsMap.set(key, { label: item.label || item.value, value: item.value });
      }
    }

    // Ensure current user's role is always present
    if (currentRole && !optionsMap.has(currentRole.toLowerCase())) {
      optionsMap.set(currentRole.toLowerCase(), { label: currentRole, value: currentRole });
    }

    return Array.from(optionsMap.values());
  }, [dbRoleLookups, currentRole]);

  const matchedRole = useMemo(() => {
    return roleOptions.find((r) => r.value.toLowerCase() === (currentRole || '').toLowerCase())?.value || currentRole || 'Fellow';
  }, [roleOptions, currentRole]);

  const [selectedRole, setSelectedRole] = useState(matchedRole);
  const [loading, setLoading] = useState(false);

  const handleAssign = async () => {
    if (selectedRole === currentRole) return;
    setLoading(true);
    try {
      await ApiService.put(`/user-accounts/${userAccountId}/assign-role`, {
        role: selectedRole,
        modifiedBy: Number(localStorage.getItem('user_id') ?? '1'),
      });
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
            options={roleOptions}
            onChange={(e) => setSelectedRole(e.value)}
            style={{ width: '100%' }}
            disabled={loading}
            appendTo="self"
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
