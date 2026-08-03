import { useState } from 'react';
import { Button } from 'primereact/button';
import { AppSelect } from '../../../../shared/components/forms';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { useTrainingSchedules, useProjects } from '../../queries';
import { EmptyState, SkeletonTable } from '../../../../shared/components/ui';
import type { TrainingScheduleDto } from '../../types';

interface TrainingScheduleListProps {
  onEdit: (item: TrainingScheduleDto) => void;
}

export default function TrainingScheduleList({ onEdit }: TrainingScheduleListProps) {
  const [selectedYear, setSelectedYear] = useState<string | undefined>(undefined);
  const [selectedProjectId, setSelectedProjectId] = useState<number | undefined>(undefined);
  const { data: projects = [] } = useProjects();
  const { data: schedules = [], isLoading } = useTrainingSchedules({
    calendarYear: selectedYear,
    projectId: selectedProjectId,
  });

  const projectOptions = projects.map((p) => ({ label: p.projectName, value: p.projectId }));
  const dateBody = (row: TrainingScheduleDto) => new Date(row.trainingDate).toLocaleDateString();
  const actionsBody = (row: TrainingScheduleDto) => (
    <Button
      icon="pi pi-pencil"
      size="small"
      text
      onClick={() => onEdit(row)}
    />
  );

  return (
    <>
      <div style={{ display: 'flex', gap: 20, marginBottom: 24, alignItems: 'flex-end' }}>
        <div style={{ width: 240 }}>
          <label style={{ display: 'block', fontSize: 13, fontWeight: 600, color: 'var(--text-secondary)', marginBottom: 6 }}>
            Calendar Year
          </label>
          <AppSelect
            value={selectedYear ?? ''}
            options={YEAR_OPTIONS}
            onChange={(val) => setSelectedYear(val as string)}
            placeholder="All Years"
            showClear
            className="w-full"
          />
        </div>
        <div style={{ width: 280 }}>
          <label style={{ display: 'block', fontSize: 13, fontWeight: 600, color: 'var(--text-secondary)', marginBottom: 6 }}>
            Project
          </label>
          <AppSelect
            value={selectedProjectId ?? ''}
            options={projectOptions}
            onChange={(val) => setSelectedProjectId(val as number)}
            placeholder="All Projects"
            showClear
            className="w-full"
          />
        </div>
      </div>
      <div className="card" style={{ padding: 0 }}>
        {isLoading ? (
          <SkeletonTable columns={7} />
        ) : schedules.length > 0 ? (
          <DataTable value={schedules} emptyMessage="No training schedules found" stripedRows paginator rows={10}>
            <Column field="calendarYear" header="Year" />
            <Column field="projectName" header="Project" />
            <Column header="Training Date" body={dateBody} />
            <Column field="venueName" header="Venue" />
            <Column field="divisionName" header="Division" />
            <Column field="districtName" header="District" />
            <Column header="Actions" body={actionsBody} style={{ width: 80 }} />
          </DataTable>
        ) : (
          <EmptyState
            icon="pi pi-calendar"
            title="No training schedules found"
            description="Add a training schedule to get started"
          />
        )}
      </div>
    </>
  );
}

const YEAR_OPTIONS = Array.from({ length: 5 }, (_, i) => {
  const year = new Date().getFullYear() + i - 1;
  return {
    label: `${year}-${(year + 1).toString().slice(-2)}`,
    value: `${year}-${(year + 1).toString().slice(-2)}`,
  };
});
