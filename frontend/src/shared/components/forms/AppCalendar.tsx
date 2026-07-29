import { Calendar, type CalendarProps } from 'primereact/calendar';

export default function AppCalendar({ showOnFocus = true, appendTo = 'self', ...props }: CalendarProps) {
  const showIcon = props.timeOnly ? false : (props.showIcon ?? true);
  return <Calendar showIcon={showIcon} showOnFocus={showOnFocus} appendTo={appendTo} {...props} />;
}
