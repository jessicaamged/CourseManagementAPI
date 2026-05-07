import { useState } from 'react';
import api from '../services/api';

function AddInstructor() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
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
      await api.post('/Users/instructors', formData);

      setMessage('Instructor created successfully.');

      setFormData({
        name: '',
        email: '',
        password: '',
      });
    } catch {
      setError('Failed to create instructor.');
    }
  }

  return (
    <div className="page">
      <div className="card">
        <h1>Add Instructor</h1>

        <form onSubmit={handleSubmit} className="form wide-form">
          <input
            type="text"
            name="name"
            placeholder="Instructor Name"
            value={formData.name}
            onChange={handleChange}
            required
          />

          <input
            type="email"
            name="email"
            placeholder="Instructor Email"
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

          <button type="submit">Create Instructor</button>
        </form>

        {message && <p className="success">{message}</p>}
        {error && <p className="error">{error}</p>}
      </div>
    </div>
  );
}

export default AddInstructor;