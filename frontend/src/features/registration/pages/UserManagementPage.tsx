import { useState } from 'react';
import { Button } from 'primereact/button';
import { UserFilterBar, UserTable, CreateUserDialog, ChangeRoleDialog, ResetPasswordDialog } from '../components/user-management';
import { useListUserAccounts, useDeactivateUserAccount } from '../queries/user-management';
import type { UserAccountListItem } from '../types/user-management';

export default function UserManagementPage() {
  const [roleFilter, setRoleFilter] = useState('');
  const [statusFilter, setStatusFilter] = useState('');
  const [showCreateDialog, setShowCreateDialog] = useState(false);
  const [roleDialogUser, setRoleDialogUser] = useState<UserAccountListItem | null>(null);
  const [resetPasswordUser, setResetPasswordUser] = useState<UserAccountListItem | null>(null);

  const { data: users, isLoading } = useListUserAccounts(
    roleFilter || undefined,
    statusFilter === '' ? undefined : statusFilter === 'true',
  );
  const deactivateMutation = useDeactivateUserAccount();

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>User Management</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Manage user accounts, roles, and access
          </p>
        </div>
        <Button label="Create User" icon="pi pi-plus" className="btn btn-primary" onClick={() => setShowCreateDialog(true)} />
      </div>

      <UserFilterBar roleFilter={roleFilter} statusFilter={statusFilter} onRoleChange={setRoleFilter} onStatusChange={setStatusFilter} />

      <UserTable
        users={users ?? []}
        isLoading={isLoading}
        onChangeRole={(u) => setRoleDialogUser(u)}
        onDeactivate={(u) => deactivateMutation.mutateAsync(u.userAccountId)}
        onResetPassword={(u) => setResetPasswordUser(u)}
      />

      <CreateUserDialog visible={showCreateDialog} onHide={() => setShowCreateDialog(false)} />
      <ChangeRoleDialog visible={!!roleDialogUser} user={roleDialogUser} onHide={() => setRoleDialogUser(null)} />
      <ResetPasswordDialog visible={!!resetPasswordUser} user={resetPasswordUser} onHide={() => setResetPasswordUser(null)} />
    </div>
  );
}
