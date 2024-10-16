using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MultiLanguage.Countries.Dto
{
    public class UpdateCountryDto : CreateCountryDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}
