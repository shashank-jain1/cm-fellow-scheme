import { Tag } from 'primereact/tag';
import type { SurveyDetailDto } from '../types';

interface SurveyDetailsDrilldownGridProps {
  data: SurveyDetailDto[];
  isLoading?: boolean;
  workProject?: string;
}

const getStatusSeverity = (status: string) => {
  switch (status.toLowerCase()) {
    case 'completed':
    case 'done':
      return 'success';
    case 'in progress':
    case 'pending':
      return 'warning';
    case 'not started':
    case 'failed':
      return 'danger';
    default:
      return 'info';
  }
};

export default function SurveyDetailsDrilldownGrid({
  data,
  isLoading = false,
  workProject,
}: SurveyDetailsDrilldownGridProps) {
  if (isLoading) {
    return (
      <div style={{ padding: 20 }}>
        {[1, 2, 3, 4, 5].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 16, padding: '14px 0', borderBottom: '1px solid var(--border-light)' }}>
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
            <div className="skeleton" style={{ width: '12%', height: 14 }} />
            <div className="skeleton" style={{ width: '18%', height: 14 }} />
            <div className="skeleton" style={{ width: '12%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (data.length === 0) {
    return (
      <div style={{ padding: 20, textAlign: 'center', color: 'var(--text-secondary)' }}>
        No survey records found
      </div>
    );
  }

  return (
    <div style={{ padding: '16px 0' }}>
      {workProject && (
        <div style={{ padding: '0 16px 12px', fontWeight: 600, fontSize: 14 }}>
          Survey Records for: {workProject}
        </div>
      )}
      <div className="table-wrapper">
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr style={{ background: 'var(--navy-50)' }}>
              {['Intern', 'Survey Person', 'Mobile', 'Panchayat', 'Village', 'Date', 'Status'].map((h) => (
                <th
                  key={h}
                  style={{
                    padding: '10px 12px',
                    textAlign: 'left',
                    fontSize: 11,
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
            {data.map((survey) => (
              <tr key={survey.surveyRecordId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                <td style={{ padding: '10px 12px', fontSize: 13, fontWeight: 500 }}>
                  {survey.internName}
                </td>
                <td style={{ padding: '10px 12px', fontSize: 13, color: 'var(--text-secondary)' }}>
                  {survey.surveyPersonName}
                </td>
                <td style={{ padding: '10px 12px', fontSize: 13 }}>
                  {survey.mobileNumber}
                </td>
                <td style={{ padding: '10px 12px', fontSize: 13 }}>
                  {survey.panchayatName}
                </td>
                <td style={{ padding: '10px 12px', fontSize: 13 }}>
                  {survey.villageName}
                </td>
                <td style={{ padding: '10px 12px', fontSize: 12, color: 'var(--text-muted)' }}>
                  {survey.surveyDate}
                </td>
                <td style={{ padding: '10px 12px' }}>
                  <Tag
                    value={survey.surveyStatus}
                    severity={getStatusSeverity(survey.surveyStatus)}
                  />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
