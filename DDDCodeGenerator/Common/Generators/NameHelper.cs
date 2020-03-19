using ShineTechQD.DDDCodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class NameHelper
    {



        public static string Get_Identities_Name(string property_name)
        {
            return GetSubNameSpace(property_name) + "ID";
        }

        public static string Get_Domain_Model_Name(string entity_name)
        {
            string new_name = "";
            foreach (var word in GetNameArray(entity_name))
            {
                new_name += word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
            return new_name;
        }

        #region ViewModel Query

        #endregion
        public static string Get_ViewModel_Query_ListParameter_Name(string entity_name)
        {
            return GetSubNameSpace(entity_name) + "_ListParameter";
        }

        #region ViewModel

        public static string Get_ViewModel_AddModel(string entity_name)
        {
            return GetSubNameSpace(entity_name) + "_AddModel";
        }

        public static string Get_ViewModel_UpdateModel(string entity_name)
        {
            return GetSubNameSpace(entity_name) + "_UpdateModel";
        }

        public static string Get_ViewModel_DeleteModel(string entity_name)
        {
            return GetSubNameSpace(entity_name) + "_DeleteModel";
        }
        public static string Get_ViewModel_BatchDeleteModel(string entity_name)
        {
            return GetSubNameSpace(entity_name) + "_BatchDeleteModel";
        }
        #endregion

        #region Method 名称

        #region QueryService

        public static string Get_MethodName_QueryService_GetDetails_Name(string entity_name)
        {
            return Get_MethodName_Base(entity_name) + "_Details";
        }

        public static string Get_MethodName_QueryService_GetList_Name(string entity_name)
        {
            return Get_MethodName_Base(entity_name) + "_List";
        }

        #endregion

        #region DataAccessQuery

        public static string Get_MethodName_DataAccess_Query_GetDetails_Name(string entity_name)
        {
            return Get_MethodName_Base(entity_name) + "_Details";
        }

        public static string Get_MethodName_DataAccess_Query_GetList_Name(string entity_name)
        {
            return Get_MethodName_Base(entity_name) + "_List";
        }

        #endregion

        #region DataAccessDomain

        public static string Get_MethodName_DataAccess_Domain_Update(string entity_name)
        {
            return Get_MethodName_Base(entity_name) + "_Update";
        }

        public static string Get_MethodName_DataAccess_Domain_Add(string entity_name)
        {
            return Get_MethodName_Base(entity_name) + "_Add";
        }

        public static string Get_MethodName_DataAccess_Domain_Delete(string entity_name)
        {
            return Get_MethodName_Base(entity_name) + "_Delete";
        }
        
        public static string Get_MethodName_DataAccess_Domain_BatchDelete(string entity_name)
        {
            return Get_MethodName_Base(entity_name) + "_BatchDelete";
        }
        #endregion

        private static string Get_MethodName_Base(string entity_name)
        {
            //每个单词用_分割，每个单词第一个字母大写
            string new_name = "";
            foreach (var word in GetNameArray(entity_name))
            {
                new_name += "_" + word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
            return new_name.Substring(1);
        }

        #endregion

        #region 命名空间名称

        public static string GetSubNameSpace(string entity_name)
        {
            string new_name = "";
            foreach (var word in GetNameArray(entity_name))
            {
                new_name += word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
            return new_name;
        }

        #endregion

        #region 属性（字段）名称

        public static string Get_Property_Name_For_ViewModel(string property_name)
        {
            //每个单词用_分割，字符全部小写
            return property_name.ToLower();
        }

        public static string Get_Property_Name_For_Command(string property_name)
        {
            //每个单词用_分割，每个单词第一个字母大写
            string new_name = "";
            foreach (var word in GetNameArray(property_name))
            {
                new_name += "_" + word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
            return new_name.Substring(1);
        }

        public static string Get_Property_Name_For_ListParameter(string property_name)
        {
            //每个单词用_分割，字符全部小写
            return property_name.ToLower();
        }

        public static string Get_Property_Name_For_Query_DetailsModel(string property_name)
        {
            //每个单词用_分割，字符全部小写
            return property_name.ToLower();
        }

        public static string Get_Property_Name_For_Query_ListModel(string property_name)
        {
            //每个单词用_分割，字符全部小写
            return property_name.ToLower();
        }

        public static string Get_Property_Name_For_DomainModels(string property_name)
        {
            //每个单词用_分割，每个单词第一个字母大写
            string new_name = "";
            foreach (var word in GetNameArray(property_name))
            {
                new_name += "_" + word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
            return new_name.Substring(1);
        }

        public static string Get_Property_Name_For_DisplayInErrorMessage_DomainModel(string property_name)
        {
            //每个单词用_分割，每个单词第一个字母大写
            string new_name = "";
            foreach (var word in GetNameArray(property_name))
            {
                new_name += " " + word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
            new_name= new_name.Substring(1);
            return new_name.Substring(0, 1).ToUpper() + new_name.Substring(1);
        }

        private static string[] GetNameArray(string name)
        {
            return name.Split('_');
        }

        #endregion

        #region Get Value type 

        public static string GetEntityPropertyTypeString(EntityProperty property)
        {
            if (property.Nullable)
            {
                return ConvertTypeToStringName(property.PropertyType.GenericTypeArguments[0]) + (property.Nullable ? "?" : "");
            }
            else
            {
                return ConvertTypeToStringName(property.PropertyType);
            }
        }

        public static string GetListParameterTypeString(EntityProperty property)
        {
            string type_name = property.PropertyType.Name;
            if (type_name == "System.String" || type_name == "String")
            {
                return "string";
            }
            else if (property.Nullable)
            {
                return ConvertTypeToStringName(property.PropertyType.GenericTypeArguments[0]) + "?";
            }
            else
            {
                return ConvertTypeToStringName(property.PropertyType) + "?";
            }
        }

        private static string ConvertTypeToStringName(Type type)
        {
            switch (type.Name)
            {
                case "Int64":
                    return "long";

                case "Boolean":
                    return "bool";

                case "String":
                    return "string";

                case "Guid":
                    return "Guid";
                default:
                    return type.Name;
            }
        }

        #endregion

    }
}