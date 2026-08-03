import { useState, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { Toast } from 'primereact/toast';
import { useTrainingSessions, useUpdateTrainingStatus } from '../queries';
import { PageHeader, AppButton, EmptyState } from '../../../shared/components/ui';
import CalendarGrid from './CalendarGrid';
import ActivityFilters from './ActivityFilters';

export default function ActivityCalendar() {
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();
  const toast = useRef<Toast>(null);
  const { data: schedules, isLoading } = useTrainingSessions();
  const updateStatusMutation = useUpdateTrainingStatus();

  const filteredSchedules = (schedules ?? []).filter(
    (s) =>
      (s.trainingTitle ?? s.meetingTitle ?? '').toLowerCase().includes(searchTerm.toLowerCase()) ||
      s.activityType.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleStatusChange = async (id: number, newStatus: string) => {
    try {
      await updateStatusMutation.mutateAsync({ trainingScheduleId: id, newStatus });
      toast.current?.show({ severity: 'success', summary: 'Status Updated', detail: `Training status changed to ${newStatus}` });
    } catch {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: 'Failed to update status' });
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader title="Activity Calendar" subtitle="View and manage training & meeting activities"
        action={<AppButton onClick={() => navigate('/training/new')} icon="pi pi-plus">New Activity</AppButton>} />
      <ActivityFilters searchTerm={searchTerm} onSearchChange={setSearchTerm} />
      {isLoading || filteredSchedules.length > 0 ? (
        <CalendarGrid schedules={filteredSchedules} isLoading={isLoading} isPending={updateStatusMutation.isPending}
          onStatusChange={handleStatusChange} />
      ) : (
        <EmptyState icon="pi pi-calendar" title="No activities found" description="Create a new activity to get started" />
      )}
    </div>
  );
}
