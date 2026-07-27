import WorkSection from '../components/works/WorkSection';

export default function WorksPage() {
  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Work Masters</h1>
          <p>Manage work definitions within projects for task assignment and tracking</p>
        </div>
      </div>
      <WorkSection />
    </div>
  );
}
