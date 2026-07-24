import { Button } from 'primereact/button';

interface SupervisorRatingInputProps {
  value: number;
  onChange: (rating: number) => void;
  onSubmit: () => void;
  isSubmitting?: boolean;
}

export default function SupervisorRatingInput({ value, onChange, onSubmit, isSubmitting }: SupervisorRatingInputProps) {
  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Supervisor Rating</h3>
      <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 16 }}>
        {[1, 2, 3, 4, 5].map((star) => (
          <button
            key={star}
            type="button"
            onClick={() => onChange(star)}
            style={{
              background: 'none',
              border: 'none',
              cursor: 'pointer',
              fontSize: 28,
              color: star <= value ? 'var(--amber-400)' : 'var(--border-color)',
              transition: 'color 150ms',
              padding: '2px 4px',
            }}
          >
            <i className={`pi ${star <= value ? 'pi-star-fill' : 'pi-star'}`} />
          </button>
        ))}
        <span style={{ marginLeft: 8, fontSize: 14, color: 'var(--text-secondary)', fontWeight: 500 }}>
          {value > 0 ? `${value}/5` : 'Not rated'}
        </span>
      </div>
      <Button
        label="Save Rating"
        className="btn btn-primary"
        onClick={onSubmit}
        disabled={value === 0 || isSubmitting}
        loading={isSubmitting}
      />
    </div>
  );
}
