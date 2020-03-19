using System.Linq;
using System.Text;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class DataAccessDomainHelper : BaseHelper
    {

        public DataAccessDomainHelper(string _enetity_name_and_namespace)
        {
            IntParmaters(_enetity_name_and_namespace);
        }


        /// <summary>
        /// 创建代码
        /// </summary>
        public void Create_Code()
        {
            #region folders

            string template_folder = context.Server.MapPath(settings.folder_templates + settings.template_folder_data_access_domain);
            string template_file = template_folder + "{{entity_sub_namespace}}DomainDbDataAccessor.cs";

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


            template = Create_Update_Code(template);

            template = Create_Delete_Code(template);

            template = Create_Batch_Delete_Code(template);

            template = Create_Add_Code(template);

            template = Create_Get_Entity_Code(template);

            template = Create_Convert_To_Domain_Model_Code(template);

            FileHelper.CheckAndCreateFolder(target_folder);
            FileHelper.WriteAll(target_file, template);
        }

        private string Create_Update_Code(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Update " + entity_name + " details");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"domain_object\"></param>");
            sb.AppendLine("        public void " + NameHelper.Get_MethodName_DataAccess_Domain_Update(entity_name) + "(" + NameHelper.Get_Domain_Model_Name(entity_name) + " domain_model)");
            sb.AppendLine("        {");
            sb.AppendLine("            var entity = GetEntity(domain_model." + NameHelper.Get_Property_Name_For_DomainModels(this.entity_id_properties.Name) + ");");
            sb.AppendLine("            if (entity == null)");
            sb.AppendLine("            {");
            sb.AppendLine("                return;");
            sb.AppendLine("            }");
            foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            {
                //sb.AppendLine("");
                sb.AppendLine("            entity." + p.Name + " = domain_model." + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + ";");
            }

            sb.Append("        }");

            template = template.Replace("{{Update-Method}}", sb.ToString());

            return template;
        }

        private string Create_Delete_Code(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Delete a(n) " + entity_name);
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"id\"></param>");
            sb.AppendLine("        public void " + NameHelper.Get_MethodName_DataAccess_Domain_Delete(entity_name) + "(" + NameHelper.Get_Identities_Name(entity_name) + " id)");
            sb.AppendLine("        {");

            sb.AppendLine("            var entity = GetEntity(id);");
            sb.AppendLine("            if (entity != null)");
            sb.AppendLine("            {");
            sb.AppendLine("                GetDbSet<" + entity_name + ">().Remove(entity);");
            sb.AppendLine("            }");

            sb.Append("        }");

            template = template.Replace("{{Delete-Method}}", sb.ToString());

            return template;
        }

        private string Create_Batch_Delete_Code(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Delete a(n) " + entity_name);
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"id\"></param>");
            sb.AppendLine("        public void " + NameHelper.Get_MethodName_DataAccess_Domain_BatchDelete(entity_name) + "(" + NameHelper.Get_Identities_Name(entity_name) + "[] ids)");
            sb.AppendLine("        {");

            sb.AppendLine("             var entity_ids = ids.Select(x => (Guid)x.GetPersistId()).ToArray();");
            sb.AppendLine("             var entities = GetDbSet<" + entity_name + ">().Where(x => entity_ids.Contains(x.id));");
            sb.AppendLine("             GetDbSet<" + entity_name + ">().RemoveRange(entities);");
            //sb.AppendLine("             foreach (var item in entities)");
            //sb.AppendLine("             {");
            //sb.AppendLine("                 GetDbSet<" + entity_name + ">().Remove(item);");
            //sb.AppendLine("             }");

            sb.Append("        }");

            template = template.Replace("{{Batch-Delete-Method}}", sb.ToString());

            return template;
        }

        private string Create_Add_Code(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Add a new " + entity_name + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"domain_object\"></param>");
            sb.AppendLine("        public " + entity_name + " " + NameHelper.Get_MethodName_DataAccess_Domain_Add(entity_name) + "(" + NameHelper.Get_Domain_Model_Name(entity_name) + " domain_model)");
            sb.AppendLine("        {");
            sb.AppendLine("            var entity = new " + entity_name + "()");
            sb.AppendLine("            {");
            sb.AppendLine("                 " + entity_id_properties.Name + " = (Guid)domain_model." + NameHelper.Get_Property_Name_For_DomainModels(entity_id_properties.Name) + ".GetPersistId(),");
            foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            {
                //sb.AppendLine("");
                sb.AppendLine("                 " + p.Name + " = domain_model." + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + ",");
            }
            sb.AppendLine("            };");
            sb.AppendLine("            GetDbSet<" + entity_name + ">().Add(entity);");
            sb.AppendLine("            return entity;");
            sb.Append("        }");

            template = template.Replace("{{Add-Method}}", sb.ToString());

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
            //sb.AppendLine("            return GetQuery<" + entity_name + ">().FirstOrDefault(x => x." + this.entity_id_properties.Name + " == entity_id);");
            sb.AppendLine("            return GetQuery<" + entity_name + ">().FirstOrDefault(x => x.id == entity_id);");
            sb.Append("        }");

            template = template.Replace("{{Get-Entity-Method}}", sb.ToString());

            return template;
        }

        private string Create_Convert_To_Domain_Model_Code(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Convert entity to domain model");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"entity\"></param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        private static " + NameHelper.Get_Domain_Model_Name(entity_name) + " ConvertTo" + NameHelper.Get_Domain_Model_Name(entity_name) + "(" + entity_name + " entity)");
            sb.AppendLine("        {");
            sb.AppendLine("            return new " + NameHelper.Get_Domain_Model_Name(entity_name) + "()");
            sb.AppendLine("            {");
            sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(this.entity_id_properties.Name) + " = new " + NameHelper.Get_Identities_Name(entity_name) + "(entity." + this.entity_id_properties.Name + "),");
            foreach (var p in entity_properties.Where(x => x.Name != entity_id_properties.Name).ToList())
            {
                //sb.AppendLine("");
                sb.AppendLine("                " + NameHelper.Get_Property_Name_For_DomainModels(p.Name) + " = entity." + p.Name + ",");
            }
            sb.AppendLine("            };");
            sb.Append("        }");

            template = template.Replace("{{Convert-To-Domain-Model-Method}}", sb.ToString());

            return template;
        }
    }
}