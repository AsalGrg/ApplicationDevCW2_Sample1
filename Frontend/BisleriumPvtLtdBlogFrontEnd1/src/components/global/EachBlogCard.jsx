import { Avatar, Text } from "@mantine/core";
import React from "react";
import { useNavigate } from "react-router";
import formatDate from "../../formatDate";

const EachBlogCard = ({ blog }) => {
  const navigate = useNavigate();

  const authorDetails = blog.AuthorDetails;

  return (
    <section
      className="rounded ps-2 row justify-content-between cursor-pointer"
      onClick={() => navigate(`/blog/${blog.Id}`)}
      style={{
        minHeight: "130px",
        maxHeight: "180px",
      }}
    >
      <div className="col-8 d-flex flex-column justify-content-center align-items-start gap-2">
        <div className="d-flex align-items-center gap-2">
          <Avatar size={"md"} />
          <Text size="sm" fw={"500"}>
            {authorDetails.Username}
          </Text>
        </div>
        <Text size="xl" fw={"700"} lineClamp={2}>
          {blog.Title}
        </Text>

        <Text size="md" fw={"400"} lineClamp={2}>
          {blog.Body}
        </Text>

        <div className="mt-1">
          <Text size="sm" fw={"400"} lineClamp={2}>
            {formatDate(blog.CreatedDate)}
          </Text>
        </div>
      </div>
      <div className="col-4">
        <img
          src={blog.CoverImage}
          className="w-100 img-fluid"
          style={{
            objectFit: "cover",
            height: "180px",
          }}
        />
      </div>
    </section>
  );
};

export default EachBlogCard;
