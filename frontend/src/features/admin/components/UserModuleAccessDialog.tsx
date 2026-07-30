import { Tag } from 'primereact/tag';
import { Checkbox } from 'primereact/checkbox';
import AppDialog from '../../../shared/components/forms/AppDialog';
import AppButton from '../../../shared/components/ui/AppButton';
import ModuleAccessHeader from './ModuleAccessHeader';
import ModuleAccessGroup from './ModuleAccessGroup';
import { useModuleAccess } from '../hooks/useModuleAccess';
import type { UserAccessSummaryDto } from '../types';

interface Props {
  visible: boolean;
  user: UserAccessSummaryDto | null;
  onHide: () => void;
}

export default function UserModuleAccessDialog({ visible, user, onHide }: Props) {
  const { modules, accessMap, expandedParents, toggle, toggleExpand, save, isSaving } = useModuleAccess(user?.userAccountId ?? 0);

  const handleSave = async () => {
    if (!user) return;
    await save(user.userAccountId);
    onHide();
  };

  return (
    <AppDialog
      visible={visible}
      header={<Header user={user} />}
      footer={<Footer onHide={onHide} onSave={handleSave} saving={isSaving} />}
      onHide={onHide}
      style={{ width: '800px', height: '70vh' }}
      contentStyle={{ display: 'flex', flexDirection: 'column', overflow: 'hidden', padding: 0 }}
    >
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
    </AppDialog>
  );
}

function Header({ user }: { user: UserAccessSummaryDto | null }) {
  return (
    <div style={{ padding: '20px 24px 16px' }}>
      <h3 style={{ margin: 0, fontSize: 18, fontWeight: 700, color: 'var(--text-heading)' }}>Manage Module Access</h3>
      {user && (
        <div style={{ marginTop: 8, display: 'flex', gap: 8, alignItems: 'center' }}>
          <Tag value={user.username} severity="info" />
          <span style={{ color: 'var(--text-secondary)', fontSize: 13 }}>{user.fullName}</span>
        </div>
      )}
    </div>
  );
}

function Footer({ onHide, onSave, saving }: { onHide: () => void; onSave: () => void; saving: boolean }) {
  return (
    <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 12, padding: '16px 24px', borderTop: '1px solid var(--border)' }}>
      <AppButton variant="ghost" onClick={onHide}>Cancel</AppButton>
      <AppButton variant="primary" loading={saving} onClick={onSave}>Save Changes</AppButton>
    </div>
  );
}
