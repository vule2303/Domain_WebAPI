using Domain_WebAPI.Data;
using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;
using System.Diagnostics.CodeAnalysis;

namespace Domain_WebAPI.Repositories
{
    public class SQLPublisherReponsitory : IPublisherReponsitory
    {
        private readonly AppDbContext _appDbContext;

        public SQLPublisherReponsitory (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var publisherDomainModel = new Publisher
            {
                Name = addPublisherRequestDTO.Name,
            };
            //Use Domain Model to create Book
            _appDbContext.Publishers.Add(publisherDomainModel);
            _appDbContext.SaveChanges();
            return addPublisherRequestDTO;
               
        }

        public Publisher? DeletePublisherById(int id)
        {
            var publisherDomain = _appDbContext.Publishers.FirstOrDefault(x => x.Id == id);
            if(publisherDomain != null)
            {
                _appDbContext.Publishers.Remove(publisherDomain);
                _appDbContext.SaveChanges ();
            }
            return null;
        }

        public List<PublisherDTO> GetAllPublisher()
        {
            //Get Data Form Database -Domain Model
            var allPublisherDomain = _appDbContext.Publishers.ToList();

            //Map domain model to DTOs
            var allPublisherDTO = new List<PublisherDTO>();
            foreach (var publisherDomain in allPublisherDomain)
            {
                allPublisherDTO.Add(new PublisherDTO()
                {
                    Id = publisherDomain.Id,
                    Name = publisherDomain.Name
                });
            }
            return allPublisherDTO;
        }

        public PublisherNoIdDTO GetPublisherById(int id)
        {
            //get book Domain model from Db
            var publisherWithIdDomain = _appDbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherWithIdDomain != null)
            {
                var publisherNoIdDTO = new PublisherNoIdDTO
                {
                    Name = publisherWithIdDomain.Name,
                };
                return publisherNoIdDTO;
            }
            return null;
            
        }

        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO updatePublisherRequestDTO)
        {
            var publisherDomain = _appDbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                publisherDomain.Name = updatePublisherRequestDTO.Name;
                _appDbContext.SaveChanges();
            }
            return null;

                
            
        }
    }
}
