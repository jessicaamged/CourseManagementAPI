import { useEffect, useState } from 'react';
import api from '../services/api';

function Courses() {
  const [courses, setCourses] = useState([]);
  const [instructors, setInstructors] = useState([]);

  const [formData, setFormData] = useState({
    title: '',
    description: '',
    instructorId: '',
  });

  const [editingId, setEditingId] = useState(null);
  const [role, setRole] = useState('');
  const [userId, setUserId] = useState('');

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');

  useEffect(() => {
    const token = localStorage.getItem('token');

    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));

      const tokenRole =
        payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      const tokenUserId =
        payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

      setRole(tokenRole);
      setUserId(tokenUserId);

      fetchCourses();

      if (tokenRole === 'Admin' || tokenRole === 'Instructor') {
        fetchInstructors();
      }
    }
  }, []);

  async function fetchCourses() {
    try {
      const response = await api.get('/Courses');
      setCourses(response.data);
    } catch {
      setError('Failed to load courses.');
    } finally {
      setLoading(false);
    }
  }

  async function fetchInstructors() {
    try {
      const response = await api.get('/Users/instructors');
      setInstructors(response.data);
    } catch {
      setError('Failed to load instructors.');
    }
  }

  function handleChange(e) {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  }

  async function handleSubmit(e) {
    e.preventDefault();

    setError('');
    setMessage('');

    const courseData = {
      title: formData.title,
      description: formData.description,
      instructorId: Number(formData.instructorId),
    };

    try {
      if (editingId) {
        await api.put(`/Courses/${editingId}`, courseData);
        setMessage('Course updated successfully.');
      } else {
        await api.post('/Courses', courseData);
        setMessage('Course added successfully.');
      }

      setEditingId(null);

      setFormData({
        title: '',
        description: '',
        instructorId: '',
      });

      fetchCourses();
    } catch {
      setError('Action failed.');
    }
  }

  function startEdit(course) {
    setEditingId(course.id);

    setFormData({
      title: course.title || '',
      description: course.description || '',
      instructorId: course.instructorId || '',
    });
  }

  function cancelEdit() {
    setEditingId(null);

    setFormData({
      title: '',
      description: '',
      instructorId: '',
    });
  }

  async function deleteCourse(id) {
    setError('');
    setMessage('');

    try {
      await api.delete(`/Courses/${id}`);
      setMessage('Course deleted successfully.');
      fetchCourses();
    } catch {
      setError('Failed to delete course.');
    }
  }

  async function enrollInCourse(courseId) {
    setError('');
    setMessage('');

    try {
      await api.post('/Enrollments', {
        userId: Number(userId),
        courseId: Number(courseId),
      });

      setMessage('Enrolled successfully.');
    } catch {
      setError('Failed to enroll.');
    }
  }

  const canManageCourses =
    role === 'Admin' || role === 'Instructor';

  const isStudent = role === 'Student';

  if (loading) return <p className="page">Loading courses...</p>;

  return (
    <div className="page">
      <div className="section-header">
        <div>
          <h1>Courses</h1>
          <p>Manage all available courses in the system.</p>
        </div>
      </div>

      {canManageCourses && (
        <div className="card">
          <h2>
            {editingId ? 'Edit Course' : 'Add New Course'}
          </h2>

          <form onSubmit={handleSubmit} className="form wide-form">
            <input
              type="text"
              name="title"
              placeholder="Course title"
              value={formData.title}
              onChange={handleChange}
              required
            />

            <input
              type="text"
              name="description"
              placeholder="Course description"
              value={formData.description}
              onChange={handleChange}
              required
            />

            <select
              name="instructorId"
              value={formData.instructorId}
              onChange={handleChange}
              required
            >
              <option value="">
                Select Instructor
              </option>

              {instructors.map((instructor) => (
                <option
                  key={instructor.id}
                  value={instructor.id}
                >
                  {instructor.name} ({instructor.email})
                </option>
              ))}
            </select>

            <div className="actions">
              <button type="submit">
                {editingId
                  ? 'Update Course'
                  : 'Add Course'}
              </button>

              {editingId && (
                <button
                  type="button"
                  onClick={cancelEdit}
                  className="btn-secondary"
                >
                  Cancel
                </button>
              )}
            </div>
          </form>
        </div>
      )}

      {message && (
        <p className="success">{message}</p>
      )}

      {error && <p className="error">{error}</p>}

      <div className="card">
        <h2>Course List</h2>

        {courses.length === 0 ? (
          <p>No courses found.</p>
        ) : (
          <div className="list">
            {courses.map((course) => (
              <div
                className="list-item"
                key={course.id}
              >
                <div>
                  <h3>{course.title}</h3>

                  <p>{course.description}</p>

                  <small>
                    Course ID: {course.id} |
                    Instructor:{' '}
                    {course.instructorName ||
                      'Unknown'}
                  </small>
                </div>

                <div className="actions">
                  {canManageCourses && (
                    <>
                      <button
                        onClick={() =>
                          startEdit(course)
                        }
                      >
                        Edit
                      </button>

                      <button
                        onClick={() =>
                          deleteCourse(course.id)
                        }
                        className="btn-danger"
                      >
                        Delete
                      </button>
                    </>
                  )}

                  {isStudent && (
                    <button
                      onClick={() =>
                        enrollInCourse(course.id)
                      }
                    >
                      Enroll
                    </button>
                  )}
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default Courses;