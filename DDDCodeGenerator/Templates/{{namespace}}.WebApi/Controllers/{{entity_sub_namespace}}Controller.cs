using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using {{namespace}}.Common;
using {{namespace}}.Common.Extensions;
using {{namespace}}.Business.Core.Commands;
using {{namespace}}.Database.POCO.Identities;
using {{namespace}}.Database.POCO.QueryModels;
using {{namespace}}.Business.Core.DomainModels;
using {{namespace}}.Database.POCO.QueryParameters;
using {{namespace}}.Common.Constants;
using {{namespace}}.ViewModels.ViewModels;
using {{namespace}}.Business.Core.Commands.{{entity_sub_namespace}};
using {{namespace}}.ViewModels.ViewModels.{{entity_sub_namespace}};
using {{namespace}}.Database.POCO.QueryModels.{{entity_sub_namespace}};
using {{namespace}}.WebApi.Attributes;
using static {{namespace}}.Common.Constants.CommonEnum;
using Newtonsoft.Json;
using HR4Edu.WebApi.Tools;
using HR4Edu.WebApi.ViewModels;

namespace {{namespace}}.WebApi.Controllers
{
    /// <summary>
    /// {{entity_name}} APIs
    /// </summary>
    //[RoutePrefix("Api/{{entity_sub_namespace}}")]
    public partial class {{entity_sub_namespace}}Controller : ApiBaseController
    {
    
        #region Add Actions
{{Add-Actions}}    
        #endregion

        #region Delete Actions
{{Delete-Actions}}    
        #endregion

        #region Batch Delete Action
{{Batch-Delete-Actions}}
	    #endregion

        #region Update Actions
{{Update-Actions}}    
        #endregion

        #region Get Actions
{{Get-Actions}}    
        #endregion

        #region Details Actions
{{Details-Actions}}
        #endregion      
    }
}