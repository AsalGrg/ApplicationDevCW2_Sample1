import { Avatar, Text } from "@mantine/core";
import React from "react";

const EachNotification = () => {
  return (
    <div className="d-flex justify-content-start gap-4 align-items-center">
      <Avatar size={"md"} />
      <div>
        <Text fw={"500"}>Asal Commented on you post</Text>
        <Text fw={"500"} size="xs" c={"dimmed"} className="text-end">
          May 20, 2023 at 8:00 am
        </Text>
      </div>
    </div>
  );
};

export default EachNotification;
