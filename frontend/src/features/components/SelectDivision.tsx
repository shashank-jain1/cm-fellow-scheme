import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface Division {
  divisionId: number;
  divisionName: string;
  divisionCode?: string;
  id?: number;
  name?: string;
}

interface Props {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  style?: React.CSSProperties;
  disabled?: boolean;
}

export default function SelectDivision({ value, onChange, placeholder = 'Select Division', style, disabled }: Props) {
  const { data, isLoading } = useQuery({
    queryKey: ['divisions'],
    queryFn: async () => {
      const res = await ApiService.get<Division[]>('masters/locations/divisions');
      return res.data ?? [];
    },
  });

  const options = (data ?? []).map((d) => {
    const id = d.divisionId ?? d.id ?? 0;
    const name = d.divisionName ?? d.name ?? '';
    return { label: name, value: String(id) };
  });

  return (
    <FormSelect
      value={value}
      onChange={(val: string) => onChange(val)}
      options={options}
      placeholder={placeholder}
      loading={isLoading}
      disabled={disabled}
      showClear
      style={style ?? { width: '100%' }}
    />
  );
}
