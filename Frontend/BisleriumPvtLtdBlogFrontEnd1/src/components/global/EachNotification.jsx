import { Avatar, Text } from "@mantine/core";
import React from "react";
import formatDate from "../../formatDate";

const EachNotification = ({notification}) => {
  return (
    <div className="d-flex justify-content-between align-items-center">
      <Avatar size={"md"} />
      <div>
        <Text fw={"500"}>{notification.Body}</Text>
        <Text fw={"500"} size="xs" c={"dimmed"} className="text-end">
          {/* May 20, 2023 at 8:00  */}
          {formatDate(notification.AddedDate)}
        </Text>
      </div>
    </div>
  );
};

export default EachNotification;
