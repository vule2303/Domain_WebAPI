using Domain_WebAPI.Data;
using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;

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
            //map doamin to DTO
            var publisherDomain = new Publisher
            {
                Name = addPublisherRequestDTO.Name,
            };
            // Use domain model to add Publisher
            _appDbContext.Publishers.Add(publisherDomain);
            _appDbContext.SaveChanges();

            foreach (var id in addPublisherRequestDTO.BookIds)
            {
                var books = new Book()
                {
                    PublisherID = publisherDomain.Id
                };

                _appDbContext.Books.Add(books);
                _appDbContext.SaveChanges ();
            }
            return addPublisherRequestDTO;
        }

        public Publisher? DeletePublisherById(int id)
        {
            var delelePublisher = _appDbContext.Publishers.FirstOrDefault(x => x.Id == id);

            if (delelePublisher != null) 
            {
                _appDbContext.Publishers.Remove(delelePublisher);
                _appDbContext.SaveChanges();
            }
            return delelePublisher;
        }

        public List<PublisherDTO> GetAllPublisher()
        {
            var allPublisher = _appDbContext.Publishers.Select(publishers => new PublisherDTO()
            {
                Id = publishers.Id,
                Name = publishers.Name,
                NameBooks = publishers.Books.Select(book => book.Title).ToList()
            }).ToList();
            return allPublisher;
        }

        public PublisherDTO GetPublisherById(int id)
        {
            //get id from publisher domain
            var Publisher = _appDbContext.Publishers.Where(n => n.Id == id);
            //map domain to DTO
            var idPublisherDTO = Publisher.Select(publisherDomain => new PublisherDTO()
            {
                Id = publisherDomain.Id,
                Name = publisherDomain.Name,
                NameBooks = publisherDomain.Books.Select(n => n.Title).ToList(),
            }).FirstOrDefault();
            return idPublisherDTO;
        }

        public AddPublisherRequestDTO UpdatePublisherById(int id, AddPublisherRequestDTO updatePublisherRequestDTO)
        {
            //get id from publisher domain
            var publisherDomain = _appDbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                publisherDomain.Name = updatePublisherRequestDTO.Name;
                _appDbContext.SaveChanges();
            }

            foreach (var Idbook in updatePublisherRequestDTO.BookIds)
            {
                var book = new Book()
                {
                    PublisherID = id
                };
                _appDbContext.Books.Add(book);
                _appDbContext.SaveChanges();
            }
            return updatePublisherRequestDTO;
        }
    }
}
