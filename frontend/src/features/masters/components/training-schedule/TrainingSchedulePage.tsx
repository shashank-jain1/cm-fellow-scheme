import { useState } from 'react';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { Dropdown, DropdownChangeEvent } from 'primereact/dropdown';
import { useTrainingSchedules, useProjects } from '../../queries';
import TrainingScheduleForm from './TrainingScheduleForm';
import type { TrainingScheduleDto } from '../../types';

const YEAR_OPTIONS = Array.from({ length: 5 }, (_, i) => {
  const year = new Date().getFullYear() + i - 1;
  return { label: `${year}-${(year + 1).toString().slice(-2)}`, value: `${year}-${(year + 1).toString().slice(-2)}` };
});

export default function TrainingSchedulePage() {
  const [selectedYear, setSelectedYear] = useState<string | undefined>(undefined);
  const [selectedProjectId, setSelectedProjectId] = useState<number | undefined>(undefined);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<TrainingScheduleDto | null>(null);

  const { data: projects = [] } = useProjects();
  const { data: schedules = [], isLoading } = useTrainingSchedules({
    calendarYear: selectedYear,
    projectId: selectedProjectId,
  });

  const projectOptions = projects.map((p) => ({ label: p.projectName, value: p.projectId }));

  return (
    <div className="page-container">
      <div className="page-header">
        <div>
          <h2>Training Schedule Calendar</h2>
          <p className="text-secondary">Plan and manage training sessions</p>
        </div>
        <Button label="Add Training Schedule" icon="pi pi-plus" onClick={() => { setEditingItem(null); setDialogOpen(true); }} />
      </div>

      <div className="form-grid mb-4">
        <div className="form-field">
          <label>Calendar Year</label>
          <Dropdown
            value={selectedYear}
            options={YEAR_OPTIONS}
            onChange={(e: DropdownChangeEvent) => setSelectedYear(e.value)}
            placeholder="All Years"
            showClear
            className="w-full"
          />
        </div>
        <div className="form-field">
          <label>Project</label>
          <Dropdown
            value={selectedProjectId}
            options={projectOptions}
            onChange={(e: DropdownChangeEvent) => setSelectedProjectId(e.value)}
            placeholder="All Projects"
            showClear
            className="w-full"
          />
        </div>
      </div>

      <div className="card">
        <table className="data-table">
          <thead>
            <tr>
              <th>Year</th>
              <th>Project</th>
              <th>Training Date</th>
              <th>Venue</th>
              <th>Division</th>
              <th>District</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {isLoading ? (
              <tr><td colSpan={7} className="text-center">Loading...</td></tr>
            ) : schedules.length === 0 ? (
              <tr><td colSpan={7} className="text-center text-secondary">No training schedules found</td></tr>
            ) : schedules.map((s) => (
              <tr key={s.trainingScheduleId}>
                <td>{s.calendarYear}</td>
                <td>{s.projectName}</td>
                <td>{new Date(s.trainingDate).toLocaleDateString()}</td>
                <td>{s.venueName ?? '-'}</td>
                <td>{s.divisionName ?? '-'}</td>
                <td>{s.districtName ?? '-'}</td>
                <td>
                  <Button
                    icon="pi pi-pencil"
                    size="small"
                    text
                    onClick={() => { setEditingItem(s); setDialogOpen(true); }}
                  />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <Dialog
        header={editingItem ? 'Edit Training Schedule' : 'Add Training Schedule'}
        visible={dialogOpen}
        onHide={() => { setDialogOpen(false); setEditingItem(null); }}
        modal
        style={{ width: '640px' }}
      >
        <TrainingScheduleForm
          initialData={editingItem}
          onDone={() => { setDialogOpen(false); setEditingItem(null); }}
        />
      </Dialog>
    </div>
  );
}
