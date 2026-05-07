import { Link, useNavigate } from 'react-router-dom';

function Navbar() {
  const navigate = useNavigate();
  const token = localStorage.getItem('token');

  let role = '';

  if (token) {
    const payload = JSON.parse(atob(token.split('.')[1]));

    role =
      payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  }

  function logout() {
    localStorage.removeItem('token');
    navigate('/login');
  }

  return (
    <nav className="navbar">
      <div className="brand">CourseManager</div>

      <div className="nav-links">
        {token && <Link to="/">Dashboard</Link>}
        {token && <Link to="/courses">Courses</Link>}
        {token && <Link to="/enrollments">Enrollments</Link>}

        {role === 'Admin' && (
          <Link to="/add-instructor">Add Instructor</Link>
        )}

        {!token && <Link to="/login">Login</Link>}
        {!token && <Link to="/register">Register</Link>}

        {token && (
          <button onClick={logout} className="logout-btn">
            Logout
          </button>
        )}
      </div>
    </nav>
  );
}

export default Navbar;