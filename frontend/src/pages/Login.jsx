import { useState } from 'react';
import api from '../services/api';

function Login() {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });

  const [error, setError] = useState('');

  function handleChange(e) {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setError('');

    try {
      const res = await api.post('/Auth/login', formData);
      localStorage.setItem('token', res.data.token);
      window.location.href = '/'; // redirect
    } catch {
      setError('Invalid email or password');
    }
  }

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h1>Login</h1>

        <form onSubmit={handleSubmit} className="form">
          <input
            type="email"
            name="email"
            placeholder="Email"
            value={formData.email}
            onChange={handleChange}
            required
          />

          <input
            type="password"
            name="password"
            placeholder="Password"
            value={formData.password}
            onChange={handleChange}
            required
          />

          <button type="submit">Login</button>
        </form>

        {error && <p className="error">{error}</p>}
      </div>
    </div>
  );
}

export default Login;