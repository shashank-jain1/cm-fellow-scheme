import { useState } from 'react';
import { Button } from 'primereact/button';
import TaskProgressGrid from '../components/TaskProgressGrid';
import TaskProgressUpdateForm from '../components/TaskProgressUpdateForm';
import SurveyDetailsDrilldownGrid from '../components/SurveyDetailsDrilldownGrid';
import { useTaskProgress, useSurveyDetails } from '../queries';
import type { TaskProgressDto } from '../types';

export default function TaskProgressPage() {
  const [selectedTask, setSelectedTask] = useState<TaskProgressDto | null>(null);

  const { data: taskProgress, isLoading } = useTaskProgress();
  const { data: surveyDetails, isLoading: isLoadingSurveys } = useSurveyDetails(
    selectedTask?.taskProgressId ?? 0
  );

  const handleRowClick = (task: TaskProgressDto) => {
    setSelectedTask(task);
  };

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Task Progress</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Monitor project survey progress and completion rates
          </p>
        </div>
        {selectedTask && (
          <Button
            label="Back to Progress"
            icon="pi pi-arrow-left"
            className="btn btn-secondary"
            onClick={() => setSelectedTask(null)}
          />
        )}
      </div>

      {selectedTask ? (
        <>
          <TaskProgressUpdateForm
            workAllocationId={selectedTask.workAllocationId}
            currentProgress={selectedTask.completionPercentage}
            currentStatus={selectedTask.workStatus}
          />
          <div style={{ marginTop: 20 }}>
            <SurveyDetailsDrilldownGrid
              data={surveyDetails ?? []}
              isLoading={isLoadingSurveys}
              workProject={selectedTask.workProject}
            />
          </div>
        </>
      ) : (
        <TaskProgressGrid
          data={taskProgress ?? []}
          isLoading={isLoading}
          onRowClick={handleRowClick}
        />
      )}
    </div>
  );
}
