import { Tag } from 'primereact/tag';
import type { SurveyDetailDto } from '../types';

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

export default function SurveyTable({ data }: { data: SurveyDetailDto[] }) {
  return (
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
  );
}
