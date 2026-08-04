import { AppButton } from '../../../shared/components/ui';
import ActivityTypeSelector from '../components/ActivityTypeSelector';
import CommonActivityFields from '../components/CommonActivityFields';

interface ActivityBasicFieldsProps {
  formData: {
    activityType: string;
    projectId: number;
    workProjectId: number;
    date: string;
    startTime: string;
    endTime: string;
    mode: string;
    applicableDivisionIds: number[];
    applicableDistrictIds: number[];
    applicableBlockIds: number[];
    remarks?: string | null;
  };
  updateField: (field: any, value: any) => void;
  onNavigate: (path: string) => void;
}

export default function ActivityBasicFields({
  formData,
  updateField,
  onNavigate,
}: ActivityBasicFieldsProps) {
  return (
    <>
      <div className="page-header">
        <div>
          <h1>Create Activity</h1>
          <p style={{ color: 'var(--text-muted)', marginTop: 4 }}>
            Schedule a new training or meeting activity
          </p>
        </div>
        <div style={{ display: 'flex', gap: 8 }}>
          <AppButton variant="secondary" icon="pi pi-times" onClick={() => onNavigate('/training')}>
            Cancel
          </AppButton>
        </div>
      </div>
      <ActivityTypeSelector
        value={formData.activityType as 'Training' | 'Meeting'}
        onChange={(v) => updateField('activityType', v)}
      />
      <CommonActivityFields
        projectId={formData.projectId}
        workProjectId={formData.workProjectId}
        date={formData.date}
        startTime={formData.startTime}
        endTime={formData.endTime}
        mode={formData.mode}
        applicableDivisionIds={formData.applicableDivisionIds}
        applicableDistrictIds={formData.applicableDistrictIds}
        applicableBlockIds={formData.applicableBlockIds}
        remarks={formData.remarks ?? ''}
        onProjectChange={(v) => updateField('projectId', v)}
        onWorkChange={(v) => updateField('workProjectId', v)}
        onDateChange={(v) => updateField('date', v)}
        onStartTimeChange={(v) => updateField('startTime', v)}
        onEndTimeChange={(v) => updateField('endTime', v)}
        onModeChange={(v) => updateField('mode', v)}
        onDivisionsChange={(v) => updateField('applicableDivisionIds', v)}
        onDistrictsChange={(v) => updateField('applicableDistrictIds', v)}
        onBlocksChange={(v) => updateField('applicableBlockIds', v)}
        onRemarksChange={(v) => updateField('remarks', v)}
      />
    </>
  );
}
