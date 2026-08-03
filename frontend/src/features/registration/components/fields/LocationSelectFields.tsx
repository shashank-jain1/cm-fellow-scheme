import { AppInput, AppSelect } from '../../../../shared/components/forms';
import { useStates, useDivisions, useDistricts, useBlocks, useGramPanchayats } from '../../../../shared/hooks/useMasters';
import type { StepProps } from '../form.hook';

export default function LocationSelectFields({ formData, update }: StepProps) {
  const { data: states } = useStates();
  const selectedStateId = states?.[0]?.stateId ?? null;
  const { data: divisions, isLoading: divLoading } = useDivisions(selectedStateId);
  const divisionId = formData.divisionId ? parseInt(formData.divisionId, 10) : null;
  const { data: districts, isLoading: distLoading } = useDistricts(divisionId);
  const districtId = formData.districtId ? parseInt(formData.districtId, 10) : null;
  const { data: blocks, isLoading: blockLoading } = useBlocks(districtId);
  const blockId = formData.blockId ? parseInt(formData.blockId, 10) : null;
  const { data: gps, isLoading: gpLoading } = useGramPanchayats(blockId);

  const divisionOptions = [
    { label: 'Select Division', value: '' },
    ...(divisions ?? []).map((d) => ({ label: d.divisionName, value: String(d.divisionId) })),
  ];
  const districtOptions = [
    { label: 'Select District', value: '' },
    ...(districts ?? []).map((d) => ({ label: d.districtName, value: String(d.districtId) })),
  ];
  const blockOptions = [
    { label: 'Select Block', value: '' },
    ...(blocks ?? []).map((b) => ({ label: b.blockName, value: String(b.blockId) })),
  ];
  const gpOptions = [
    { label: 'Select Gram Panchayat', value: '' },
    ...(gps ?? []).map((g) => ({ label: g.gramPanchayatName, value: String(g.gramPanchayatId) })),
  ];

  return (
    <>
      <div className="form-field">
        <label>Division *</label>
        <AppSelect
          value={formData.divisionId}
          onChange={(val: string) => {
            update('divisionId', val);
            update('districtId', '');
            update('blockId', '');
            update('gramPanchayatId', '');
          }}
          options={divisionOptions}
          placeholder="Select division"
          loading={divLoading}
        />
      </div>
      <div className="form-field">
        <label>District *</label>
        <AppSelect
          value={formData.districtId}
          onChange={(val: string) => {
            update('districtId', val);
            update('blockId', '');
            update('gramPanchayatId', '');
          }}
          options={districtOptions}
          placeholder="Select district"
          loading={distLoading}
          disabled={!divisionId}
        />
      </div>
      <div className="form-field">
        <label>Block *</label>
        <AppSelect
          value={formData.blockId}
          onChange={(val: string) => {
            update('blockId', val);
            update('gramPanchayatId', '');
          }}
          options={blockOptions}
          placeholder="Select block"
          loading={blockLoading}
          disabled={!districtId}
        />
      </div>
      <div className="form-field">
        <label>Gram Panchayat *</label>
        <AppSelect
          value={formData.gramPanchayatId}
          onChange={(val: string) => update('gramPanchayatId', val)}
          options={gpOptions}
          placeholder="Select gram panchayat"
          loading={gpLoading}
          disabled={!blockId}
        />
      </div>
    </>
  );
}
