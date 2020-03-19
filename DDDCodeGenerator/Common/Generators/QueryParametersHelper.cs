using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class QueryParametersHelper : BaseHelper
    {

        public QueryParametersHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }


        /// <summary>
        /// 创建代码
        /// </summary>
        public void Create_Code()
        {
            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_query_parameters);
            string template_file = template_folder + "{{entity_sub_namespace}}ListParameter.cs";

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
            template = template.Replace("{{query-list-parameter-name}}", NameHelper.Get_ViewModel_Query_ListParameter_Name(entity_name));

            #region Parameters
            StringBuilder sb = new StringBuilder();

            foreach (var p in entity_properties)
            {
                sb.AppendLine("        public " + NameHelper.GetListParameterTypeString(p) + " " + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + " { get; set; }");
            }
            
            sb.AppendLine("        public bool? is_order_asc { get; set; }");
            sb.Append("        public string order_property { get; set; }");

            template = template.Replace("{{Parameters}}", sb.ToString());

            #endregion

            #region Parameter-Properties-Convertor

            sb = new StringBuilder();

            foreach (var p in entity_properties)
            {
                sb.AppendLine("            " + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + " = source." + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + ";");
            }
            //for (int i = 0; i < entity_properties.Count; i++)
            //{
            //    if (i == entity_properties.Count - 1)
            //    {
            //        sb.Append("            " + NameHelper.Get_Property_Name_For_ListParameter(entity_properties[i].Name) + " = source." + NameHelper.Get_Property_Name_For_ListParameter(entity_properties[i].Name) + ";");
            //    }
            //    else
            //    {
            //        sb.AppendLine("            " + NameHelper.Get_Property_Name_For_ListParameter(entity_properties[i].Name) + " = source." + NameHelper.Get_Property_Name_For_ListParameter(entity_properties[i].Name) + ";");
            //    }
            //}

            sb.AppendLine("            order_property = source.order_property;");
            sb.Append("            is_order_asc = source.is_order_asc;");

            template = template.Replace("{{Parameter-Properties-Convertor}}", sb.ToString());
            #endregion

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }
    }
}