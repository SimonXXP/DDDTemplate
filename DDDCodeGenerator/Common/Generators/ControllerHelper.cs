using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class ControllerHelper : BaseHelper
    {
        public ControllerHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }

        #region Create_Controller_Code
        /// <summary>
        /// 创建Controllers代码
        /// </summary>
        public void Create_Code()
        {
            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_webapi_controllers);
            string template_file = template_folder + "{{entity_sub_namespace}}Controller.cs";
            
            string target_folder = template_folder.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));
            string target_file = template_file.Replace(context.Server.MapPath(settings.folder_templates), context.Server.MapPath(settings.folder_result));
            
            //string target_folder = context.Server.MapPath(settings.folder_result) + "\\" + entity_sub_namespace + "Controller";
            //string target_file = target_folder + "\\" + entity_sub_namespace + "Controller.cs";

            target_folder = target_folder.Replace("{{namespace}}", settings.code_namespace);
            target_folder = target_folder.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            target_file = target_file.Replace("{{namespace}}", settings.code_namespace);
            target_file = target_file.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            string template = FileHelper.ReadAll(template_file);

            template = template.Replace("{{namespace}}", settings.code_namespace);
            template = template.Replace("{{entity_name}}", entity_name);
            template = template.Replace("{{entity_sub_namespace}}", entity_sub_namespace);

            template = Create_Controller_Code_Add(template);
            template = Create_Controller_Code_Delete(template);
            template = Create_Controller_Code_Batch_Delete(template);
            template = Create_Controller_Code_Update(template);
            template = Create_Controller_Code_Get(template);
            template = Create_Controller_Code_Details(template);

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        private string Create_Controller_Code_Add(string template)
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Add a new " + entity_name);
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [Route(\"api/"+ entity_sub_namespace + "/Post\")]");
            sb.AppendLine("        [PermissionVerify(new CommonEnum.Operations[] { CommonEnum.Operations.Setup })]");
            sb.AppendLine("        [HttpPost]");
            sb.AppendLine("        public IHttpActionResult Post(" + NameHelper.Get_ViewModel_AddModel(entity_name) + " model)");
            sb.AppendLine("        {");
            sb.AppendLine("             var userInfo = WebData.GetSimpleLoginedUserInfo(LoginUser);");
            sb.AppendLine("             if (userInfo == null) { return Json(new ApiActionResult() { Success = false, Message = \"登录信息失效，请重新登录\" }); }");

            sb.AppendLine("             var command = new AddCommand()");
            sb.AppendLine("             {");
            //foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            foreach (var p in entity_properties.ToList())
            {
                if ("Organization_Id".Equals(NameHelper.Get_Property_Name_For_Command(p.Name)))
                {
                    sb.AppendLine("                " + NameHelper.Get_Property_Name_For_Command(p.Name) + " = userInfo.OrganizationId.Value,");
                }
                else if ("Created_Time".Equals(NameHelper.Get_Property_Name_For_Command(p.Name)))
                {
                    sb.AppendLine("                " + NameHelper.Get_Property_Name_For_Command(p.Name) + " = DateTime.Now,");
                }
                else if ("Created_User_Id".Equals(NameHelper.Get_Property_Name_For_Command(p.Name))) {
                    sb.AppendLine("                " + NameHelper.Get_Property_Name_For_Command(p.Name) + " = userInfo.Id,");
                }
                else if ("Id".Equals(NameHelper.Get_Property_Name_For_Command(p.Name)))
                {
                    sb.AppendLine("                " + NameHelper.Get_Property_Name_For_Command(p.Name) + " = Guid.NewGuid(),");
                }
                else
                {
                    sb.AppendLine("                " + NameHelper.Get_Property_Name_For_Command(p.Name) + " = model." + NameHelper.Get_Property_Name_For_ViewModel(p.Name) + ",");
                }
            }
            sb.AppendLine("            };");
            sb.AppendLine("            CommandBus.Send(command);");
            //sb.AppendLine("");
            sb.AppendLine("            if (NeedRecordSystemLog(Operations.Create)) ");
            sb.AppendLine("            {");
            sb.AppendLine("                 RecordSystemLog(null, JsonConvert.SerializeObject(command), \"All\", Operations.Create);");
            sb.AppendLine("            }");
            //sb.AppendLine("");
            sb.AppendLine("            return CommandResult(command.ExecuteResult);");
            sb.Append("        }");

            template = template.Replace("{{Add-Actions}}", sb.ToString());

            return template;
        }

        private string Create_Controller_Code_Delete(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Delete selected " + entity_name);
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"id\"></param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [Route(\"api/" + entity_sub_namespace + "/Delete\")]");
            sb.AppendLine("        [PermissionVerify(new CommonEnum.Operations[] { CommonEnum.Operations.Delete })]");
            sb.AppendLine("        [HttpDelete]");
            sb.AppendLine("        public IHttpActionResult Delete(" + NameHelper.Get_ViewModel_DeleteModel(entity_name) + " model)");
            sb.AppendLine("        {");
            sb.AppendLine("            var command = new DeleteCommand() { ID = model.id };");
            sb.AppendLine("");
            sb.AppendLine("            CommandBus.Send(command);");
            sb.AppendLine("            if (NeedRecordSystemLog(Operations.Delete)) ");
            sb.AppendLine("            {");
            sb.AppendLine("                 RecordSystemLog(JsonConvert.SerializeObject(command), null, \"All\", Operations.Delete);");
            sb.AppendLine("            }");
            sb.AppendLine("            return CommandResult(command.ExecuteResult);");
            sb.Append("        }");

            template = template.Replace("{{Delete-Actions}}", sb.ToString());

            return template;
        }

        private string Create_Controller_Code_Batch_Delete(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Delete selected " + entity_name);
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"id\"></param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [Route(\"api/" + entity_sub_namespace + "/BatchDelete\")]");
            sb.AppendLine("        [PermissionVerify(new CommonEnum.Operations[] { CommonEnum.Operations.Delete })]");
            sb.AppendLine("        [HttpDelete]");
            sb.AppendLine("        public IHttpActionResult BatchDelete(" + NameHelper.Get_ViewModel_BatchDeleteModel(entity_name) + " model)");
            sb.AppendLine("        {");
            sb.AppendLine("            var command = new BatchDeleteCommand() { IDs = model.ids };");
            sb.AppendLine("");
            sb.AppendLine("            CommandBus.Send(command);");
            sb.AppendLine("            if (NeedRecordSystemLog(Operations.Delete)) ");
            sb.AppendLine("            {");
            sb.AppendLine("                 RecordSystemLog(JsonConvert.SerializeObject(command), null, \"All\", Operations.Delete);");
            sb.AppendLine("            }");
            sb.AppendLine("            return CommandResult(command.ExecuteResult);");
            sb.Append("        }");

            template = template.Replace("{{Batch-Delete-Actions}}", sb.ToString());

            return template;
        }

        private string Create_Controller_Code_Update(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Update a(n) " + entity_name + " details");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [Route(\"api/" + entity_sub_namespace + "/Put\")]");
            sb.AppendLine("        [PermissionVerify(new CommonEnum.Operations[] { CommonEnum.Operations.Setup })]");
            sb.AppendLine("        [HttpPut]");
            sb.AppendLine("        public IHttpActionResult Put(" + NameHelper.Get_ViewModel_UpdateModel(entity_name) + " model)");
            sb.AppendLine("        {");
            sb.AppendLine("            " + NameHelper.Get_Identities_Name(entity_name) + " " + entity_id_properties.Name.ToString() + " = new " + NameHelper.Get_Identities_Name(entity_name) + "(model.id);");
            sb.AppendLine("            var oldValue = QueryService." + NameHelper.Get_MethodName_QueryService_GetDetails_Name(entity_name) + "(" + entity_id_properties.Name.ToString() + ");");
            sb.AppendLine("            var command = new UpdateCommand()");
            sb.AppendLine("            {");
            foreach (var p in entity_properties)
            {
                if (NameHelper.Get_Property_Name_For_Command(p.Name).Equals("Created_Time")
                    || NameHelper.Get_Property_Name_For_Command(p.Name).Equals("Created_User_Id")
                    || NameHelper.Get_Property_Name_For_Command(p.Name).Equals("Last_Updated_Time")
                    || NameHelper.Get_Property_Name_For_Command(p.Name).Equals("Last_Updated_User_Id"))
                    break;

                if (NameHelper.Get_Property_Name_For_Command(p.Name).Equals("Organization_Id"))
                {
                    sb.AppendLine("                " + NameHelper.Get_Property_Name_For_Command(p.Name) + " = oldValue." + NameHelper.Get_Property_Name_For_ViewModel(p.Name) + ",");
                }
                else
                {
                    sb.AppendLine("                " + NameHelper.Get_Property_Name_For_Command(p.Name) + " = model." + NameHelper.Get_Property_Name_For_ViewModel(p.Name) + ",");
                }
            }
            sb.AppendLine("            };");
            //sb.AppendLine("");
            sb.AppendLine("            command.Created_Time = oldValue.created_time;");
            sb.AppendLine("            command.Created_User_Id = oldValue.created_user_id;");
            sb.AppendLine("            command.Last_Updated_Time = DateTime.Now;");
            sb.AppendLine("            command.Last_Updated_User_Id = WebData.LoginedUser.UserId;");
            //sb.AppendLine("");
            sb.AppendLine("            CommandBus.Send(command);");
            //sb.AppendLine("");
            sb.AppendLine("            var properties = string.Empty;");
            foreach (var p in entity_properties)
            {
                sb.AppendLine("            if(oldValue."+ NameHelper.Get_Property_Name_For_ViewModel(p.Name) + " != model."+ NameHelper.Get_Property_Name_For_ViewModel(p.Name) + ")");
                sb.AppendLine("            {");
                sb.AppendLine("                 properties += \""+ NameHelper.Get_Property_Name_For_ViewModel(p.Name) + ";\";");
                sb.AppendLine("            }");
            }

            //sb.AppendLine("");
            sb.AppendLine("            if (!properties.IsNullOrEmpty()) { properties.Substring(0, properties.Length - 1); }");
            //sb.AppendLine("");
            sb.AppendLine("            if (NeedRecordSystemLog(Operations.Update)) ");
            sb.AppendLine("            {");
            sb.AppendLine("                 RecordSystemLog(JsonConvert.SerializeObject(command), null, properties, Operations.Delete);");
            sb.AppendLine("            }");

            sb.AppendLine("            return CommandResult(command.ExecuteResult);");
            sb.Append("        }");

            template = template.Replace("{{Update-Actions}}", sb.ToString());
            return template;
        }

        private string Create_Controller_Code_Get(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Get " + entity_name + " list");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"model\"></param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [Route(\"api/" + entity_sub_namespace + "/Get\")]");
            sb.AppendLine("        [PermissionVerify(new CommonEnum.Operations[] { CommonEnum.Operations.Read, Operations.Detail })]");
            sb.AppendLine("        [HttpGet]");
            sb.AppendLine("        public IHttpActionResult Get([FromUri]" + NameHelper.Get_ViewModel_Query_ListParameter_Name(entity_name) + " model)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (model == null)");
            sb.AppendLine("            {");
            sb.AppendLine("                model = new " + NameHelper.Get_ViewModel_Query_ListParameter_Name(entity_name) + "();");
            sb.AppendLine("            }");
            //sb.AppendLine("");
            sb.AppendLine("            var userInfo = WebData.GetSimpleLoginedUserInfo(LoginUser);");
            sb.AppendLine("             if (userInfo == null) { return Json(new ApiActionResult() { Success = false, Message = \"登录信息失效，请重新登录\" }); }");
            sb.AppendLine("");
            sb.AppendLine("            model.organization_id = userInfo.OrganizationId;");
            //sb.AppendLine("");
            sb.AppendLine("            var queryResult = QueryService." + NameHelper.Get_MethodName_QueryService_GetList_Name(entity_name) + "(model);");
            sb.AppendLine("            return Json(queryResult);");
            sb.Append("        }");

            template = template.Replace("{{Get-Actions}}", sb.ToString());
            return template;

        }

        private string Create_Controller_Code_Details(string template)
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Get " + entity_name + " detail");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"id\"></param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [Route(\"api/" + entity_sub_namespace + "/Details\")]");
            sb.AppendLine("        [PermissionVerify(CommonEnum.Operations.Read)]");
            sb.AppendLine("        [HttpGet]");
            sb.AppendLine("        public IHttpActionResult Get(Guid modelId)");
            sb.AppendLine("        {");
            sb.AppendLine("            " + NameHelper.Get_Identities_Name(entity_name) + " " + entity_id_properties.Name.ToString() + " = new " + NameHelper.Get_Identities_Name(entity_name) + "(modelId);");
            sb.AppendLine("            var queryResult = QueryService." + NameHelper.Get_MethodName_QueryService_GetDetails_Name(entity_name) + "(" + entity_id_properties.Name.ToString() + ");");
            sb.AppendLine("            if (queryResult != null)");
            sb.AppendLine("            {");
            sb.AppendLine("                return Json(queryResult);");
            sb.AppendLine("            }");
            sb.AppendLine("            return Json(string.Empty);");
            sb.Append("        }");

            template = template.Replace("{{Details-Actions}}", sb.ToString());
            return template;
        }
        #endregion

    }
}