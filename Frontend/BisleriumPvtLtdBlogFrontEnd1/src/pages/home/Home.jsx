import React, { useState } from "react";
import IntroCoverImage from "../../components/home/IntroCoverImage";
import FilteredBlogs from "../../components/home/FilteredBlogs";

const Home = () => {

  return (
    <section>
      <IntroCoverImage />

      <div className="px-5 mt-3">
        <FilteredBlogs />
      </div>
    </section>
  );
};

export default Home;
