import { Avatar, Divider, Text, Title } from "@mantine/core";
import React from "react";
import UpvoteBtn from "../global/UpvoteBtn";
import DownvoteBtn from "../global/DownvoteBtn";

const BlogIntroduction = () => {
  return (
    <div className="mt-4 d-flex flex-column gap-4">
      <Title order={1} fw={"700"}>
        Blog Title
      </Title>

      <Divider />
      <div className="d-flex justify-content-between">
        <div className="d-flex align-items-center gap-3">
          <Avatar size={"lg"} />
          <div>
            <Text fw={500}>Asal Gurung</Text>
            <Text c={"dimmed"}>May 20, 2033</Text>
          </div>
        </div>

        <div className="d-flex gap-3">
          <UpvoteBtn />
          <DownvoteBtn />
        </div>
      </div>
      <Divider />
    </div>
  );
};

export default BlogIntroduction;
