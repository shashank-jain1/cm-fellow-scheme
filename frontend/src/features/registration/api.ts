import ApiService from '../../services/ApiService';
import { REGISTRATION_URLS } from './urls';
import type { Fellow, Division, District, Block, Project, ProfileUpdatePayload, BulkApprovalPayload } from './types';

export interface SubmitRegistrationPayload {
  firstName: string;
  middleName?: string;
  lastName: string;
  fatherName: string;
  aadhaarNumber?: string;
  panNumber?: string;
  drivingLicenseNumber?: string;
  samagraId?: string;
  mobileNumber: string;
  emailId: string;
  dateOfBirth: string;
  permanentAddress: string;
  divisionId: number;
  districtId: number;
  blockId: number;
  gramPanchayatId: number;
  pinCode: string;
  appliedForTraining: number;
  preferredTrainingLocationId: number;
  qualificationId: number;
  boardUniversityName: string;
  passingYear: number;
  percentageCgpa: number;
  experienceDetails?: string;
  declarationAccepted: boolean;
}

export interface VerifyOtpPayload {
  mobileNumber: string;
  otpCode: string;
}

export interface RegistrationListItem {
  applicantId: number;
  firstName: string;
  lastName: string;
  mobileNumber: string;
  emailId: string;
  status: string;
  createdOn: string;
}

export const registrationApi = {
  submitRegistration: (data: SubmitRegistrationPayload) =>
    ApiService.post<number>(REGISTRATION_URLS.FELLOWS, data),

  verifyMobileOtp: (data: VerifyOtpPayload) =>
    ApiService.put<void>(`${REGISTRATION_URLS.FELLOWS}/verify-otp`, data),

  listRegistrations: (params?: { searchTerm?: string; status?: string; pageNumber?: number; pageSize?: number }) => {
    const query = new URLSearchParams();
    if (params?.searchTerm) query.set('searchTerm', params.searchTerm);
    if (params?.status) query.set('status', params.status);
    if (params?.pageNumber) query.set('pageNumber', String(params.pageNumber));
    if (params?.pageSize) query.set('pageSize', String(params.pageSize));
    const qs = query.toString();
    return ApiService.get<{ items: RegistrationListItem[]; totalCount: number }>(
      qs ? `${REGISTRATION_URLS.FELLOWS}?${qs}` : REGISTRATION_URLS.FELLOWS
    );
  },

  approveRegistration: (applicantId: number, approvedBy: number) =>
    ApiService.put<void>(`${REGISTRATION_URLS.FELLOWS}/${applicantId}/approve`, { approvedBy }),

  rejectRegistration: (applicantId: number, rejectedBy: number, reason: string) =>
    ApiService.put<void>(`${REGISTRATION_URLS.FELLOWS}/${applicantId}/reject`, { rejectedBy, reason }),

  getRegistrationById: (id: number) =>
    ApiService.get<Fellow>(REGISTRATION_URLS.FELLOW_BY_ID(id)),

  getDivisions: () => ApiService.get<Division[]>(REGISTRATION_URLS.DIVISIONS),
  getDistricts: (divisionId?: number) =>
    ApiService.get<District[]>(divisionId ? `${REGISTRATION_URLS.DISTRICTS}?divisionId=${divisionId}` : REGISTRATION_URLS.DISTRICTS),
  getBlocks: (districtId?: number) =>
    ApiService.get<Block[]>(districtId ? `${REGISTRATION_URLS.BLOCKS}?districtId=${districtId}` : REGISTRATION_URLS.BLOCKS),
  getProjects: () => ApiService.get<Project[]>(REGISTRATION_URLS.PROJECTS),

  forgotPassword: (email: string) =>
    ApiService.post<{ message: string; resetToken?: string }>(REGISTRATION_URLS.FORGOT_PASSWORD, { email }),

  resetPassword: (token: string, newPassword: string) =>
    ApiService.post<void>(REGISTRATION_URLS.RESET_PASSWORD, { token, newPassword }),

  updateProfile: (applicantId: number, data: ProfileUpdatePayload) =>
    ApiService.put<void>(`${REGISTRATION_URLS.FELLOWS}/${applicantId}/profile`, data),

  sendOtp: (mobileNumber: string) =>
    ApiService.post<void>(`${REGISTRATION_URLS.FELLOWS}/send-otp`, { mobileNumber }),

  bulkApprove: (data: BulkApprovalPayload) =>
    ApiService.post<void>(`${REGISTRATION_URLS.FELLOWS}/bulk-approve`, data),

  bulkImportUsers: (file: File) => {
    const formData = new FormData();
    formData.append('file', file);
    return ApiService.postFormData<{ imported: number; errors: string[] }>('admin/import/users', formData);
  },
};
