import { Avatar, Button, Divider, Text, Title } from "@mantine/core";
import React, { useEffect, useState } from "react";
import UpvoteBtn from "../global/UpvoteBtn";
import DownvoteBtn from "../global/DownvoteBtn";
import { add_blog_reaction } from "../../services/addBlogReaction";
import { delete_blog_reaction } from "../../services/deleteBlogReaction";
import { useNavigate, useParams } from "react-router";
import { update_blog_reaction } from "../../services/updateBlogReaction";
import { delete_blog } from "../../services/deleteBlog";
import formatDate from "../../formatDate";

const BlogIntroduction = ({ blogDescription }) => {
  const authorDetails = blogDescription.AuthorDetails;

  const { id } = useParams();

  const navigate = useNavigate();

  const [reactionDetails, setReactionDetails] = useState({
    ReactionId: "",
    ReactionName: "",
  });

  const [upvotes, setupvotes] = useState(blogDescription.NoOfUpVotes);
  const [downvotes, setdownVotes] = useState(blogDescription.NoOfDownVotes);

  useEffect(() => {
    if (blogDescription.BlogReactionDetail !== null) {
      setReactionDetails(blogDescription.BlogReactionDetail);
    }
  }, []);

  console.log(blogDescription.BlogReactionDetail);

  async function handleReaction(reaction) {
    if (reactionDetails.ReactionName.length > 0) {
      //if same reaction, delete the reaction
      if (reaction === reactionDetails.ReactionName) {
        await deleteReaction(reactionDetails);
      } else {
        updateReaction(reaction);
      }
    } else {
      addReaction(reaction);
    }
  }

  async function addReaction(reaction) {
    const reactionData = {
      reactionType: reaction,
      blogId: id,
    };

    console.log(reactionData);

    const res = await add_blog_reaction(reactionData);

    if (res.ok) {
      console.log("doneee added");
      const data = await res.json();
      setReactionDetails(data);

      if (reaction === "Upvote") setupvotes((prev) => prev + 1);
      else if (reaction === "Downvote") setdownVotes((prev) => prev + 1);
    }
  }

  async function updateReaction(reaction) {
    const reactionData = {
      reactionType: reaction,
      blogId: id,
    };

    const res = await update_blog_reaction(
      reactionDetails.ReactionId,
      reactionData
    );
    if (res.ok) {
      const data = await res.json();
      console.log("updated");
      console.log(data);
      setReactionDetails(data);

      if (reaction === "Upvote") {
        setupvotes((prev) => prev + 1);
        setdownVotes((prev) => prev - 1);
      } else if (reaction === "Downvote") {
        setupvotes((prev) => prev - 1);
        setdownVotes((prev) => prev + 1);
      }
    }
  }

  async function deleteReaction(reaction) {
    const res = await delete_blog_reaction(reaction.ReactionId);
    if (res.ok) {
      console.log("doneee deleted");
      setReactionDetails({
        ReactionId: "",
        ReactionName: "",
      });

      if (reaction.ReactionName === "Upvote") setupvotes((prev) => prev - 1);
      else if (reaction.ReactionName === "Downvote")
        setdownVotes((prev) => prev - 1);
    }
  }

  async function deleteBlog (){
    const res = await delete_blog(id);

    if(res.ok){
      navigate('/');
    }
  }

  return (
    <div className="mt-4 d-flex flex-column gap-4">
      <div className="d-flex justify-content-between">
        <Title order={1} fw={"700"}>
          {blogDescription.Title}
        </Title>

        {blogDescription.isAuthor&&(
          <div className="d-flex gap-2">
          <Button
          onClick={()=>navigate(`/blog/edit/${blogDescription.Id}`)}
          >Edit</Button>
          <Button color="red"
          onClick={deleteBlog}
          >Delete</Button>
        </div>
        )}
        
      </div>

      <Divider />
      <div className="d-flex justify-content-between">
        <div className="d-flex align-items-center gap-3">
          <Avatar size={"lg"} />
          <div>
            <Text fw={500}>{authorDetails.Username}</Text>
            <Text c={"dimmed"}>{formatDate(blogDescription.CreatedDate)}</Text>
          </div>
        </div>

        <div className="d-flex gap-3">
          <UpvoteBtn
            noOfUpvotes={upvotes}
            handleReaction={handleReaction}
            reactionDetails={reactionDetails}
          />
          <DownvoteBtn
            noOfDownvotes={downvotes}
            handleReaction={handleReaction}
            reactionDetails={reactionDetails}
          />
        </div>
      </div>
      <Divider />
    </div>
  );
};

export default BlogIntroduction;
