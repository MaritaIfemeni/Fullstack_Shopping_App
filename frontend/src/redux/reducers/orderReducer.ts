import {
    createAsyncThunk,
    createSlice,
    isAction,
    PayloadAction,
  } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

import { CartItem } from "../../types/CartItem";
import { Order, OrderDetail } from "../../types/Order";
import { clearCart } from "./cartReducer";

const initialState: Order = {
    fullName: "",
    deliveryAddress: "",
    userId: "",
    orderDetails: [],
};

export const fetchCreateOrder = createAsyncThunk(
    "order/createOrder",
    async (order: Order, { dispatch }) => {
        const token = localStorage.getItem("token");
        const headers = {
            Authorization: `Bearer ${token}`,
        };
        const response = await axios.post<Order>(
            "http://localhost:5292/api/v1/orders",
            order,
            {
                headers: headers,
            }
        );
        dispatch(clearCart());
        return response.data;
    }
);

const orderSlice = createSlice({
    name: "order",
    initialState,
    reducers: {
        createOrder: (state, action: PayloadAction<Order>) => {
            return action.payload;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchCreateOrder.fulfilled, (state, action) => {
            return action.payload;
        });
    },
});
     
const orderReducer = orderSlice.reducer;
export const { createOrder } = orderSlice.actions;
export default orderReducer;