using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common
{

    public class Settings
    {


        #region 设置键值


        public string entities_namespace { get; set; }

        public string entities_dll_name { get; set; }

        public string code_namespace { get; set; }

        public string folder_result = "/Results";

        public string folder_templates = "/Templates";

        #region templates

        public string template_folder_webapi_controllers { get; set; }

        public string template_folder_view_model { get; set; }

        public string template_folder_data_access_domain { get; set; }

        public string template_folder_data_access_query { get; set; }

        public string template_folder_query_parameters { get; set; }

        public string template_folder_query_models { get; set; }

        public string template_folder_domain_models { get; set; }

        public string template_folder_domain_models_identities { get; set; }

        public string template_folder_data_access_domain_interface { get; set; }

        public string template_folder_data_access_query_interface { get; set; }

        public string template_folder_query_service { get; set; }

        public string template_folder_query_service_interface { get; set; }

        public string template_folder_command { get; set; }

        public string template_folder_command_handlers { get; set; }

        #endregion

        #endregion

        public Settings()
        {

            entities_namespace = GetSetting("entities_namespace");
            code_namespace = GetSetting("code_namespace");
            entities_dll_name = GetSetting("entities_dll_name");

            template_folder_webapi_controllers = GetSetting("template_folder_webapi_controllers");
            template_folder_view_model = GetSetting("template_folder_view_model");
            template_folder_data_access_domain = GetSetting("template_folder_data_access_domain");
            template_folder_data_access_domain_interface = GetSetting("template_folder_data_access_domain_interface");
            template_folder_data_access_query = GetSetting("template_folder_data_access_query");
            template_folder_data_access_query_interface = GetSetting("template_folder_data_access_query_interface");
            template_folder_query_parameters = GetSetting("template_folder_query_parameters");
            template_folder_query_models = GetSetting("template_folder_query_models");
            template_folder_domain_models = GetSetting("template_folder_domain_models");
            template_folder_domain_models_identities = GetSetting("template_folder_domain_models_identities");
            template_folder_query_service = GetSetting("template_folder_query_service");
            template_folder_query_service_interface = GetSetting("template_folder_query_service_interface");
            template_folder_command = GetSetting("template_folder_command");
            template_folder_command_handlers = GetSetting("template_folder_command_handlers");

        }

        private string GetSetting(string key)
        {

            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                return "";
            }

        }

    }

}