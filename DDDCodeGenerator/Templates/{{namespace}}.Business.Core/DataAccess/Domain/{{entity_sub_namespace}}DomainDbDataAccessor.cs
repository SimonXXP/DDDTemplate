using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using {{namespace}}.Business.Core.IDataAccess.Domain;
using {{namespace}}.Business.Core.DomainModels;
using {{namespace}}.Database.POCO.Identities;
using {{namespace}}.Common.Constants;
using {{namespace}}.Common.Utils;
using {{namespace}}.Database.Entities;

namespace {{namespace}}.Business.Core.DataAccess.Domain
{
    public partial class DomainDbDataAccessor : DbDataAccessorBase, IDomainDataAccessor
    {
        #region Public methods
{{Update-Method}} 

{{Delete-Method}}

{{Batch-Delete-Method}}
     
{{Add-Method}}
        #endregion

        #region Private methods
{{Get-Entity-Method}}

{{Convert-To-Domain-Model-Method}}     
        #endregion
    }
}