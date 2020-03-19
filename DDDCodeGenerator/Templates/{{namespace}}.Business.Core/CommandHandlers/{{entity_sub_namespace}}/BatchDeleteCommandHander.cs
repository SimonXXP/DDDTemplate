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
    public partial class BatchDeleteCommandHander : CommandHandlerBase, ICommandHandler<BatchDeleteCommand>
    {
        public void Execute(BatchDeleteCommand command)
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
            var record_ids = command.IDs.Select(x => new {{domain_model_id_name}}(x)).ToArray();

            DataAccessor.{{batch_delete_method_name}}(record_ids);
            DataAccessor.Save();
            command.ExecuteSuccess();
            #endregion
        }

        public void Execute_Custom(BatchDeleteCommand command) { }

        private List<BusinessError> Validate(BatchDeleteCommand command)
        {
            var result = new List<BusinessError>();
            // ImportContainerTypeID validation
            if (command.IDs == null || command.IDs.Length == 0)
            {
                result.Add(ErrorFactory.CreateFieldIsRequiredError("ID", "The record id is invalid!"));
            }
            return result;
        }
    }
}