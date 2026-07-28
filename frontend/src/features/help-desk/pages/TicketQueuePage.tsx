import { useState } from 'react';
import { useAuth } from '../../auth/useAuth';
import { useTickets } from '../queries';
import TicketKpiCards from '../components/TicketKpiCards';
import TicketFilters from '../components/TicketFilters';
import TicketTable from '../components/TicketTable';
import { SkeletonTable } from '../../../shared/components/ui';

export default function TicketQueuePage() {
  const { user } = useAuth();
  const [search, setSearch] = useState('');
  const [statusFilter, setStatusFilter] = useState('');
  const [priorityFilter, setPriorityFilter] = useState('');
  const { data: tickets, isLoading } = useTickets(user?.role, user?.userAccountId);

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
          <p>View and manage support tickets</p>
        </div>
      </div>

      <TicketKpiCards tickets={tickets ?? []} />

      <TicketFilters
        search={search}
        onSearchChange={setSearch}
        statusFilter={statusFilter}
        onStatusFilterChange={setStatusFilter}
        priorityFilter={priorityFilter}
        onPriorityFilterChange={setPriorityFilter}
      />

      <div className="table-wrapper">
        {isLoading ? <SkeletonTable columns={7} /> : <TicketTable tickets={filtered} />}
      </div>
    </div>
  );
}
