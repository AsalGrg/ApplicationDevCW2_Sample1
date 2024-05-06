import { TextInput, Title } from "@mantine/core";
import React from "react";
import EachComment from "./EachComment";
import { IoMdSend } from "react-icons/io";


const CommentsSection = () => {
  return (
    <section className="mt-5">
      <Title order={3} fw={600}>
        Comments (10)
      </Title>

      <TextInput placeholder="What's on your thought?" 
      className="mt-3"
      rightSection={
        <div
        className="cursor-pointer"
        >
          <IoMdSend/>
        </div>
      }/>
      <div className="d-flex flex-column gap-5 mt-3">
        <EachComment/>

        <EachComment/>

        <EachComment/>
      </div>

    </section>
  );
};

export default CommentsSection;
