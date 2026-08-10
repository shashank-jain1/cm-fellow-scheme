import { useState, useMemo } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { InputText } from 'primereact/inputtext';
import { Dropdown } from 'primereact/dropdown';
import PageHeader from '../../../shared/components/ui/PageHeader';
import SkeletonTable from '../../../shared/components/ui/SkeletonTable';
import AppButton from '../../../shared/components/ui/AppButton';
import { useAllModuleAccessQuery } from '../queries';
import UserModuleAccessDialog from '../components/UserModuleAccessDialog';
import type { UserAccessSummaryDto } from '../types';

const ROLES_OPTIONS = [
  { label: 'All Roles', value: '' },
  { label: 'Admin', value: 'Admin' },
  { label: 'Fellow', value: 'Fellow' },
  { label: 'Intern', value: 'Intern' },
  { label: 'Guide', value: 'Guide' },
  { label: 'Coordinator', value: 'Coordinator' },
];

export default function UserAccessPage() {
  const { data: usersAccess, isLoading, refetch } = useAllModuleAccessQuery();
  const [selectedUser, setSelectedUser] = useState<UserAccessSummaryDto | null>(null);
  const [searchTerm, setSearchTerm] = useState('');
  const [roleFilter, setRoleFilter] = useState('');

  // The backend already returns UserAccessSummaryDto[], handle fallback if data format changes
  const userSummaries: UserAccessSummaryDto[] = useMemo(() => {
    if (!usersAccess) return [];
    if (Array.isArray(usersAccess) && usersAccess.length > 0 && 'moduleAccesses' in usersAccess[0]) {
      return usersAccess as unknown as UserAccessSummaryDto[];
    }
    // Fallback if raw list of ModuleAccessDto is returned
    const map = new Map<number, UserAccessSummaryDto>();
    for (const item of usersAccess as any) {
      if (!map.has(item.userAccountId)) {
        map.set(item.userAccountId, {
          userAccountId: item.userAccountId,
          username: item.username || '',
          fullName: item.fullName || item.username || 'User',
          role: item.role || 'User',
          divisionName: item.divisionName,
          moduleAccesses: [],
        });
      }
      map.get(item.userAccountId)!.moduleAccesses.push(item);
    }
    return Array.from(map.values());
  }, [usersAccess]);

  // Filter users based on search term & role filter
  const filteredUsers = useMemo(() => {
    return userSummaries.filter((user) => {
      const matchesSearch =
        !searchTerm.trim() ||
        user.username.toLowerCase().includes(searchTerm.toLowerCase()) ||
        user.fullName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        (user.divisionName && user.divisionName.toLowerCase().includes(searchTerm.toLowerCase()));

      const matchesRole = !roleFilter || (user.role && user.role.toLowerCase() === roleFilter.toLowerCase());

      return matchesSearch && matchesRole;
    });
  }, [userSummaries, searchTerm, roleFilter]);

  // Compute summary statistics
  const stats = useMemo(() => {
    const totalUsers = userSummaries.length;
    const adminCount = userSummaries.filter((u) => u.role?.toLowerCase() === 'admin').length;
    const fellowCount = userSummaries.filter((u) => u.role?.toLowerCase() === 'fellow' || u.role?.toLowerCase() === 'intern').length;
    const totalAccessCount = userSummaries.reduce((acc, u) => acc + (u.moduleAccesses?.length || 0), 0);

    return { totalUsers, adminCount, fellowCount, totalAccessCount };
  }, [userSummaries]);

  const getInitials = (name: string) => {
    if (!name) return 'U';
    const parts = name.trim().split(' ');
    if (parts.length >= 2) return `${parts[0][0]}${parts[1][0]}`.toUpperCase();
    return name.slice(0, 2).toUpperCase();
  };

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

  const userProfileBody = (row: UserAccessSummaryDto) => (
    <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
      <div
        style={{
          width: 36,
          height: 36,
          borderRadius: '50%',
          background: 'linear-gradient(135deg, #4F46E5 0%, #6366F1 100%)',
          color: '#FFFFFF',
          fontWeight: 700,
          fontSize: 13,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          flexShrink: 0,
          boxShadow: '0 2px 6px rgba(79, 70, 229, 0.2)',
        }}
      >
        {getInitials(row.fullName || row.username)}
      </div>
      <div>
        <div style={{ fontWeight: 600, color: 'var(--text-heading, #1E293B)', fontSize: 14 }}>
          {row.fullName || row.username}
        </div>
        <div style={{ fontSize: 12, color: 'var(--text-secondary, #64748B)' }}>@{row.username}</div>
      </div>
    </div>
  );

  const roleBody = (row: UserAccessSummaryDto) => (
    <Tag value={row.role || 'Unassigned'} severity={getRoleSeverity(row.role)} style={{ padding: '4px 10px', fontSize: 12 }} />
  );

  const divisionBody = (row: UserAccessSummaryDto) => (
    row.divisionName ? (
      <span
        style={{
          fontSize: 12,
          fontWeight: 500,
          padding: '3px 8px',
          borderRadius: 4,
          background: 'var(--surface-200, #F1F5F9)',
          color: 'var(--text-body, #334155)',
        }}
      >
        {row.divisionName}
      </span>
    ) : (
      <span style={{ fontSize: 12, color: 'var(--text-muted, #94A3B8)', fontStyle: 'italic' }}>All / State Level</span>
    )
  );

  const moduleNameBody = (row: UserAccessSummaryDto) => {
    const accesses = row.moduleAccesses || [];
    if (accesses.length === 0) {
      return <span style={{ fontSize: 12, color: 'var(--text-muted, #94A3B8)' }}>No modules assigned</span>;
    }

    const displayCount = 3;
    const visibleModules = accesses.slice(0, displayCount);
    const hiddenCount = accesses.length - displayCount;

    return (
      <div style={{ display: 'flex', flexWrap: 'wrap', gap: 4, alignItems: 'center' }}>
        {visibleModules.map((m) => (
          <Tag
            key={m.moduleMasterId}
            value={m.moduleCode || m.moduleName}
            severity="info"
            style={{ fontSize: 11, padding: '2px 8px', fontWeight: 500 }}
          />
        ))}
        {hiddenCount > 0 && (
          <span
            style={{
              fontSize: 11,
              fontWeight: 600,
              padding: '2px 6px',
              borderRadius: 4,
              background: 'var(--primary-50, #EEF2FF)',
              color: 'var(--primary-700, #4338CA)',
            }}
            title={accesses.slice(displayCount).map((m) => m.moduleName || m.moduleCode).join(', ')}
          >
            +{hiddenCount} more
          </span>
        )}
      </div>
    );
  };

  const actionsBody = (row: UserAccessSummaryDto) => (
    <AppButton
      variant="secondary"
      size="sm"
      icon="pi pi-user-edit"
      onClick={() => setSelectedUser(row)}
      style={{ fontSize: 13 }}
    >
      Manage
    </AppButton>
  );

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 24 }}>
      <PageHeader
        title="User Access & Role Management"
        subtitle="Configure role permissions, geographic scopes, and granular module access control (RBAC)"
      />

      {/* KPI Metric Summary Cards */}
      <div
        style={{
          display: 'grid',
          gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))',
          gap: 16,
        }}
      >
        <div className="card" style={{ padding: '16px 20px', borderRadius: 12, background: 'var(--surface-card, #FFFFFF)', border: '1px solid var(--border-color, #E2E8F0)' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <span style={{ fontSize: 13, fontWeight: 500, color: 'var(--text-secondary, #64748B)' }}>Total User Accounts</span>
            <div style={{ width: 36, height: 36, borderRadius: 8, background: '#EEF2FF', color: '#4F46E5', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
              <i className="pi pi-users" style={{ fontSize: 18 }} />
            </div>
          </div>
          <div style={{ fontSize: 24, fontWeight: 700, marginTop: 8, color: 'var(--text-heading, #1E293B)' }}>
            {stats.totalUsers}
          </div>
        </div>

        <div className="card" style={{ padding: '16px 20px', borderRadius: 12, background: 'var(--surface-card, #FFFFFF)', border: '1px solid var(--border-color, #E2E8F0)' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <span style={{ fontSize: 13, fontWeight: 500, color: 'var(--text-secondary, #64748B)' }}>Administrators</span>
            <div style={{ width: 36, height: 36, borderRadius: 8, background: '#FEF2F2', color: '#EF4444', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
              <i className="pi pi-shield" style={{ fontSize: 18 }} />
            </div>
          </div>
          <div style={{ fontSize: 24, fontWeight: 700, marginTop: 8, color: 'var(--text-heading, #1E293B)' }}>
            {stats.adminCount}
          </div>
        </div>

        <div className="card" style={{ padding: '16px 20px', borderRadius: 12, background: 'var(--surface-card, #FFFFFF)', border: '1px solid var(--border-color, #E2E8F0)' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <span style={{ fontSize: 13, fontWeight: 500, color: 'var(--text-secondary, #64748B)' }}>Fellows & Interns</span>
            <div style={{ width: 36, height: 36, borderRadius: 8, background: '#ECFDF5', color: '#10B981', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
              <i className="pi pi-user" style={{ fontSize: 18 }} />
            </div>
          </div>
          <div style={{ fontSize: 24, fontWeight: 700, marginTop: 8, color: 'var(--text-heading, #1E293B)' }}>
            {stats.fellowCount}
          </div>
        </div>

        <div className="card" style={{ padding: '16px 20px', borderRadius: 12, background: 'var(--surface-card, #FFFFFF)', border: '1px solid var(--border-color, #E2E8F0)' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <span style={{ fontSize: 13, fontWeight: 500, color: 'var(--text-secondary, #64748B)' }}>Assigned Permissions</span>
            <div style={{ width: 36, height: 36, borderRadius: 8, background: '#F0FDFA', color: '#0D9488', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
              <i className="pi pi-key" style={{ fontSize: 18 }} />
            </div>
          </div>
          <div style={{ fontSize: 24, fontWeight: 700, marginTop: 8, color: 'var(--text-heading, #1E293B)' }}>
            {stats.totalAccessCount}
          </div>
        </div>
      </div>

      {/* Main Table Container */}
      <div className="card" style={{ borderRadius: 12, overflow: 'hidden', border: '1px solid var(--border-color, #E2E8F0)', background: 'var(--surface-card, #FFFFFF)' }}>
        {/* Search & Filter Toolbar */}
        <div
          style={{
            padding: '16px 20px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'space-between',
            gap: 16,
            flexWrap: 'wrap',
            borderBottom: '1px solid var(--border-color, #E2E8F0)',
            background: 'var(--surface-50, #F8FAFC)',
          }}
        >
          <div style={{ display: 'flex', gap: 12, flex: 1, minWidth: 280, flexWrap: 'wrap' }}>
            <div style={{ position: 'relative', flex: 1, minWidth: 220 }}>
              <i
                className="pi pi-search"
                style={{
                  position: 'absolute',
                  left: 12,
                  top: '50%',
                  transform: 'translateY(-50%)',
                  color: 'var(--text-secondary, #64748B)',
                  zIndex: 2,
                  pointerEvents: 'none',
                  fontSize: 14,
                }}
              />
              <InputText
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                placeholder="Search user, username, or division..."
                style={{ width: '100%', height: 38, paddingLeft: 34 }}
              />
            </div>
            <Dropdown
              value={roleFilter}
              options={ROLES_OPTIONS}
              onChange={(e) => setRoleFilter(e.value)}
              placeholder="Filter by Role"
              style={{ width: 180, height: 38 }}
              appendTo="self"
            />
          </div>

          {(searchTerm || roleFilter) && (
            <AppButton
              variant="ghost"
              size="sm"
              icon="pi pi-filter-slash"
              onClick={() => {
                setSearchTerm('');
                setRoleFilter('');
              }}
            >
              Clear Filters
            </AppButton>
          )}
        </div>

        {/* Table Content */}
        {isLoading ? (
          <SkeletonTable columns={5} />
        ) : (
          <DataTable
            value={filteredUsers}
            dataKey="userAccountId"
            paginator
            rows={10}
            rowsPerPageOptions={[10, 25, 50]}
            stripedRows
            responsiveLayout="scroll"
            emptyMessage={
              <div style={{ padding: 40, textAlign: 'center', color: 'var(--text-secondary)' }}>
                <i className="pi pi-users" style={{ fontSize: 32, color: 'var(--text-muted)', marginBottom: 12 }} />
                <h4 style={{ margin: '0 0 4px', fontSize: 16 }}>No users found</h4>
                <p style={{ margin: 0, fontSize: 13 }}>Try adjusting your search query or role filter.</p>
              </div>
            }
          >
            <Column header="User" body={userProfileBody} sortable sortField="fullName" style={{ minWidth: 220 }} />
            <Column header="Role" body={roleBody} sortable sortField="role" style={{ width: 140 }} />
            <Column header="Scope / Division" body={divisionBody} sortable sortField="divisionName" style={{ width: 180 }} />
            <Column header="Active Module Permissions" body={moduleNameBody} style={{ minWidth: 260 }} />
            <Column header="Actions" body={actionsBody} style={{ width: 120, textAlign: 'right' }} />
          </DataTable>
        )}
      </div>

      {/* User Access Modal */}
      <UserModuleAccessDialog
        visible={!!selectedUser}
        user={selectedUser}
        onHide={() => setSelectedUser(null)}
        onUserUpdated={() => refetch()}
      />
    </div>
  );
}
