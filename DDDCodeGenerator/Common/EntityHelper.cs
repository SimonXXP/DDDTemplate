using ShineTechQD.DDDCodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common
{

    public class EntityHelper
    {

        Settings settings { get; set; }

        public EntityHelper()
        {
            settings = new Settings();
        }

        /// <summary>
        /// 查找dll里面的实体类型
        /// 依赖web.config的appSettings里面的以下两个设置：
        /// entities_dll_name
        /// entities_namespace
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllEntyNames()
        {
            List<string> ol = new List<string>();

            var classes = Assembly.Load(settings.entities_dll_name).GetTypes();

            foreach (var c in classes)
            {
                if (c.Namespace != null && c.Namespace.StartsWith(settings.entities_namespace) && !c.FullName.EndsWith("Entities"))
                {
                    ol.Add(c.FullName);
                }
            }

            return ol;
        }

        /// <summary>
        /// 按照名字查找类型，并返回类型
        /// </summary>
        /// <param name="entity_name"></param>
        /// <returns></returns>
        public Type GetEntityType(string entity_name)
        {
            Type t = null;
            var classes = Assembly.Load(settings.entities_dll_name).GetTypes();

            foreach (var c in classes)
            {
                if (c.FullName == entity_name)
                {
                    t = c;
                }
            }

            return t;
        }

        /// <summary>
        /// 列出实体的属性列表
        /// 包括值类型，字符串类型，Enum类型
        /// 其他类型不出现在列表中，请生成代码后检查代码
        /// </summary>
        /// <param name="entity_name"></param>
        /// <returns></returns>
        public List<EntityProperty> GetEntityProperties(string entity_name)
        {
            var type = GetEntityType(entity_name);
            return GetEntityProperties(type);
        }

        /// <summary>
        /// 列出实体的属性列表
        /// 包括值类型，字符串类型，Enum类型
        /// 其他类型不出现在列表中，请生成代码后检查代码
        /// </summary>
        /// <param name="entity_type"></param>
        /// <returns></returns>
        public List<EntityProperty> GetEntityProperties(Type entity_type)
        {
            List<EntityProperty> ol = new List<EntityProperty>();
            var type = entity_type;
            if (type != null)
            {
                //获取Entity的属性和数据类型
                Object obj = Activator.CreateInstance(type);

                var PropertyList = type.GetProperties().Where(x => x.MemberType == MemberTypes.Property).ToList();
                foreach (PropertyInfo item in PropertyList)
                {
                    var item_type = item.PropertyType;
                    if (item_type.IsValueType || item_type == typeof(System.String) || item_type.IsEnum)
                    {

                        //bool is_primary_key = item_type.CustomAttributes.Where(x => x.AttributeType == typeof(KeyAttribute)).FirstOrDefault() == null ? false : true;
                        bool is_primary_key = item.CustomAttributes.Where(x => x.AttributeType == typeof(KeyAttribute)).FirstOrDefault() == null ? false : true;


                        ol.Add(new EntityProperty()
                        {
                            IsPrimaryKey = is_primary_key,
                            Name = item.Name,
                            PropertyType = item.PropertyType,
                            Nullable = (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        });
                    }
                }
            }

            return ol;
        }

        /// <summary>
        /// 获取实体类的默认主键
        /// 规则是 1）不能为null， 2）默认的第一个值属性为主键
        /// 无法确认entity的哪个属性是主键，所以默认把第一个属性作为主键，请生成代码后检查这里
        /// </summary>
        /// <param name="enetity_properties"></param>
        /// <returns></returns>
        public EntityProperty GetPrimaryKey(List<EntityProperty> enetity_properties)
        {
            EntityProperty key = enetity_properties.Where(x => x.IsPrimaryKey).FirstOrDefault();
            if (key == null)
            {
                return enetity_properties.Where(x => x.PropertyType.IsValueType && x.Nullable == false).FirstOrDefault();
            }
            else
            {
                return key;
            }
        }

    }

}