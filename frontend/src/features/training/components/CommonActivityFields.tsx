import { Calendar } from 'primereact/calendar';
import { InputTextarea } from 'primereact/inputtextarea';
import FormSelect from '../../../shared/components/FormSelect';

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
          showOnFocus={true}
          appendTo="self"
        />
      </div>
      <div className="form-field">
        <label>Mode</label>
        <FormSelect
          value={mode}
          options={modeOptions}
          onChange={(val) => onModeChange(val)}
          placeholder="Select Mode"
        />
      </div>
      <div className="form-field">
        <label>Start Time</label>
        <Calendar
          value={timeStringToDate(startTime)}
          onChange={(e) => onStartTimeChange(dateToTimeString(e.value as Date | null))}
          timeOnly
          hourFormat="24"
          placeholder="Select start time"
          showOnFocus={true}
          appendTo="self"
        />
      </div>
      <div className="form-field">
        <label>End Time</label>
        <Calendar
          value={timeStringToDate(endTime)}
          onChange={(e) => onEndTimeChange(dateToTimeString(e.value as Date | null))}
          timeOnly
          hourFormat="24"
          placeholder="Select end time"
          showOnFocus={true}
          appendTo="self"
        />
      </div>
      <div className="form-field full-width">
        <label>Remarks</label>
        <InputTextarea
          value={remarks}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onRemarksChange(e.target.value)}
          rows={3}
        />
      </div>
    </div>
  );
}
