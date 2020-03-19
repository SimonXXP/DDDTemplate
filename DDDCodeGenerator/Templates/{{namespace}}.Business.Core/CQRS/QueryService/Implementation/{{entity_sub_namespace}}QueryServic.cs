using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using {{namespace}}.Database.POCO.Identities;
using {{namespace}}.Database.POCO.QueryModels;
using {{namespace}}.Database.POCO.QueryModels.{{entity_sub_namespace}};
using {{namespace}}.Database.POCO.QueryParameters;
using {{namespace}}.Common;

namespace {{namespace}}.Business.Core.CQRS
{
    public partial class QueryService : IQueryService
    {
        public PagingRecords<ListModel> {{list_method_name}}({{list_parameters_model_name}} parameter)
        {
            return DataAccessor.{{data_access_list_method_name}}(parameter);
        }

        public DetailsModel {{get_details_method_name}}({{domain_model_id_name}} id)
        {
            return DataAccessor.{{data_access_get_details_method_name}}(id);
        }
    }
}