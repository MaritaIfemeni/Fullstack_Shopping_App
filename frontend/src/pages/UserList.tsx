import React, { useEffect, useState } from "react";
import {
  Container,
  Typography,
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Avatar,
  IconButton,
  TextField,
} from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";

import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import { fetchAllUsers } from "../redux/reducers/userReducer";

const UserList = () => {
  const dispatch = useAppDispatch();
  const users = useAppSelector((state) => state.userReducer.users);
  const [showPasswordList, setShowPasswordList] = useState(
    new Array(users.length).fill(false)
  );

  useEffect(() => {
    dispatch(fetchAllUsers());
  }, []);
  const handleShowPassword = (index: number) => {
    setShowPasswordList((prevShowPasswordList) => {
      const updatedList = [...prevShowPasswordList];
      updatedList[index] = !updatedList[index];
      return updatedList;
    });
  };

  return (
    <Container>
      <Typography variant="h4" color="primary" sx={{ margin: "0.5em 0" }}>
        List of Users
      </Typography>
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>User Name</TableCell>
              <TableCell>User Email</TableCell>
              <TableCell>User Avatar</TableCell>
              <TableCell>User Role</TableCell>
              <TableCell>Password</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {users.map((user, index) => (
              <TableRow key={user.id}>
                <TableCell>{user.firstName}</TableCell>
                <TableCell>{user.email}</TableCell>
                <TableCell>
                  <Avatar
                    sx={{ width: 100, height: 100 }}
                    src={user.avatar}
                    alt="Profile pic"
                    variant="square"
                  />
                </TableCell>
                <TableCell>{user.UserRole}</TableCell>
                <TableCell>
                  {showPasswordList[index] ? (
                    user.password
                  ) : (
                    <TextField
                      type="password"
                      value="********" // Display a masked password value
                      variant="standard"
                      disabled
                    />
                  )}
                  <IconButton onClick={() => handleShowPassword(index)}>
                    {showPasswordList[index] ? (
                      <VisibilityOff />
                    ) : (
                      <Visibility />
                    )}
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default UserList;
