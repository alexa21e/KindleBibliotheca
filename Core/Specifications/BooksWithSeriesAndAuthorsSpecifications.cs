﻿using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BooksWithSeriesAndAuthorsSpecifications: BaseSpecification<Book>
    {
        public BooksWithSeriesAndAuthorsSpecifications(BookSpecParam bookParams)
            : base(x => 
                    (string.IsNullOrEmpty(bookParams.Search) || x.Title.ToLower().Contains(bookParams.Search)) &&
                    (!bookParams.SeriesId.HasValue || x.SeriesId == bookParams.SeriesId) &&
                    (!bookParams.AuthorId.HasValue || x.AuthorId == bookParams.AuthorId))
        {
            AddInclude(x => x.Series);
            AddInclude(x => x.Author);
            AddOrderBy(x => x.Title);
            ApplyPaging(bookParams.PageSize * (bookParams.PageIndex - 1), 
                bookParams.PageSize);

            if (!string.IsNullOrEmpty(bookParams.Sort))
            {
                switch (bookParams.Sort)
                {
                    case "TitleAsc":
                        AddOrderBy(b => b.Title);
                        break;
                    case "TitleDesc":
                        AddOrderByDescending(b => b.Title);
                        break;
                    case "PagesAsc":
                        AddOrderBy(b => b.PagesNumber);
                        break;
                    case "PagesDesc":
                        AddOrderByDescending(b => b.PagesNumber);
                        break;
                    case "DateAsc":
                        AddOrderBy(b => b.PublishingDate);
                        break;
                    case "DateDesc":
                        AddOrderByDescending(b => b.PublishingDate);
                        break;
                    case "RatingAsc":
                        AddOrderBy(b => b.Rating);
                        break;
                    case "RatingDesc":
                        AddOrderByDescending(b => b.Rating);
                        break;
                    case "PlaceAsc":
                        AddOrderBy(b => b.SeriesPlace);
                        break;
                    case "PlaceDesc":
                        AddOrderByDescending(b => b.SeriesPlace);
                        break;
                    default:
                        AddOrderByDescending(b => b.PublishingDate);
                        break;
                }
            }
        }
        public BooksWithSeriesAndAuthorsSpecifications(Guid id): base(x => x.Id == id)
        {
            AddInclude(x => x.Series);
            AddInclude(x => x.Author);
        }
    }
}
