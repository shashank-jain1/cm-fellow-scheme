import { useState, useCallback } from 'react';
import { ToastService } from '../../../shared/utils/toast';
import { registrationApi } from '../api';
import { TOTAL_STEPS, initialState } from './formTypes';
import type { FormData, FormField, StepProps } from './formTypes';
import { validatePersonalInfo } from './usePersonalInfo';
import { validateAddressInfo } from './useAddressInfo';
import { validateTrainingInfo, validateEducationalInfo } from './useEducationalInfo';
import { validateDeclaration } from './useDeclaration';

export type { FormData, FormField, StepProps };

export function useRegistrationForm() {
  const [currentStep, setCurrentStep] = useState(0);
  const [formData, setFormData] = useState<FormData>(initialState);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const update = useCallback(
    (field: FormField, value: string | boolean) =>
      setFormData((prev) => ({ ...prev, [field]: value })),
    [],
  );

  const validate = useCallback((): boolean => {
    const validators: Record<number, (fd: FormData) => string[]> = {
      0: validatePersonalInfo,
      2: validateAddressInfo,
      3: validateTrainingInfo,
      4: validateEducationalInfo,
      5: validateDeclaration,
    };
    const errors = (validators[currentStep] ?? (() => []))(formData);
    if (errors.length > 0) { ToastService.error(errors[0]); return false; }
    return true;
  }, [currentStep, formData]);

  const next = useCallback(() => {
    if (validate() && currentStep < TOTAL_STEPS - 1) setCurrentStep((s) => s + 1);
  }, [currentStep, validate]);

  const prev = useCallback(() => {
    if (currentStep > 0) setCurrentStep((s) => s - 1);
  }, [currentStep]);

  const goToStep = useCallback((step: number) => {
    if (step >= 0 && step < TOTAL_STEPS) setCurrentStep(step);
  }, []);

  const handleSubmit = useCallback(async () => {
    if (!validate()) return;
    setIsSubmitting(true);
    try {
      await registrationApi.submitRegistration({
        firstName: formData.firstName.trim(), middleName: formData.middleName.trim() || undefined,
        lastName: formData.lastName.trim(), fatherName: formData.fatherName.trim(),
        aadhaarNumber: formData.aadhaarNumber.trim() || undefined,
        panNumber: formData.panNumber.trim().toUpperCase() || undefined,
        drivingLicenseNumber: formData.drivingLicenseNumber.trim() || undefined,
        samagraId: formData.samagraId.trim() || undefined,
        mobileNumber: formData.mobileNumber.trim(), emailId: formData.emailId.trim(),
        dateOfBirth: formData.dateOfBirth, permanentAddress: formData.permanentAddress.trim(),
        divisionId: parseInt(formData.divisionId, 10), districtId: parseInt(formData.districtId, 10),
        blockId: parseInt(formData.blockId, 10), gramPanchayatId: parseInt(formData.gramPanchayatId, 10),
        pinCode: formData.pinCode.trim(),
        appliedForTraining: parseInt(formData.appliedForTraining, 10),
        preferredTrainingLocationId: parseInt(formData.preferredTrainingLocationId, 10),
        qualificationId: parseInt(formData.qualificationId, 10),
        boardUniversityName: formData.boardUniversityName.trim(),
        passingYear: parseInt(formData.passingYear, 10),
        percentageCgpa: parseFloat(formData.percentageCgpa),
        experienceDetails: formData.experienceDetails.trim() || undefined,
        declarationAccepted: formData.declarationAccepted,
      });
      ToastService.success('Registration submitted successfully!');
      setFormData(initialState); setCurrentStep(0);
    } catch (err) {
      ToastService.error(err instanceof Error ? err.message : 'Submission failed');
    } finally { setIsSubmitting(false); }
  }, [formData, validate]);

  return { formData, update, validate, currentStep, next, prev, goToStep, handleSubmit, isSubmitting };
}
