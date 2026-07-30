import { useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import PageHeader from '../../../shared/components/ui/PageHeader';
import SkeletonTable from '../../../shared/components/ui/SkeletonTable';
import AppButton from '../../../shared/components/ui/AppButton';
import { useAllModuleAccessQuery } from '../queries';
import UserModuleAccessDialog from '../components/UserModuleAccessDialog';
import type { UserAccessSummaryDto } from '../types';

function buildSummaries(data: NonNullable<ReturnType<typeof useAllModuleAccessQuery>['data']>): UserAccessSummaryDto[] {
  const map = new Map<number, UserAccessSummaryDto>();
  for (const access of data) {
    if (!map.has(access.userAccountId)) {
      map.set(access.userAccountId, {
        userAccountId: access.userAccountId,
        username: access.username,
        fullName: access.fullName,
        role: '',
        moduleAccesses: [],
      });
    }
    map.get(access.userAccountId)!.moduleAccesses.push(access);
  }
  return Array.from(map.values());
}

export default function UserAccessPage() {
  const { data: allAccess, isLoading } = useAllModuleAccessQuery();
  const [selectedUser, setSelectedUser] = useState<UserAccessSummaryDto | null>(null);

  const summaries = allAccess ? buildSummaries(allAccess) : [];

  const moduleNameBody = (row: UserAccessSummaryDto) => (
    <div style={{ display: 'flex', flexWrap: 'wrap', gap: 4 }}>
      {row.moduleAccesses.map((m) => (
        <Tag key={m.moduleMasterId} value={m.moduleCode} severity="info" />
      ))}
    </div>
  );

  const actionsBody = (row: UserAccessSummaryDto) => (
    <AppButton
      variant="secondary"
      size="sm"
      onClick={() => setSelectedUser(row)}
    >
      Manage
    </AppButton>
  );

  return (
    <div>
      <PageHeader
        title="Module Access Management"
        subtitle="Grant or revoke module access for users"
      />

      {isLoading ? (
        <SkeletonTable columns={5} />
      ) : (
        <DataTable
          value={summaries}
          dataKey="userAccountId"
          paginator
          rows={10}
          rowsPerPageOptions={[10, 25, 50]}
          stripedRows
          emptyMessage="No users found."
        >
          <Column field="username" header="Username" sortable />
          <Column field="fullName" header="Full Name" sortable />
          <Column field="role" header="Role" sortable />
          <Column field="divisionName" header="Division" sortable />
          <Column header="Modules" body={moduleNameBody} />
          <Column header="Actions" body={actionsBody} style={{ width: 120 }} />
        </DataTable>
      )}

      <UserModuleAccessDialog
        visible={!!selectedUser}
        user={selectedUser}
        onHide={() => setSelectedUser(null)}
      />
    </div>
  );
}
