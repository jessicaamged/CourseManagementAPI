import { useEffect, useState } from 'react';
import api from '../services/api';

function Enrollments() {
  const [enrollments, setEnrollments] = useState([]);
  const [courses, setCourses] = useState([]);
  const [students, setStudents] = useState([]);

  const [formData, setFormData] = useState({
    userId: '',
    courseId: '',
  });

  const [role, setRole] = useState('');
  const [userId, setUserId] = useState('');

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');

  useEffect(() => {
    const token = localStorage.getItem('token');

    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));

      const tokenUserId =
        payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

      const tokenRole =
        payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      setUserId(tokenUserId);
      setRole(tokenRole);

      setFormData({
        userId: tokenUserId,
        courseId: '',
      });

      fetchCourses();

      if (tokenRole === 'Admin' || tokenRole === 'Instructor') {
        fetchStudents();
        fetchEnrollments();
      } else {
        setLoading(false);
      }
    }
  }, []);

  async function fetchCourses() {
    try {
      const response = await api.get('/Courses');
      setCourses(response.data);
    } catch {
      setError('Failed to load courses.');
    }
  }

  async function fetchStudents() {
    try {
      const response = await api.get('/Users/students');
      setStudents(response.data);
    } catch {
      setError('Failed to load students.');
    }
  }

  async function fetchEnrollments() {
    try {
      const response = await api.get('/Enrollments');
      setEnrollments(response.data);
    } catch {
      setError('Failed to load enrollments. Admin/Instructor only can view all enrollments.');
    } finally {
      setLoading(false);
    }
  }

  function handleChange(e) {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  }

  async function addEnrollment(e) {
    e.preventDefault();
    setError('');
    setMessage('');

    try {
      await api.post('/Enrollments', {
        userId: Number(formData.userId),
        courseId: Number(formData.courseId),
      });

      setMessage('Enrollment added successfully.');

      setFormData({
        userId: role === 'Student' ? userId : '',
        courseId: '',
      });

      if (role === 'Admin' || role === 'Instructor') {
        fetchEnrollments();
      }
    } catch {
      setError('Failed to add enrollment. Student may already be enrolled or data is invalid.');
    }
  }

  async function deleteEnrollment(userId, courseId) {
    setError('');
    setMessage('');

    try {
      await api.delete(`/Enrollments/${userId}/${courseId}`);
      setMessage('Enrollment deleted successfully.');
      fetchEnrollments();
    } catch {
      setError('Failed to delete enrollment. Admin only.');
    }
  }

  if (loading) return <p className="page">Loading enrollments...</p>;

  return (
    <div className="page">
      <div className="section-header">
        <div>
          <h1>Enrollments</h1>
          <p>Enroll students in courses and manage existing course registrations.</p>
        </div>
      </div>

      <div className="card">
        <h2>Add Enrollment</h2>

        {role === 'Student' && (
          <p className="info-box">
            Choose a course name to enroll yourself.
          </p>
        )}

        {(role === 'Admin' || role === 'Instructor') && (
          <p className="info-box">
            Choose a student name and a course name.
          </p>
        )}

        <form onSubmit={addEnrollment} className="form wide-form">
          {role !== 'Student' && (
            <select
              name="userId"
              value={formData.userId}
              onChange={handleChange}
              required
            >
              <option value="">Select Student</option>
              {students.map((student) => (
                <option key={student.id} value={student.id}>
                  {student.name} ({student.email})
                </option>
              ))}
            </select>
          )}

          <select
            name="courseId"
            value={formData.courseId}
            onChange={handleChange}
            required
          >
            <option value="">Select Course</option>
            {courses.map((course) => (
              <option key={course.id} value={course.id}>
                {course.title}
              </option>
            ))}
          </select>

          <div className="actions">
            <button type="submit">Add Enrollment</button>
          </div>
        </form>

        {message && <p className="success">{message}</p>}
        {error && <p className="error">{error}</p>}
      </div>

      <div className="card">
        <h2>Enrollment List</h2>

        {role === 'Admin' || role === 'Instructor' ? (
          enrollments.length === 0 ? (
            <p>No enrollments found.</p>
          ) : (
            <div className="list">
              {enrollments.map((enrollment) => (
                <div
                  className="list-item"
                  key={`${enrollment.userId}-${enrollment.courseId}`}
                >
                  <div>
                    <h3>{enrollment.userName}</h3>
                    <p>Course: {enrollment.courseTitle}</p>
                    <small>
                      Student ID: {enrollment.userId} | Course ID: {enrollment.courseId}
                    </small>
                  </div>

                  <div className="actions">
                    {role === 'Admin' && (
                      <button
                        onClick={() =>
                          deleteEnrollment(enrollment.userId, enrollment.courseId)
                        }
                        className="btn-danger"
                      >
                        Delete
                      </button>
                    )}
                  </div>
                </div>
              ))}
            </div>
          )
        ) : (
          <p className="info-box">
            Students cannot view all enrollments. You can only enroll yourself in a course.
          </p>
        )}
      </div>
    </div>
  );
}

export default Enrollments;