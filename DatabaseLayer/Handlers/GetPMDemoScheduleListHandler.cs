using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using PremiseGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseLayer.Handlers
{
    public class GetPMDemoScheduleListHandler : IRequestHandler<GetPMDemoScheduleList, IEnumerable<Dictionary<string, object>>>
    {
        private EquipmentsDal _equipmentsDal; 
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        public GetPMDemoScheduleListHandler(EquipmentsDal equipmentsDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _equipmentsDal = equipmentsDal; 
            _premiseService = premiseService;
            _dbCollections = dbCollections;
        }
        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetPMDemoScheduleList request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.PMDemoSchedule);
                string scheduleInstantCollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.PManagementScheduleInstant);
                return await _equipmentsDal.GetPmDemoScheduleWithScheduleInstant(collectionName, scheduleInstantCollectionName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
