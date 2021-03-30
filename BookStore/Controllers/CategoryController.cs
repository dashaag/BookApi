using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Dto.ResultDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CategoryController(ApplicationContext context)
        {
            _context = context;
        }

        public CollectionResultDto<CategoryDto> GetCategories()
        {
            var categories =_context.Categories.Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return new CollectionResultDto<CategoryDto>
            {
                IsSuccessful = true,
                Data = categories
            };
        }

        [HttpDelete]
        public ResultDto DeleteCategory( int id)
        {
            try
            {
                if (id != null)
                {
                    var c = _context.Categories.Find(id);
                    _context.Categories.Remove(c);
                    _context.SaveChanges();
                    return new ResultDto
                    {
                        IsSuccessful = true,
                        Message = "Successfully deleted"
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccessful = false,
                        Message = "Id is not defined"
                    };
                }
            }
            catch(Exception)
            {
                return new ResultDto
                {
                    IsSuccessful = false,
                    Message = "Something goes wrong"
                };
            }

        }
    }
}