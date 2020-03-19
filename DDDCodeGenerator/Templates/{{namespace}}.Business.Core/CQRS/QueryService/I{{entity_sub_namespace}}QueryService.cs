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
    public partial interface IQueryService
    {
        PagingRecords<ListModel> {{list_method_name}}({{list_parameters_model_name}} parameter);
        DetailsModel {{get_details_method_name}}({{domain_model_id_name}} id);
    }
}