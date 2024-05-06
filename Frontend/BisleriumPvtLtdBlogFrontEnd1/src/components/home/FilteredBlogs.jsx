import { Select } from "@mantine/core";
import React, { useState } from "react";
import EachBlogCard from "../global/EachBlogCard";

const FilteredBlogs = () => {
  const [selectedFilter, setselectedFilter] = useState("All");
  const [blogs, setblogs] = useState([]);

  return (
    <div className="d-flex flex-column gap-4">
      <div className="d-flex justify-content-start">
        <Select
          label=""
          size="sm"
          placeholder="Pick a filter"
          value={selectedFilter}
          data={["All", "Popluarity", "Recent", "Random"]}
          onChange={(value) => setselectedFilter(value)}
        />
      </div>

      <div className="w-75 d-flex flex-column gap-5">
        <EachBlogCard />
        <EachBlogCard />
      </div>
    </div>
  );
};

export default FilteredBlogs;
