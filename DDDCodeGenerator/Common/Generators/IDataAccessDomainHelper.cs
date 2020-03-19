using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class IDataAccessDomainHelper : BaseHelper
    {
        public IDataAccessDomainHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }

        /// <summary>
        /// 创建代码
        /// </summary>
        public void Create_Code()
        {
            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_data_access_domain_interface);
            string template_file = template_folder + "I{{entity_sub_namespace}}DataAccessor.cs";

            string target_folder = template_folder.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));
            string target_file = template_file.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));

            target_folder = target_folder.Replace("{{namespace}}", settings.code_namespace);
            target_folder = target_folder.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            target_file = target_file.Replace("{{namespace}}", settings.code_namespace);
            target_file = target_file.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            #endregion

            string template = FileHelper.ReadAll(template_file);

            template = template.Replace("{{namespace}}", settings.code_namespace);
            template = template.Replace("{{entity_name}}", entity_name);
            template = template.Replace("{{entity_sub_namespace}}", entity_sub_namespace);
            template = template.Replace("{{domain_model_name}}", NameHelper.Get_Domain_Model_Name(entity_name));
            template = template.Replace("{{domain_model_id_name}}", NameHelper.Get_Identities_Name(entity_name));


            template = template.Replace("{{add_return_name}}", entity_name);
            template = template.Replace("{{add_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_Add(entity_name));
            template = template.Replace("{{upate_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_Update(entity_name));
            template = template.Replace("{{delete_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_Delete(entity_name));
            template = template.Replace("{{batch_delete_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_BatchDelete(entity_name));

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }
    }
}