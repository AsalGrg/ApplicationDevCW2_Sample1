import { Select } from "@mantine/core";
import React, { useEffect, useState } from "react";
import EachBlogCard from "../global/EachBlogCard";
import { get_all_blogs } from "../../services/getAllBlogs";

const FilteredBlogs = () => {
  const [selectedFilter, setselectedFilter] = useState("Random");
  const [blogs, setblogs] = useState([]);

  useEffect(() => {
    
    async function getBlogs(){

      const res= await get_all_blogs(selectedFilter);
      const data = await res.json();

      if(res.ok){
        setblogs(data);
      }
    }
    getBlogs();
  }, [selectedFilter])
  
  return (
    <div className="d-flex flex-column gap-4">
      <div className="d-flex justify-content-start">
        <Select
          label=""
          size="sm"
          placeholder="Pick a filter"
          value={selectedFilter}
          data={["Popluarity", "Recency", "Random"]}
          onChange={(value) => setselectedFilter(value)}
        />
      </div>

      <div className="w-75 d-flex flex-column gap-5">
        {blogs.map(each=>(
          <EachBlogCard blog={each}/>
        ))}
      </div>
    </div>
  );
};

export default FilteredBlogs;
