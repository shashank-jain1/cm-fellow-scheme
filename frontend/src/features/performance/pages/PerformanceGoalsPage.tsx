import { useState } from 'react';
import { useAuth } from '../../auth/useAuth';
import { useGoalsByUser, useCreateGoal, useUpdateGoalStatus } from '../queries';
import { AppButton, PageHeader } from '../../../shared/components/ui';
import type { GoalStatus } from '../types';
import GoalCreateForm from '../components/GoalCreateForm';
import GoalsTable from '../components/GoalsTable';

const nextStatusMap: Record<GoalStatus, GoalStatus | null> = {
  NotStarted: 'InProgress',
  InProgress: 'Completed',
  Completed: null,
  Missed: null,
};

export default function PerformanceGoalsPage() {
  const { user } = useAuth();
  const userId = user?.userAccountId ?? 0;
  const { data: goals = [], isLoading } = useGoalsByUser(userId);
  const createGoal = useCreateGoal();
  const updateGoalStatus = useUpdateGoalStatus();

  const [showForm, setShowForm] = useState(false);
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [targetDate, setTargetDate] = useState<Date | null>(null);

  const handleCreate = async () => {
    if (!title.trim() || !targetDate) return;
    await createGoal.mutateAsync({ title: title.trim(), description: description.trim(), targetDate: targetDate.toISOString() });
    setTitle(''); setDescription(''); setTargetDate(null); setShowForm(false);
  };

  const handleAdvanceStatus = async (goalId: number, currentStatus: GoalStatus) => {
    const next = nextStatusMap[currentStatus];
    if (!next) return;
    await updateGoalStatus.mutateAsync({ goalId, command: { status: next } });
  };

  return (
    <div>
      <PageHeader
        title="Performance Goals"
        subtitle="Set and track your performance goals"
        action={<AppButton icon="pi pi-plus" onClick={() => setShowForm(!showForm)}>{showForm ? 'Cancel' : 'New Goal'}</AppButton>}
      />
      {showForm && (
        <GoalCreateForm title={title} setTitle={setTitle} description={description} setDescription={setDescription}
          targetDate={targetDate} setTargetDate={setTargetDate} onSubmit={handleCreate}
          onCancel={() => setShowForm(false)} isPending={createGoal.isPending} />
      )}
      <div className="table-wrapper">
        <GoalsTable goals={goals} isLoading={isLoading} onAdvanceStatus={handleAdvanceStatus} isPending={updateGoalStatus.isPending} />
      </div>
    </div>
  );
}
