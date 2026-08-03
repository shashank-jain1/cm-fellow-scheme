import { useState } from 'react';
import { Button } from 'primereact/button';
import { AppDialog } from '../../../../shared/components/forms';
import TrainingScheduleForm from './TrainingScheduleForm';
import TrainingScheduleList from './TrainingScheduleList';
import type { TrainingScheduleDto } from '../../types';

export default function TrainingSchedulePage() {
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<TrainingScheduleDto | null>(null);

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Training Schedule Calendar</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Plan and manage training sessions
          </p>
        </div>
        <Button
          label="Add Training Schedule"
          icon="pi pi-plus"
          onClick={() => {
            setEditingItem(null);
            setDialogOpen(true);
          }}
        />
      </div>
      <TrainingScheduleList
        onEdit={(row) => {
          setEditingItem(row);
          setDialogOpen(true);
        }}
      />
      <AppDialog
        header={editingItem ? 'Edit Training Schedule' : 'Add Training Schedule'}
        visible={dialogOpen}
        onHide={() => {
          setDialogOpen(false);
          setEditingItem(null);
        }}
        modal
        style={{ width: '640px' }}
      >
        <TrainingScheduleForm
          initialData={editingItem}
          onDone={() => {
            setDialogOpen(false);
            setEditingItem(null);
          }}
        />
      </AppDialog>
    </div>
  );
}
