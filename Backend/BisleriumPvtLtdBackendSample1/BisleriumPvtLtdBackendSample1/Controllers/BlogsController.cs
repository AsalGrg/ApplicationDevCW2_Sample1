using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Blog;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Blogger")]
        public async Task<IActionResult> CreateBlog([FromForm] AddBlogRequest addBlogRequest)
        {
            if (addBlogRequest.CoverImage.Length > 3 * 1024 * 1024)//3MB
                return BadRequest("Image file above 3MB");

            return Ok(await _blogService.AddNewBlog(addBlogRequest));
        }

        [HttpPut]
        [Route("/{id}")]
        [Authorize(Roles ="Blogger")]
        public async Task<IActionResult> EditBlog([FromRoute] Guid id,[FromForm] AddBlogRequest addBlogRequest)
        {
            BlogDetails updatedDetails=  await _blogService.EditBlog(addBlogRequest, id);

            if (updatedDetails==null)
            {
                System.Diagnostics.Debug.WriteLine("here");
                BadRequest();
            }
            return Ok(updatedDetails);
        }

        [HttpGet]
        [Route("/getBlogsFilter/{filter}")]
        public IActionResult GetFilterBlogs([FromRoute] string filter)
        {
            return Ok(_blogService.GetAllFilterBlogs(filter));
        }

        [HttpGet]
        [Route("/{id}")]
        [AllowAnonymous]
        [Authorize]
        public IActionResult GetBlogDetails([FromRoute] Guid id)
        {
            BlogDetails deletedBlog = _blogService.GetBlogDetails(id);
            if (deletedBlog == null)
            {
                return BadRequest();
            }
            return Ok(deletedBlog);
        }


        [HttpDelete]
        [Route("/{id}")]
        [Authorize]
        public IActionResult DeleteBlog([FromRoute] Guid id)
        {
            BlogDetails deletedBlog = _blogService.DeleteBlog(id);
            if (deletedBlog == null)
            {
                return BadRequest();
            }
            return Ok(deletedBlog);
        }


        [HttpPost]
        [Route("/addReaction")]
        [Authorize]
        public IActionResult AddBlogReaction([FromBody] AddReactionDto addReactionDto)
        {
            UserBlogReactionDetail eachNotification = _blogService.AddBlogReaction(addReactionDto);

            if (eachNotification == null)
            {
                return BadRequest();
            }
            return Ok(eachNotification);
        }

        [HttpDelete]
        [Route("/deleteReaction/{reactionId}")]
        [Authorize]
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
        [Authorize]
        public IActionResult UpdateBlogReaction([FromRoute] Guid reactionId, [FromBody] AddReactionDto addReactionDto)
        {
            UserBlogReactionDetail eachNotification = _blogService.UpdateBlogReaction(reactionId, addReactionDto);
            if (eachNotification == null)
            {
                return BadRequest();
            }
            return Ok(eachNotification);
        }


    }
}
