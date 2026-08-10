interface PerformanceScoreCardProps {
  performanceScore: number;
  performanceGrade: string;
  completionPercentage: number;
  surveysCompleted: number;
  totalSurveysAssigned: number;
}

export default function PerformanceScoreCard({
  performanceScore,
  performanceGrade,
  completionPercentage,
  surveysCompleted,
  totalSurveysAssigned,
}: PerformanceScoreCardProps) {
  return (
    <div className="metric-bar" style={{ marginBottom: 'var(--space-4)' }}>
      <div className="metric-item">
        <span className="metric-label">Score</span>
        <span className="metric-value">{performanceScore}</span>
      </div>
      <div className="metric-item">
        <span className="metric-label">Grade</span>
        <span className="metric-value">{performanceGrade}</span>
      </div>
      <div className="metric-item">
        <span className="metric-label">Completion</span>
        <span className="metric-value">{completionPercentage}%</span>
      </div>
      <div className="metric-item">
        <span className="metric-label">Surveys</span>
        <span className="metric-value">{surveysCompleted}/{totalSurveysAssigned}</span>
      </div>
    </div>
  );
}
