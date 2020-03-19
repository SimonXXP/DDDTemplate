using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class QueryModelsHelper : BaseHelper
    {

        public QueryModelsHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }

        public void Create_Code()
        {
            Create_DetailsModel_Code();
            Create_ListModel_Code();
        }

        /// <summary>
        /// 创建代码
        /// </summary>
        public void Create_DetailsModel_Code()
        {

            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_query_models)+ "{{entity_sub_namespace}}\\";
            string template_file = template_folder + "DetailsModel.cs";

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

            #region Create code

            StringBuilder sb = new StringBuilder();

            //foreach (var p in entity_properties)
            //{
            //    //sb.AppendLine("");
            //    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(p) + " " + NameHelper.Get_Property_Name_For_Query_ListModel(p.Name) + " { get; set; }");
            //}
            for (int i = 0; i < entity_properties.Count; i++)
            {
                if (i == entity_properties.Count - 1)
                {
                    sb.Append("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_Query_ListModel(entity_properties[i].Name) + " { get; set; }");
                }
                else
                {
                    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_Query_ListModel(entity_properties[i].Name) + " { get; set; }");
                }
            }
            template = template.Replace("{{Details-Model-Parmeters}}", sb.ToString());

            #endregion

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);

        }

        public void Create_ListModel_Code()
        {

            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_query_models);
            string template_file = template_folder + "{{entity_sub_namespace}}\\ListModel.cs";

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

            #region Create code

            StringBuilder sb = new StringBuilder();

            //foreach (var p in entity_properties)
            //{
            //    //sb.AppendLine("");
            //    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(p) + " " + NameHelper.Get_Property_Name_For_Query_ListModel(p.Name) + " { get; set; }");
            //}
            for (int i = 0; i < entity_properties.Count; i++)
            {
                if (i == entity_properties.Count - 1)
                {
                    sb.Append("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_Query_ListModel(entity_properties[i].Name) + " { get; set; }");
                }
                else
                {
                    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_Query_ListModel(entity_properties[i].Name) + " { get; set; }");
                }
            }
            template = template.Replace("{{List-Model-Parmeters}}", sb.ToString());

            #endregion

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);

        }

    }

}