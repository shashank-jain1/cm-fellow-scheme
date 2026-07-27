import { useState } from 'react';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { Dialog } from 'primereact/dialog';
import FormSelect from '../../../../shared/components/FormSelect';
import { roleOptions } from './constants';
import { useCreateUserAccount } from '../../queries/user-management';
import { useAuth } from '../../../auth';

interface CreateUserDialogProps {
  visible: boolean;
  onHide: () => void;
}

export default function CreateUserDialog({ visible, onHide }: CreateUserDialogProps) {
  const { user: currentUser } = useAuth();
  const createMutation = useCreateUserAccount();
  const [form, setForm] = useState({ applicantId: 0, username: '', password: '', role: 'Intern' });

  const handleCreate = async () => {
    if (!form.username || !form.password) return;
    await createMutation.mutateAsync({ ...form, createdBy: currentUser?.userAccountId ?? 0 });
    setForm({ applicantId: 0, username: '', password: '', role: 'Intern' });
    onHide();
  };

  return (
    <Dialog header="Create User Account" visible={visible} onHide={onHide} style={{ width: 500 }} modal>
      <div className="form-grid" style={{ paddingTop: 8 }}>
        <div className="form-field">
          <label>Applicant ID</label>
          <InputText
            value={form.applicantId ? String(form.applicantId) : ''}
            onChange={(e) => setForm({ ...form, applicantId: Number(e.target.value) || 0 })}
            placeholder="Enter applicant ID"
          />
        </div>
        <div className="form-field">
          <label>Username</label>
          <InputText
            value={form.username}
            onChange={(e) => setForm({ ...form, username: e.target.value })}
            placeholder="Enter username"
          />
        </div>
        <div className="form-field">
          <label>Password</label>
          <InputText
            type="password"
            value={form.password}
            onChange={(e) => setForm({ ...form, password: e.target.value })}
            placeholder="Enter password"
          />
        </div>
        <div className="form-field">
          <label>Role</label>
          <FormSelect value={form.role} onChange={(val) => setForm({ ...form, role: val })} options={roleOptions} />
        </div>
      </div>
      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end', marginTop: 24 }}>
        <Button label="Cancel" className="btn btn-secondary" onClick={onHide} />
        <Button label="Create" className="btn btn-primary" onClick={handleCreate} loading={createMutation.isPending} />
      </div>
    </Dialog>
  );
}
