import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { styled } from "@mui/material/styles";
import {
  Container,
  InputBase,
  Select,
  MenuItem,
  Button,
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  List,
  ListItem,
  IconButton,
  Typography,
  TextField,
  Autocomplete,
  Card,
  CardMedia,
  Grid,
  CardContent,
  CardActions,
  FormControlLabel,
  Checkbox,
} from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import SortIcon from "@mui/icons-material/Sort";

import {
  fetchAllProducts,
  filterProductsByPrice,
} from "../redux/reducers/productsReducer";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import { Product } from "../types/Product";
import useDebounce from "../hooks/useDebounce";
import { addCartItem } from "../redux/reducers/cartReducer";


const ProductPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState<string>("");
  const debouncedSearch = useDebounce<string>(search, 1000);
  const [page, setPage] = useState(1);
  const [order, setOrder] = useState<string>("UpdatedAt");
  const [descending, setDescending] = useState<boolean>(true);
  const [priceFilter, setPriceFilter] = useState<number>(0);
  const products = useAppSelector((state) => state.productsReducer.products);

  /// fix this pagination if time!
  useEffect(() => {
    dispatch(
      fetchAllProducts({
        pageNumber: 1,
        pageSize: 6,
        search: debouncedSearch,
        order,
        descending,
      })
    );
  }, [dispatch, debouncedSearch, order, descending]);

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(event.target.value);
  };

  const handleOrderChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setOrder(event.target.value);
  };

  const handleDescendingChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setDescending(event.target.checked);
  };

  const handleFilterByPrice = () => {
    dispatch(filterProductsByPrice(priceFilter));
  };

  const handleAddToCart = (product: Product) => {
    dispatch(addCartItem(product));
  };

  const handleNextPage = () => {
    setPage(page + 1);
  };
  const handlePrevPage = () => {
    if (page > 1) {
      setPage(page - 1);
    }
  };

  const handlePageSizeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(
      fetchAllProducts({
        pageNumber: 1,
        pageSize: parseInt(event.target.value),
        search: debouncedSearch,
        order,
        descending,
      })
    );
  };

  const handlePageNumberChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPage(parseInt(event.target.value));
  };

  return (
    <Container>
      <Typography variant="h4" color="primary" sx={{ margin: "0.5em 0" }}>
        Here you can start browsing the products!
      </Typography>
      <TextField
        label="Search"
        value={search}
        onChange={handleSearchChange}
        variant="outlined"
        size="small"
        fullWidth
      />
      <Typography variant="h5" sx={{ margin: "1 1 2em 2em" }}>
        <Select
          value={priceFilter}
          onChange={(e) => setPriceFilter(Number(e.target.value))}
          sx={{ minWidth: "15%", height: "2.5em" }}
        >
          <MenuItem value={0}>Low to High</MenuItem>
          <MenuItem value={1}>High to Low</MenuItem>
        </Select>
        <Button
          variant="contained"
          onClick={handleFilterByPrice}
          sx={{ marginLeft: "1em" }}
        >
          <SortIcon sx={{ marginRight: "0.5em" }} /> Sort by Price
        </Button>
      </Typography>

      <Typography variant="h5" sx={{ margin: "1 1 1em 1em" }}>
        <div>
          <FormControlLabel
            control={
              <Checkbox
                checked={descending}
                onChange={handleDescendingChange}
              />
            }
            label="Newest first"
          />
        </div>
      </Typography>

      <Grid container spacing={4}>
        {products.map((product) => (
          <Grid item key={product.id} xs={12} sm={6} md={4}>
            <Card
              sx={{ height: "100%", display: "flex", flexDirection: "column" }}
            >
              <CardMedia
                component="div"
                sx={{
                  pt: "56.25%",
                }}
                image={product.productImages[0].link}
              />
              <CardContent sx={{ flexGrow: 1 }}>
                <Typography gutterBottom variant="h5" component="h2">
                  {product.productName}
                </Typography>
                <Typography gutterBottom variant="h6">
                  Price: {product.price} £
                </Typography>
                <Typography>{product.description}</Typography>
              </CardContent>
              <CardActions>
                <Link to={`/product/${product.id}`}>
                  <Button size="small">Details</Button>
                </Link>
                <Button size="small" onClick={() => handleAddToCart(product)}>
                  Add to cart
                </Button>
              </CardActions>
            </Card>
          </Grid>
        ))}
      </Grid>
      <Button onClick={handlePrevPage} disabled={page === 1}>
        Prev
      </Button>
      <Button onClick={handleNextPage}>Next</Button>
      <div>
        <label>Page Number:</label>
        <input type="number" value={1} onChange={handlePageNumberChange} />
      </div>
      <div>
        <label>Page Size:</label>
        <input type="number" value={5} onChange={handlePageSizeChange} />
      </div>
    </Container>
  );
};

export default ProductPage;
