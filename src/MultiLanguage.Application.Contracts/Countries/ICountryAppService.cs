using MultiLanguage.Countries.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MultiLanguage.Countries
{
    public interface ICountryAppService :ICrudAppService<CountryDto, int, PagedCountryResultRequestDto, CreateCountryDto, UpdateCountryDto>
    {
    }
}
