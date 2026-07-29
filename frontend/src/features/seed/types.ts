export type SeedStatus = 'idle' | 'running' | 'success' | 'error';

export interface ModuleSeedState {
  status: SeedStatus;
  message: string;
  count: number;
}

export interface SeedLog {
  module: string;
  message: string;
  type: 'info' | 'success' | 'error';
  timestamp: Date;
}
