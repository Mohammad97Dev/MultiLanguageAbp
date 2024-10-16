using AutoMapper;
using MultiLanguage.Countries.Dto;
using MultiLanguage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLanguage.Countries.Mapper
{
    public class CountryMapProfile:Profile
    {
        public CountryMapProfile()
        {
            CreateMap<CreateCountryDto,Country>().ReverseMap();
            CreateMap<UpdateCountryDto,Country>().ReverseMap();
            CreateMap<CountryTranslation,CountryTranslationDto>().ReverseMap();

        }
    }
}
