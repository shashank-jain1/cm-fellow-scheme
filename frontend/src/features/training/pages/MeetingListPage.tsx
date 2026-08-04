import { useNavigate } from 'react-router-dom';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { useTrainingMeetings } from '../queries';
import { AppButton, SkeletonTable } from '../../../shared/components/ui';
import type { TrainingScheduleDto } from '../types';

export default function MeetingListPage() {
  const navigate = useNavigate();
  const { data: meetings, isLoading } = useTrainingMeetings();

  const statusBodyTemplate = (rowData: TrainingScheduleDto) => {
    const severity =
      rowData.status === 'Completed' ? 'success' :
      rowData.status === 'Scheduled' ? 'info' :
      rowData.status === 'Cancelled' ? 'danger' : 'warning';
    return <Tag value={rowData.status} severity={severity} />;
  };

  const dateBodyTemplate = (rowData: TrainingScheduleDto) => {
    if (!rowData.date) return '—';
    return new Date(rowData.date).toLocaleDateString();
  };

  const actionBodyTemplate = (rowData: TrainingScheduleDto) => {
    return (
      <AppButton
        variant="secondary"
        icon="pi pi-eye"
        onClick={() => navigate(`/training/meetings/${rowData.trainingScheduleId}`)}
      >
        View
      </AppButton>
    );
  };

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Meetings</h1>
          <p style={{ color: 'var(--text-muted)', marginTop: 4 }}>
            Overview of all scheduled team and stakeholder meetings
          </p>
        </div>
        <AppButton
          icon="pi pi-plus"
          onClick={() => navigate('/training/new')}
        >
          New Meeting
        </AppButton>
      </div>

      <div className="card" style={{ padding: 24 }}>
        {isLoading ? (
          <SkeletonTable columns={6} />
        ) : (
          <DataTable
            value={meetings ?? []}
            paginator
            rows={10}
            rowsPerPageOptions={[5, 10, 25]}
            responsiveLayout="scroll"
            emptyMessage="No meetings scheduled yet."
          >
            <Column field="trainingScheduleId" header="ID" sortable style={{ width: '80px' }} />
            <Column field="meetingTitle" header="Meeting Title" sortable body={(r) => r.meetingTitle || r.trainingTitle || 'Untitled Meeting'} />
            <Column field="date" header="Date" sortable body={dateBodyTemplate} />
            <Column field="mode" header="Mode" sortable />
            <Column field="status" header="Status" sortable body={statusBodyTemplate} />
            <Column body={actionBodyTemplate} header="Action" style={{ width: '100px', textAlign: 'center' }} />
          </DataTable>
        )}
      </div>
    </div>
  );
}
