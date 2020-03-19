using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using {{namespace}}.Database.POCO.QueryModels.{{entity_sub_namespace}};
using {{namespace}}.Business.Core.DomainModels;
using {{namespace}}.Database.POCO.Identities;
using {{namespace}}.Database.POCO.QueryModels;
using {{namespace}}.Database.POCO.QueryParameters;
using {{namespace}}.Common;

namespace {{namespace}}.Business.Core.IDataAccess.Query
{
    public partial interface IQueryDataAccessor : IDisposable
    {
        DetailsModel {{get_details_method_name}}({{domain_model_id_name}} id);
        PagingRecords<ListModel> {{list_method_name}}({{list_parameters_model_name}} parameter);
    }
}