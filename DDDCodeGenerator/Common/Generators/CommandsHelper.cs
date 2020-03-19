using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{

    public class CommandsHelper : BaseHelper
    {

        public CommandsHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }

        public void Create_Code()
        {
            Create_Add_Command();
            Create_DeleteCommand_Command();
            Create_BatchDeleteCommand_Command();
            Create_Update_Command();
        }

        /// <summary>
        /// 创建代码
        /// </summary>
        public void Create_Add_Command()
        {

            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command);
            string template_file = template_folder + "AddCommand.cs";

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
            
            StringBuilder sb = new StringBuilder();

            //foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            //foreach (var p in entity_properties.ToList())
            //{
            //    //sb.AppendLine("");
            //    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(p) + " " + NameHelper.Get_Property_Name_For_Command(p.Name) + " { get; set; }");
            //}
            for (int i = 0; i < entity_properties.Count; i++)
            {
                if (i == entity_properties.Count - 1)
                {
                    sb.Append("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_Command(entity_properties[i].Name) + " { get; set; }");
                }
                else
                {
                    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_Command(entity_properties[i].Name) + " { get; set; }");
                }
            }

            template = template.Replace("{{Properties}}", sb.ToString());

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);

        }

        public void Create_DeleteCommand_Command()
        {

            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command);
            string template_file = template_folder + "DeleteCommand.cs";

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

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);

        }

        public void Create_BatchDeleteCommand_Command()
        {
            #region folders
            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command);
            string template_file = template_folder + "BatchDeleteCommand.cs";

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

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void Create_Update_Command()
        {

            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command);
            string template_file = template_folder + "UpdateCommand.cs";

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


            StringBuilder sb = new StringBuilder();

            //foreach (var p in entity_properties)
            //{
            //    //sb.AppendLine("");
            //    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(p) + " " + NameHelper.Get_Property_Name_For_Command(p.Name) + " { get; set; }");
            //}
            for (int i = 0; i < entity_properties.Count; i++)
            {
                if (i == entity_properties.Count - 1)
                {
                    sb.Append("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_Command(entity_properties[i].Name) + " { get; set; }");
                }
                else
                {
                    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_Command(entity_properties[i].Name) + " { get; set; }");
                }
            }

            template = template.Replace("{{Properties}}", sb.ToString());

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }
    }
}