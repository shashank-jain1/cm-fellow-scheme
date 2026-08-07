import { useState, useEffect, useCallback } from 'react';
import { useUserModuleAccessQuery, useBulkUpdateMutation, useModulesQuery } from '../queries';
import type { ModuleAccessItem, ModuleAccessDto, ModuleMasterDto } from '../types';

export type PermissionField = 'canRead' | 'canWrite' | 'canApprove' | 'canExport';

export const PERMISSION_FIELDS: PermissionField[] = ['canRead', 'canWrite', 'canApprove', 'canExport'];

export const PERMISSION_LABELS: Record<PermissionField, string> = {
  canRead: 'Read',
  canWrite: 'Write',
  canApprove: 'Approve',
  canExport: 'Export',
};

export const PERMISSION_DESCRIPTIONS: Record<PermissionField, string> = {
  canRead: 'View & access module data',
  canWrite: 'Create & edit module records',
  canApprove: 'Approve applications, leaves & tasks',
  canExport: 'Export data reports & files',
};

function buildInitialMap(modules: ModuleMasterDto[] | undefined, userAccess: ModuleAccessDto[] | undefined) {
  const map = new Map<number, ModuleAccessItem>();
  if (!modules || !userAccess) return map;

  const toItem = (id: number, existing?: ModuleAccessDto): ModuleAccessItem => ({
    moduleMasterId: id,
    canRead: existing?.canRead ?? false,
    canWrite: existing?.canWrite ?? false,
    canApprove: existing?.canApprove ?? false,
    canExport: existing?.canExport ?? false,
    divisionId: existing?.divisionId,
    districtId: existing?.districtId,
    blockId: existing?.blockId,
  });

  for (const parent of modules) {
    map.set(parent.moduleMasterId, toItem(parent.moduleMasterId, userAccess.find((a) => a.moduleMasterId === parent.moduleMasterId)));
    for (const child of parent.children ?? []) {
      map.set(child.moduleMasterId, toItem(child.moduleMasterId, userAccess.find((a) => a.moduleMasterId === child.moduleMasterId)));
    }
  }
  return map;
}

export function useModuleAccess(userAccountId: number) {
  const { data: modules } = useModulesQuery();
  const { data: userAccess } = useUserModuleAccessQuery(userAccountId);
  const bulkUpdate = useBulkUpdateMutation();

  const [accessMap, setAccessMap] = useState<Map<number, ModuleAccessItem>>(new Map());
  const [expandedParents, setExpandedParents] = useState<Set<number>>(new Set());

  useEffect(() => {
    setAccessMap(buildInitialMap(modules, userAccess));
  }, [modules, userAccess]);

  const toggle = useCallback((id: number, field: PermissionField, value: boolean) => {
    setAccessMap((prev) => {
      const next = new Map(prev);
      const item = next.get(id);
      if (item) next.set(id, { ...item, [field]: value });
      return next;
    });
  }, []);

  const toggleExpand = useCallback((id: number) => {
    setExpandedParents((prev) => {
      const next = new Set(prev);
      if (next.has(id)) next.delete(id); else next.add(id);
      return next;
    });
  }, []);

  const toggleAllChildren = useCallback((parent: ModuleMasterDto, field: PermissionField, value: boolean) => {
    setAccessMap((prev) => {
      const next = new Map(prev);
      next.set(parent.moduleMasterId, { ...next.get(parent.moduleMasterId)!, [field]: value });
      for (const child of parent.children ?? []) {
        const item = next.get(child.moduleMasterId);
        if (item) next.set(child.moduleMasterId, { ...item, [field]: value });
      }
      return next;
    });
  }, []);

  const grantAllRead = useCallback(() => {
    setAccessMap((prev) => {
      const next = new Map(prev);
      for (const [id, item] of next.entries()) {
        next.set(id, { ...item, canRead: true });
      }
      return next;
    });
  }, []);

  const grantAllFull = useCallback(() => {
    setAccessMap((prev) => {
      const next = new Map(prev);
      for (const [id, item] of next.entries()) {
        next.set(id, { ...item, canRead: true, canWrite: true, canApprove: true, canExport: true });
      }
      return next;
    });
  }, []);

  const clearAll = useCallback(() => {
    setAccessMap((prev) => {
      const next = new Map(prev);
      for (const [id, item] of next.entries()) {
        next.set(id, { ...item, canRead: false, canWrite: false, canApprove: false, canExport: false });
      }
      return next;
    });
  }, []);

  const expandAll = useCallback(() => {
    if (modules) {
      setExpandedParents(new Set(modules.map((m) => m.moduleMasterId)));
    }
  }, [modules]);

  const collapseAll = useCallback(() => {
    setExpandedParents(new Set());
  }, []);

  const updateGeographicScope = useCallback((scope: { divisionId?: number; districtId?: number; blockId?: number }) => {
    setAccessMap((prev) => {
      const next = new Map(prev);
      for (const [id, item] of next.entries()) {
        next.set(id, {
          ...item,
          divisionId: scope.divisionId,
          districtId: scope.districtId,
          blockId: scope.blockId,
        });
      }
      return next;
    });
  }, []);

  const save = useCallback(async (userAccountId: number) => {
    await bulkUpdate.mutateAsync({
      userAccountId,
      performedBy: Number(localStorage.getItem('user_id') ?? '1'),
      accesses: Array.from(accessMap.values()),
    });
  }, [accessMap, bulkUpdate]);

  return {
    modules,
    accessMap,
    expandedParents,
    toggle,
    toggleExpand,
    toggleAllChildren,
    grantAllRead,
    grantAllFull,
    clearAll,
    expandAll,
    collapseAll,
    updateGeographicScope,
    save,
    isSaving: bulkUpdate.isPending,
  };
}
