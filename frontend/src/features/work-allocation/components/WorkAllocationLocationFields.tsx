import { AppSelect } from '../../../shared/components/forms';
import { useDivisions, useDistricts, useBlocks } from '../../../shared/hooks/useMasters';
import type { WorkAllocationFormData } from '../types';

interface Props {
  formData: WorkAllocationFormData;
  errors: Partial<Record<keyof WorkAllocationFormData, string>>;
  onChange: (field: keyof WorkAllocationFormData, value: string | number) => void;
}

export default function WorkAllocationLocationFields({ formData, errors, onChange }: Props) {
  const { data: divisions } = useDivisions(null);
  const divisionOptions = (divisions ?? []).map((d) => ({ label: d.divisionName, value: String(d.divisionId) }));
  const { data: districts } = useDistricts(formData.divisionId || null);
  const districtOptions = (districts ?? []).map((d) => ({ label: d.districtName, value: String(d.districtId) }));
  const { data: blocks } = useBlocks(formData.districtId || null);
  const blockOptions = (blocks ?? []).map((b) => ({ label: b.blockName, value: String(b.blockId) }));

  return (
    <div className="form-grid">
      <div className="form-field">
        <label>Division <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <AppSelect
          value={formData.divisionId ? String(formData.divisionId) : ''}
          options={divisionOptions}
          onChange={(val) => { onChange('divisionId', Number(val)); onChange('districtId', 0); onChange('blockId', 0); }}
          placeholder="Select division"
        />
        {errors.divisionId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.divisionId}</small>}
      </div>
      <div className="form-field">
        <label>District <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <AppSelect
          value={formData.districtId ? String(formData.districtId) : ''}
          options={districtOptions}
          onChange={(val) => { onChange('districtId', Number(val)); onChange('blockId', 0); }}
          placeholder="Select district"
          disabled={!formData.divisionId}
        />
        {errors.districtId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.districtId}</small>}
      </div>
      <div className="form-field">
        <label>Block <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <AppSelect
          value={formData.blockId ? String(formData.blockId) : ''}
          options={blockOptions}
          onChange={(val) => onChange('blockId', Number(val))}
          placeholder="Select block"
          disabled={!formData.districtId}
        />
        {errors.blockId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.blockId}</small>}
      </div>
    </div>
  );
}
