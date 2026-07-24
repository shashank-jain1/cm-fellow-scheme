export const ToastService = {
  success: (msg: string) => {
    const el = document.createElement('div');
    el.className = 'toast toast-success';
    el.textContent = msg;
    document.body.appendChild(el);
    setTimeout(() => el.classList.add('show'), 10);
    setTimeout(() => { el.classList.remove('show'); setTimeout(() => el.remove(), 300); }, 3000);
  },
  error: (msg: string) => {
    const el = document.createElement('div');
    el.className = 'toast toast-error';
    el.textContent = msg;
    document.body.appendChild(el);
    setTimeout(() => el.classList.add('show'), 10);
    setTimeout(() => { el.classList.remove('show'); setTimeout(() => el.remove(), 300); }, 3000);
  },
};
