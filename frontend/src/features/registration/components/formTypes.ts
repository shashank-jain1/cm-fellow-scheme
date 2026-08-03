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

export const TOTAL_STEPS = 7;

export const initialState: FormData = {
  firstName: '', middleName: '', lastName: '', fatherName: '',
  aadhaarNumber: '', panNumber: '', drivingLicenseNumber: '', samagraId: '',
  mobileNumber: '', emailId: '', dateOfBirth: '', gender: '',
  permanentAddress: '', divisionId: '', districtId: '', blockId: '',
  gramPanchayatId: '', pinCode: '', appliedForTraining: '',
  preferredTrainingLocationId: '', qualificationId: '', boardUniversityName: '',
  passingYear: '', percentageCgpa: '', experienceDetails: '',
  declarationAccepted: false,
};
