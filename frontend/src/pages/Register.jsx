import { useState } from 'react';
import api from '../services/api';

function Register() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    role: 'Student',
  });

  const [message, setMessage] = useState('');
  const [error, setError] = useState('');

  function handleChange(e) {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setMessage('');
    setError('');

    try {
      await api.post('/Auth/register', {
        ...formData,
        role: 'Student',
      });

      setMessage('Registration successful! You can now login.');
      setFormData({
        name: '',
        email: '',
        password: '',
        role: 'Student',
      });
    } catch {
      setError('Registration failed. Try another email.');
    }
  }

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h1>Register</h1>

        <form onSubmit={handleSubmit} className="form">
          <input
            type="text"
            name="name"
            placeholder="Full name"
            value={formData.name}
            onChange={handleChange}
            required
          />

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

          <button type="submit">Create Student Account</button>
        </form>

        {message && <p className="success">{message}</p>}
        {error && <p className="error">{error}</p>}
      </div>
    </div>
  );
}

export default Register;