using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class AdminACC_ChartOfAccountLabel2Display : System.Web.UI.Page
{
    
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        
        hfaCC_ChartOfAccountLabel2ID.Value = id.ToString();
        showACC_ChartOfAccountLabel2Data();
        btnAdd.Visible = false;
        btnUpdate.Visible = true;
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ACC_ChartOfAccountLabel2Manager.DeleteACC_ChartOfAccountLabel2(Convert.ToInt32(linkButton.CommandArgument));
        showACC_ChartOfAccountLabel2Grid();
    }

    private void showACC_ChartOfAccountLabel2Grid()
    {
        if (ddlStatusSearch.SelectedValue == "0")
        {
            gvACC_ChartOfAccountLabel2.DataSource = ACC_ChartOfAccountLabel2Manager.GetAllACC_ChartOfAccountLabel2s();
        }
        else
        {
            gvACC_ChartOfAccountLabel2.DataSource = ACC_ChartOfAccountLabel2Manager.GetAllACC_ChartOfAccountLabel2s().FindAll(x => (x.RowStatusID.ToString() == ddlStatusSearch.SelectedValue));
        }
        gvACC_ChartOfAccountLabel2.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadACC_ChartOfAccountLabel1();
            loadRowStatus();

            btnUpdate.Visible = false;
            btnAdd.Visible = true;

            showACC_ChartOfAccountLabel2Grid();
        }
    }

    private Login getLogin()
    {
        Login login = new Login();
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

            login = (Login)Session["Login"];
        }
        catch (Exception ex)
        { }

        return login;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2 = new ACC_ChartOfAccountLabel2();

        aCC_ChartOfAccountLabel2.Code = txtCode.Text;
        aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID = Int32.Parse(ddlACC_ChartOfAccountLabel1.SelectedValue);
        aCC_ChartOfAccountLabel2.ChartOfAccountLabel2Text = txtChartOfAccountLabel2Text.Text;
        aCC_ChartOfAccountLabel2.ExtraField1 = txtExtraField1.Text;
        aCC_ChartOfAccountLabel2.ExtraField2 = txtExtraField2.Text;
        aCC_ChartOfAccountLabel2.ExtraField3 = txtExtraField3.Text;
        aCC_ChartOfAccountLabel2.AddedBy = getLogin().LoginID;
        aCC_ChartOfAccountLabel2.AddedDate = DateTime.Now;
        aCC_ChartOfAccountLabel2.UpdatedBy = getLogin().LoginID;
        aCC_ChartOfAccountLabel2.UpdatedDate = DateTime.Now;
        aCC_ChartOfAccountLabel2.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ACC_ChartOfAccountLabel2Manager.InsertACC_ChartOfAccountLabel2(aCC_ChartOfAccountLabel2);
        showACC_ChartOfAccountLabel2Grid();
        btnClear_Click(this, new EventArgs());
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2 = new ACC_ChartOfAccountLabel2();
        aCC_ChartOfAccountLabel2 = ACC_ChartOfAccountLabel2Manager.GetACC_ChartOfAccountLabel2ByID(Int32.Parse(hfaCC_ChartOfAccountLabel2ID.Value));
        ACC_ChartOfAccountLabel2 tempACC_ChartOfAccountLabel2 = new ACC_ChartOfAccountLabel2();
        tempACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID = aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID;

        tempACC_ChartOfAccountLabel2.Code = txtCode.Text;
        tempACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID = Int32.Parse(ddlACC_ChartOfAccountLabel1.SelectedValue);
        tempACC_ChartOfAccountLabel2.ChartOfAccountLabel2Text = txtChartOfAccountLabel2Text.Text;
        tempACC_ChartOfAccountLabel2.ExtraField1 = txtExtraField1.Text;
        tempACC_ChartOfAccountLabel2.ExtraField2 = txtExtraField2.Text;
        tempACC_ChartOfAccountLabel2.ExtraField3 = txtExtraField3.Text;
        tempACC_ChartOfAccountLabel2.AddedBy = getLogin().LoginID;
        tempACC_ChartOfAccountLabel2.AddedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel2.UpdatedBy = getLogin().LoginID;
        tempACC_ChartOfAccountLabel2.UpdatedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel2.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ACC_ChartOfAccountLabel2Manager.UpdateACC_ChartOfAccountLabel2(tempACC_ChartOfAccountLabel2);
        showACC_ChartOfAccountLabel2Grid();
        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        btnClear_Click(this, new EventArgs());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtCode.Text = "";
        //ddlACC_ChartOfAccountLabel1.SelectedIndex = 0;
        txtChartOfAccountLabel2Text.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        //ddlRowStatus.SelectedIndex = 0;
    }
    private void showACC_ChartOfAccountLabel2Data()
    {
        ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2 = new ACC_ChartOfAccountLabel2();
        aCC_ChartOfAccountLabel2 = ACC_ChartOfAccountLabel2Manager.GetACC_ChartOfAccountLabel2ByID(Int32.Parse(hfaCC_ChartOfAccountLabel2ID.Value));

        txtCode.Text = aCC_ChartOfAccountLabel2.Code;
        ddlACC_ChartOfAccountLabel1.SelectedValue = aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID.ToString();
        txtChartOfAccountLabel2Text.Text = aCC_ChartOfAccountLabel2.ChartOfAccountLabel2Text;
        txtExtraField1.Text = aCC_ChartOfAccountLabel2.ExtraField1;
        txtExtraField2.Text = aCC_ChartOfAccountLabel2.ExtraField2;
        txtExtraField3.Text = aCC_ChartOfAccountLabel2.ExtraField3;
        txtAddedBy.Text = aCC_ChartOfAccountLabel2.AddedBy.ToString();
        txtUpdatedBy.Text = aCC_ChartOfAccountLabel2.UpdatedBy.ToString();
        //txtUpdatedDate.Text = aCC_ChartOfAccountLabel2.UpdatedDate;
        ddlRowStatus.SelectedValue = aCC_ChartOfAccountLabel2.RowStatusID.ToString();
    }
    private void loadACC_ChartOfAccountLabel1()
    {
        ListItem li = new ListItem("Select Label-1...", "0");
        ddlACC_ChartOfAccountLabel1.Items.Add(li);

        List<ACC_ChartOfAccountLabel1> aCC_ChartOfAccountLabel1s = new List<ACC_ChartOfAccountLabel1>();
        aCC_ChartOfAccountLabel1s = ACC_ChartOfAccountLabel1Manager.GetAllACC_ChartOfAccountLabel1s().FindAll(x => x.RowStatusID == 1);
        foreach (ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 in aCC_ChartOfAccountLabel1s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel1.ChartOfAccountLabel1Text.ToString(), aCC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID.ToString());
            ddlACC_ChartOfAccountLabel1.Items.Add(item);
        }
    }
    private void loadRowStatus()
    {
        ListItem li = new ListItem("Select Status...", "0");
        ddlRowStatus.Items.Add(li);
        ddlStatusSearch.Items.Add(new ListItem("All", "0"));

        List<RowStatus> rowStatuss = new List<RowStatus>();
        rowStatuss = RowStatusManager.GetAllRowStatuss();
        foreach (RowStatus rowStatus in rowStatuss)
        {
            ListItem item = new ListItem(rowStatus.RowStatusName.ToString(), rowStatus.RowStatusID.ToString());
            ddlRowStatus.Items.Add(item);
            ddlStatusSearch.Items.Add(item);
        }
        ddlStatusSearch.SelectedValue = "1";
    }
    protected void ddlStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        showACC_ChartOfAccountLabel2Grid();
    }
}
