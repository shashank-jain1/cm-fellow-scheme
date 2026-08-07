import { useState } from 'react';
import { Tag } from 'primereact/tag';
import { Button } from 'primereact/button';
import AppDialog from '../../../shared/components/forms/AppDialog';
import AppButton from '../../../shared/components/ui/AppButton';
import ModuleAccessHeader from './ModuleAccessHeader';
import ModuleAccessGroup from './ModuleAccessGroup';
import RoleAssignmentCard from './RoleAssignmentCard';
import ModuleScopeSelector from './ModuleScopeSelector';
import { useModuleAccess } from '../hooks/useModuleAccess';
import type { UserAccessSummaryDto } from '../types';

interface Props {
  visible: boolean;
  user: UserAccessSummaryDto | null;
  onHide: () => void;
  onUserUpdated?: () => void;
}

export default function UserModuleAccessDialog({ visible, user, onHide, onUserUpdated }: Props) {
  const {
    modules,
    accessMap,
    expandedParents,
    toggle,
    toggleExpand,
    grantAllRead,
    grantAllFull,
    clearAll,
    expandAll,
    collapseAll,
    updateGeographicScope,
    save,
    isSaving,
  } = useModuleAccess(user?.userAccountId ?? 0);

  const [activeTab, setActiveTab] = useState<'permissions' | 'role'>('permissions');

  const handleSave = async () => {
    if (!user) return;
    await save(user.userAccountId);
    if (onUserUpdated) onUserUpdated();
    onHide();
  };

  const getInitials = (name: string) => {
    if (!name) return 'U';
    const parts = name.trim().split(' ');
    if (parts.length >= 2) return `${parts[0][0]}${parts[1][0]}`.toUpperCase();
    return name.slice(0, 2).toUpperCase();
  };

  return (
    <AppDialog
      visible={visible}
      header={<Header user={user} initials={getInitials(user?.fullName || user?.username || '')} activeTab={activeTab} setActiveTab={setActiveTab} />}
      footer={<Footer onHide={onHide} onSave={handleSave} saving={isSaving} activeTab={activeTab} />}
      onHide={onHide}
      style={{ width: '840px', maxWidth: '95vw', height: '85vh', maxHeight: '720px' }}
      contentStyle={{ display: 'flex', flexDirection: 'column', overflow: 'hidden', padding: 0 }}
    >
      {activeTab === 'role' ? (
        <div style={{ padding: 24, flex: 1, overflowY: 'auto', display: 'flex', flexDirection: 'column', gap: 20 }}>
          {user && (
            <>
              {/* Role Assignment Card */}
              <RoleAssignmentCard
                userAccountId={user.userAccountId}
                currentRole={user.role}
                onUpdate={() => {
                  if (onUserUpdated) onUserUpdated();
                }}
              />

              {/* Geographic Scope Assignment Card */}
              <div
                style={{
                  border: '1px solid var(--border-light, #E2E8F0)',
                  borderRadius: 10,
                  padding: '18px 22px',
                  background: 'var(--surface-card, #FFFFFF)',
                }}
              >
                <div style={{ marginBottom: 4, fontWeight: 600, fontSize: 15, color: 'var(--text-heading, #1E293B)' }}>
                  Geographic Scope Assignment
                </div>
                <p style={{ margin: '0 0 16px', fontSize: 13, color: 'var(--text-secondary, #64748B)' }}>
                  Restrict or grant access scoped to a specific Division, District, or Block level.
                </p>
                <ModuleScopeSelector
                  divisionId={user.divisionId}
                  onChange={(scope) => {
                    updateGeographicScope(scope);
                  }}
                />
              </div>

              {/* Account Metadata Summary */}
              <div
                style={{
                  border: '1px solid var(--border-light, #E2E8F0)',
                  borderRadius: 10,
                  padding: '18px 22px',
                  background: 'var(--surface-50, #F8FAFC)',
                }}
              >
                <div style={{ fontWeight: 600, fontSize: 14, color: 'var(--text-heading, #1E293B)', marginBottom: 12 }}>
                  Account Metadata
                </div>
                <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: 12, fontSize: 13 }}>
                  <div>
                    <span style={{ color: 'var(--text-secondary, #64748B)' }}>User Account ID:</span>{' '}
                    <strong style={{ color: 'var(--text-heading, #1E293B)' }}>#{user.userAccountId}</strong>
                  </div>
                  <div>
                    <span style={{ color: 'var(--text-secondary, #64748B)' }}>Username:</span>{' '}
                    <strong style={{ color: 'var(--text-heading, #1E293B)' }}>@{user.username}</strong>
                  </div>
                  <div>
                    <span style={{ color: 'var(--text-secondary, #64748B)' }}>Division:</span>{' '}
                    <strong style={{ color: 'var(--text-heading, #1E293B)' }}>{user.divisionName || 'All / State Level'}</strong>
                  </div>
                  <div>
                    <span style={{ color: 'var(--text-secondary, #64748B)' }}>Assigned Modules:</span>{' '}
                    <strong style={{ color: 'var(--text-heading, #1E293B)' }}>{user.moduleAccesses?.length || 0} Modules</strong>
                  </div>
                </div>
              </div>
            </>
          )}
        </div>
      ) : (
        <div style={{ flex: 1, display: 'flex', flexDirection: 'column', overflow: 'hidden' }}>
          {/* Quick Action Batch Toolbar */}
          <div
            style={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'space-between',
              padding: '10px 20px',
              background: 'var(--surface-50, #F8FAFC)',
              borderBottom: '1px solid var(--border-color, #E2E8F0)',
              gap: 8,
              flexWrap: 'wrap',
            }}
          >
            <div style={{ display: 'flex', gap: 6 }}>
              <Button
                type="button"
                label="Expand All"
                icon="pi pi-angle-double-down"
                className="p-button-text p-button-secondary p-button-sm"
                onClick={expandAll}
                style={{ fontSize: 12, padding: '4px 8px' }}
              />
              <Button
                type="button"
                label="Collapse All"
                icon="pi pi-angle-double-up"
                className="p-button-text p-button-secondary p-button-sm"
                onClick={collapseAll}
                style={{ fontSize: 12, padding: '4px 8px' }}
              />
            </div>
            <div style={{ display: 'flex', gap: 8 }}>
              <Button
                type="button"
                label="Grant All Read"
                icon="pi pi-eye"
                className="p-button-outlined p-button-info p-button-sm"
                onClick={grantAllRead}
                style={{ fontSize: 12, padding: '4px 10px' }}
              />
              <Button
                type="button"
                label="Grant Full Access"
                icon="pi pi-check-square"
                className="p-button-outlined p-button-success p-button-sm"
                onClick={grantAllFull}
                style={{ fontSize: 12, padding: '4px 10px' }}
              />
              <Button
                type="button"
                label="Revoke All"
                icon="pi pi-trash"
                className="p-button-outlined p-button-danger p-button-sm"
                onClick={clearAll}
                style={{ fontSize: 12, padding: '4px 10px' }}
              />
            </div>
          </div>

          <div style={{ flex: 1, overflowY: 'auto', overflowX: 'hidden' }}>
            <ModuleAccessHeader />
            {modules?.map((parent) => (
              <ModuleAccessGroup
                key={parent.moduleMasterId}
                parent={parent}
                expanded={expandedParents.has(parent.moduleMasterId)}
                accessMap={accessMap}
                onToggleExpand={toggleExpand}
                onToggle={toggle}
              />
            ))}
          </div>
        </div>
      )}
    </AppDialog>
  );
}

function Header({
  user,
  initials,
  activeTab,
  setActiveTab,
}: {
  user: UserAccessSummaryDto | null;
  initials: string;
  activeTab: 'permissions' | 'role';
  setActiveTab: (tab: 'permissions' | 'role') => void;
}) {
  const getRoleSeverity = (role?: string) => {
    switch (role?.toLowerCase()) {
      case 'admin':
        return 'danger';
      case 'fellow':
        return 'success';
      case 'intern':
        return 'info';
      case 'guide':
      case 'coordinator':
        return 'warning';
      default:
        return 'secondary';
    }
  };

  return (
    <div style={{ padding: '20px 24px 12px', borderBottom: '1px solid var(--border-color, #E2E8F0)' }}>
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: 12 }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 14 }}>
          <div
            style={{
              width: 44,
              height: 44,
              borderRadius: '50%',
              background: 'linear-gradient(135deg, #4F46E5 0%, #7C3AED 100%)',
              color: '#FFFFFF',
              fontWeight: 700,
              fontSize: 16,
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              boxShadow: '0 2px 8px rgba(79, 70, 229, 0.25)',
            }}
          >
            {initials}
          </div>
          <div>
            <h3 style={{ margin: 0, fontSize: 18, fontWeight: 700, color: 'var(--text-heading)' }}>
              {user?.fullName || user?.username || 'User Access Control'}
            </h3>
            {user && (
              <div style={{ marginTop: 4, display: 'flex', gap: 8, alignItems: 'center', fontSize: 13, color: 'var(--text-secondary)' }}>
                <span>@{user.username}</span>
                <span>•</span>
                <Tag value={user.role || 'No Role'} severity={getRoleSeverity(user.role)} />
                {user.divisionName && (
                  <>
                    <span>•</span>
                    <span style={{ fontSize: 12, background: 'var(--surface-200)', padding: '2px 8px', borderRadius: 4 }}>
                      {user.divisionName}
                    </span>
                  </>
                )}
              </div>
            )}
          </div>
        </div>
      </div>

      {/* Tabs */}
      <div style={{ display: 'flex', gap: 16, borderBottom: '2px solid transparent', marginTop: 16 }}>
        <button
          type="button"
          onClick={() => setActiveTab('permissions')}
          style={{
            background: 'none',
            border: 'none',
            padding: '8px 12px',
            fontSize: 14,
            fontWeight: activeTab === 'permissions' ? 600 : 500,
            color: activeTab === 'permissions' ? 'var(--primary-color, #4F46E5)' : 'var(--text-secondary)',
            borderBottom: activeTab === 'permissions' ? '2px solid var(--primary-color, #4F46E5)' : '2px solid transparent',
            cursor: 'pointer',
            display: 'flex',
            alignItems: 'center',
            gap: 6,
          }}
        >
          <i className="pi pi-shield" /> Module Permissions
        </button>
        <button
          type="button"
          onClick={() => setActiveTab('role')}
          style={{
            background: 'none',
            border: 'none',
            padding: '8px 12px',
            fontSize: 14,
            fontWeight: activeTab === 'role' ? 600 : 500,
            color: activeTab === 'role' ? 'var(--primary-color, #4F46E5)' : 'var(--text-secondary)',
            borderBottom: activeTab === 'role' ? '2px solid var(--primary-color, #4F46E5)' : '2px solid transparent',
            cursor: 'pointer',
            display: 'flex',
            alignItems: 'center',
            gap: 6,
          }}
        >
          <i className="pi pi-user-edit" /> Role & Scope Settings
        </button>
      </div>
    </div>
  );
}

function Footer({
  onHide,
  onSave,
  saving,
}: {
  onHide: () => void;
  onSave: () => void;
  saving: boolean;
  activeTab: 'permissions' | 'role';
}) {
  return (
    <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 12, padding: '16px 24px', borderTop: '1px solid var(--border-color, #E2E8F0)' }}>
      <AppButton variant="ghost" onClick={onHide}>
        Close
      </AppButton>
      <AppButton variant="primary" loading={saving} onClick={onSave} icon="pi pi-save">
        Save Changes
      </AppButton>
    </div>
  );
}
