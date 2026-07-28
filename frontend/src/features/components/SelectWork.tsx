import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface Work {
  workId: number;
  workName: string;
}

interface Props {
  projectId: number | null;
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  style?: React.CSSProperties;
}

export default function SelectWork({ projectId, value, onChange, placeholder = 'Select Work', style }: Props) {
  const { data, isLoading } = useQuery<Work[]>({
    queryKey: ['masters', 'works', projectId],
    queryFn: async () => {
      const url = projectId
        ? `masters/works?projectId=${projectId}`
        : 'masters/works';
      const res = await ApiService.get<Work[]>(url);
      return res.data ?? [];
    },
    enabled: !!projectId,
  });

  const options = (data ?? []).map((d) => ({ label: d.workName, value: String(d.workId) }));

  return (
    <FormSelect
      value={value}
      onChange={(val: string) => onChange(val)}
      options={options}
      placeholder={placeholder}
      loading={isLoading}
      disabled={!projectId}
      showClear
      style={style ?? { width: '100%' }}
    />
  );
}
