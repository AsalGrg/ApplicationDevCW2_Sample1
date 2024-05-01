using BisleriumPvtLtdBackendSample1.DTOs.Comment;
using BisleriumPvtLtdBackendSample1.DTOs.Comment;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;

namespace BisleriumPvtLtdBackendSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            this._commentService= commentService;
        }

        [HttpPost]
        [Route("/addComment")]
        public IActionResult PostComment([FromBody] AddCommentRequestDto addCommentRequest)
        {
            CommentResponse commentResponse =_commentService.AddComment(addCommentRequest);

            if (commentResponse == null)
            {
                return BadRequest();
            }
            return Ok(commentResponse);
        }


        [HttpPost]
        [Route("/editComment")]
        public IActionResult EditCommnet([FromBody] AddCommentRequestDto addCommentRequest)
        {
            CommentResponse commentResponse = _commentService.EditComment(addCommentRequest);

            if (commentResponse == null)
            {
                return BadRequest();
            }
            return Ok(commentResponse);
        }
    }
}
