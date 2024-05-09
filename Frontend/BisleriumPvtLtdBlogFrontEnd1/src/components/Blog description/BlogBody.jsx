import { Text } from "@mantine/core";
import React from "react";

const BlogBody = ({blogDescription}) => {
  return (
    <section className="mt-5 d-flex flex-column gap-4">
      <div className="d-flex justify-content-center">
        <img
          src={blogDescription.CoverImage}
          alt=""
          style={{
            height: "400px",
            objectFit: "cover",
          }}
          className=""
        />
      </div>

      <Text size="md" fw={400}>
        {blogDescription.Body}
      </Text>
    </section>
  );
};

export default BlogBody;
