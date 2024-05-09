import { Avatar, Button, Text, TextInput } from "@mantine/core";
import React, { useEffect, useState } from "react";
import UpvoteBtn from "../global/UpvoteBtn";
import DownvoteBtn from "../global/DownvoteBtn";
import { add_comment_reaction } from "../../services/addCommentReaction";
import { update_comment_reaction } from "../../services/updateCommentReaction";
import { delete_comment_reaction } from "../../services/deleteCommentReaction";
import { MdDeleteForever } from "react-icons/md";
import { FaEdit } from "react-icons/fa";
import { useParams } from "react-router";
import { edit_comment } from "../../services/edit_comment";

const EachComment = ({ commentDescription }) => {

  const {id}= useParams();

  const [reactionDetails, setReactionDetails] = useState({
    ReactionId: "",
    ReactionName: "",
  });

  const [upvotes, setupvotes] = useState(commentDescription.NoOfUpVotes);
  const [downvotes, setdownVotes] = useState(commentDescription.NoOfDownVotes);
  const [isEditing, setisEditing] = useState(false);
  const [commentDetails, setcommentDetails] = useState({
    CommentId: "",
    AuthorDetails: {Username: ''},
    CommentContent: "",
    AddedDate: "",
    NoOfUpVotes: 0,
    NoOfDownVotes: 0,
    IsAuthor: false,
    HasReacted: false,
    ReactionDetail: null,
  });

  useEffect(() => {
    setcommentDetails(commentDescription);
  }, []);

  useEffect(() => {
    if (commentDescription.ReactionDetail !== null) {
      setReactionDetails(commentDescription.ReactionDetail);
    }
  }, []);

  console.log(commentDescription.ReactionDetail);

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
      commentId: commentDetails.CommentId,
    };

    console.log(reactionData);

    const res = await add_comment_reaction(reactionData);

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
      commentId: commentDescription.CommentId,
    };

    const res = await update_comment_reaction(
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
    const res = await delete_comment_reaction(reaction.ReactionId);
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

  async function editComment(){
    const editDetails = {
      CommentId: commentDetails.CommentId,
      BlogId: id,
      CommentContent: commentDetails.CommentContent
    }

    const res = await edit_comment(editDetails);
    if(res.ok){
      const data = await res.json();

      setcommentDetails(data);
      setisEditing(false);
    }
  }
  return (
    <div>
      {!isEditing ? (
        <div className="d-flex justify-content-between">
          <div>
            <div className="d-flex gap-2 align-items-center">
              <Avatar size={"md"} />
              <Text fw={500}>{commentDetails.AuthorDetails.Username}</Text>
            </div>

            <Text className="mt-2">{commentDetails.CommentContent}</Text>

            <div className="d-flex gap-3 mt-2">
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

          {commentDetails.IsAuthor ? (
            <div className="d-flex gap-2">
              <Button variant="light" c={"green"}
              onClick={()=> setisEditing(true)}
              >
                <FaEdit style={{ fontSize: "20px" }} />
              </Button>

              <Button variant="light" c={"red"}>
                <MdDeleteForever style={{ fontSize: "20px" }} />
              </Button>
            </div>
          ) : null}
        </div>
      ) : (
        <div>
          <div className="d-flex gap-2 align-items-center">
            <Avatar size={"md"} />
            <Text fw={500}>{commentDetails.AuthorDetails.Username}</Text>
          </div>

          <TextInput className="mt-2"
          value={commentDetails.CommentContent}
          onChange={(e)=>{
            setcommentDetails({
              ...commentDetails, CommentContent: e.target.value
            })
          }}
          />

          <div className="d-flex gap-2 mt-2">
            <Button variant="light" c={"green"}
            onClick={editComment}
            >
              Done
            </Button>

            <Button variant="light" c={"red"}
            onClick={()=>setisEditing(false)}
            >
              Cancel
            </Button>
          </div>
        </div>
      )}
    </div>
  );
};

export default EachComment;
