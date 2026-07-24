import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { useTrainingSchedules } from '../queries';
import { formatDate } from '../../../shared/utils/format';

export default function ActivityList() {
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();
  const { data: schedules, isLoading } = useTrainingSchedules();

  const filteredSchedules = (schedules ?? []).filter(
    (s) =>
      (s.trainingTitle ?? s.meetingTitle ?? '').toLowerCase().includes(searchTerm.toLowerCase()) ||
      s.activityType.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const statusBody = (status: string) => {
    const severity = (() => {
      switch (status) {
        case 'upcoming': return 'info';
        case 'ongoing': return 'success';
        case 'completed': return 'secondary';
        case 'cancelled': return 'danger';
        default: return 'secondary';
      }
    })();
    return <Tag value={status} severity={severity} />;
  };

  const titleBody = (row: typeof filteredSchedules[number]) => row.trainingTitle ?? row.meetingTitle ?? '-';

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Activity List</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            All scheduled training and meeting activities
          </p>
        </div>
        <Button
          label="New Activity"
          icon="pi pi-plus"
          className="btn btn-primary"
          onClick={() => navigate('/training/new')}
        />
      </div>

      <div style={{ display: 'flex', gap: 12, marginBottom: 24 }}>
        <div style={{ position: 'relative', flex: '0 0 320px' }}>
          <i className="pi pi-search" style={{ position: 'absolute', left: 12, top: '50%', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
          <InputText
            value={searchTerm}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearchTerm(e.target.value)}
            placeholder="Search activities..."
            style={{ width: '100%', paddingLeft: 36 }}
          />
        </div>
      </div>

      <div className="card" style={{ padding: 0 }}>
        <DataTable
          value={filteredSchedules}
          loading={isLoading}
          emptyMessage="No activities found"
          stripedRows
          paginator
          rows={10}
        >
          <Column field="activityType" header="Type" />
          <Column header="Title" body={titleBody} />
          <Column field="date" header="Date" body={(row) => formatDate(row.date)} />
          <Column field="startTime" header="Start" />
          <Column field="endTime" header="End" />
          <Column field="mode" header="Mode" />
          <Column field="status" header="Status" body={statusBody} />
        </DataTable>
      </div>
    </div>
  );
}
