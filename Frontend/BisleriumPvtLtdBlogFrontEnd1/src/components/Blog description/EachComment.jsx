import { Avatar, Text } from "@mantine/core";
import React from "react";
import UpvoteBtn from "../global/UpvoteBtn";
import DownvoteBtn from "../global/DownvoteBtn";

const EachComment = () => {
  return (
    <div>
      <div className="d-flex gap-2 align-items-center">
        <Avatar size={"md"} />
        <Text fw={500}>Asal Gurung</Text>
      </div>

      <Text className="mt-2">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Eaque in
        adipisci quia delectus assumenda cumque reiciendis quas sapiente ipsam,
        rerum quaerat officia id aliquid mollitia! Minima possimus alias veniam
        facilis.
      </Text>

      <div className="d-flex gap-3 mt-2">
        <UpvoteBtn/>
        <DownvoteBtn/>
      </div>
    </div>
  );
};

export default EachComment;
