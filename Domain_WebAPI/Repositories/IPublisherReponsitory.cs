using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;

namespace Domain_WebAPI.Repositories
{
    public interface IPublisherReponsitory
    {
        List<PublisherDTO> GetAllPublisher();
        PublisherDTO GetPublisherById(int id);

        AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        AddPublisherRequestDTO UpdatePublisherById(int id, AddPublisherRequestDTO updatePublisherRequestDTO);

        Publisher? DeletePublisherById(int id);


    }
}
