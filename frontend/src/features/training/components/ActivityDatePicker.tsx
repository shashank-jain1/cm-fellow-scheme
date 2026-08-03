import { AppCalendar } from '../../../shared/components/forms';

function timeStringToDate(time: string): Date | null {
  if (!time) return null;
  const [h, m] = time.split(':').map(Number);
  const d = new Date(); d.setHours(h, m, 0, 0); return d;
}

function dateToTimeString(date: Date | null): string {
  if (!date) return '';
  return `${String(date.getHours()).padStart(2, '0')}:${String(date.getMinutes()).padStart(2, '0')}`;
}

interface Props {
  date: string;
  startTime: string;
  endTime: string;
  onDateChange: (v: string) => void;
  onStartTimeChange: (v: string) => void;
  onEndTimeChange: (v: string) => void;
}

export default function ActivityDatePicker({ date, startTime, endTime, onDateChange, onStartTimeChange, onEndTimeChange }: Props) {
  return (
    <>
      <div className="form-field">
        <label>Date *</label>
        <AppCalendar value={date ? new Date(date) : null} onChange={(e) => onDateChange(e.value ? e.value.toISOString().split('T')[0] : '')} dateFormat="dd/mm/yy" showIcon showOnFocus appendTo="self" />
      </div>
      <div className="form-field">
        <label>Start Time *</label>
        <AppCalendar value={timeStringToDate(startTime)} onChange={(e) => onStartTimeChange(dateToTimeString(e.value as Date | null))} timeOnly hourFormat="24" placeholder="Select start time" showOnFocus appendTo="self" />
      </div>
      <div className="form-field">
        <label>End Time *</label>
        <AppCalendar value={timeStringToDate(endTime)} onChange={(e) => onEndTimeChange(dateToTimeString(e.value as Date | null))} timeOnly hourFormat="24" placeholder="Select end time" showOnFocus appendTo="self" />
      </div>
    </>
  );
}
