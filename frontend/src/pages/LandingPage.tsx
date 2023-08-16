import React from "react";
import { Outlet } from "react-router-dom";

import Header from "../components/Header";
import Footer from "../components/Footer";

const LandingPage = () => {
  return (
    <div>
      <div>
        <header>
          <Header />
        </header>
        <main>
          <div>
            <Outlet />
          </div>
        </main>
      </div>
      <Footer />
    </div>
  );
};

export default LandingPage;
