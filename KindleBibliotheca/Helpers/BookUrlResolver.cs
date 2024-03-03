using AutoMapper;
using Core.Entities;
using KindleBibliotheca.DTOs;

namespace KindleBibliotheca.Helpers
{
    public class BookUrlResolver : IValueResolver<Book, BookToReturn, string>
    {
        private readonly IConfiguration _config;

        public BookUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Book source, BookToReturn destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.CoverUrl))
            {
                return _config["ApiUrl"] + source.CoverUrl;
            }
            else if (!string.IsNullOrEmpty(source.PDFUrl))
            {
                return _config["ApiUrl"] + source.PDFUrl;
            }

            return null;
        }
    }
}
