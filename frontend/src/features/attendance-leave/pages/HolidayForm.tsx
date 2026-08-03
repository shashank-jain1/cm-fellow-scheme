import { AppDialog, AppInput, AppSwitch, AppCalendar } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';

interface Props {
  visible: boolean;
  isEditing: boolean;
  holidayName: string;
  holidayDate: Date | null;
  description: string;
  isOptional: boolean;
  isSaving: boolean;
  onHolidayNameChange: (v: string) => void;
  onHolidayDateChange: (v: Date) => void;
  onDescriptionChange: (v: string) => void;
  onIsOptionalChange: (v: boolean) => void;
  onSave: () => void;
  onHide: () => void;
}

export default function HolidayForm({
  visible, isEditing, holidayName, holidayDate, description, isOptional, isSaving,
  onHolidayNameChange, onHolidayDateChange, onDescriptionChange, onIsOptionalChange, onSave, onHide,
}: Props) {
  return (
    <AppDialog header={isEditing ? 'Edit Holiday' : 'Add Holiday'} visible={visible}
      style={{ width: '480px' }} modal onHide={onHide}>
      <div className="form-grid" style={{ marginTop: 16 }}>
        <div className="form-field full-width">
          <label>Holiday Name *</label>
          <AppInput value={holidayName} onChange={(e) => onHolidayNameChange(e.target.value)} style={{ width: '100%' }} />
        </div>
        <div className="form-field">
          <label>Date *</label>
          <AppCalendar value={holidayDate} onChange={(e) => onHolidayDateChange(e.value as Date)}
            dateFormat="dd/mm/yy" style={{ width: '100%' }} showIcon />
        </div>
        <div className="form-field">
          <label>Optional Holiday</label>
          <AppSwitch checked={isOptional} onChange={(e) => onIsOptionalChange(Boolean(e.value))} />
        </div>
        <div className="form-field full-width">
          <label>Description</label>
          <AppInput value={description} onChange={(e) => onDescriptionChange(e.target.value)} style={{ width: '100%' }} />
        </div>
      </div>
      <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 16 }}>
        <AppButton variant="secondary" onClick={onHide}>Cancel</AppButton>
        <AppButton onClick={onSave} loading={isSaving}>Save</AppButton>
      </div>
    </AppDialog>
  );
}
