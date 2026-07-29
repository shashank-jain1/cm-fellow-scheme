import { InputText, type InputTextProps } from 'primereact/inputtext';

export default function AppInput({ className, ...props }: InputTextProps) {
  return <InputText className={`w-full ${className ?? ''}`} {...props} />;
}
