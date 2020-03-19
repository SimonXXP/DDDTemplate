using System;

namespace {{namespace}}.Business.Core.Commands.{{entity_sub_namespace}}
{
    public partial class BatchDeleteCommand : CommonCommand
    {
        /// <summary>
        /// {{entity_name}}'s id
        /// </summary>
        public Guid[] IDs { get; set; }
    }
}