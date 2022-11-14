﻿using AutoMapper;
using Library.Data;
using Library.Models;

namespace Library.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, CreateBookDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
        } 
    }
}