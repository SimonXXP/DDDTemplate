using ShineTechQD.DDDCodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common.Generators
{
    public class BaseHelper
    {

        #region MyRegion

        public  EntityHelper entity_helper { get; set; }

        public Settings settings { get; set; }

        public string enetity_name_and_namespace { get; set; }


        public Type entity { get; set; }

        public List<EntityProperty> entity_properties { get; set; }

        public EntityProperty entity_id_properties { get; set; }

        public HttpContext context { get; set; }

        public string entity_name { get; set; }

        public string entity_sub_namespace { get; set; }

        #endregion

        public void IntParmaters(string _enetity_name_and_namespace)
        {

            context = HttpContext.Current;

            settings = new Settings();
            entity_helper = new EntityHelper();

            enetity_name_and_namespace = _enetity_name_and_namespace;

            entity = entity_helper.GetEntityType(enetity_name_and_namespace);
            entity_name = entity.Name;
            entity_sub_namespace = NameHelper.GetSubNameSpace(entity_name);
            entity_properties = entity_helper.GetEntityProperties(entity);
            entity_id_properties = entity_helper.GetPrimaryKey(entity_properties);

        }

    }
}