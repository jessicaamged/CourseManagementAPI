import { Link } from 'react-router-dom';
import { useEffect, useState } from 'react';

function Dashboard() {
  const [name, setName] = useState('');
  const [role, setRole] = useState('');

  useEffect(() => {
    const token = localStorage.getItem('token');

    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));

      setName(payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
      setRole(payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
    }
  }, []);

  return (
    <div className="page">
      <div className="hero-card">
        <h1>Welcome back, {name} 👋</h1>
        <p>You are logged in as <strong>{role}</strong></p>
      </div>

      <div className="dashboard-grid">
        <Link to="/courses" className="dashboard-card">
          <h2>📚 Courses</h2>
          <p>View, add, edit, and delete courses.</p>
        </Link>

        <Link to="/enrollments" className="dashboard-card">
          <h2>📝 Enrollments</h2>
          <p>Manage course enrollments and student access.</p>
        </Link>
      </div>
    </div>
  );
}

export default Dashboard;