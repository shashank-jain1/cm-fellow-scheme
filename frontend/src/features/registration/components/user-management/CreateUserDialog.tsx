import { useState } from 'react';
import { Button } from 'primereact/button';
import { AppInput, AppDialog, AppSelect } from '../../../../shared/components/forms';
import { useLookupOptions } from '../../../../shared/hooks/useMasters';
import { useCreateUserAccount } from '../../queries/user-management';
import { useListRegistrationsQuery } from '../../queries';
import { useAuth } from '../../../auth';

interface CreateUserDialogProps {
  visible: boolean;
  onHide: () => void;
}

export default function CreateUserDialog({ visible, onHide }: CreateUserDialogProps) {
  const { user: currentUser } = useAuth();
  const createMutation = useCreateUserAccount();
  const roleOptions = useLookupOptions('Role');
  const { data: registrationsData } = useListRegistrationsQuery({ status: 'Approved', pageSize: 200 });
  const [form, setForm] = useState({ applicantId: 0, username: '', password: '', role: 'Intern' });

  const applicantOptions = (registrationsData?.items ?? []).map((r) => ({
    label: `${r.firstName} ${r.lastName} (${r.emailId ?? r.mobileNumber})`,
    value: r.applicantId,
  }));

  const handleCreate = async () => {
    if (!form.applicantId || !form.username || !form.password) return;
    await createMutation.mutateAsync({ ...form, createdBy: currentUser?.userAccountId ?? 0 });
    setForm({ applicantId: 0, username: '', password: '', role: 'Intern' });
    onHide();
  };

  return (
    <AppDialog header="Create User Account" visible={visible} onHide={onHide} style={{ width: 500 }} modal>
      <div className="form-grid" style={{ paddingTop: 8 }}>
        <div className="form-field">
          <label>Applicant *</label>
          <AppSelect
            value={form.applicantId || ''}
            options={applicantOptions}
            onChange={(val) => setForm({ ...form, applicantId: (val as number) ?? 0 })}
            placeholder="Select Applicant"
            className="w-full"
          />
        </div>
        <div className="form-field">
          <label>Username *</label>
          <AppInput
            value={form.username}
            onChange={(e) => setForm({ ...form, username: e.target.value })}
            placeholder="Enter username"
          />
        </div>
        <div className="form-field">
          <label>Password *</label>
          <AppInput
            type="password"
            value={form.password}
            onChange={(e) => setForm({ ...form, password: e.target.value })}
            placeholder="Enter password"
          />
        </div>
        <div className="form-field">
          <label>Role *</label>
          <AppSelect value={form.role} onChange={(val) => setForm({ ...form, role: val })} options={roleOptions} />
        </div>
      </div>
      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end', marginTop: 24 }}>
        <Button label="Cancel" className="btn btn-secondary" onClick={onHide} />
        <Button label="Create" className="btn btn-primary" onClick={handleCreate} loading={createMutation.isPending} />
      </div>
    </AppDialog>
  );
}
