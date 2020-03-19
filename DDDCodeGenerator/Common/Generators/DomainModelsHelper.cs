using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class DomainModelsHelper : BaseHelper
    {

        public DomainModelsHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }

        /// <summary>
        /// 创建代码
        /// </summary>
        public void Create_Code()
        {

            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_domain_models);
            string template_file = template_folder + "{{entity_sub_namespace}}.cs";

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

            #region Create code

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        public " + NameHelper.Get_Identities_Name(entity_name) + " " + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + " { get; set; }");

            foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            {
                sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(p) + " " + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + " { get; set; }");
            }
            //sb.AppendLine("");
            sb.AppendLine("        public " + entity_name + " ConvertToEntity()");
            sb.AppendLine("        {");
            sb.AppendLine("            " + entity_name + " entity = new " + entity_name + "();");
            foreach (var p in entity_properties.ToList())
            {
                if (p.Name == entity_id_properties.Name)
                {
                    sb.AppendLine("            entity." + p.Name + " = (Guid)this." + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + ".GetPersistId();");
                }
                else
                {
                    sb.AppendLine("            entity." + p.Name + " = this." + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + ";");
                }
            }

            sb.AppendLine("            return entity;");
            sb.Append("        }");

            template = template.Replace("{{Domain-Model-Parmeters}}", sb.ToString());

            #endregion

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);

        }

    }

}