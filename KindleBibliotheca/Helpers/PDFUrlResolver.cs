using AutoMapper;
using Core.Entities;
using KindleBibliotheca.DTOs;

namespace KindleBibliotheca.Helpers
{
    public class PDFUrlResolver : IValueResolver<Book, BookToReturn, string>
    {
        private readonly IConfiguration _config;

        public PDFUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Book source, BookToReturn destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PDFUrl))
            {
                return _config["ApiUrl"] + source.PDFUrl;
            }

            return null;
        }
    }
}