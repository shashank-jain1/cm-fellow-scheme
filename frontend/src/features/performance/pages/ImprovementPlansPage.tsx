import { useState } from 'react';
import { useAuth } from '../../auth/useAuth';
import { useImprovementPlansByUser, useCreateImprovementPlan } from '../queries';
import { AppButton, PageHeader } from '../../../shared/components/ui';
import ImprovementPlanCreateForm from '../components/ImprovementPlanCreateForm';
import ImprovementPlansTable from '../components/ImprovementPlansTable';

export default function ImprovementPlansPage() {
  const { user } = useAuth();
  const userId = user?.userAccountId ?? 0;
  const { data: plans = [], isLoading } = useImprovementPlansByUser(userId);
  const createPlan = useCreateImprovementPlan();

  const [showForm, setShowForm] = useState(false);
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);

  const handleCreate = async () => {
    if (!title.trim() || !startDate || !endDate) return;
    await createPlan.mutateAsync({ title: title.trim(), description: description.trim(), startDate: startDate.toISOString(), endDate: endDate.toISOString() });
    setTitle(''); setDescription(''); setStartDate(null); setEndDate(null); setShowForm(false);
  };

  return (
    <div>
      <PageHeader
        title="Improvement Plans"
        subtitle="Track performance improvement plans"
        action={<AppButton icon="pi pi-plus" onClick={() => setShowForm(!showForm)}>{showForm ? 'Cancel' : 'New Plan'}</AppButton>}
      />
      {showForm && (
        <ImprovementPlanCreateForm title={title} setTitle={setTitle} description={description} setDescription={setDescription}
          startDate={startDate} setStartDate={setStartDate} endDate={endDate} setEndDate={setEndDate}
          onSubmit={handleCreate} onCancel={() => setShowForm(false)} isPending={createPlan.isPending} />
      )}
      <div className="table-wrapper">
        <ImprovementPlansTable plans={plans} isLoading={isLoading} />
      </div>
    </div>
  );
}
