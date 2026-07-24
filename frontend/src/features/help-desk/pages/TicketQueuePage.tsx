import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { useTickets } from '../queries';
import TicketStatusBadge from '../components/TicketStatusBadge';
import { formatDateTime } from '../../../shared/utils/format';
import FormSelect from '../../../shared/components/FormSelect';

const statusOptions = [
  { label: 'All', value: '' },
  { label: 'Open', value: 'open' },
  { label: 'In Progress', value: 'in_progress' },
  { label: 'Resolved', value: 'resolved' },
  { label: 'Closed', value: 'closed' },
];

const priorityOptions = [
  { label: 'All', value: '' },
  { label: 'High', value: 'High' },
  { label: 'Medium', value: 'Medium' },
  { label: 'Low', value: 'Low' },
];

export default function TicketQueuePage() {
  const [search, setSearch] = useState('');
  const [statusFilter, setStatusFilter] = useState('');
  const [priorityFilter, setPriorityFilter] = useState('');
  const { data: tickets, isLoading } = useTickets();

  const filtered = (tickets ?? []).filter((t) => {
    const matchSearch =
      t.issueDescription.toLowerCase().includes(search.toLowerCase()) ||
      t.email.toLowerCase().includes(search.toLowerCase());
    const matchStatus = !statusFilter || t.status === statusFilter;
    const matchPriority = !priorityFilter || t.priority === priorityFilter;
    return matchSearch && matchStatus && matchPriority;
  });

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Ticket Queue</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            View and manage support tickets
          </p>
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: 16, marginBottom: 24 }}>
        {[
          { label: 'Total Tickets', value: tickets?.length ?? 0, color: 'var(--navy-600)' },
          { label: 'Open', value: (tickets ?? []).filter((t) => t.status === 'open').length, color: 'var(--amber-500)' },
          { label: 'In Progress', value: (tickets ?? []).filter((t) => t.status === 'in_progress').length, color: 'var(--emerald-500)' },
          { label: 'Resolved', value: (tickets ?? []).filter((t) => t.status === 'resolved' || t.status === 'closed').length, color: 'var(--emerald-600)' },
        ].map((stat, i) => (
          <div key={i} className="kpi-card" style={{ textAlign: 'center', padding: 20 }}>
            <div style={{ fontSize: 28, fontWeight: 700, color: stat.color }}>{stat.value}</div>
            <div style={{ fontSize: 13, color: 'var(--text-secondary)', marginTop: 4 }}>{stat.label}</div>
          </div>
        ))}
      </div>

      <div style={{ display: 'flex', gap: 12, marginBottom: 20 }}>
        <div style={{ position: 'relative', flex: '0 0 320px' }}>
          <i className="pi pi-search" style={{ position: 'absolute', left: 12, top: '50%', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
          <InputText
            value={search}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearch(e.target.value)}
            placeholder="Search tickets..."
            style={{ width: '100%', paddingLeft: 36 }}
          />
        </div>
        <FormSelect value={statusFilter} onChange={setStatusFilter} options={statusOptions} showClear style={{ width: 150 }} />
        <FormSelect value={priorityFilter} onChange={setPriorityFilter} options={priorityOptions} showClear style={{ width: 150 }} />
      </div>

      <div className="table-wrapper">
        {isLoading ? (
          <div style={{ padding: 20 }}>
            {[1, 2, 3, 4, 5].map((n) => (
              <div key={n} style={{ display: 'flex', gap: 16, padding: '14px 0', borderBottom: '1px solid var(--border-light)' }}>
                <div className="skeleton" style={{ width: '12%', height: 14 }} />
                <div className="skeleton" style={{ width: '25%', height: 14 }} />
                <div className="skeleton" style={{ width: '15%', height: 14 }} />
                <div className="skeleton" style={{ width: '10%', height: 14 }} />
                <div className="skeleton" style={{ width: '10%', height: 14 }} />
              </div>
            ))}
          </div>
        ) : filtered.length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--navy-50)' }}>
                {['Ticket ID', 'Category', 'Email', 'Priority', 'Status', 'Created'].map((h) => (
                  <th
                    key={h}
                    style={{
                      padding: '12px 16px',
                      textAlign: 'left',
                      fontSize: 12,
                      fontWeight: 600,
                      color: 'var(--text-secondary)',
                      textTransform: 'uppercase',
                      letterSpacing: '0.5px',
                      borderBottom: '1px solid var(--border-color)',
                    }}
                  >
                    {h}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              {filtered.map((t) => (
                <tr key={t.ticketId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                  <td style={{ padding: '14px 16px', fontSize: 13, fontFamily: 'monospace', color: 'var(--emerald-600)', fontWeight: 500 }}>
                    #{t.ticketId}
                  </td>
                  <td style={{ padding: '14px 16px', fontWeight: 500, fontSize: 14 }}>{t.issueCategory}</td>
                  <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{t.email}</td>
                  <td style={{ padding: '14px 16px' }}>
                    <span
                      className="tag"
                      style={{
                        color: t.priority === 'High' ? 'var(--red-500)' : t.priority === 'Low' ? 'var(--emerald-500)' : 'var(--amber-500)',
                      }}
                    >
                      {t.priority}
                    </span>
                  </td>
                  <td style={{ padding: '14px 16px' }}>
                    <TicketStatusBadge status={t.status} />
                  </td>
                  <td style={{ padding: '14px 16px', fontSize: 12, color: 'var(--text-muted)' }}>{formatDateTime(t.createdOn)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <div className="empty-state">
            <i className="pi pi-question-circle" />
            <h3>No tickets found</h3>
            <p>Raise a ticket to get support from the admin team</p>
          </div>
        )}
      </div>
    </div>
  );
}
