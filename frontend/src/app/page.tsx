import { Button } from "../presentation/components/ui/Button";

export default function Home() {
  return (
    <div className="animate-fade-in" style={{ padding: '4rem 0', textAlign: 'center' }}>
      <h1 style={{ fontSize: '3.5rem', marginBottom: '1rem', background: 'linear-gradient(to right, var(--primary), var(--secondary))', WebkitBackgroundClip: 'text', WebkitTextFillColor: 'transparent' }}>
        SurveyMaster
      </h1>
      <p style={{ fontSize: '1.25rem', color: 'var(--text-muted)', marginBottom: '3rem', maxWidth: '600px', margin: '0 auto 3rem auto' }}>
        Create dynamic surveys, share them instantly with a unique slug, and analyze the responses seamlessly. Built with Clean Architecture.
      </p>

      <div style={{ display: 'flex', gap: '1rem', justifyContent: 'center' }}>
        <Button size="lg">Create New Survey</Button>
        <Button variant="outline" size="lg">View Dashboard</Button>
      </div>

      <div style={{ marginTop: '5rem', display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '2rem', textAlign: 'left' }}>
        
        <div className="glass-panel">
          <h3>Customer Satisfaction</h3>
          <p style={{ color: 'var(--text-muted)', marginBottom: '1.5rem', fontSize: '0.9rem' }}>Created 2 days ago • 45 Responses</p>
          <div style={{ display: 'flex', gap: '0.5rem' }}>
            <Button variant="secondary" size="sm">Edit</Button>
            <Button variant="ghost" size="sm" style={{ color: 'var(--secondary)' }}>Analytics</Button>
          </div>
        </div>

        <div className="glass-panel">
          <h3>Employee Feedback Q1</h3>
          <p style={{ color: 'var(--text-muted)', marginBottom: '1.5rem', fontSize: '0.9rem' }}>Created 1 week ago • 120 Responses</p>
          <div style={{ display: 'flex', gap: '0.5rem' }}>
            <Button variant="secondary" size="sm">Edit</Button>
            <Button variant="ghost" size="sm" style={{ color: 'var(--secondary)' }}>Analytics</Button>
          </div>
        </div>

      </div>
    </div>
  );
}
