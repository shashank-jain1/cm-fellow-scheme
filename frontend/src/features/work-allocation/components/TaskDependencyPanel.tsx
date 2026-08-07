import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { AppButton } from '../../../shared/components/ui';
import { useTaskDependenciesByWorkAllocation, useCreateTaskDependency, useTaskProgress } from '../queries';
import TaskDependencyList from './TaskDependencyList';
import TaskDependencyAddForm from './TaskDependencyAddForm';

interface TaskDependencyPanelProps {
  workAllocationId: number;
}

export default function TaskDependencyPanel({ workAllocationId }: TaskDependencyPanelProps) {
  const toast = useRef<Toast>(null);
  const { data: dependencies, isLoading } = useTaskDependenciesByWorkAllocation(workAllocationId);
  const { data: allTasks } = useTaskProgress();
  const createDependency = useCreateTaskDependency();

  const [selectedTaskId, setSelectedTaskId] = useState<number | null>(null);
  const [prerequisiteId, setPrerequisiteId] = useState<number | null>(null);
  const [showForm, setShowForm] = useState(false);

  const workAllocationTasks = (allTasks ?? []).filter((t) => t.workAllocationId === workAllocationId);

  const handleAddDependency = async () => {
    if (!selectedTaskId || !prerequisiteId) {
      toast.current?.show({ severity: 'warn', summary: 'Validation', detail: 'Select both task and prerequisite' });
      return;
    }
    if (selectedTaskId === prerequisiteId) {
      toast.current?.show({ severity: 'warn', summary: 'Validation', detail: 'Task cannot depend on itself' });
      return;
    }
    try {
      await createDependency.mutateAsync({ workAllocationId: selectedTaskId, dependsOnWorkAllocationId: prerequisiteId });
      toast.current?.show({ severity: 'success', summary: 'Success', detail: 'Dependency added' });
      setShowForm(false);
      setSelectedTaskId(null);
      setPrerequisiteId(null);
    } catch {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: 'Failed to add dependency' });
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <h3 style={{ margin: 0, fontSize: 16, fontWeight: 600 }}>Task Dependencies</h3>
        <AppButton size="sm" icon="pi pi-plus" onClick={() => setShowForm(!showForm)}>Add Dependency</AppButton>
      </div>
      {showForm && (
        <TaskDependencyAddForm
          tasks={workAllocationTasks} selectedTaskId={selectedTaskId} setSelectedTaskId={setSelectedTaskId}
          prerequisiteId={prerequisiteId} setPrerequisiteId={setPrerequisiteId}
          onSubmit={handleAddDependency} onCancel={() => setShowForm(false)} isPending={createDependency.isPending}
        />
      )}
      <TaskDependencyList dependencies={dependencies ?? []} isLoading={isLoading} />
    </div>
  );
}
