using System;

namespace {{namespace}}.Business.Core.Commands.{{entity_sub_namespace}}
{
    public partial class DeleteCommand : CommonCommand
    {
        /// <summary>
        /// {{entity_name}}'s id
        /// </summary>
        public Guid ID { get; set; }
    }
}