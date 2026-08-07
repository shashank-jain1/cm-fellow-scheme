import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface District {
  districtId: number;
  districtName: string;
  divisionId: number;
  id?: number;
  name?: string;
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
      const url = divisionId ? `masters/locations/districts?divisionId=${divisionId}` : 'masters/locations/districts';
      const res = await ApiService.get<District[]>(url);
      return res.data ?? [];
    },
    enabled: !!divisionId,
  });

  const options = (data ?? []).map((d) => {
    const id = d.districtId ?? d.id ?? 0;
    const name = d.districtName ?? d.name ?? '';
    return { label: name, value: String(id) };
  });

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
