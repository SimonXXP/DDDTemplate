using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class ViewModelHelper : BaseHelper
    {
        public ViewModelHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }

        public void Create_Code()
        {
            CreateModelCode();
            CreateAddModelCode();
            CreateUpdateModelCode();
            CreateDeleteModelCode();
            CreateBatchDeleteModelCode();
        }

        public void CreateModelCode()
        {
            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_view_model);
            string template_file = template_folder + "{{entity_sub_namespace}}_Model.cs";

            string target_folder = template_folder.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));
            string target_file = template_file.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));

            target_folder = target_folder.Replace("{{namespace}}", settings.code_namespace);
            target_folder = target_folder.Replace("{{entity_sub_namespace}}", entity_sub_namespace);
            target_file = target_file.Replace("{{namespace}}", settings.code_namespace);
            target_file = target_file.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            string template = FileHelper.ReadAll(template_file);

            template = template.Replace("{{namespace}}", settings.code_namespace);
            template = template.Replace("{{entity_name}}", entity_name);
            template = template.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            StringBuilder sb = new StringBuilder();
            //foreach (var p in entity_properties)
            //{
            //    ////sb.AppendLine("");;
            //    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(p) + " " + NameHelper.Get_Property_Name_For_ViewModel(p.Name) + " { get; set; }");
            //}
            for (int i = 0; i < entity_properties.Count; i++)
            {
                if (i == entity_properties.Count - 1)
                {
                    sb.Append("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_ViewModel(entity_properties[i].Name) + " { get; set; }");
                }
                else
                {
                    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_ViewModel(entity_properties[i].Name) + " { get; set; }");
                }
            }
            template = template.Replace("{{Model-Parameters}}", sb.ToString());

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void CreateAddModelCode()
        {
            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_view_model);
            string template_file = template_folder + "{{entity_sub_namespace}}_AddModel.cs";

            string target_folder = template_folder.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));
            string target_file = template_file.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));

            target_folder = target_folder.Replace("{{namespace}}", settings.code_namespace);
            target_folder = target_folder.Replace("{{entity_sub_namespace}}", entity_sub_namespace);
            target_file = target_file.Replace("{{namespace}}", settings.code_namespace);
            target_file = target_file.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            string template = FileHelper.ReadAll(template_file);

            template = template.Replace("{{namespace}}", settings.code_namespace);
            template = template.Replace("{{entity_name}}", entity_name);
            template = template.Replace("{{entity_sub_namespace}}", entity_sub_namespace);


            StringBuilder sb = new StringBuilder();
            //foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            //{
            //    //////sb.AppendLine("");;
            //    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(p) + " " + NameHelper.Get_Property_Name_For_ViewModel(p.Name) + " { get; set; }");
            //}
            for (int i = 0; i < entity_properties.Count; i++)
            {
                if (i == entity_properties.Count - 1)
                {
                    sb.Append("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_ViewModel(entity_properties[i].Name) + " { get; set; }");
                }
                else
                {
                    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_ViewModel(entity_properties[i].Name) + " { get; set; }");
                }
            }
            template = template.Replace("{{Model-Parameters}}", sb.ToString());

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void CreateUpdateModelCode()
        {
            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_view_model);
            string template_file = template_folder + "{{entity_sub_namespace}}_UpdateModel.cs";

            string target_folder = template_folder.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));
            string target_file = template_file.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));

            target_folder = target_folder.Replace("{{namespace}}", settings.code_namespace);
            target_folder = target_folder.Replace("{{entity_sub_namespace}}", entity_sub_namespace);
            target_file = target_file.Replace("{{namespace}}", settings.code_namespace);
            target_file = target_file.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            string template = FileHelper.ReadAll(template_file);

            template = template.Replace("{{namespace}}", settings.code_namespace);
            template = template.Replace("{{entity_name}}", entity_name);
            template = template.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < entity_properties.Count; i++)
            {
                if (i == entity_properties.Count - 1)
                {
                    sb.Append("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_ViewModel(entity_properties[i].Name) + " { get; set; }");
                }
                else
                {
                    sb.AppendLine("        public " + NameHelper.GetEntityPropertyTypeString(entity_properties[i]) + " " + NameHelper.Get_Property_Name_For_ViewModel(entity_properties[i].Name) + " { get; set; }");
                }
            }
            template = template.Replace("{{Model-Parameters}}", sb.ToString());

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void CreateDeleteModelCode()
        {
            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_view_model);
            string template_file = template_folder + "{{entity_sub_namespace}}_DeleteModel.cs";

            string target_folder = template_folder.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));
            string target_file = template_file.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));

            target_folder = target_folder.Replace("{{namespace}}", settings.code_namespace);
            target_folder = target_folder.Replace("{{entity_sub_namespace}}", entity_sub_namespace);
            target_file = target_file.Replace("{{namespace}}", settings.code_namespace);
            target_file = target_file.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            string template = FileHelper.ReadAll(template_file);

            template = template.Replace("{{namespace}}", settings.code_namespace);
            template = template.Replace("{{entity_name}}", entity_name);
            template = template.Replace("{{entity_sub_namespace}}", entity_sub_namespace);


            StringBuilder sb = new StringBuilder();
            sb.Append("     public " + NameHelper.GetEntityPropertyTypeString(entity_properties.First(x => x.Name == entity_id_properties.Name)) + " id { get; set; }");
            //sb.Append("         public Guid id { get; set; }");
            //sb.Append("         public Guid[] ids { get; set; }");
            template = template.Replace("{{Model-Parameters}}", sb.ToString());

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void CreateBatchDeleteModelCode()
        {
            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_view_model);
            string template_file = template_folder + "{{entity_sub_namespace}}_BatchDeleteModel.cs";

            string target_folder = template_folder.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));
            string target_file = template_file.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));

            target_folder = target_folder.Replace("{{namespace}}", settings.code_namespace);
            target_folder = target_folder.Replace("{{entity_sub_namespace}}", entity_sub_namespace);
            target_file = target_file.Replace("{{namespace}}", settings.code_namespace);
            target_file = target_file.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            string template = FileHelper.ReadAll(template_file);

            template = template.Replace("{{namespace}}", settings.code_namespace);
            template = template.Replace("{{entity_name}}", entity_name);
            template = template.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            StringBuilder sb = new StringBuilder();
            sb.Append("     public " + NameHelper.GetEntityPropertyTypeString(entity_properties.First(x => x.Name == entity_id_properties.Name)) + "[] ids { get; set; }");
            template = template.Replace("{{Model-Parameters}}", sb.ToString());

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }
    }
}