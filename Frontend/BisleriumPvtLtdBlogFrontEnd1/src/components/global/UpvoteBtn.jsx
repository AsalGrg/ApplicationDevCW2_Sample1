import { Text } from "@mantine/core";
import React, { useState } from "react";
import { BiUpvote } from "react-icons/bi";

const UpvoteBtn = ({reactionDetails, noOfUpvotes, handleReaction}) => {

  function handleActiveStatus(){
    handleReaction("Upvote")
  }

  console.log(reactionDetails);
  
  return (
    <div
      className="d-flex cursor-pointer"
      style={{
        fontSize: "25px",
      }}
    >
      {reactionDetails.ReactionName==="Upvote"? <BiUpvote className="text-primary" 
      onClick={handleActiveStatus}
      /> : <BiUpvote 
      onClick={handleActiveStatus}
      />}
      <Text>{noOfUpvotes}</Text>
    </div>
  );
};

export default UpvoteBtn;
