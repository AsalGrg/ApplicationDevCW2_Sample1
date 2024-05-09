import { Text } from '@mantine/core';
import React, { useState } from 'react'
import { BiDownvote } from "react-icons/bi";

const DownvoteBtn = ({reactionDetails, noOfDownvotes, handleReaction}) => {

    function handleActiveStatus(){
      handleReaction("Downvote")
    }
  
    return (
      <div
        className="d-flex cursor-pointer"
        style={{
          fontSize: "25px",
        }}
      >
        {reactionDetails.ReactionName==="Downvote"?  <BiDownvote className="text-primary" 
        onClick={handleActiveStatus}
        /> : <BiDownvote
        onClick={handleActiveStatus}
        />}
        <Text>{noOfDownvotes}</Text>
      </div>
    );
  };
  

export default DownvoteBtn