﻿using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BooksWithSeriesSpecifications: BaseSpecification<Book>
    {
        public BooksWithSeriesSpecifications(string sort)
        {
            AddInclude(x => x.Series);
            AddOrderBy(x => x.Title);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
                    case "RatingAsc":
                        AddOrderBy(b => b.Rating);
                        break;
                    case "RatingDesc":
                        AddOrderByDescending(b => b.Rating);
                        break;
                    default:
                        AddOrderByDescending(b => b.PublishingDate);
                        break;
                }
            }
        }
        public BooksWithSeriesSpecifications(Guid id): base(x => x.Id == id)
        {
            AddInclude(x => x.Series);
        }
    }
}
