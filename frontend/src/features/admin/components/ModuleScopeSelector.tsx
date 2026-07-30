import SelectDivision from '../../components/SelectDivision';
import SelectDistrict from '../../components/SelectDistrict';
import SelectBlock from '../../components/SelectBlock';

interface ModuleScopeSelectorProps {
  divisionId?: number;
  districtId?: number;
  blockId?: number;
  onChange: (scope: { divisionId?: number; districtId?: number; blockId?: number }) => void;
  disabled?: boolean;
}

export default function ModuleScopeSelector({
  divisionId,
  districtId,
  blockId,
  onChange,
  disabled = false,
}: ModuleScopeSelectorProps) {
  return (
    <div style={{ display: 'flex', gap: 12, flexWrap: 'wrap' }}>
      <div style={{ flex: 1, minWidth: 180 }}>
        <label style={{ display: 'block', marginBottom: 4, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
          Division
        </label>
        <SelectDivision
          value={divisionId ? String(divisionId) : ''}
          onChange={(val) => {
            const num = val ? Number(val) : undefined;
            onChange({ divisionId: num, districtId: undefined, blockId: undefined });
          }}
          disabled={disabled}
        />
      </div>
      <div style={{ flex: 1, minWidth: 180 }}>
        <label style={{ display: 'block', marginBottom: 4, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
          District
        </label>
        <SelectDistrict
          value={districtId ? String(districtId) : ''}
          onChange={(val) => {
            const num = val ? Number(val) : undefined;
            onChange({ divisionId, districtId: num, blockId: undefined });
          }}
          divisionId={divisionId ? String(divisionId) : undefined}
          disabled={disabled || !divisionId}
        />
      </div>
      <div style={{ flex: 1, minWidth: 180 }}>
        <label style={{ display: 'block', marginBottom: 4, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
          Block
        </label>
        <SelectBlock
          value={blockId ? String(blockId) : ''}
          onChange={(val) => {
            const num = val ? Number(val) : undefined;
            onChange({ divisionId, districtId, blockId: num });
          }}
          districtId={districtId ? String(districtId) : undefined}
          disabled={disabled || !districtId}
        />
      </div>
    </div>
  );
}
