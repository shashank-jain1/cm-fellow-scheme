import { Button } from 'primereact/button';
import { Tag } from 'primereact/tag';
import { getRoleSeverity } from './constants';
import { useAuth } from '../../../auth';
import type { UserAccountListItem } from '../../types/user-management';

interface UserTableProps {
  users: UserAccountListItem[];
  isLoading: boolean;
  onChangeRole: (user: UserAccountListItem) => void;
  onDeactivate: (user: UserAccountListItem) => void;
}

export default function UserTable({ users, isLoading, onChangeRole, onDeactivate }: UserTableProps) {
  const { user: currentUser } = useAuth();

  return (
    <div className="table-wrapper">
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr>
            {['User', 'Contact', 'Role', 'Status', 'Created', 'Actions'].map((h) => (
              <th key={h} style={{ padding: '12px 16px', textAlign: h === 'Actions' ? 'right' : 'left', fontWeight: 600, fontSize: 12, textTransform: 'uppercase', letterSpacing: '0.5px', color: 'var(--text-secondary)', background: 'var(--bg-primary)', borderBottom: '1px solid var(--border-color)' }}>{h}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {isLoading ? (
            <tr><td colSpan={6} style={{ padding: 40, textAlign: 'center', color: 'var(--text-muted)' }}>Loading...</td></tr>
          ) : !users.length ? (
            <tr><td colSpan={6} style={{ padding: 40, textAlign: 'center', color: 'var(--text-muted)' }}>No users found</td></tr>
          ) : (
            users.map((u) => (
              <tr key={u.userAccountId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                <td style={{ padding: '12px 16px' }}>
                  <div style={{ fontWeight: 600, fontSize: 14 }}>{u.firstName} {u.lastName}</div>
                  <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>@{u.username}</div>
                </td>
                <td style={{ padding: '12px 16px' }}>
                  <div style={{ fontSize: 13 }}>{u.emailId}</div>
                  <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>{u.mobileNumber}</div>
                </td>
                <td style={{ padding: '12px 16px' }}>
                  <Tag value={u.role} severity={getRoleSeverity(u.role)} />
                </td>
                <td style={{ padding: '12px 16px' }}>
                  <Tag value={u.isActive ? 'Active' : 'Inactive'} severity={u.isActive ? 'success' : 'danger'} />
                </td>
                <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>
                  {new Date(u.createdOn).toLocaleDateString()}
                </td>
                <td style={{ padding: '12px 16px', textAlign: 'right' }}>
                  <div style={{ display: 'flex', gap: 6, justifyContent: 'flex-end' }}>
                    <Button
                      icon="pi pi-user-edit"
                      className="btn btn-ghost btn-sm"
                      title="Change Role"
                      onClick={() => onChangeRole(u)}
                      size="small"
                    />
                    {u.isActive && u.userAccountId !== currentUser?.userAccountId && (
                      <Button
                        icon="pi pi-ban"
                        className="btn btn-ghost btn-sm"
                        title="Deactivate"
                        onClick={() => onDeactivate(u)}
                        size="small"
                        style={{ color: '#ef4444' }}
                      />
                    )}
                  </div>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
}
