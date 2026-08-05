import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface Block {
  id: number;
  name: string;
  districtId: number;
}

interface Props {
  value: string;
  onChange: (value: string) => void;
  districtId?: string;
  placeholder?: string;
  style?: React.CSSProperties;
  disabled?: boolean;
}

export default function SelectBlock({ value, onChange, districtId, placeholder = 'Select Block', style, disabled }: Props) {
  const { data, isLoading } = useQuery({
    queryKey: ['blocks', districtId],
    queryFn: async () => {
      const url = districtId ? `blocks?districtId=${districtId}` : 'blocks';
      const res = await ApiService.get<Block[]>(url);
      return res.data ?? [];
    },
    enabled: !!districtId,
  });

  const options = (data ?? []).map((d) => ({ label: d.name, value: String(d.id) }));

  return (
    <FormSelect
      value={value}
      onChange={(val: string) => onChange(val)}
      options={options}
      placeholder={placeholder}
      loading={isLoading}
      disabled={disabled || !districtId}
      showClear
      style={style ?? { width: '100%' }}
    />
  );
}
