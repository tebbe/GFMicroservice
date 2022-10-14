using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetAppSettings : IRequest<PremiseAppSettingsModel>
    {
        public string SettingKey { get; set; }
        public string AppID { get; set; }
        public string AppKey { get; set; }

        
    }
}
