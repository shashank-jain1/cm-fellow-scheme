import FormSelect from '../../shared/components/FormSelect';

interface Props {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  style?: React.CSSProperties;
}

const PRIORITY_OPTIONS = [
  { label: 'High', value: 'High' },
  { label: 'Medium', value: 'Medium' },
  { label: 'Low', value: 'Low' },
];

export default function SelectPriority({ value, onChange, placeholder = 'Select Priority', style }: Props) {
  return (
    <FormSelect
      value={value}
      onChange={(val: string) => onChange(val)}
      options={PRIORITY_OPTIONS}
      placeholder={placeholder}
      showClear
      style={style ?? { width: '100%' }}
    />
  );
}
