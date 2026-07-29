import { Dialog, type DialogProps } from 'primereact/dialog';

export default function AppDialog({ header, modal = true, draggable = false, resizable = false, ...props }: DialogProps) {
  return <Dialog header={header} modal={modal} draggable={draggable} resizable={resizable} {...props} />;
}
