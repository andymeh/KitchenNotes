using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using UniversalAppService;

namespace SampleWCFDataService
{
    public class WcfDataService1 : DataService<KitchNotesDatabaseEntities>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
            config.DataServiceBehavior.MaxProtocolVersion =
                     DataServiceProtocolVersion.V2;
        }
    }
}