export interface LookupMasterDto {
  lookupMasterId: number;
  masterType: string;
  label: string;
  value: string;
  sortOrder: number;
  isActive: boolean;
}

export interface CreateLookupMasterCommand {
  masterType: string;
  label: string;
  value: string;
  sortOrder: number;
}
