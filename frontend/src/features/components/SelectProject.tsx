import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface Project {
  id: number;
  name: string;
  code: string;
}

interface Props {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  style?: React.CSSProperties;
}

export default function SelectProject({ value, onChange, placeholder = 'Select Project', style }: Props) {
  const { data, isLoading } = useQuery({
    queryKey: ['projects'],
    queryFn: async () => {
      const res = await ApiService.get<Project[]>('projects');
      return res.data ?? [];
    },
  });

  const options = (data ?? []).map((d) => ({ label: d.name, value: String(d.id) }));

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
