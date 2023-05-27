using AutoMapper;
using BlogApp.DataAccess;
using BlogApp.Entities;
using BlogApp.Results;
using BlogApp.Results.DTOs.Request;
using BlogApp.Results.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Service
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepo repo;
        private readonly IMapper mapper;

        public BlogService(IBlogRepo repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public IResult Add(BlogAddRequest blog)
        {
            Blog item = mapper.Map<Blog>(blog);
            repo.Add(item);
            if (item.Id!= null)
            {
                return new SuccessResult("blog eklendi");
            }
            return new ErrorResult("blog eklenemedi");
        }

        public Task<IResult> AddAsync(BlogAddRequest blog)
        {
            throw new NotImplementedException();
        }

        public ICollection<BlogDisplayResponse> GetAllBlogs()
        {
            return mapper.Map<ICollection<BlogDisplayResponse>>(repo.GetAll());
        }
    }
}
