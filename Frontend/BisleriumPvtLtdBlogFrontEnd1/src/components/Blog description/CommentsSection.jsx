import { TextInput, Title } from "@mantine/core";
import React, { useEffect, useState } from "react";
import EachComment from "./EachComment";
import { IoMdSend } from "react-icons/io";
import { add_comment } from "../../services/addComment";
import { useParams } from "react-router";

const CommentsSection = ({ blogDescription }) => {
  const { id } = useParams();

  const [comments, setcomments] = useState([]);
  useEffect(() => {
    setcomments(blogDescription.BlogComments);
  }, []);

  const [comment, setcomment] = useState("");

  async function addComment() {
    if (comment.length > 0) {
      const commentData = {
        BlogId: id,
        CommentContent: comment,
      };
      const res = await add_comment(commentData);

      if (res.ok) {
        const data = await res.json();
        console.log(data)
        setcomments([...comments, data]);
      }
    }
  }

  return (
    <section className="mt-5">
      <Title order={3} fw={600}>
        Comments ({comments.length})
      </Title>

      <TextInput
        placeholder="What's on your thought?"
        className="mt-3"
        value={comment}
        onChange={(e)=>setcomment(e.target.value)}
        rightSection={
          <div className="cursor-pointer" onClick={addComment}>
            <IoMdSend />
          </div>
        }
      />
      <div className="d-flex flex-column gap-5 mt-3">
        {comments.map((each) => (
          <EachComment commentDescription={each} />
        ))}
      </div>
    </section>
  );
};

export default CommentsSection;
