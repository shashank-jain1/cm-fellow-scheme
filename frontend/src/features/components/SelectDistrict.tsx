import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface District {
  id: number;
  name: string;
  divisionId: number;
}

interface Props {
  value: string;
  onChange: (value: string) => void;
  divisionId?: string;
  placeholder?: string;
  style?: React.CSSProperties;
  disabled?: boolean;
}

export default function SelectDistrict({ value, onChange, divisionId, placeholder = 'Select District', style, disabled }: Props) {
  const { data, isLoading } = useQuery({
    queryKey: ['districts', divisionId],
    queryFn: async () => {
      const url = divisionId ? `districts?divisionId=${divisionId}` : 'districts';
      const res = await ApiService.get<District[]>(url);
      return res.data ?? [];
    },
    enabled: !!divisionId,
  });

  const options = (data ?? []).map((d) => ({ label: d.name, value: String(d.id) }));

  return (
    <FormSelect
      value={value}
      onChange={(val: string) => onChange(val)}
      options={options}
      placeholder={placeholder}
      loading={isLoading}
      disabled={disabled || !divisionId}
      showClear
      style={style ?? { width: '100%' }}
    />
  );
}
