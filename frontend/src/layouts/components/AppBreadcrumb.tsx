import { Link, useLocation } from 'react-router-dom';
import { breadcrumbMap } from '../data/breadcrumbs';

export default function AppBreadcrumb() {
  const location = useLocation();
  const segments = location.pathname.split('/').filter(Boolean);

  return (
    <nav className="app-breadcrumb" aria-label="Breadcrumb">
      <Link to="/dashboard" className="breadcrumb-link" title="Dashboard">
        <i className="pi pi-home" style={{ fontSize: 13 }} />
      </Link>
      {segments.map((seg, i) => {
        const path = '/' + segments.slice(0, i + 1).join('/');
        const isLast = i === segments.length - 1;
        const label = breadcrumbMap[seg] || seg.replace(/-/g, ' ');

        return (
          <span key={path} style={{ display: 'inline-flex', alignItems: 'center', gap: 6 }}>
            <i className="pi pi-chevron-right breadcrumb-sep" style={{ fontSize: 10 }} />
            {isLast ? (
              <span className="breadcrumb-current">{label}</span>
            ) : (
              <Link to={path} className="breadcrumb-link">{label}</Link>
            )}
          </span>
        );
      })}
    </nav>
  );
}
