import React, { useState } from "react";

import useAppDispatch from "../../hooks/useAppDispatch";
import {
  createNewProduct
} from "../../redux/reducers/productsReducer";

const AddNewProduct = () => {
  const dispatch = useAppDispatch();
  const [productName, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState(0);
  const [stock, setStock] = useState(0);
  const [productImages, setProductImages] = useState<{ link: string }[]>([]);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const response = await dispatch(
        createNewProduct({ productName, price, stock, description, productImages })
      );

      if (response.payload) {
        const product = response.payload;
        alert("Product created successfully!");
      } else {
        alert("Failed to create product");
      }
    } catch (error) {
      alert("An error occurred while creating the product");
    }
  };

  return (
    <div className="add-new-product">
      <h2>Create New Product</h2>
      <div className="form-container">
        <form onSubmit={(e) => handleSubmit(e)}>
          <div className="form-group">
            <label htmlFor="title">Title:</label>
            <input
              type="text"
              id="title"
              name="title"
              value={productName}
              onChange={(e) => setTitle(e.target.value)}
            />
          </div>
          <div className="form-group">
            <label htmlFor="description">Description:</label>
            <input
              type="text"
              id="description"
              name="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
          </div>
          <div className="form-group">
            <label htmlFor="price">Price:</label>
            <input
              type="number"
              id="price"
              name="price"
              value={price}
              onChange={(e) => setPrice(Number(e.target.value))}
            />
          </div>
          <div className="form-group">
            <label htmlFor="stock">stock:</label>
            <input
              type="number"
              id="stock"
              name="stock"
              value={stock}
              onChange={(e) => setStock(Number(e.target.value))}
            />
          </div>
          <div className="form-group">
            <label htmlFor="images">Images:</label>
            <input
              type="text"
              id="images"
              name="images"
              value={productImages.join(",")}
              onChange={(e) => setProductImages(e.target.value.split(",").map((link) => ({ link })))}
            />
          </div>
          <div className="form-group">
            <button type="submit">Create New Product</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AddNewProduct;
