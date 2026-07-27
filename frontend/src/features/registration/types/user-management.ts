export interface UserAccountListItem {
  userAccountId: number;
  applicantId: number;
  username: string;
  role: string;
  isActive: boolean;
  firstName: string;
  lastName: string;
  emailId: string;
  mobileNumber: string;
  createdOn: string;
}

export interface CreateUserAccountRequest {
  applicantId: number;
  username: string;
  password: string;
  role: string;
  createdBy: number;
}

export interface AssignRoleRequest {
  role: string;
  modifiedBy: number;
}
