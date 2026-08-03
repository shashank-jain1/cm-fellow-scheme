import { useState } from 'react';
import { Button } from 'primereact/button';
import { useCreateTrainingSchedule, useUpdateTrainingSchedule, useProjects, useWorks, useDivisions, useDistricts, useBlocks } from '../../queries';
import ScheduleBasicFields from './ScheduleBasicFields';
import ScheduleDateFields from './ScheduleDateFields';
import ScheduleVenueFields from './ScheduleVenueFields';
import type { TrainingScheduleDto } from '../../types';

interface Props {
  initialData: TrainingScheduleDto | null;
  onDone: () => void;
}

export default function TrainingScheduleForm({ initialData, onDone }: Props) {
  const [calendarYear, setCalendarYear] = useState<string>(initialData?.calendarYear ?? '');
  const [projectId, setProjectId] = useState<number | null>(initialData?.projectId ?? null);
  const [workId, setWorkId] = useState<number | null>(initialData?.workId ?? null);
  const [divisionId, setDivisionId] = useState<number | null>(initialData?.divisionId ?? null);
  const [districtId, setDistrictId] = useState<number | null>(initialData?.districtId ?? null);
  const [blockId, setBlockId] = useState<number | null>(initialData?.blockId ?? null);
  const [trainingDate, setTrainingDate] = useState<Date | null>(
    initialData?.trainingDate ? new Date(initialData.trainingDate) : null
  );
  const [venueName, setVenueName] = useState<string>(initialData?.venueName ?? '');
  const [trainingDescription, setTrainingDescription] = useState<string>(initialData?.trainingDescription ?? '');

  const { data: projects = [] } = useProjects();
  const { data: works = [] } = useWorks(projectId ?? undefined);
  const { data: divisions = [] } = useDivisions();
  const { data: districts = [] } = useDistricts(divisionId ?? undefined);
  const { data: blocks = [] } = useBlocks(districtId ?? undefined);
  const createMutation = useCreateTrainingSchedule();
  const updateMutation = useUpdateTrainingSchedule();

  const projectOptions = projects.map((p) => ({ label: p.projectName, value: p.projectId }));
  const workOptions = works.map((w) => ({ label: w.workName, value: w.workId }));
  const divisionOptions = divisions.map((d) => ({ label: d.divisionName, value: d.divisionId }));
  const districtOptions = districts.map((d) => ({ label: d.districtName, value: d.districtId }));
  const blockOptions = blocks.map((b) => ({ label: b.blockName, value: b.blockId }));

  const handleSubmit = async () => {
    if (!calendarYear || !projectId || !trainingDate) return;
    const payload = {
      calendarYear, projectId, workId: workId ?? undefined,
      divisionId: divisionId ?? undefined, districtId: districtId ?? undefined,
      blockId: blockId ?? undefined, trainingDate: trainingDate.toISOString(),
      venueName: venueName || undefined, trainingDescription: trainingDescription || undefined,
    };
    if (initialData) {
      await updateMutation.mutateAsync({ id: initialData.trainingScheduleId, data: { ...payload, trainingScheduleId: initialData.trainingScheduleId } });
    } else {
      await createMutation.mutateAsync(payload);
    }
    onDone();
  };

  return (
    <div className="form-grid">
      <ScheduleBasicFields calendarYear={calendarYear} projectId={projectId} workId={workId}
        projectOptions={projectOptions} workOptions={workOptions}
        onCalendarYearChange={setCalendarYear} onProjectChange={(v) => { setProjectId(v); setWorkId(null); }}
        onWorkChange={setWorkId} />
      <ScheduleVenueFields divisionId={divisionId} districtId={districtId} blockId={blockId}
        divisionOptions={divisionOptions} districtOptions={districtOptions} blockOptions={blockOptions}
        onDivisionChange={(v) => { setDivisionId(v); setDistrictId(null); setBlockId(null); }}
        onDistrictChange={(v) => { setDistrictId(v); setBlockId(null); }} onBlockChange={setBlockId} />
      <ScheduleDateFields trainingDate={trainingDate} venueName={venueName} trainingDescription={trainingDescription}
        onTrainingDateChange={setTrainingDate} onVenueNameChange={setVenueName} onTrainingDescriptionChange={setTrainingDescription} />
      <div className="form-field col-span-full" style={{ display: 'flex', justifyContent: 'flex-end', gap: 12, marginTop: 8 }}>
        <Button label="Cancel" severity="secondary" text onClick={onDone} />
        <Button label={initialData ? 'Update' : 'Create'} onClick={handleSubmit}
          loading={createMutation.isPending || updateMutation.isPending} disabled={!calendarYear || !projectId || !trainingDate} />
      </div>
    </div>
  );
}
