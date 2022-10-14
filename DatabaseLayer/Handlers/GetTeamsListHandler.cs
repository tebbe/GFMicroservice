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
    public class GetTeamsListHandler : IRequestHandler<GetTeamsList, IEnumerable<Dictionary<string, object>>>
    {
        private TeamsDal _teamsDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetTeamsListHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, TeamsDal teamsDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _teamsDal = teamsDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetTeamsList request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string CollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.GFTeams);
                return await _teamsDal.GetAsync(CollectionName);
            }
            catch
            {
                throw;
            }
        }
    }
}
