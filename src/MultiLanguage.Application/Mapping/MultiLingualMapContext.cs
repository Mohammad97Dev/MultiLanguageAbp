using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;

namespace MultiLanguage.Mapping
{
    public class MultiLingualMapContext
    {
        public ISettingManager SettingManager { get; set; }

        public MultiLingualMapContext(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }
    }
}
