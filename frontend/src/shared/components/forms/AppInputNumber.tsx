import { InputNumber, type InputNumberProps } from 'primereact/inputnumber';

export default function AppInputNumber({ className, ...props }: InputNumberProps) {
  return <InputNumber className={`w-full ${className ?? ''}`} {...props} />;
}
