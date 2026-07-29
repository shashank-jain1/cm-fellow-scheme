import { MultiSelect, type MultiSelectProps } from 'primereact/multiselect';

export default function AppMultiSelect({ display = 'chip', className, ...props }: MultiSelectProps) {
  return <MultiSelect display={display} className={`w-full ${className ?? ''}`} {...props} />;
}
