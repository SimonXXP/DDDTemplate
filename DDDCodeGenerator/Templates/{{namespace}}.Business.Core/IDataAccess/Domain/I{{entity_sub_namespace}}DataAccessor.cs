using {{namespace}}.Business.Core.DomainModels;
using HR4Edu.Database.Entities;
using {{namespace}}.Database.POCO.Identities;
using System;

namespace {{namespace}}.Business.Core.IDataAccess.Domain
{
    public partial interface IDomainDataAccessor : IDisposable
    {
        void {{upate_method_name}}({{domain_model_name}} domain_object);
        {{add_return_name}} {{add_method_name}}({{domain_model_name}} domain_object);
        void {{delete_method_name}}({{domain_model_id_name}} id);
        void {{batch_delete_method_name}}({{domain_model_id_name}}[] ids);
    }
}