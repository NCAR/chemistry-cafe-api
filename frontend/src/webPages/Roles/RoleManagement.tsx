import React, { useState, useEffect } from 'react';
import axios from 'axios';

interface User {
  uuid: string;
  log_in_info: string;
  role: string;
}

const RoleManagement: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedRoles, setSelectedRoles] = useState<{ [key: string]: string }>({});

  // Fetch users from the backend when the component mounts
  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await axios.get<User[]>('http://localhost:8080/api/User/all');
        setUsers(response.data);
        // Initialize selectedRoles with each user's current role
        const initialRoles = response.data.reduce((acc, user) => {
          acc[user.uuid] = user.role;
          return acc;
        }, {} as { [key: string]: string });
        setSelectedRoles(initialRoles);
      } catch (error) {
        console.error('Error fetching users:', error);
        setError('Failed to fetch users');
      } finally {
        setLoading(false);
      }
    };
    fetchUsers();
  }, []);

  const handleRoleChange = (uuid: string, newRole: string) => {
    setSelectedRoles((prevRoles) => ({
      ...prevRoles,
      [uuid]: newRole,
    }));
  };

  const updateRole = async (uuid: string) => {
    try {
      const user = users.find((u) => u.uuid === uuid);
      if (!user) return;
  
      const updatedUser = {
        ...user,
        role: selectedRoles[uuid],
      };
  
      await axios.put(`http://localhost:8080/api/User/update`, updatedUser);
      alert('Role updated successfully!');
    } catch (error) {
      console.error('Error updating role:', error);
      alert(error);
    }
  };

  if (loading) {
    return <div>Loading users...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div>
      <h1>All Users</h1>
      <table>
        <thead>
          <tr>
            <th>Email</th>
            <th>Role</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.uuid}>
              <td>{user.log_in_info}</td>
              <td>
                <select
                  value={selectedRoles[user.uuid]}
                  onChange={(e) => handleRoleChange(user.uuid, e.target.value)}
                >
                  <option value="unverified">Unverified</option>
                  <option value="verified">Verified</option>
                  <option value="admin">Admin</option>
                </select>
              </td>
              <td>
                <button onClick={() => updateRole(user.uuid)}>Update Role</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default RoleManagement;
