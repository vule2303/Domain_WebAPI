using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;

namespace Domain_WebAPI.Repositories
{
    public interface IPublisherReponsitory
    {
        List<PublisherDTO> GetAllPublisher();
        PublisherNoIdDTO GetPublisherById(int id);

        AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO updatePublisherRequestDTO);

        Publisher? DeletePublisherById(int id);


    }
}
