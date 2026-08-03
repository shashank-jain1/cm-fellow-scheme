import { AppSelect } from '../../../shared/components/forms';
import { useProjects, useWorks } from '../../masters/queries';

interface Props {
  projectId: number;
  workProjectId: number;
  onProjectChange: (v: number) => void;
  onWorkChange: (v: number) => void;
}

export default function ActivityTypeSelect({ projectId, workProjectId, onProjectChange, onWorkChange }: Props) {
  const { data: projects = [] } = useProjects();
  const { data: works = [] } = useWorks(projectId || undefined);

  const projectOptions = projects.map((p) => ({ label: p.projectName, value: p.projectId }));
  const workOptions = works.map((w) => ({ label: w.workName, value: w.workId }));

  return (
    <>
      <div className="form-field">
        <label>Project Name *</label>
        <AppSelect value={projectId} options={projectOptions} onChange={(v) => { onProjectChange(v); onWorkChange(0); }} placeholder="Select project" />
      </div>
      <div className="form-field">
        <label>Work Project *</label>
        <AppSelect value={workProjectId} options={workOptions} onChange={(v) => onWorkChange(v)} placeholder="Select work project" disabled={!projectId} />
      </div>
    </>
  );
}
