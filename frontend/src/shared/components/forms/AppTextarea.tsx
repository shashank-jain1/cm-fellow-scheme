import { InputTextarea, type InputTextareaProps } from 'primereact/inputtextarea';

export default function AppTextarea({ className, ...props }: InputTextareaProps) {
  return <InputTextarea className={`w-full ${className ?? ''}`} rows={3} {...props} />;
}
