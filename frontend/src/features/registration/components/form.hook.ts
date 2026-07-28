import { useState, useCallback } from 'react';
import { ToastService } from '../../../shared/utils/toast';
import { registrationApi } from '../api';

export interface FormData {
  firstName: string;
  middleName: string;
  lastName: string;
  fatherName: string;
  aadhaarNumber: string;
  panNumber: string;
  drivingLicenseNumber: string;
  samagraId: string;
  mobileNumber: string;
  emailId: string;
  dateOfBirth: string;
  gender: string;
  permanentAddress: string;
  divisionId: string;
  districtId: string;
  blockId: string;
  gramPanchayatId: string;
  pinCode: string;
  appliedForTraining: string;
  preferredTrainingLocationId: string;
  qualificationId: string;
  boardUniversityName: string;
  passingYear: string;
  percentageCgpa: string;
  experienceDetails: string;
  declarationAccepted: boolean;
}

export type FormField = keyof FormData;

export interface StepProps {
  formData: FormData;
  update: (field: FormField, value: string | boolean) => void;
}

const initialState: FormData = {
  firstName: '',
  middleName: '',
  lastName: '',
  fatherName: '',
  aadhaarNumber: '',
  panNumber: '',
  drivingLicenseNumber: '',
  samagraId: '',
  mobileNumber: '',
  emailId: '',
  dateOfBirth: '',
  gender: '',
  permanentAddress: '',
  divisionId: '',
  districtId: '',
  blockId: '',
  gramPanchayatId: '',
  pinCode: '',
  appliedForTraining: '',
  preferredTrainingLocationId: '',
  qualificationId: '',
  boardUniversityName: '',
  passingYear: '',
  percentageCgpa: '',
  experienceDetails: '',
  declarationAccepted: false,
};

const TOTAL_STEPS = 7;

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
    const errors: string[] = [];

    if (currentStep === 0) {
      if (!formData.firstName.trim()) errors.push('First name is required');
      if (!formData.lastName.trim()) errors.push('Last name is required');
      if (!formData.fatherName.trim()) errors.push('Father name is required');
      if (!formData.mobileNumber.trim()) errors.push('Mobile number is required');
      if (!/^\d{10}$/.test(formData.mobileNumber)) errors.push('Mobile must be 10 digits');
      if (!formData.emailId.trim()) errors.push('Email is required');
      if (!formData.dateOfBirth.trim()) errors.push('Date of birth is required');
    }

    if (currentStep === 2) {
      if (!formData.permanentAddress.trim()) errors.push('Address is required');
      if (!formData.divisionId) errors.push('Division is required');
      if (!formData.districtId) errors.push('District is required');
      if (!formData.blockId) errors.push('Block is required');
      if (!formData.gramPanchayatId) errors.push('Gram Panchayat is required');
      if (!formData.pinCode.trim()) errors.push('PIN code is required');
      if (!/^\d{6}$/.test(formData.pinCode)) errors.push('PIN must be 6 digits');
    }

    if (currentStep === 3) {
      if (!formData.appliedForTraining) errors.push('Please select training type');
      if (!formData.preferredTrainingLocationId) errors.push('Preferred training location is required');
    }

    if (currentStep === 4) {
      if (!formData.qualificationId) errors.push('Qualification is required');
      if (!formData.boardUniversityName.trim()) errors.push('Board/University name is required');
      if (!formData.passingYear.trim()) errors.push('Passing year is required');
      if (!formData.percentageCgpa.trim()) errors.push('Percentage/CGPA is required');
    }

    if (errors.length > 0) {
      ToastService.error(errors[0]);
      return false;
    }
    return true;
  }, [currentStep, formData]);

  const next = useCallback(() => {
    if (validate() && currentStep < TOTAL_STEPS - 1) {
      setCurrentStep((s) => s + 1);
    }
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
        firstName: formData.firstName.trim(),
        middleName: formData.middleName.trim() || undefined,
        lastName: formData.lastName.trim(),
        fatherName: formData.fatherName.trim(),
        aadhaarNumber: formData.aadhaarNumber.trim() || undefined,
        panNumber: formData.panNumber.trim().toUpperCase() || undefined,
        drivingLicenseNumber: formData.drivingLicenseNumber.trim() || undefined,
        samagraId: formData.samagraId.trim() || undefined,
        mobileNumber: formData.mobileNumber.trim(),
        emailId: formData.emailId.trim(),
        dateOfBirth: formData.dateOfBirth,
        permanentAddress: formData.permanentAddress.trim(),
        divisionId: parseInt(formData.divisionId, 10),
        districtId: parseInt(formData.districtId, 10),
        blockId: parseInt(formData.blockId, 10),
        gramPanchayatId: parseInt(formData.gramPanchayatId, 10),
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
      setFormData(initialState);
      setCurrentStep(0);
    } catch (err) {
      const message = err instanceof Error ? err.message : 'Submission failed';
      ToastService.error(message);
    } finally {
      setIsSubmitting(false);
    }
  }, [formData, validate]);

  return {
    formData,
    update,
    validate,
    currentStep,
    next,
    prev,
    goToStep,
    handleSubmit,
    isSubmitting,
  };
}
