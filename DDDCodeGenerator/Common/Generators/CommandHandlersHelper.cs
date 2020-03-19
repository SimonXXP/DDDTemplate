using System.Linq;
using System.Text;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class CommandHandlersHelper : BaseHelper
    {
        public CommandHandlersHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }

        public void Create_Code()
        {
            Create_Add_Command();
            //Create_Add_Command_Custom();
            Create_DeleteCommand_Command();
            Create_Batch_DeleteCommand_Command();
            Create_Update_Command();
            //Create_Update_Command_Custom();
        }

        /// <summary>
        /// 创建代码
        /// </summary>
        public void Create_Add_Command()
        {
            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command_handlers);
            string template_file = template_folder + "AddCommandHander.cs";

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
            //template = template.Replace("{{domain_model_id_name}}", NameHelper.Get_Identities_Name(entity_name));
            //template = template.Replace("{{update_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_Update(entity_name));
            
            StringBuilder sb = new StringBuilder();

            #region Exec_Command_Code

            sb.AppendLine("            DomainModels." + NameHelper.Get_Domain_Model_Name(entity_name) + " data = new DomainModels." + NameHelper.Get_Domain_Model_Name(entity_name) + "()");
            sb.AppendLine("            {");
            sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + " = new " + NameHelper.Get_Identities_Name(entity_name) + "(command." + NameHelper.Get_Property_Name_For_Command(entity_id_properties.Name) + "),");
            foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            {
                sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + " = command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ",");
            }
            sb.AppendLine("            };");
            sb.AppendLine("");
            sb.AppendLine("            var entity = DataAccessor." + NameHelper.Get_MethodName_DataAccess_Domain_Add(entity_name) + "(data);");
            sb.AppendLine("            DataAccessor.Save();");
            sb.AppendLine("            command.ExecuteSuccess(new " + NameHelper.Get_Identities_Name(entity_name) + "(entity.id));");

            template = template.Replace("{{Exec_Command_Code}}", sb.ToString());

            #endregion

            #region Validate
            sb = new StringBuilder();

            sb.AppendLine("            var result = new List<BusinessError>();");

            sb.AppendLine("            //---------------------------------------");
            sb.AppendLine("            //Rules");
            sb.AppendLine("            //---------------------------------------");

            foreach (var p in entity_properties)
            {
                switch (p.PropertyType.Name)
                {
                    case "String":
                        sb.AppendLine("            if (string.IsNullOrWhiteSpace(command." + NameHelper.Get_Property_Name_For_Command(p.Name) + "))");
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is required!\"));");
                        sb.AppendLine("            }");
                        break;

                    case "DateTime":
                        if (p.Nullable)
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".HasValue() && !command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".Value.IsValid())");
                        }
                        else
                        {
                            sb.AppendLine("            if (!command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".IsValid())");
                        }
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is invalid!\"));");
                        sb.AppendLine("            }");
                        break;

                    case "Guid":
                        if (p.Nullable)
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".HasValue() && !command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".Value == Guid.Empty)");
                        }
                        else
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + " == Guid.Empty)");
                        }
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is invalid!\"));");
                        sb.AppendLine("            }");
                        break;
                }
            }

            sb.AppendLine("            //---------------------------------------");
            sb.AppendLine("            //Other rules");
            sb.AppendLine("            //---------------------------------------");

            template = template.Replace("{{Validate_Command_Parmeters_Code}}", sb.ToString());
            #endregion

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void Create_Add_Command_Custom()
        {
            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command_handlers);
            string template_file = template_folder + "AddCommandHander_Custom.cs";

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
            //template = template.Replace("{{domain_model_id_name}}", NameHelper.Get_Identities_Name(entity_name));
            //template = template.Replace("{{update_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_Update(entity_name));

            StringBuilder sb = new StringBuilder();

            #region Exec_Command_Code
            sb.AppendLine("            DomainModels." + NameHelper.Get_Domain_Model_Name(entity_name) + " data = new DomainModels." + NameHelper.Get_Domain_Model_Name(entity_name) + "()");
            sb.AppendLine("            {");
            sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + " = new " + NameHelper.Get_Identities_Name(entity_name) + "(command." + NameHelper.Get_Property_Name_For_Command(entity_id_properties.Name) + "),");
            foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            //foreach (var p in entity_properties.ToList())
            {
                sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + " = command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ",");
            }
            sb.AppendLine("            };");
            sb.AppendLine("            DataAccessor." + NameHelper.Get_MethodName_DataAccess_Domain_Add(entity_name) + "(data);");
            sb.AppendLine("            if (!needTransaction)");
            sb.AppendLine("            {");
            sb.AppendLine("                 DataAccessor.Save();");
            sb.AppendLine("             }");
            sb.Append("            command.ExecuteSuccess(data." + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + ");");

            template = template.Replace("{{Exec_Command_Code}}", sb.ToString());
            #endregion

            #region Validate
            sb = new StringBuilder();

            sb.AppendLine("            var result = new List<BusinessError>();");

            sb.AppendLine("            //---------------------------------------");
            sb.AppendLine("            //Rules");
            sb.AppendLine("            //---------------------------------------");

            foreach (var p in entity_properties)
            {
                switch (p.PropertyType.Name)
                {
                    case "String":
                        sb.AppendLine("            if (string.IsNullOrWhiteSpace(command." + NameHelper.Get_Property_Name_For_Command(p.Name) + "))");
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is required!\"));");
                        sb.AppendLine("            }");
                        break;

                    case "DateTime":
                        sb.AppendLine("");
                        if (p.Nullable)
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".HasValue() && !command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".Value.IsValid())");
                        }
                        else
                        {
                            sb.AppendLine("            if (!command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".IsValid())");
                        }
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is invalid!\"));");
                        sb.AppendLine("            }");
                        break;

                    case "Guid":
                        if (p.Nullable)
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".HasValue() && !command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".Value == Guid.Empty)");
                        }
                        else
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + " == Guid.Empty)");
                        }
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is invalid!\"));");
                        sb.AppendLine("            }");
                        break;
                }
            }

            sb.AppendLine("            //---------------------------------------");
            sb.AppendLine("            //Other rules");
            sb.Append("            //---------------------------------------");

            template = template.Replace("{{Validate_Command_Parmeters_Code}}", sb.ToString());
            #endregion

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void Create_DeleteCommand_Command()
        {
            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command_handlers);
            string template_file = template_folder + "DeleteCommandHander.cs";

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

            template = template.Replace("{{domain_model_id_name}}", NameHelper.Get_Identities_Name(entity_name));
            template = template.Replace("{{delete_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_Delete(entity_name));

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void Create_Batch_DeleteCommand_Command()
        {
            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command_handlers);
            string template_file = template_folder + "BatchDeleteCommandHander.cs";

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

            template = template.Replace("{{domain_model_id_name}}", NameHelper.Get_Identities_Name(entity_name));
            template = template.Replace("{{batch_delete_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_BatchDelete(entity_name));

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void Create_Update_Command()
        {

            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command_handlers);
            string template_file = template_folder + "UpdateCommandHander.cs";

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
            //template = template.Replace("{{domain_model_id_name}}", NameHelper.Get_Identities_Name(entity_name));
            //template = template.Replace("{{update_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_Update(entity_name));

            StringBuilder sb = new StringBuilder();

            #region Exec_Command_Code
            sb.AppendLine("            DomainModels." + NameHelper.Get_Domain_Model_Name(entity_name) + " data = new DomainModels." + NameHelper.Get_Domain_Model_Name(entity_name) + "()");
            sb.AppendLine("            {");
            sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + " = new " + NameHelper.Get_Identities_Name(entity_name) + "(command." + NameHelper.Get_Property_Name_For_Command(entity_id_properties.Name) + "),");
            foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            {
                sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + " = command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ",");
            }
            sb.AppendLine("            };");
            sb.AppendLine("            DataAccessor." + NameHelper.Get_MethodName_DataAccess_Domain_Update(entity_name) + "(data);");
            sb.AppendLine("            DataAccessor.Save();");
            sb.Append("            command.ExecuteSuccess(data." + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + ");");

            template = template.Replace("{{Exec_Command_Code}}", sb.ToString());
            #endregion

            #region Validate
            sb = new StringBuilder();

            sb.AppendLine("            var result = new List<BusinessError>();");

            sb.AppendLine("            //---------------------------------------");
            sb.AppendLine("            //Rules");
            sb.AppendLine("            //---------------------------------------");

            foreach (var p in entity_properties)
            {
                switch (p.PropertyType.Name)
                {
                    case "String":
                        sb.AppendLine("            if (string.IsNullOrWhiteSpace(command." + NameHelper.Get_Property_Name_For_Command(p.Name) + "))");
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is required!\"));");
                        sb.AppendLine("            }");
                        break;

                    case "DateTime":
                        if (p.Nullable)
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".HasValue() && !command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".Value.IsValid())");
                        }
                        else
                        {
                            sb.AppendLine("            if (!command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".IsValid())");
                        }
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is invalid!\"));");
                        sb.AppendLine("            }");
                        break;

                    case "Guid":
                        if (p.Nullable)
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".HasValue() && !command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".Value == Guid.Empty)");
                        }
                        else
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + " == Guid.Empty)");
                        }
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is invalid!\"));");
                        sb.AppendLine("            }");
                        break;
                }
            }

            sb.AppendLine("            //---------------------------------------");
            sb.AppendLine("            //Other rules");
            sb.Append("            //---------------------------------------");

            template = template.Replace("{{Validate_Command_Parmeters_Code}}", sb.ToString());
            #endregion

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        public void Create_Update_Command_Custom()
        {
            #region folders
            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_command_handlers);
            string template_file = template_folder + "UpdateCommandHander_Custom.cs";

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
            //template = template.Replace("{{domain_model_id_name}}", NameHelper.Get_Identities_Name(entity_name));
            //template = template.Replace("{{update_method_name}}", NameHelper.Get_MethodName_DataAccess_Domain_Update(entity_name));
            
            StringBuilder sb = new StringBuilder();

            #region Exec_Command_Code
            sb.AppendLine("            DomainModels." + NameHelper.Get_Domain_Model_Name(entity_name) + " data = new DomainModels." + NameHelper.Get_Domain_Model_Name(entity_name) + "()");
            sb.AppendLine("            {");
            sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + " = new " + NameHelper.Get_Identities_Name(entity_name) + "(command." + NameHelper.Get_Property_Name_For_Command(entity_id_properties.Name) + "),");
            foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            {
                sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + " = command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ",");
            }
            sb.AppendLine("            };");
            sb.AppendLine("            DataAccessor." + NameHelper.Get_MethodName_DataAccess_Domain_Update(entity_name) + "(data);");
            sb.AppendLine("            if (!needTransaction)");
            sb.AppendLine("            {");
            sb.AppendLine("                 DataAccessor.Save();");
            sb.AppendLine("             }");
            sb.AppendLine("            command.ExecuteSuccess(data." + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + ");");

            template = template.Replace("{{Exec_Command_Code}}", sb.ToString());
            #endregion

            #region Validate
            sb = new StringBuilder();

            sb.AppendLine("            var result = new List<BusinessError>();");

            sb.AppendLine("            //---------------------------------------");
            sb.AppendLine("            //Rules");
            sb.AppendLine("            //---------------------------------------");

            foreach (var p in entity_properties)
            {
                switch (p.PropertyType.Name)
                {
                    case "String":
                        sb.AppendLine("            if (string.IsNullOrWhiteSpace(command." + NameHelper.Get_Property_Name_For_Command(p.Name) + "))");
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is required!\"));");
                        sb.AppendLine("            }");
                        break;

                    case "DateTime":
                        if (p.Nullable)
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".HasValue() && !command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".Value.IsValid())");
                        }
                        else
                        {
                            sb.AppendLine("            if (!command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".IsValid())");
                        }
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is invalid!\"));");
                        sb.AppendLine("            }");
                        break;
                    case "Guid":
                        if (p.Nullable)
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".HasValue() && !command." + NameHelper.Get_Property_Name_For_Command(p.Name) + ".Value == Guid.Empty)");
                        }
                        else
                        {
                            sb.AppendLine("            if (command." + NameHelper.Get_Property_Name_For_Command(p.Name) + " == Guid.Empty)");
                        }
                        sb.AppendLine("            {");
                        sb.AppendLine("                result.Add(ErrorFactory.CreateFieldIsRequiredError(\"" + NameHelper.Get_Property_Name_For_Command(p.Name) + "\", \"" + NameHelper.Get_Property_Name_For_DisplayInErrorMessage_DomainModel(p.Name) + " is required!\"));");
                        sb.AppendLine("            }");
                        break;
                }
            }

            sb.AppendLine("            //---------------------------------------");
            sb.AppendLine("            //Other rules");
            sb.AppendLine("            //---------------------------------------");

            template = template.Replace("{{Validate_Command_Parmeters_Code}}", sb.ToString());
            #endregion

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }
    }
}