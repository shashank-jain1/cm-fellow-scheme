import { useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { Tooltip } from 'primereact/tooltip';
import PageHeader from '../../../shared/components/ui/PageHeader';
import SkeletonTable from '../../../shared/components/ui/SkeletonTable';
import { useAuditLogQuery, useModulesQuery } from '../queries';
import type { AuditLogEntry } from '../types';

export default function AuditLogPage() {
  const [pageNumber, setPageNumber] = useState(1);
  const { data: logs, isLoading } = useAuditLogQuery({ pageNumber, pageSize: 50 });
  const { data: modules } = useModulesQuery();

  const dateBody = (row: AuditLogEntry) =>
    new Date(row.performedOn).toLocaleString();

  const actionBody = (row: AuditLogEntry) => {
    const severity =
      row.action === 'GRANT' ? 'success' :
      row.action === 'REVOKE' ? 'danger' :
      row.action === 'UPDATE' ? 'warn' : 'info';
    return <Tag value={row.action} severity={severity} />;
  };

  const detailsBody = (row: AuditLogEntry) => {
    if (!row.oldValues && !row.newValues) return <span style={{ color: 'var(--text-muted)' }}>—</span>;
    const tooltipId = `details-${row.moduleAccessAuditLogId}`;
    return (
      <>
        <i
          className="pi pi-info-circle"
          style={{ cursor: 'pointer', color: 'var(--text-secondary)' }}
          data-pr-tooltip={buildTooltipContent(row)}
          data-pr-position="top"
        />
        <Tooltip target={`#${tooltipId}`} />
      </>
    );
  };

  return (
    <div>
      <PageHeader
        title="Audit Log"
        subtitle="Track all module access changes"
      />

      {isLoading ? (
        <SkeletonTable columns={6} />
      ) : (
        <DataTable
          value={logs ?? []}
          dataKey="moduleAccessAuditLogId"
          paginator
          rows={25}
          rowsPerPageOptions={[10, 25, 50]}
          stripedRows
          emptyMessage="No audit log entries found."
          first={(pageNumber - 1) * 25}
          onPage={(e) => setPageNumber(Math.floor(e.first / e.rows) + 1)}
        >
          <Column field="performedOn" header="Date/Time" body={dateBody} sortable style={{ minWidth: 160 }} />
          <Column field="fullName" header="User" sortable />
          <Column field="moduleName" header="Module" sortable />
          <Column field="action" header="Action" body={actionBody} sortable />
          <Column field="performerName" header="Performed By" sortable />
          <Column header="Details" body={detailsBody} style={{ width: 80 }} />
        </DataTable>
      )}
    </div>
  );
}

function buildTooltipContent(row: AuditLogEntry): string {
  const parts: string[] = [];
  if (row.reason) parts.push(`Reason: ${row.reason}`);
  if (row.oldValues) parts.push(`Old: ${row.oldValues}`);
  if (row.newValues) parts.push(`New: ${row.newValues}`);
  return parts.join('\n') || 'No details';
}
