import React, { useState, useEffect } from 'react';
import { fetchAllProductsApi } from '../api/productsApi';
import { Product } from '../types/Product';

const ProductPage = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [pageNumber, setPageNumber] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(5);
  const [search, setSearch] = useState<string>('');

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await fetchAllProductsApi(pageNumber, pageSize, search);
        setProducts(response);
      } catch (error) {
        console.error(error);
      }
    };
    fetchProducts();
  }, [pageNumber, pageSize, search]);

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(event.target.value);
  };

  const handlePageNumberChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPageNumber(parseInt(event.target.value));
  };

  const handlePageSizeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPageSize(parseInt(event.target.value));
  };

  return (
    <div>
      <div>
        <input type="text" value={search} onChange={handleSearchChange} />
        <button onClick={() => setSearch('')}>Clear</button>
      </div>
      <div>
        <label>Page Number:</label>
        <input type="number" value={pageNumber} onChange={handlePageNumberChange} />
      </div>
      <div>
        <label>Page Size:</label>
        <input type="number" value={pageSize} onChange={handlePageSizeChange} />
      </div>
      <ul>
        {products.map((product) => (
          <li key={product.id}>
            <div>{product.productName}</div>
            <div>{product.price}</div>
            <div>
            {product.productImages.map((image) => (
                <img height={150} width={100} key={image.id} src={image.link} alt="Product Image" />
              ))}
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ProductPage;