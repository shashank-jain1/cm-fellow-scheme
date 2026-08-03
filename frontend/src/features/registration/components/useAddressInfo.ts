import type { FormData } from './formTypes';

export function validateAddressInfo(formData: FormData): string[] {
  const errors: string[] = [];
  if (!formData.permanentAddress.trim()) errors.push('Address is required');
  if (!formData.divisionId) errors.push('Division is required');
  if (!formData.districtId) errors.push('District is required');
  if (!formData.blockId) errors.push('Block is required');
  if (!formData.gramPanchayatId) errors.push('Gram Panchayat is required');
  if (!formData.pinCode.trim()) errors.push('PIN code is required');
  if (!/^\d{6}$/.test(formData.pinCode)) errors.push('PIN must be 6 digits');
  return errors;
}
