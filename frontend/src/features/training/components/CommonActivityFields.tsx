import { AppMultiSelect, AppSelect } from '../../../shared/components/forms';
import { useDivisions, useDistricts, useBlocks } from '../../masters/queries';
import ActivityTypeSelect from './ActivityTypeSelect';
import ActivityDatePicker from './ActivityDatePicker';
import ActivityDescriptionField from './ActivityDescriptionField';

interface Props {
  projectId: number; workProjectId: number; date: string; startTime: string; endTime: string;
  mode: string; applicableDivisionIds: number[]; applicableDistrictIds: number[]; applicableBlockIds: number[];
  remarks: string; onProjectChange: (v: number) => void; onWorkChange: (v: number) => void;
  onDateChange: (v: string) => void; onStartTimeChange: (v: string) => void; onEndTimeChange: (v: string) => void;
  onModeChange: (v: string) => void; onDivisionsChange: (v: number[]) => void; onDistrictsChange: (v: number[]) => void;
  onBlocksChange: (v: number[]) => void; onRemarksChange: (v: string) => void;
}

export default function CommonActivityFields({ projectId, workProjectId, date, startTime, endTime, mode, applicableDivisionIds, applicableDistrictIds, applicableBlockIds, remarks, onProjectChange, onWorkChange, onDateChange, onStartTimeChange, onEndTimeChange, onModeChange, onDivisionsChange, onDistrictsChange, onBlocksChange, onRemarksChange }: Props) {
  const { data: divisions = [] } = useDivisions();
  const { data: districts = [] } = useDistricts();
  const { data: blocks = [] } = useBlocks();

  const modeOptions = [{ label: 'Online', value: 'Online' }, { label: 'Offline', value: 'Offline' }, { label: 'Hybrid', value: 'Hybrid' }];
  const divisionOptions = divisions.map((d) => ({ label: d.divisionName, value: d.divisionId }));
  const districtOptions = districts.map((d) => ({ label: d.districtName, value: d.districtId }));
  const blockOptions = blocks.map((b) => ({ label: b.blockName, value: b.blockId }));

  return (
    <div className="form-grid">
      <ActivityTypeSelect projectId={projectId} workProjectId={workProjectId} onProjectChange={onProjectChange} onWorkChange={onWorkChange} />
      <ActivityDatePicker date={date} startTime={startTime} endTime={endTime} onDateChange={onDateChange} onStartTimeChange={onStartTimeChange} onEndTimeChange={onEndTimeChange} />
      <div className="form-field">
        <label>Mode *</label>
        <AppSelect value={mode} options={modeOptions} onChange={(v) => onModeChange(v)} placeholder="Select mode" />
      </div>
      <div className="form-field">
        <label>Applicable Divisions *</label>
        <AppMultiSelect value={applicableDivisionIds} options={divisionOptions} onChange={(e) => { onDivisionsChange(e.value); onDistrictsChange([]); onBlocksChange([]); }} placeholder="Select divisions" display="chip" className="w-full" />
      </div>
      <div className="form-field">
        <label>Applicable Districts *</label>
        <AppMultiSelect value={applicableDistrictIds} options={districtOptions} onChange={(e) => { onDistrictsChange(e.value); onBlocksChange([]); }} placeholder="Select districts" disabled={!applicableDivisionIds.length} display="chip" className="w-full" />
      </div>
      <div className="form-field">
        <label>Applicable Blocks</label>
        <AppMultiSelect value={applicableBlockIds} options={blockOptions} onChange={(e) => onBlocksChange(e.value)} placeholder="Select blocks" disabled={!applicableDistrictIds.length} display="chip" className="w-full" />
      </div>
      <ActivityDescriptionField remarks={remarks} onRemarksChange={onRemarksChange} />
    </div>
  );
}
