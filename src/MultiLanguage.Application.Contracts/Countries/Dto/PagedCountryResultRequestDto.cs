using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MultiLanguage.Countries.Dto
{
    public class PagedCountryResultRequestDto : PagedResultRequestDto, ISortedResultRequest
    {
        public string? Keyword { get; set; }
        public string? Sorting { get; set; }

    }
}
