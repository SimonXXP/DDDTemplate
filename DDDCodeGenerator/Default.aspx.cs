using ShineTechQD.DDDCodeGenerator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShineTechQD.DDDCodeGenerator
{

    public partial class Default : System.Web.UI.Page
    {

        Settings settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                settings = new Settings();
                BindCtrls();
            }
        }

        void BindCtrls()
        {
            EntityHelper entity_helper = new Common.EntityHelper();
            var ol = entity_helper.GetAllEntyNames().Select(x => new { Text = x, Value = x });
            cb_entities.DataSource = ol;
            cb_entities.DataTextField = "Text";
            cb_entities.DataValueField = "Value";
            cb_entities.DataBind();
        }

        protected void btn_create_code(object sender, EventArgs e)
        {

            #region 0. 清空代码文件夹

            Settings settings = new Settings();
            var result_folder = Server.MapPath(settings.folder_result);
            FileHelper.ReCreateFolder(result_folder);

            #endregion

            l_msg.Text = "";
            foreach (ListItem item in cb_entities.Items)
            {
                if (item.Selected == true)
                {
                    var class_name = item.Value;
                    Generator g = new Common.Generator(class_name);
                    g.CreateCode();
                }
            }
            l_msg.Text = "完成";
        }

        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_all.Checked)
            {
                foreach (ListItem item in cb_entities.Items)
                {
                    item.Selected = true;
                }
            }else
            {
                foreach (ListItem item in cb_entities.Items)
                {
                    item.Selected = false;
                }
            }
        }
    }

}