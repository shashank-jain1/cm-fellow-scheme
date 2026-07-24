import { useState, useCallback } from 'react';
import { ToastService } from '../../../shared/utils/toast';

export interface FormData {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  dob: string;
  gender: string;
  address: string;
  city: string;
  state: string;
  pincode: string;
  trainingCenter: string;
  trainingBatch: string;
  qualification: string;
  university: string;
  yearOfPassing: string;
  declaration: boolean;
}

export type FormField = keyof FormData;

export interface StepProps {
  formData: FormData;
  update: (field: FormField, value: string | boolean) => void;
}

const initialState: FormData = {
  firstName: '',
  lastName: '',
  email: '',
  phone: '',
  dob: '',
  gender: '',
  address: '',
  city: '',
  state: '',
  pincode: '',
  trainingCenter: '',
  trainingBatch: '',
  qualification: '',
  university: '',
  yearOfPassing: '',
  declaration: false,
};

const TOTAL_STEPS = 6;

export function useRegistrationForm() {
  const [currentStep, setCurrentStep] = useState(0);
  const [formData, setFormData] = useState<FormData>(initialState);

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
      if (!formData.email.trim()) errors.push('Email is required');
      if (!formData.phone.trim()) errors.push('Phone number is required');
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

  const handleSubmit = useCallback(() => {
    ToastService.success('Registration submitted successfully!');
  }, []);

  return {
    formData,
    update,
    validate,
    currentStep,
    next,
    prev,
    goToStep,
    handleSubmit,
  };
}
