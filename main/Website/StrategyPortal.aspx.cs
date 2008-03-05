using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Collections.Generic;
using AgentStoryComponents;
using Strategy.code;

public partial class StrategyPortal : SessionAwareWebForm
{
    private strategyTable _st = null;

    public strategyTable stratTable
    {
        get
        {
            return _st;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);


        //browser detection?
        string browser = Request["HTTP_USER_AGENT"];
        bool pass = true;

        //mozilla
        //Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.8.1.6) Gecko/20070725 Firefox/2.0.0.6
        if (browser.IndexOf("Firefox") != -1) pass = false;

        //safari
        //Mozilla/5.0 (Windows; U; Windows NT 5.2; en) AppleWebKit/522.15.5 (KHTML, like Gecko) Version/3.0.3 Safari/522.15
        if (browser.IndexOf("Safari") != -1) pass = false;

        //ie
        //Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)

        //Netscape
        //Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.7.5) Gecko/20060912 Netscape/8.1.2
        if (browser.IndexOf("Netscape") != -1) pass = false;
        if (browser.IndexOf("Gecko") != -1) pass = false;


        if (pass == false)
        {
            Response.Write("STRATEGY TABLE VIEWER not tested for this browser, <br> Unfortunately right now, only IE 6 OR 7 on PC is supported. ");
            Response.Write("<br><br><br>");
            Response.Write("Your current browser registers as - ");
            Response.Write(browser);
            Response.End();
            return;
        }


        strategyTableManager stm = new strategyTableManager(config.conn);

        string stratGUID = Request.QueryString["stratGUID"];

        if (stratGUID == null)
        {
            //try get GUID off POST
            stratGUID = Request.Form["hdnGUID"];
            if (stratGUID == null)
            {
                Response.Write("no GUID passed");
                Response.End();
                return;
            }
        }

        

        string action = Request.Form["hdnAction"];
        if (action != null)
        {
            if (action.ToLower() == "save")
            {
                string stJSON64 = Request.Form["hdnST"];
                stm.SaveStrategy(stratGUID, stJSON64);
            }
        }

        _st = stm.GetStrategy(stratGUID);  


        

        

    }
}
