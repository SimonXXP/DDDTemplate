using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using {{namespace}}.Business.Core.Commands.{{entity_sub_namespace}};
using {{namespace}}.Business.Core.Commands;
using {{namespace}}.Business.Core.DomainModels;
using {{namespace}}.Database.POCO.Identities;
using {{namespace}}.Business.Core.Errors;
using HR4Edu.Business.Core.IDataAccess.Domain;
using {{namespace}}.Common.Utils;

namespace {{namespace}}.Business.Core.CommandHandlers.{{entity_sub_namespace}}
{
    public partial class DeleteCommandHander : CommandHandlerBase, ICommandHandler<DeleteCommand>
    {
        public void Execute(DeleteCommand command)
        {
            #region check errors
            List<BusinessError> errors = Validate(command);
            if (errors.Any())
            {
                command.ExecuteFail(errors);
                return;
            }
            #endregion

            #region exec command
            var record_id = new {{domain_model_id_name}}(command.ID);

            DataAccessor.{{delete_method_name}}(record_id);
            DataAccessor.Save();
            command.ExecuteSuccess(record_id);
            #endregion
        }

        public void Execute_Custom(DeleteCommand command){ }

        private List<BusinessError> Validate(DeleteCommand command)
        {
            var result = new List<BusinessError>();
            // ImportContainerTypeID validation
            if (command.ID == Guid.Empty)
            {
                result.Add(ErrorFactory.CreateFieldIsRequiredError("ID", "The record id is invalid!"));
            }
            return result;
        }
    }
}