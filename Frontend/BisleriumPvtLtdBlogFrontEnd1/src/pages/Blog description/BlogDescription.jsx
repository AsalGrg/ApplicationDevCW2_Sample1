import React, { useEffect, useState } from "react";
import BlogIntroduction from "../../components/Blog description/BlogIntroduction";
import BlogBody from "../../components/Blog description/BlogBody";
import CommentsSection from "../../components/Blog description/CommentsSection";
import { useParams } from "react-router";
import { get_blog_details } from "../../services/getBlogDetails";

const BlogDescription = () => {
  const { id } = useParams();
  const [blogDescription, setblogDescription] = useState(null);

  useEffect(() => {
    async function getBlogDescription() {
      const res = await get_blog_details(id);
      if (res.ok) {
        const data = await res.json();
        console.log(data);
        setblogDescription(data);
      }
    }
    getBlogDescription();
  }, []);

  return (
    <div className="d-flex justify-content-center w-100">
      {blogDescription !== null ? (
        <div
          className="w-75"
          style={{
            minHeight: "100vh",
          }}
        >
          <BlogIntroduction blogDescription={blogDescription}/>
          <BlogBody blogDescription={blogDescription}/>
          <CommentsSection blogDescription={blogDescription}/>
        </div>
      ) : null}
    </div>
  );
};

export default BlogDescription;
