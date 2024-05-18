using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Comment;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BisleriumPvtLtdBackendSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        [HttpPost]
        [Route("/addComment")]
        [Authorize]
        public IActionResult PostComment([FromBody] AddCommentRequestDto addCommentRequest)
        {
            EachCommentDetail commentResponse = _commentService.AddComment(addCommentRequest);

            if (commentResponse == null)
            {
                return BadRequest();
            }
            return Ok(commentResponse);
        }


        [HttpPost]
        [Route("/editComment")]
        [Authorize]
        public IActionResult EditCommnet([FromBody] AddCommentRequestDto addCommentRequest)
        {
            EachCommentDetail commentResponse = _commentService.EditComment(addCommentRequest);

            if (commentResponse == null)
            {
                return BadRequest();
            }
            return Ok(commentResponse);
        }

        [HttpDelete]
        [Route("/deleteComment/{commentId}")]
        [Authorize]
        public IActionResult DeleteComment([FromRoute] Guid commentId)
        {
            string commentResponse = _commentService.DeleteComment(commentId);

            if (commentResponse == null)
            {
                return BadRequest();
            }
            return Ok(commentResponse);
        }

        [HttpPost]
        [Route("/addCommentReaction")]
        [Authorize]
        public IActionResult AddCommentReaction([FromBody] AddCommentReactionDto addReactionDto)
        {
            UserCommentReactionDetail eachNotification = _commentService.AddCommentReaction(addReactionDto);

            if (eachNotification == null)
            {
                return BadRequest();
            }
            return Ok(eachNotification);
        }

        [HttpDelete]
        [Route("/deleteCommentReaction/{reactionId}")]
        [Authorize]
        public IActionResult DeleteCommentReaction([FromRoute] Guid reactionId)
        {
            EachNotificationDetails eachNotification = _commentService.DeleteCommentReaction(reactionId);
            if (eachNotification == null)
            {
                return BadRequest();
            }
            return Ok(eachNotification);
        }

        [HttpPut]
        [Route("/updateCommentReaction/{reactionId}")]
        [Authorize]
        public IActionResult UpdateCommentReaction([FromRoute] Guid reactionId, [FromBody] AddCommentReactionDto addReactionDto)
        {
            UserCommentReactionDetail eachNotification = _commentService.UpdateCommentReaction(reactionId, addReactionDto);
            if (eachNotification == null)
            {
                return BadRequest();
            }
            return Ok(eachNotification);
        }
    }
}
