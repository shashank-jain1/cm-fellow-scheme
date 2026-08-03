import { AppInput, AppMultiSelect } from '../../../shared/components/forms';
import { useLookupOptions } from '../../../shared/hooks/useMasters';

interface Props {
  trainingTitle: string;
  trainingCategory: string;
  targetUserTypes: string[];
  onTrainingTitleChange: (v: string) => void;
  onTrainingCategoryChange: (v: string) => void;
  onTargetUserTypesChange: (v: string[]) => void;
}

export default function TrainingModeSelect({
  trainingTitle, trainingCategory, targetUserTypes,
  onTrainingTitleChange, onTrainingCategoryChange, onTargetUserTypesChange,
}: Props) {
  const categoryOptions = useLookupOptions('TrainingCategory');
  const userTypeOptions = [
    { label: 'Fellow', value: 'Fellow' },
    { label: 'Coordinator', value: 'Coordinator' },
    { label: 'Intern', value: 'Intern' },
  ];

  return (
    <>
      <div className="form-field">
        <label>Training Title *</label>
        <AppInput value={trainingTitle} onChange={(e) => onTrainingTitleChange(e.target.value)}
          placeholder="Enter training title" />
      </div>
      <div className="form-field">
        <label>Training Category *</label>
        <AppMultiSelect value={trainingCategory ? trainingCategory.split(',') : []} options={categoryOptions}
          onChange={(e) => onTrainingCategoryChange(e.value?.join(',') ?? '')}
          placeholder="Select Category" display="chip" className="w-full" />
      </div>
      <div className="form-field full-width">
        <label>Target User Type *</label>
        <AppMultiSelect value={targetUserTypes} options={userTypeOptions}
          onChange={(e) => onTargetUserTypesChange(e.value ?? [])}
          placeholder="Select Target Users" display="chip" className="w-full" />
      </div>
    </>
  );
}
