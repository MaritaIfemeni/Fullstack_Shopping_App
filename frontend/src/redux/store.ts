import { configureStore } from "@reduxjs/toolkit";

import productsReducer from "./reducers/productsReducer";
import cartReducer from "./reducers/cartReducer";
import {
  saveCartStateToLocalStorage,
  loadCartStateFromLocalStorage,
} from "../utils/localStorageUtils";
import modalReducer from "./reducers/modalReducer";

const persistedCartState = loadCartStateFromLocalStorage();

const store = configureStore({
  reducer: {
    productsReducer,
    cartReducer,
    modalReducer,
  },
  preloadedState: {
    cartReducer: persistedCartState,
  },
});

store.subscribe(() => {
  const { cartReducer } = store.getState();
  saveCartStateToLocalStorage(cartReducer);
});

export type GlobalState = ReturnType<typeof store.getState>; 
export type AppDispatch = typeof store.dispatch;
export default store;
