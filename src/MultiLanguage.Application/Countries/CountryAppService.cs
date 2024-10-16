using MultiLanguage.Countries.Dto;
using MultiLanguage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MultiLanguage.Countries
{
    public class CountryAppService : CrudAppService<Country, CountryDto, int, PagedCountryResultRequestDto, CreateCountryDto, UpdateCountryDto>, ICountryAppService
    {

        public CountryAppService(IRepository<Country, int> repository) : base(repository)
        {
        }
        public override async Task<CountryDto> GetAsync(int id)
        {
            var country = Repository.WithDetailsAsync(x => x.Translations).Result.Where(c => c.Id == id)
                    .FirstOrDefault() ??
                    throw new EntityNotFoundException(typeof(Country), id);
            return ObjectMapper.Map<Country, CountryDto>(country);


        }
        public override Task<PagedResultDto<CountryDto>> GetListAsync(PagedCountryResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
        protected override Task<IQueryable<Country>> CreateFilteredQueryAsync(PagedCountryResultRequestDto input)
        {
            return Repository.WithDetailsAsync(x => x.Translations);

        }
    }
}
