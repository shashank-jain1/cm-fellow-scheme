export interface StateDto {
  stateId: number;
  stateName: string;
  stateCode: string;
  stateShortName: string | null;
  displayOrder: number | null;
  isActive: boolean;
}

export interface DivisionDto {
  divisionId: number;
  stateId: number;
  divisionName: string;
  divisionCode: string | null;
  isActive: boolean;
}

export interface DistrictDto {
  districtId: number;
  divisionId: number;
  districtName: string;
  districtCode: string | null;
  isActive: boolean;
}

export interface BlockDto {
  blockId: number;
  districtId: number;
  blockName: string;
  blockCode: string | null;
  isActive: boolean;
}

export interface GramPanchayatDto {
  gramPanchayatId: number;
  blockId: number;
  gramPanchayatName: string;
  gpCode: string | null;
  isActive: boolean;
}

export interface CreateStateCommand {
  stateName: string;
  stateCode: string;
  stateShortName?: string;
  displayOrder?: number;
}

export interface CreateDivisionCommand {
  stateId: number;
  divisionName: string;
  divisionCode?: string;
}

export interface CreateDistrictCommand {
  divisionId: number;
  districtName: string;
  districtCode?: string;
}

export interface CreateBlockCommand {
  districtId: number;
  blockName: string;
  blockCode?: string;
}

export interface CreateGramPanchayatCommand {
  blockId: number;
  gramPanchayatName: string;
  gpCode?: string;
}
