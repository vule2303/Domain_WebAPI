using Domain_WebAPI.Data;
using Domain_WebAPI.Model.DTO;
using Domain_WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain_WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPublisherController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublisherReponsitory _publisherReponsitory;

        public IPublisherController(AppDbContext context, IPublisherReponsitory publisherReponsitory)
        {
            _context = context;
            _publisherReponsitory = publisherReponsitory;
        }

        //GET ALL PUBLISHER
        [HttpGet("get-all-publisher")]
        public IActionResult GetAll()
        {
            var allPublishers = _publisherReponsitory.GetAllPublisher();
            return Ok(allPublishers);
        }

        //GET PUBLISHER BY ID
        [HttpGet ("get-publisher-by-id")]
        public IActionResult GetById(int id)
        {
            var PublisherID = _publisherReponsitory.GetPublisherById(id);
            return Ok(PublisherID);
        }

        //ADD PUBLISHER
        [HttpPost("add-publisher")]
        public IActionResult AddPublisherById(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var addPublisher = _publisherReponsitory.AddPublisher(addPublisherRequestDTO);
            return Ok(addPublisher);
        }

        //UPDATE PUBLISHER
        [HttpPut("update-publisher-by-id/{id}")]
        public IActionResult UpdatePublisherById(int id, [FromBody] PublisherNoIdDTO updatePublisher)
        {
            var updatePublishe = _publisherReponsitory.UpdatePublisherById(id, updatePublisher);
            return Ok(updatePublisher);
        }

        //DETELE PUBLISHER
        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            var publisherDelete = _publisherReponsitory.DeletePublisherById(id);
            return Ok(publisherDelete);
        }

    }
}
