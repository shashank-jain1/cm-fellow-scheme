import { useCheckOutMutation } from '../queries';
import AppButton from '../../../shared/components/ui/AppButton';

interface CheckOutButtonProps {
  applicantId: number;
  attendanceDate: string;
  disabled?: boolean;
}

export default function CheckOutButton({ applicantId, attendanceDate, disabled }: CheckOutButtonProps) {
  const checkOut = useCheckOutMutation();

  const handleCheckOut = () => {
    checkOut.mutate({ applicantId, attendanceDate });
  };

  return (
    <AppButton
      onClick={handleCheckOut}
      loading={checkOut.isPending}
      disabled={disabled || checkOut.isPending}
      icon="pi pi-sign-out"
      variant="danger"
    >
      Check Out
    </AppButton>
  );
}
