import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface Block {
  blockId: number;
  blockName: string;
  districtId: number;
  id?: number;
  name?: string;
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
      const url = districtId ? `masters/locations/blocks?districtId=${districtId}` : 'masters/locations/blocks';
      const res = await ApiService.get<Block[]>(url);
      return res.data ?? [];
    },
    enabled: !!districtId,
  });

  const options = (data ?? []).map((d) => {
    const id = d.blockId ?? d.id ?? 0;
    const name = d.blockName ?? d.name ?? '';
    return { label: name, value: String(id) };
  });

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
