import { InputText } from 'primereact/inputtext';
import { Dropdown } from 'primereact/dropdown';
import { Calendar } from 'primereact/calendar';
import { InputTextarea } from 'primereact/inputtextarea';

interface Props {
  date: string;
  startTime: string;
  endTime: string;
  mode: string;
  remarks: string;
  onDateChange: (v: string) => void;
  onStartTimeChange: (v: string) => void;
  onEndTimeChange: (v: string) => void;
  onModeChange: (v: string) => void;
  onRemarksChange: (v: string) => void;
}

const modeOptions = [
  { label: 'Online', value: 'Online' },
  { label: 'Offline', value: 'Offline' },
  { label: 'Hybrid', value: 'Hybrid' },
];

export default function CommonActivityFields({
  date,
  startTime,
  endTime,
  mode,
  remarks,
  onDateChange,
  onStartTimeChange,
  onEndTimeChange,
  onModeChange,
  onRemarksChange,
}: Props) {
  return (
    <div className="form-grid">
      <div className="form-field">
        <label>Date</label>
        <Calendar
          value={date ? new Date(date) : null}
          onChange={(e) => onDateChange(e.value ? e.value.toISOString().split('T')[0] : '')}
          dateFormat="dd/mm/yy"
          showIcon
          style={{ width: '100%' }}
        />
      </div>
      <div className="form-field">
        <label>Start Time</label>
        <InputText
          value={startTime}
          onChange={(e) => onStartTimeChange(e.target.value)}
          placeholder="HH:MM"
        />
      </div>
      <div className="form-field">
        <label>End Time</label>
        <InputText
          value={endTime}
          onChange={(e) => onEndTimeChange(e.target.value)}
          placeholder="HH:MM"
        />
      </div>
      <div className="form-field">
        <label>Mode</label>
        <Dropdown
          value={mode}
          options={modeOptions}
          onChange={(e) => onModeChange(e.value)}
          placeholder="Select Mode"
          style={{ width: '100%' }}
        />
      </div>
      <div className="form-field full-width">
        <label>Remarks</label>
        <InputTextarea
          value={remarks}
          onChange={(e) => onRemarksChange(e.target.value)}
          rows={3}
          style={{ width: '100%' }}
        />
      </div>
    </div>
  );
}
