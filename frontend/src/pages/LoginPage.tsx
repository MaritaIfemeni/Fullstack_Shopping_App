import React from 'react'
import axios from 'axios';
import { useState } from 'react';

interface UserCredentials {
    email: string;
    password: string;
  }

const LoginPage = () => {
    const [credentials, setCredentials] = useState<UserCredentials>({
        email: '',
        password: '',
      });
    
      const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setCredentials({ ...credentials, [name]: value });
      };
    
      const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        try {
          const response = await axios.post('http://localhost:5292/api/v1/auth', credentials);
          console.log(response.data);
          // handle successful login
        } catch (error) {
          console.error(error);
          // handle login error
        }
      };
    
      return (
        <form onSubmit={handleSubmit}>
          <label>
            Email:
            <input type="email" name="email" value={credentials.email} onChange={handleInputChange} />
          </label>
          <label>
            Password:
            <input type="password" name="password" value={credentials.password} onChange={handleInputChange} />
          </label>
          <button type="submit">Log In</button>
        </form>
      );
}

export default LoginPage