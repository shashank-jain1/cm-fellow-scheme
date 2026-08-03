import type { FormData } from './formTypes';

export function validateTrainingInfo(formData: FormData): string[] {
  const errors: string[] = [];
  if (!formData.appliedForTraining) errors.push('Please select training type');
  if (!formData.preferredTrainingLocationId) errors.push('Preferred training location is required');
  return errors;
}

export function validateEducationalInfo(formData: FormData): string[] {
  const errors: string[] = [];
  if (!formData.qualificationId) errors.push('Qualification is required');
  if (!formData.boardUniversityName.trim()) errors.push('Board/University name is required');
  if (!formData.passingYear.trim()) errors.push('Passing year is required');
  if (!formData.percentageCgpa.trim()) errors.push('Percentage/CGPA is required');
  return errors;
}
