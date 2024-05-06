import { Text } from "@mantine/core";
import React, { useState } from "react";
import { BiUpvote } from "react-icons/bi";

const UpvoteBtn = () => {
  const [isActive, setisActive] = useState(false);

  function handleActiveStatus(){
    setisActive(prev=>!prev);
  }
  return (
    <div
      className="d-flex cursor-pointer"
      style={{
        fontSize: "25px",
      }}
    >
      {isActive ? <BiUpvote className="text-primary" 
      onClick={handleActiveStatus}
      /> : <BiUpvote 
      onClick={handleActiveStatus}
      />}
      <Text>10</Text>
    </div>
  );
};

export default UpvoteBtn;
