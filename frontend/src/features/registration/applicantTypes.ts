export interface Fellow {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  gender: string;
  dateOfBirth: string;
  status: string;
  divisionId: number;
  districtId: number;
  blockId: number;
  createdAt: string;
}

export interface Division {
  id: number;
  name: string;
  code: string;
}

export interface District {
  id: number;
  name: string;
  divisionId: number;
}

export interface Block {
  id: number;
  name: string;
  districtId: number;
}

export interface Project {
  id: number;
  name: string;
  code: string;
}
