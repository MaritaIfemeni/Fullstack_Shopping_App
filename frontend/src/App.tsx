import React from "react";
import { useState, useEffect } from "react";

interface User {
  username: string;
}

const App = () => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    const fetchUsers = async () => {
      const response = await fetch("http://localhost:5292/api/v1/users");
      const data = await response.json();
      setUsers(data);
    };

    fetchUsers();
  }, []);

  return (
    <ul>
      {users.map((user) => (
        <li key={user.username}>{user.username}</li>
      ))}
    </ul>
  );
};

export default App;
