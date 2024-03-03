using AutoMapper;
using Core.Entities;
using KindleBibliotheca.DTOs;

namespace KindleBibliotheca.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookToReturn>()
                .ForMember(d => d.Series, o => o.MapFrom(s => s.Series.Name))
                .ForMember(d => d.Author, o => o.MapFrom(s => s.Author.Name))
                .ForMember(d => d.CoverUrl, o => o.MapFrom<BookUrlResolver>())
                .ForMember(d => d.PDFUrl, o => o.MapFrom<BookUrlResolver>());
        }
    }
}
