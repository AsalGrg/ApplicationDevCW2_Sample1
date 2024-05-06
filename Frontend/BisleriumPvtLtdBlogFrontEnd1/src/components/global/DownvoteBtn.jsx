import { Text } from '@mantine/core';
import React, { useState } from 'react'
import { BiDownvote } from "react-icons/bi";

const DownvoteBtn = () => {
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
        {isActive ? <BiDownvote className="text-primary" 
        onClick={handleActiveStatus}
        /> : <BiDownvote
        onClick={handleActiveStatus}
        />}
        <Text>10</Text>
      </div>
    );
  };
  

export default DownvoteBtn