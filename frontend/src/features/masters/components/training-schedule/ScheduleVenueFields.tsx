import { AppSelect } from '../../../../shared/components/forms';

interface Props {
  divisionId: number | null;
  districtId: number | null;
  blockId: number | null;
  divisionOptions: { label: string; value: number }[];
  districtOptions: { label: string; value: number }[];
  blockOptions: { label: string; value: number }[];
  onDivisionChange: (v: number) => void;
  onDistrictChange: (v: number) => void;
  onBlockChange: (v: number) => void;
}

export default function ScheduleVenueFields({
  divisionId,
  districtId,
  blockId,
  divisionOptions,
  districtOptions,
  blockOptions,
  onDivisionChange,
  onDistrictChange,
  onBlockChange,
}: Props) {
  return (
    <>
      <div className="form-field">
        <label>Division</label>
        <AppSelect
          value={divisionId}
          options={divisionOptions}
          onChange={(val) => onDivisionChange(val as number)}
          placeholder="Select Division"
          showClear
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>District</label>
        <AppSelect
          value={districtId}
          options={districtOptions}
          onChange={(val) => onDistrictChange(val as number)}
          placeholder="Select District"
          disabled={!divisionId}
          showClear
          className="w-full"
        />
      </div>
      <div className="form-field">
        <label>Block</label>
        <AppSelect
          value={blockId}
          options={blockOptions}
          onChange={(val) => onBlockChange(val as number)}
          placeholder="Select Block"
          disabled={!districtId}
          showClear
          className="w-full"
        />
      </div>
    </>
  );
}
