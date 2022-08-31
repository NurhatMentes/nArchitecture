using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand
{
    //bir commend çağırıyoruz api'den
    public class CreateBrandCommand : IRequest<CreatedBrandDto>
    {
        //bir commend bir işlem için kullanılır örneğin create
        //bura da bu commend ihtiyacımız olan field leri çağırıyoruz.
        public string Name { get; set; }
        public int id { get; set; }


        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {

            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                //BusinessRules dan geliyor.
                //Aynı ismin tekrarı olamaz.
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);
                await _brandBusinessRules.BrandShouldExistWhenRequested(request.id);

                //Elimizdeki command'in alanlarını vt'ki alanlarla auto mapper ile eşledik.
                Brand mappedBrand = _mapper.Map<Brand>(request);
                //eklediğim verinin id'si ile birlikte kullanıcıya döndürüyorum.
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
                //döndürmek istediğim kolonları gönderiyorum.
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);

                return createdBrandDto;
            }
        }
    }
}
