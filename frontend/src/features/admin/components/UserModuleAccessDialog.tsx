import { useState, useEffect } from 'react';
import { Dialog } from 'primereact/dialog';
import { Tag } from 'primereact/tag';
import AppButton from '../../../shared/components/ui/AppButton';
import { useUserModuleAccessQuery, useBulkUpdateMutation, useModulesQuery } from '../queries';
import ModuleAccessToggle from './ModuleAccessToggle';
import type { UserAccessSummaryDto, ModuleAccessItem, ModuleAccessDto } from '../types';

interface UserModuleAccessDialogProps {
  visible: boolean;
  user: UserAccessSummaryDto | null;
  onHide: () => void;
}

export default function UserModuleAccessDialog({ visible, user, onHide }: UserModuleAccessDialogProps) {
  const { data: modules } = useModulesQuery();
  const { data: userAccess } = useUserModuleAccessQuery(user?.userAccountId ?? 0);
  const bulkUpdate = useBulkUpdateMutation();

  const [accessMap, setAccessMap] = useState<Map<number, ModuleAccessItem>>(new Map());

  useEffect(() => {
    if (!userAccess || !modules) return;
    const map = new Map<number, ModuleAccessItem>();
    for (const mod of modules) {
      const existing = userAccess.find((a: ModuleAccessDto) => a.moduleMasterId === mod.moduleMasterId);
      map.set(mod.moduleMasterId, {
        moduleMasterId: mod.moduleMasterId,
        canRead: existing?.canRead ?? false,
        canWrite: existing?.canWrite ?? false,
        canApprove: existing?.canApprove ?? false,
        canExport: existing?.canExport ?? false,
        divisionId: existing?.divisionId,
        districtId: existing?.districtId,
        blockId: existing?.blockId,
      });
    }
    setAccessMap(map);
  }, [userAccess, modules]);

  const handleToggle = (moduleMasterId: number, field: keyof ModuleAccessItem, value: boolean) => {
    setAccessMap((prev) => {
      const next = new Map(prev);
      const item = next.get(moduleMasterId);
      if (item) {
        next.set(moduleMasterId, { ...item, [field]: value });
      }
      return next;
    });
  };

  const handleSave = async () => {
    if (!user) return;
    const accesses = Array.from(accessMap.values());
    await bulkUpdate.mutateAsync({
      userAccountId: user.userAccountId,
      performedBy: Number(localStorage.getItem('user_id') ?? '0'),
      accesses,
    });
    onHide();
  };

  const header = (
    <div>
      <h3 style={{ margin: 0 }}>Manage Module Access</h3>
      {user && (
        <div style={{ marginTop: 8, display: 'flex', gap: 8, alignItems: 'center' }}>
          <Tag value={user.username} severity="info" />
          <span style={{ color: 'var(--text-secondary)' }}>{user.fullName}</span>
        </div>
      )}
    </div>
  );

  const footer = (
    <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8 }}>
      <AppButton variant="ghost" onClick={onHide}>Cancel</AppButton>
      <AppButton variant="primary" loading={bulkUpdate.isPending} onClick={handleSave}>Save</AppButton>
    </div>
  );

  return (
    <Dialog
      visible={visible}
      header={header}
      footer={footer}
      onHide={onHide}
      style={{ width: '700px' }}
      modal
    >
      <div style={{ display: 'flex', flexDirection: 'column', gap: 12, maxHeight: '60vh', overflowY: 'auto' }}>
        {modules?.map((mod) => {
          const item = accessMap.get(mod.moduleMasterId);
          return (
            <ModuleAccessToggle
              key={mod.moduleMasterId}
              moduleCode={mod.moduleCode}
              moduleName={mod.moduleName}
              canRead={item?.canRead ?? false}
              canWrite={item?.canWrite ?? false}
              canApprove={item?.canApprove ?? false}
              canExport={item?.canExport ?? false}
              onToggle={(field, value) => handleToggle(mod.moduleMasterId, field, value)}
            />
          );
        })}
      </div>
    </Dialog>
  );
}
