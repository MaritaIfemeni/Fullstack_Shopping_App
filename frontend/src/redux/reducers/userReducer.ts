import React from "react";
import {
  createAsyncThunk,
  createSlice,
  isAction,
  PayloadAction,
} from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

import {
  fetchAllUsersApi,
  fetchUserByIdApi,
  updateUserApi,
  deleteUserApi,
  createNewUserApi,
} from "../../api/userApi";
import { User } from "../../types/User";
import { UserReducer } from "../../types/UserReducer";
import { NewUser } from "../../types/NewUser";
import { UserCredential } from "../../types/UserCredentials";
import { UpdateUser } from "../../types/UpdateUser";
import { number } from "yup";

const initialState: UserReducer = {
  users: [],
  currentUser: null,
  userResponse: {
    id: "",
    firstName: "",
    lastName: "",
    username: "",
    address: "",
    city: "",
    postcode: "",
    phone: "",
    email: "",
    userRole: "",
    avatar: "",
  },
  loading: false,
  error: "",
};

export const fetchAllUsers = createAsyncThunk(
  "user/fetchAllUsers",
  async () => {
    return await fetchAllUsersApi();
  }
);

export const createNewUser = createAsyncThunk(
  "user/createNewUser",
  async (user: NewUser) => {
    return await createNewUserApi(user);
  }
);

export const fetchUserById = createAsyncThunk(
  "user/fetchUserById",
  async (id: string) => {
    return await fetchUserByIdApi(id);
  }
);

export const updateUser = createAsyncThunk(
  "user/updateUser",
  async (user: UpdateUser) => {
    return await updateUserApi(user);
  }
);

export const deleteUser = createAsyncThunk(
  "user/deleteUser",
  async (id: string) => {
    return await deleteUserApi(id);
  }
);


export const login = createAsyncThunk(
  "user/login",
  async ({ email, password }: UserCredential, { dispatch }) => {
    try {
      const result = await axios.post<{ access_token: string }>(
        "https://api.escuelajs.co/api/v1/auth/login",
        { email, password }
      );
      localStorage.setItem("token", result.data.access_token);
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);




const usersReducer = usersSlice.reducer;
export const { cleanUpUserReducer, setUserResponse } = usersSlice.actions;
export default usersReducer;
