import React from "react";
import { Box, Modal, Typography, Button, IconButton } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
import RemoveIcon from "@mui/icons-material/Remove";

import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import { closeModal } from "../redux/reducers/modalReducer";
import {
  addMoreOneItem,
  decreaseOneItem,
  deleteCartItem,
  clearCart,
} from "../redux/reducers/cartReducer";
import { ModalProps } from "../types/ModalProps";
import { CartItem } from "../types/CartItem";
import { CartType } from "../types/CartType";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

const Cart = (props: ModalProps) => {
  const { isOpen } = props;
  const { items, totalSum, totalProducts }: CartType = useAppSelector(
    (state) => state.cartReducer
  );
  const dispatch = useAppDispatch();
  if (!isOpen) {
    return null;
  }
  const handleCloseModal = () => {
    dispatch(closeModal());
  };
  const handleClearCart = () => {
    dispatch(clearCart());
  };
  const handleDeleteCartItem = (cartId: string) => {
    dispatch(deleteCartItem(cartId));
  };
  const handleAddMoreOneItem = (cartId: string) => {
    dispatch(addMoreOneItem(cartId));
  };
  const handleDecreaseOneItem = (cartId: string) => {
    dispatch(decreaseOneItem(cartId));
  };

  return (
    <div>
      <Modal
        open={isOpen}
        onClose={handleCloseModal}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Shopping cart
          </Typography>
          {props.children}
          {items.length === 0 ? (
            <Typography variant="body1">
              Your shopping cart is empty.
            </Typography>
          ) : (
            <>
              {items.map((item: CartItem) => (
                <Box
                  key={item.cartId}
                  sx={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "space-between",
                    mb: 2,
                  }}
                >
                  <Box sx={{ display: "flex", alignItems: "center" }}>
                    <Typography variant="body1" sx={{ mr: 2 }}>
                      Product: {item.productName}
                    </Typography>
                    <Typography variant="body1" sx={{ mr: 2 }}>
                      Price: {item.price}
                    </Typography>
                    <Typography variant="body1" sx={{ mr: 2 }}>
                      Quantity: {item.total}
                    </Typography>
                  </Box>
                  <Box>
                    <IconButton
                      size="small"
                      onClick={() => handleDeleteCartItem(item.cartId)}
                    >
                      <DeleteIcon />
                    </IconButton>
                    <IconButton
                      size="small"
                      onClick={() => handleAddMoreOneItem(item.cartId)}
                    >
                      <AddIcon />
                    </IconButton>
                    <IconButton
                      size="small"
                      onClick={() => handleDecreaseOneItem(item.cartId)}
                    >
                      <RemoveIcon />
                    </IconButton>
                  </Box>
                </Box>
              ))}
              <Typography variant="body1">
                Total Products in Your Cart: {totalProducts}
              </Typography>
              <Typography variant="body1">
                Total Sum of Your Cart: {totalSum}
              </Typography>
              <Button variant="contained" onClick={handleClearCart}>
                Clear
              </Button>
            </>
          )}
        </Box>
      </Modal>
    </div>
  );
};

export default Cart;
