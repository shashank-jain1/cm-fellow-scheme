interface EmptyStateProps {
  icon: string;
  title: string;
  description?: string;
}

export default function EmptyState({ icon, title, description }: EmptyStateProps) {
  return (
    <div className="empty-state">
      <i className={icon} />
      <h3>{title}</h3>
      {description && <p>{description}</p>}
    </div>
  );
}
