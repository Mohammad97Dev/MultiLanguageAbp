﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MultiLanguage.Countries.Dto
{
    public class CountryDto : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
        public List<CountryTranslationDto> Translations { get; set; }
    }
}
