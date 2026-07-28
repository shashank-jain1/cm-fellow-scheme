import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';
import FormSelect from '../../shared/components/FormSelect';

interface GramPanchayat {
  gramPanchayatId: number;
  gramPanchayatName: string;
}

interface Props {
  blockId: number | null;
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  style?: React.CSSProperties;
}

export default function SelectGramPanchayat({ blockId, value, onChange, placeholder = 'Select Gram Panchayat', style }: Props) {
  const { data, isLoading } = useQuery<GramPanchayat[]>({
    queryKey: ['masters', 'gramPanchayats', blockId],
    queryFn: async () => {
      const url = blockId
        ? `masters/locations/gram-panchayats?blockId=${blockId}`
        : 'masters/locations/gram-panchayats';
      const res = await ApiService.get<GramPanchayat[]>(url);
      return res.data ?? [];
    },
    enabled: !!blockId,
  });

  const options = (data ?? []).map((d) => ({ label: d.gramPanchayatName, value: String(d.gramPanchayatId) }));

  return (
    <FormSelect
      value={value}
      onChange={(val: string) => onChange(val)}
      options={options}
      placeholder={placeholder}
      loading={isLoading}
      disabled={!blockId}
      showClear
      style={style ?? { width: '100%' }}
    />
  );
}
