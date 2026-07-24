import { useState } from 'react';
import type { WorkAllocationFormData } from '../types';

const initialFormData: WorkAllocationFormData = {
  projectId: 0,
  workProjectId: 0,
  workDescription: '',
  priority: 'Medium',
  startDate: '',
  endDate: '',
  surveysPerIntern: 0,
  divisionId: 0,
  districtId: 0,
  blockId: 0,
};

export const useWorkAllocationForm = () => {
  const [formData, setFormData] = useState<WorkAllocationFormData>(initialFormData);
  const [errors, setErrors] = useState<Partial<Record<keyof WorkAllocationFormData, string>>>({});

  const handleChange = (field: keyof WorkAllocationFormData, value: string | number) => {
    setFormData(prev => ({ ...prev, [field]: value }));
    if (errors[field]) {
      setErrors(prev => ({ ...prev, [field]: undefined }));
    }
  };

  const validate = (): boolean => {
    const newErrors: Partial<Record<keyof WorkAllocationFormData, string>> = {};

    if (!formData.projectId) newErrors.projectId = 'Project is required';
    if (!formData.workProjectId) newErrors.workProjectId = 'Work Project is required';
    if (!formData.workDescription.trim()) newErrors.workDescription = 'Description is required';
    if (!formData.startDate) newErrors.startDate = 'Start date is required';
    if (!formData.endDate) newErrors.endDate = 'End date is required';
    if (!formData.surveysPerIntern) newErrors.surveysPerIntern = 'Surveys per intern is required';
    if (!formData.divisionId) newErrors.divisionId = 'Division is required';
    if (!formData.districtId) newErrors.districtId = 'District is required';
    if (!formData.blockId) newErrors.blockId = 'Block is required';

    if (formData.startDate && formData.endDate && formData.startDate > formData.endDate) {
      newErrors.endDate = 'End date must be after start date';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const resetForm = () => {
    setFormData(initialFormData);
    setErrors({});
  };

  const setFormDataForEdit = (data: WorkAllocationFormData) => {
    setFormData(data);
    setErrors({});
  };

  return {
    formData,
    errors,
    handleChange,
    validate,
    resetForm,
    setFormDataForEdit,
  };
};
