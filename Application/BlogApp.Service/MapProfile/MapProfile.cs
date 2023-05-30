using AutoMapper;
using BlogApp.Entities;
using BlogApp.Results.DTOs.Request;
using BlogApp.Results.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Service.MapProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Blog, BlogDisplayResponse>();
            CreateMap<BlogAddRequest,Blog> ();
            CreateMap<User, UserLoginResponse>();
        }
    }
}
