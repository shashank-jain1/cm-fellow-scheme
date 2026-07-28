import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface State {
  stateId: number;
  stateName: string;
}

interface Props {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  style?: React.CSSProperties;
}

export default function SelectState({ value, onChange, placeholder = 'Select State', style }: Props) {
  const { data, isLoading } = useQuery<State[]>({
    queryKey: ['masters', 'states'],
    queryFn: async () => {
      const res = await ApiService.get<State[]>('masters/locations/states');
      return res.data ?? [];
    },
  });

  const options = (data ?? []).map((d) => ({ label: d.stateName, value: String(d.stateId) }));

  return (
    <FormSelect
      value={value}
      onChange={(val: string) => onChange(val)}
      options={options}
      placeholder={placeholder}
      loading={isLoading}
      showClear
      style={style ?? { width: '100%' }}
    />
  );
}
