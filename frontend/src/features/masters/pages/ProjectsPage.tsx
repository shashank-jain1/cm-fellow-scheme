import ProjectSection from '../components/projects/ProjectSection';

export default function ProjectsPage() {
  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Project Masters</h1>
          <p>Manage projects that are used for work allocation across the system</p>
        </div>
      </div>
      <ProjectSection />
    </div>
  );
}
