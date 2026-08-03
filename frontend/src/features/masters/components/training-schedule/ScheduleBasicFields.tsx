import { AppSelect } from '../../../../shared/components/forms';

interface Props {
  calendarYear: string;
  projectId: number | null;
  workId: number | null;
  projectOptions: { label: string; value: number }[];
  workOptions: { label: string; value: number }[];
  onCalendarYearChange: (v: string) => void;
  onProjectChange: (v: number) => void;
  onWorkChange: (v: number) => void;
}

export default function ScheduleBasicFields({
  calendarYear,
  projectId,
  workId,
  projectOptions,
  workOptions,
  onCalendarYearChange,
  onProjectChange,
  onWorkChange,
}: Props) {
  const YEAR_OPTIONS = Array.from({ length: 5 }, (_, i) => {
    const year = new Date().getFullYear() + i - 1;
    return { label: `${year}-${(year + 1).toString().slice(-2)}`, value: `${year}-${(year + 1).toString().slice(-2)}` };
  });

  return (
    <>
      <div className="form-field">
        <label>Calendar Year *</label>
        <AppSelect
          value={calendarYear}
          options={YEAR_OPTIONS}
          onChange={(val) => onCalendarYearChange(val as string)}
          placeholder="Select Year"
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>Project *</label>
        <AppSelect
          value={projectId}
          options={projectOptions}
          onChange={(val) => onProjectChange(val as number)}
          placeholder="Select Project"
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>Work</label>
        <AppSelect
          value={workId}
          options={workOptions}
          onChange={(val) => onWorkChange(val as number)}
          placeholder="Select Work"
          disabled={!projectId}
          className="w-full"
        />
      </div>
    </>
  );
}
