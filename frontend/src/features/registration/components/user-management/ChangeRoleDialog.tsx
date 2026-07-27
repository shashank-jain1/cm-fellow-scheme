import { useState, useEffect } from 'react';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import FormSelect from '../../../../shared/components/FormSelect';
import { useLookupOptions } from '../../../../shared/hooks/useMasters';
import { useAssignRole } from '../../queries/user-management';
import { useAuth } from '../../../auth';
import type { UserAccountListItem } from '../../types/user-management';

interface ChangeRoleDialogProps {
  visible: boolean;
  user: UserAccountListItem | null;
  onHide: () => void;
}

export default function ChangeRoleDialog({ visible, user, onHide }: ChangeRoleDialogProps) {
  const { user: currentUser } = useAuth();
  const assignRoleMutation = useAssignRole();
  const roleOptions = useLookupOptions('Role');
  const [newRole, setNewRole] = useState('');

  useEffect(() => {
    if (user) setNewRole(user.role);
  }, [user]);

  const handleAssignRole = async () => {
    if (!user || !newRole) return;
    await assignRoleMutation.mutateAsync({
      userAccountId: user.userAccountId,
      request: { role: newRole, modifiedBy: currentUser?.userAccountId ?? 0 },
    });
    onHide();
  };

  return (
    <Dialog header="Change Role" visible={visible} onHide={onHide} style={{ width: 440 }} modal>
      {user && (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 18, paddingTop: 8 }}>
          <p style={{ color: 'var(--text-secondary)', fontSize: 14, margin: 0 }}>
            Changing role for <strong>{user.username}</strong> ({user.firstName} {user.lastName})
          </p>
          <div className="form-field">
            <label>New Role</label>
            <FormSelect value={newRole} onChange={setNewRole} options={roleOptions} />
          </div>
        </div>
      )}
      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end', marginTop: 24 }}>
        <Button label="Cancel" className="btn btn-secondary" onClick={onHide} />
        <Button label="Update Role" className="btn btn-primary" onClick={handleAssignRole} loading={assignRoleMutation.isPending} />
      </div>
    </Dialog>
  );
}
