using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using {{namespace}}.Business.Core.Commands.{{entity_sub_namespace}};
using {{namespace}}.Common.Extensions;
using {{namespace}}.Business.Core.CQRS;
using {{namespace}}.Business.Core.Commands;
using {{namespace}}.Business.Core.DomainModels;
using {{namespace}}.Database.POCO.Identities;
using {{namespace}}.Business.Core.Errors;
using HR4Edu.Business.Core.IDataAccess.Domain;
using {{namespace}}.Common.Utils;

namespace {{namespace}}.Business.Core.CommandHandlers.{{entity_sub_namespace}}
{

    public partial class AddCommandHander : CommandHandlerBase, ICommandHandler<AddCommand>
    {

        public void Execute_Custom(AddCommand command)
        {
        }
    

    }
}
