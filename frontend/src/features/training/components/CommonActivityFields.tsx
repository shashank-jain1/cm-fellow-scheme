import { useState, useEffect } from 'react';
import { Calendar } from 'primereact/calendar';
import { InputTextarea } from 'primereact/inputtextarea';
import { Dropdown } from 'primereact/dropdown';
import { MultiSelect } from 'primereact/multiselect';
import { useProjects, useWorks, useDivisions, useDistricts, useBlocks } from '../../masters/queries';

interface Props {
  projectId: number;
  workProjectId: number;
  date: string;
  startTime: string;
  endTime: string;
  mode: string;
  applicableDivisionIds: number[];
  applicableDistrictIds: number[];
  applicableBlockIds: number[];
  remarks: string;
  onProjectChange: (v: number) => void;
  onWorkChange: (v: number) => void;
  onDateChange: (v: string) => void;
  onStartTimeChange: (v: string) => void;
  onEndTimeChange: (v: string) => void;
  onModeChange: (v: string) => void;
  onDivisionsChange: (v: number[]) => void;
  onDistrictsChange: (v: number[]) => void;
  onBlocksChange: (v: number[]) => void;
  onRemarksChange: (v: string) => void;
}

function timeStringToDate(time: string): Date | null {
  if (!time) return null;
  const [h, m] = time.split(':').map(Number);
  const d = new Date();
  d.setHours(h, m, 0, 0);
  return d;
}

function dateToTimeString(date: Date | null): string {
  if (!date) return '';
  const h = String(date.getHours()).padStart(2, '0');
  const m = String(date.getMinutes()).padStart(2, '0');
  return `${h}:${m}`;
}

export default function CommonActivityFields({
  projectId,
  workProjectId,
  date,
  startTime,
  endTime,
  mode,
  applicableDivisionIds,
  applicableDistrictIds,
  applicableBlockIds,
  remarks,
  onProjectChange,
  onWorkChange,
  onDateChange,
  onStartTimeChange,
  onEndTimeChange,
  onModeChange,
  onDivisionsChange,
  onDistrictsChange,
  onBlocksChange,
  onRemarksChange,
}: Props) {
  const { data: projects = [] } = useProjects();
  const { data: works = [] } = useWorks(projectId || undefined);
  const { data: divisions = [] } = useDivisions();
  const { data: districts = [] } = useDistricts();
  const { data: blocks = [] } = useBlocks();

  const modeOptions = [
    { label: 'Online', value: 'Online' },
    { label: 'Offline', value: 'Offline' },
    { label: 'Hybrid', value: 'Hybrid' },
  ];

  const projectOptions = projects.map((p) => ({ label: p.projectName, value: p.projectId }));
  const workOptions = works.map((w) => ({ label: w.workName, value: w.workId }));
  const divisionOptions = divisions.map((d) => ({ label: d.divisionName, value: d.divisionId }));
  const districtOptions = districts.map((d) => ({ label: d.districtName, value: d.districtId }));
  const blockOptions = blocks.map((b) => ({ label: b.blockName, value: b.blockId }));

  return (
    <div className="form-grid">
      <div className="form-field">
        <label>Project Name *</label>
        <Dropdown
          value={projectId || undefined}
          options={projectOptions}
          onChange={(e) => { onProjectChange(e.value ?? 0); onWorkChange(0); }}
          placeholder="Select Project"
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>Work Project *</label>
        <Dropdown
          value={workProjectId || undefined}
          options={workOptions}
          onChange={(e) => onWorkChange(e.value ?? 0)}
          placeholder="Select Work"
          disabled={!projectId}
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>Date *</label>
        <Calendar
          value={date ? new Date(date) : null}
          onChange={(e) => onDateChange(e.value ? e.value.toISOString().split('T')[0] : '')}
          dateFormat="dd/mm/yy"
          showIcon
          showOnFocus
          appendTo="self"
        />
      </div>
      <div className="form-field">
        <label>Mode *</label>
        <Dropdown
          value={mode || undefined}
          options={modeOptions}
          onChange={(e) => onModeChange(e.value ?? '')}
          placeholder="Select Mode"
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>Start Time *</label>
        <Calendar
          value={timeStringToDate(startTime)}
          onChange={(e) => onStartTimeChange(dateToTimeString(e.value as Date | null))}
          timeOnly
          hourFormat="24"
          placeholder="Select start time"
          showOnFocus
          appendTo="self"
        />
      </div>
      <div className="form-field">
        <label>End Time *</label>
        <Calendar
          value={timeStringToDate(endTime)}
          onChange={(e) => onEndTimeChange(dateToTimeString(e.value as Date | null))}
          timeOnly
          hourFormat="24"
          placeholder="Select end time"
          showOnFocus
          appendTo="self"
        />
      </div>
      <div className="form-field">
        <label>Applicable Divisions *</label>
        <MultiSelect
          value={applicableDivisionIds}
          options={divisionOptions}
          onChange={(e) => { onDivisionsChange(e.value); onDistrictsChange([]); onBlocksChange([]); }}
          placeholder="Select Divisions"
          display="chip"
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>Applicable Districts *</label>
        <MultiSelect
          value={applicableDistrictIds}
          options={districtOptions}
          onChange={(e) => { onDistrictsChange(e.value); onBlocksChange([]); }}
          placeholder="Select Districts"
          disabled={!applicableDivisionIds.length}
          display="chip"
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>Applicable Blocks</label>
        <MultiSelect
          value={applicableBlockIds}
          options={blockOptions}
          onChange={(e) => onBlocksChange(e.value)}
          placeholder="Select Blocks"
          disabled={!applicableDistrictIds.length}
          display="chip"
          className="w-full"
        />
      </div>
      <div className="form-field full-width">
        <label>Remarks</label>
        <InputTextarea
          value={remarks}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onRemarksChange(e.target.value)}
          rows={3}
          placeholder="Enter remarks"
        />
      </div>
    </div>
  );
}
