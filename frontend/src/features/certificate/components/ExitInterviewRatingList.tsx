import ExitInterviewRatingInput from './ExitInterviewRatingInput';

interface RatingField {
  key: string;
  label: string;
  value: number | null;
}

interface ExitInterviewRatingListProps {
  ratings: RatingField[];
  onChange: (key: string, value: number | null) => void;
}

export default function ExitInterviewRatingList({ ratings, onChange }: ExitInterviewRatingListProps) {
  return (
    <div style={{ display: 'grid', gap: 20 }}>
      {ratings.map((field) => (
        <ExitInterviewRatingInput
          key={field.key}
          label={field.label}
          value={field.value}
          onChange={(v) => onChange(field.key, v)}
        />
      ))}
    </div>
  );
}
