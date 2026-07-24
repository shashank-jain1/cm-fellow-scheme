import RaiseTicketForm from '../components/RaiseTicketForm';

export default function RaiseTicketPage() {
  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Raise a Ticket</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Create a new support request
          </p>
        </div>
      </div>
      <RaiseTicketForm />
    </div>
  );
}
