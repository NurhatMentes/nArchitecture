using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
        {
            CreatedBrandDto result = await Mediator.Send(createBrandCommand);
            return Created("", result);
        }


        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListBrandQuery getListBrandQuery = new() { PageRequest = pageRequest };
            BrandListModel result = await Mediator.Send(getListBrandQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByID([FromRoute] GetByIdBrandQuery getByIdBrandQuery)
        {
            BrandGetByIdDto brandGetByIdDto = await Mediator.Send(getByIdBrandQuery);
            return Ok(brandGetByIdDto);
        }
    }  
}

//Behavioral Pattern’ler özetle nesneler arasındaki iletişimi tasarımlayan ve böylece iletişimin daha esnek yapılarda 
//yapılmasını sağlayan tasarım kalıplarıdır. Mediator Pattern bir Behavioral Design Pattern’dir. 
//Mediator Pattern ile nesnelerin iletişimi ortak bir noktadan sağlanmakta, nesnelerin birbirleriyle doğrudan iletişime girmesi 
//yerine bir aracıyla iletişime girip haberleşmesini tasarımlamaktadır.
//Mediator Pattern iletişimin merkezine bir aracı koyar ve tüm iletişim bunun üzerinden gerçekleşir. Böylece nesneler arası 
//loosely-coupled(gevşek-bağlı) bir bağın kullanılmasına imkan tanır. Nesneler iletişim kurmak istediği 
//diğer nesnelerin referanslarını barındırmaz, 
//doğrudan bağlantı kurmaz, aracıyı kullanarak tüm iletişimlerini bu aracı katman üzerinden sağlarlar.