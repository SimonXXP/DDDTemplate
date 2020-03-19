using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using {{namespace}}.Business.Core.IDataAccess.Query;
using {{namespace}}.Database.POCO.QueryModels;
using {{namespace}}.Database.POCO.QueryParameters;
using {{namespace}}.Database.POCO.Extensions;
using {{namespace}}.Database.POCO.Identities;
using {{namespace}}.Database.POCO.QueryModels.{{entity_sub_namespace}};
using {{namespace}}.Database.Entities;
using {{namespace}}.Common;

namespace {{namespace}}.Business.Core.DataAccess.Query
{
    public partial class QueryDbDataAccessor : DbDataAccessorBase, IQueryDataAccessor
    {   
{{Get-Details-Method}}
{{Get-List-Method}}    
{{Get-Entity-Method}}
    }
}