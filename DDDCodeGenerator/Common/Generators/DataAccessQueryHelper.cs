using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class DataAccessQueryHelper : BaseHelper
    {
        public DataAccessQueryHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }

        /// <summary>
        /// 创建代码
        /// </summary>
        public void Create_Code()
        {
            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_data_access_query);
            string template_file = template_folder + "{{entity_sub_namespace}}RequestQueryDbDataAccessor.cs";

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

            // {{Get-Details-Method}}
            template = Create_GetDetails_Code(template);

            template = Create_List_Code(template);

            template = Create_Get_Entity_Code(template);

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        private string Create_GetDetails_Code(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Get " + entity_name + " domain model");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"id\"></param> ");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        public DetailsModel " + NameHelper.Get_MethodName_DataAccess_Query_GetDetails_Name(entity_name) + "(" + NameHelper.Get_Identities_Name(entity_name) + " id)");
            sb.AppendLine("        {");
            sb.AppendLine("            var entity = GetEntity(id);");
            sb.AppendLine("            if (entity == null)");
            sb.AppendLine("            {");
            sb.AppendLine("                return null;");
            sb.AppendLine("            }");
            sb.AppendLine("            else");
            sb.AppendLine("            {");
            sb.AppendLine("                return new DetailsModel()");
            sb.AppendLine("                {");
            foreach (var p in entity_properties)
            {
                //sb.AppendLine("");
                sb.AppendLine("                    " + NameHelper.Get_Property_Name_For_Query_DetailsModel(p.Name) + " = entity." + p.Name + ",");
            }
            sb.AppendLine("                };");
            sb.AppendLine("            }");
            sb.AppendLine("        }");

            template = template.Replace("{{Get-Details-Method}}", sb.ToString());

            return template;
        }

        private string Create_Get_Entity_Code(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Get " + entity_name + " entity");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"domain_object\"></param>");
            sb.AppendLine("        private " + entity_name + " GetEntity(" + NameHelper.Get_Identities_Name(entity_name) + " id)");
            sb.AppendLine("        {");
            sb.AppendLine("            var entity_id = GetPersistId<Guid>(id);");
            sb.AppendLine("            return GetQuery<" + entity_name + ">().FirstOrDefault(x => x." + this.entity_id_properties.Name + " == entity_id);");
            sb.Append("        }");
            
            template = template.Replace("{{Get-Entity-Method}}", sb.ToString());

            return template;
        }

        private string Create_List_Code(string template)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("        public PagingRecords<ListModel> " + NameHelper.Get_MethodName_DataAccess_Query_GetList_Name(entity_name) + "(" + NameHelper.Get_ViewModel_Query_ListParameter_Name(entity_name) + " parameter)");
            sb.AppendLine("        {");
            //sb.AppendLine("");
            sb.AppendLine("            var query = GetQuery<" + entity_name + ">();");
            //sb.AppendLine("");
            sb.AppendLine("            #region Where");
            //sb.AppendLine("");
            sb.AppendLine("            var predicate = PredicateBuilder.New<" + entity_name + ">();");
            
            foreach (var p in entity_properties)
            {
                //sb.AppendLine("");
                if (p.PropertyType == typeof(System.String))
                {
                    sb.AppendLine("            if (!string.IsNullOrWhiteSpace(parameter." + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + "))");
                    sb.AppendLine("            {");
                    sb.AppendLine("                predicate = predicate.And(x => x." + p.Name + ".Contains(parameter." + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + "));");
                    sb.AppendLine("            }");
                }
                else if (p.PropertyType.IsValueType)
                {
                    sb.AppendLine("            if (parameter." + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + ".HasValue)");
                    sb.AppendLine("            {");
                    sb.AppendLine("                predicate = predicate.And(x => x." + p.Name + " == parameter." + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + ".Value); ");
                    sb.AppendLine("            }");
                }
                else if (p.PropertyType.IsEnum)
                {
                    sb.AppendLine("            if (parameter." + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + ".HasValue)");
                    sb.AppendLine("            {");
                    sb.AppendLine("                var v = (int)parameter." + NameHelper.Get_Property_Name_For_ListParameter(p.Name) + ".Value");
                    sb.AppendLine("                predicate = predicate.And(x => x." + p.Name + " == v ");
                    sb.AppendLine("            }");
                }
            }

            //sb.AppendLine("");
            sb.AppendLine("            #endregion");
            //sb.AppendLine("");
            sb.AppendLine("            #region Built SQL");
            //sb.AppendLine("");
            sb.AppendLine("            IQueryable<" + entity_name + "> queryResult = query;");
            //sb.AppendLine("");
            sb.AppendLine("            if (predicate.IsStarted)");
            sb.AppendLine("            {");
            sb.AppendLine("                queryResult = queryResult.Where(predicate);");
            sb.AppendLine("            }");
            //sb.AppendLine("");
            sb.AppendLine("            queryResult = queryResult.OrderByDescending(r => r." + entity_id_properties.Name + ");");
            //sb.AppendLine("");
            sb.AppendLine("            #endregion");
            //sb.AppendLine("");
            //sb.AppendLine("            List<" + entity_name + "> result = queryResult.GetPagingRecords<" + entity_name + ">(parameter.page, parameter.page_size).ToList();");
            sb.AppendLine("            List<" + entity_name + "> result = queryResult.GetPagingRecords(parameter.page, parameter.page_size).ToList();");
            //sb.AppendLine("");
            sb.AppendLine("            var pageList = result.Select(r =>");
            sb.AppendLine("            {");
            //sb.AppendLine("");
            sb.AppendLine("                return new ListModel()");
            sb.AppendLine("                {");
            foreach (var p in entity_properties)
            {
                //sb.AppendLine("");
                sb.AppendLine("                    " + NameHelper.Get_Property_Name_For_Query_ListModel(p.Name) + " = r." + p.Name + ",");
            }
            sb.AppendLine("                };");
            //sb.AppendLine("");
            sb.AppendLine("            });");
            //sb.AppendLine("");
            sb.AppendLine("            return new PagingRecords<ListModel>(pageList, queryResult.Count(), parameter.page, parameter.page_size);");
            //sb.AppendLine("");
            sb.AppendLine("        }");

            template = template.Replace("{{Get-List-Method}}", sb.ToString());

            return template;
        }
    }
}