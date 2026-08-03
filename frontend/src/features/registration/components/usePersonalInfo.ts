import type { FormData } from './formTypes';

export function validatePersonalInfo(formData: FormData): string[] {
  const errors: string[] = [];
  if (!formData.firstName.trim()) errors.push('First name is required');
  if (!formData.lastName.trim()) errors.push('Last name is required');
  if (!formData.fatherName.trim()) errors.push('Father name is required');
  if (!formData.mobileNumber.trim()) errors.push('Mobile number is required');
  if (!/^\d{10}$/.test(formData.mobileNumber)) errors.push('Mobile must be 10 digits');
  if (!formData.emailId.trim()) errors.push('Email is required');
  if (!formData.dateOfBirth.trim()) errors.push('Date of birth is required');
  return errors;
}
