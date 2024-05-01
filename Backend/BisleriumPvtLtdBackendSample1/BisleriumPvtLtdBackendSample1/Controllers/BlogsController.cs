using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Blog;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BisleriumPvtLtdBackendSample1.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public IActionResult CreateBlog([FromForm] AddBlogRequest addBlogRequest)
        {
            if (addBlogRequest.UserId== null) { BadRequest(); }
            return Ok(_blogService.AddNewBlog(addBlogRequest));
        }

        [HttpPut]
        [Route("/{id}")]
        public IActionResult EditBlog([FromRoute] Guid id,[FromForm] AddBlogRequest addBlogRequest)
        {
            BlogDetails updatedDetails=  _blogService.EditBlog(addBlogRequest, id);

            if (updatedDetails==null)
            {
                System.Diagnostics.Debug.WriteLine("here");
                BadRequest();
            }
            return Ok(updatedDetails);
        }

        [HttpGet]
        [Route("/{id}")]
        public IActionResult GetBlogDetails([FromRoute] Guid id, [FromQuery] Guid? userId)
        {
            BlogDetails deletedBlog = _blogService.GetBlogDetails(id, userId);
            if (deletedBlog == null)
            {
                return BadRequest();
            }
            return Ok(deletedBlog);
        }


        [HttpDelete]
        [Route("/{id}/{userId}")]
        public IActionResult DeleteBlog([FromRoute] Guid id, [FromRoute] Guid userId)
        {
            BlogDetails deletedBlog = _blogService.DeleteBlog(id,userId);
            if (deletedBlog == null)
            {
                return BadRequest();
            }
            return Ok(deletedBlog);
        }


        [HttpPost]
        [Route("/addReaction")]
        public IActionResult AddBlogReaction([FromBody] AddReactionDto addReactionDto)
        {
            EachNotificationDetails eachNotification = _blogService.AddBlogReaction(addReactionDto);

            if (eachNotification == null)
            {
                return BadRequest();
            }
            return Ok(eachNotification);
        }

        [HttpDelete]
        [Route("/deleteReaction/{reactionId}")]
        public IActionResult DeleteBlogReaction([FromRoute] Guid reactionId)
        {
            EachNotificationDetails eachNotification= _blogService.DeleteBlogReaction(reactionId);
            if (eachNotification == null)
            {
                return BadRequest();
            }
            return Ok(eachNotification);
        }

        [HttpPut]
        [Route("/updateReaction/{reactionId}")]
        public IActionResult UpdateBlogReaction([FromRoute] Guid reactionId, [FromBody] AddReactionDto addReactionDto)
        {
            EachNotificationDetails eachNotification = _blogService.UpdateBlogReaction(reactionId, addReactionDto);
            if (eachNotification == null)
            {
                return BadRequest();
            }
            return Ok(eachNotification);
        }

    }
}
