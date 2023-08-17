import React from "react";
import { createBrowserRouter } from "react-router-dom";

import LandingPage from "../pages/LandingPage";
import HomePage from "../pages/HomePage";
import PageNotFound from "../pages/PageNotFound";
import LoginPage from "../pages/LoginPage";
import ProductPage from "../pages/ProductPage";
import SingleProductPage from "../pages/SingleProductPage";



const routes: any = createBrowserRouter([
    {
      path: "/",
      element: <LandingPage />,
      errorElement: <PageNotFound />,
      children: [
        {
          path: "/",
          element: <HomePage />,
        },
        {
          path: "/product/:id",
          element: <SingleProductPage />,
        },
        {
          path: "/login",
          element: <LoginPage />,
        },
        {
          path: "/products",
          element: <ProductPage/>,
        },
      ],
    },
  ]);
  
  export default routes;
  