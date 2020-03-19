using ShineTechQD.DDDCodeGenerator.Common.Generators;
using ShineTechQD.DDDCodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Text;

namespace ShineTechQD.DDDCodeGenerator.Common
{

    public class Generator
    {

        #region MyRegion

        private EntityHelper entity_helper { get; set; }

        Settings settings { get; set; }

        public string enetity_name_and_namespace { get; set; }

        public string result_folder { get; set; }

        //public Type entity { get; set; }

        //public List<EntityProperty> entity_properties { get; set; }

        //public EntityProperty entity_id_properties { get; set; }

        public HttpContext context { get; set; }

        //string entity_name { get; set; }

        //string entity_sub_namespace { get; set; }

        #endregion

        public Generator(string _enetity_name_and_namespace)
        {

            context = HttpContext.Current;

            settings = new Settings();
            entity_helper = new EntityHelper();

            enetity_name_and_namespace = _enetity_name_and_namespace;

            //entity = entity_helper.GetEntityType(enetity_name_and_namespace);
            //entity_name = entity.Name;
            //entity_sub_namespace = NameHelper.GetSubNameSpace(entity_name);
            //entity_properties = entity_helper.GetEntityProperties(entity);
            //entity_id_properties = entity_helper.GetPrimaryKey(entity_properties);

        }

        public void CreateCode()
        {



            #region 1. IST.DSR.WebApi

            //1.创建Controllers代码
            ControllerHelper ch = new ControllerHelper(enetity_name_and_namespace);
            ch.Create_Code();

            #endregion

            #region 2. IST.DSR.ViewModels

            //2.创建ViewModel代码
            ViewModelHelper vh = new Generators.ViewModelHelper(enetity_name_and_namespace);
            vh.Create_Code();

            #endregion

            #region 3. IST.DSR.Business.Core

            //创建CommandHandlers.CdslContainer.AddCommandHander代码
            //创建CommandHandlers.CdslContainer.DeleteCommandHander代码
            //创建CommandHandlers.CdslContainer.UpdateCommandHander代码
            CommandHandlersHelper cohh = new Generators.CommandHandlersHelper(enetity_name_and_namespace);
            cohh.Create_Code();

            //创建Commands.CdslContainer.AddCommand代码
            //创建Commands.CdslContainer.DeleteCommand代码
            //创建Commands.CdslContainer.UpdateCommand代码
            CommandsHelper coh = new Generators.CommandsHelper(enetity_name_and_namespace);
            coh.Create_Code();

            //创建CQRS.QueryService.Implementation.CdslContainerQueryServic
            QueryServicHelper qsh = new Generators.QueryServicHelper(enetity_name_and_namespace);
            qsh.Create_Code();

            //创建CQRS.QueryService.IImportContainerTypeQueryService
            IQueryServicHelper iqsh = new Generators.IQueryServicHelper(enetity_name_and_namespace);
            iqsh.Create_Code();

            //创建DataAccess.Domain.ICdslContainerDataAccessor.cs
            IDataAccessDomainHelper idadh = new IDataAccessDomainHelper(enetity_name_and_namespace);
            idadh.Create_Code();

            //创建DataAccess.Query.ICdslContainerQueryDataAccessor
            IDataAccessQueryHelper idaqh = new IDataAccessQueryHelper(enetity_name_and_namespace);
            idaqh.Create_Code();

            //创建DomainModels.Identities.CdslContainerID
            DomainModelsIdentitiesHelper dmidh = new DomainModelsIdentitiesHelper(enetity_name_and_namespace);
            dmidh.Create_Code();

            //创建DomainModels.CdslContainer 
            DomainModelsHelper dmh = new DomainModelsHelper(enetity_name_and_namespace);
            dmh.Create_Code();

            //创建QueryModels.CdslContainer.DetailsModel
            //创建QueryModels.CdslContainer.ListModel
            QueryModelsHelper qh = new QueryModelsHelper(enetity_name_and_namespace);
            qh.Create_Code();

            //创建QueryParameters.CdslContainerListParameter.cs
            QueryParametersHelper qph = new QueryParametersHelper(enetity_name_and_namespace);
            qph.Create_Code();

            #endregion

            #region 4. IST.DSR.Database.POCO

            //DataAccess.Domain.CdslContainerDomainDbDataAccessor
            DataAccessDomainHelper ddh = new DataAccessDomainHelper(enetity_name_and_namespace);
            ddh.Create_Code();

            //DataAccess.Query.CdslContainerRequestQueryDbDataAccessor
            DataAccessQueryHelper dqh = new DataAccessQueryHelper(enetity_name_and_namespace);
            dqh.Create_Code();

            #endregion

        }


    }

}