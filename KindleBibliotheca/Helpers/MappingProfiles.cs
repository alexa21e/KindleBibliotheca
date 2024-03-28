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
                .ForMember(d => d.CoverUrl, o => o.MapFrom<CoverUrlResolver>())
                .ForMember(d => d.PDFUrl, o => o.MapFrom<PDFUrlResolver>());
            CreateMap<BookToCreate, Book>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid()))
                .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.PDFUrl, opt => opt.MapFrom(src => ""))
                .AfterMap((src, dest, ctx) => {
                    var author = (Author)ctx.Items["Author"];
                    dest.Author = author;
                    dest.AuthorId = author.Id;
                });
        }
    }
}
