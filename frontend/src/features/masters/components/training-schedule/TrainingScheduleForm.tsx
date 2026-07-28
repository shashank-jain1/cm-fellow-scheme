import { useState } from 'react';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { Dropdown, DropdownChangeEvent } from 'primereact/dropdown';
import { Calendar, CalendarChangeEvent } from 'primereact/calendar';
import { useCreateTrainingSchedule, useUpdateTrainingSchedule, useProjects, useWorks, useDivisions, useDistricts, useBlocks } from '../../queries';
import type { TrainingScheduleDto } from '../../types';

interface Props {
  initialData: TrainingScheduleDto | null;
  onDone: () => void;
}

const YEAR_OPTIONS = Array.from({ length: 5 }, (_, i) => {
  const year = new Date().getFullYear() + i - 1;
  return { label: `${year}-${(year + 1).toString().slice(-2)}`, value: `${year}-${(year + 1).toString().slice(-2)}` };
});

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
      calendarYear,
      projectId,
      workId: workId ?? undefined,
      divisionId: divisionId ?? undefined,
      districtId: districtId ?? undefined,
      blockId: blockId ?? undefined,
      trainingDate: trainingDate.toISOString(),
      venueName: venueName || undefined,
      trainingDescription: trainingDescription || undefined,
    };

    if (initialData) {
      await updateMutation.mutateAsync({ id: initialData.trainingScheduleId, data: { ...payload, trainingScheduleId: initialData.trainingScheduleId } });
    } else {
      await createMutation.mutateAsync(payload);
    }
    onDone();
  };

  const handleParentChange = (setter: React.Dispatch<React.SetStateAction<number | null>>) => (e: DropdownChangeEvent) => {
    setter(e.value);
  };

  return (
    <div className="form-grid">
      <div className="form-field">
        <label>Calendar Year *</label>
        <Dropdown
          value={calendarYear}
          options={YEAR_OPTIONS}
          onChange={(e: DropdownChangeEvent) => setCalendarYear(e.value)}
          placeholder="Select Year"
          className="w-full"
        />
      </div>

      <div className="form-field">
        <label>Project *</label>
        <Dropdown
          value={projectId}
          options={projectOptions}
          onChange={(e: DropdownChangeEvent) => { setProjectId(e.value); setWorkId(null); }}
          placeholder="Select Project"
          className="w-full"
        />
      </div>

      <div className="form-field">
        <label>Work</label>
        <Dropdown
          value={workId}
          options={workOptions}
          onChange={(e: DropdownChangeEvent) => setWorkId(e.value)}
          placeholder="Select Work"
          disabled={!projectId}
          className="w-full"
        />
      </div>

      <div className="form-field">
        <label>Division</label>
        <Dropdown
          value={divisionId}
          options={divisionOptions}
          onChange={(e: DropdownChangeEvent) => { setDivisionId(e.value); setDistrictId(null); setBlockId(null); }}
          placeholder="Select Division"
          showClear
          className="w-full"
        />
      </div>

      <div className="form-field">
        <label>District</label>
        <Dropdown
          value={districtId}
          options={districtOptions}
          onChange={(e: DropdownChangeEvent) => { setDistrictId(e.value); setBlockId(null); }}
          placeholder="Select District"
          disabled={!divisionId}
          showClear
          className="w-full"
        />
      </div>

      <div className="form-field">
        <label>Block</label>
        <Dropdown
          value={blockId}
          options={blockOptions}
          onChange={(e: DropdownChangeEvent) => setBlockId(e.value)}
          placeholder="Select Block"
          disabled={!districtId}
          showClear
          className="w-full"
        />
      </div>

      <div className="form-field">
        <label>Training Date *</label>
        <Calendar
          value={trainingDate}
          onChange={(e: CalendarChangeEvent) => setTrainingDate(e.value as Date)}
          dateFormat="dd/mm/yy"
          placeholder="Select Date"
          className="w-full"
          showIcon
        />
      </div>

      <div className="form-field">
        <label>Venue Name</label>
        <InputText
          value={venueName}
          onChange={(e) => setVenueName(e.target.value)}
          placeholder="Enter venue name"
          className="w-full"
        />
      </div>

      <div className="form-field col-span-full">
        <label>Training Description</label>
        <InputTextarea
          value={trainingDescription}
          onChange={(e) => setTrainingDescription(e.target.value)}
          rows={3}
          placeholder="Enter training description"
          className="w-full"
        />
      </div>

      <div className="form-field col-span-full flex justify-content-end gap-2">
        <Button label="Cancel" severity="secondary" text onClick={onDone} />
        <Button
          label={initialData ? 'Update' : 'Create'}
          onClick={handleSubmit}
          loading={createMutation.isPending || updateMutation.isPending}
          disabled={!calendarYear || !projectId || !trainingDate}
        />
      </div>
    </div>
  );
}
