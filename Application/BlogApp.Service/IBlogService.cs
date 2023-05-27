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
    public interface IBlogService 
    {
        ICollection<BlogDisplayResponse> GetAllBlogs();
        IResult Add(BlogAddRequest blog);
        Task<IResult> AddAsync(BlogAddRequest blog);

    }
}
