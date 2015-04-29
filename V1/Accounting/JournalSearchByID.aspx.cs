using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class AdminACC_JournalDetailInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
        }
    }

    private Login getLogin()
    {
        Login login = new Login();

        try
        {
            if (Session["Login"] != null)
            {
                login = (Login)Session["Login"];
            }
            else if (hfLoginID.Value != "")
            {
                login = LoginManager.GetLoginByID(int.Parse(hfLoginID.Value));
            }
            else
            { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

        }
        catch (Exception ex)
        { }

        return login;
    }

    private void initialLoad()
    {
        if (hfLoginID.Value == "")
        {
            hfLoginID.Value = getLogin().LoginID.ToString();
        }

        btnDeleteJournal.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("btnDelete", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
    }

   
  

    protected void btnDeleteJournal_Click(object sender, EventArgs e)
    {
        ACC_JournalMasterManager.DeleteACC_JournalMaster(int.Parse(txtJournalMasterID.Text));
        txtJournalMasterID.Text = "";
        btnTransactionSummary_Click(this, new EventArgs());
    }

    protected void btnTransactionSummary_Click(object sender, EventArgs e)
    {
         
        if (txtJournalMasterID.Text != "")
        {
            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + txtJournalMasterID.Text + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }

    protected void btnSearchByPurchaseID_Click(object sender, EventArgs e)
    {
        if (txtPurchaseID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Inventory Purchase-" + txtPurchaseID.Text + "' ";


            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }

    protected void btnSearchByPosPurchaseID_Click(object sender, EventArgs e)
    {
        if (txtPosPurchaseID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Product Purchase-" + txtPosPurchaseID.Text + "' ";


            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }

    protected void btnSearchByIssueID_Click(object sender, EventArgs e)
    {
        if (txtIssueID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Inventory Issue-" + txtIssueID.Text + "' ";


            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }
    protected void btnSearchByUtilizationID_Click(object sender, EventArgs e)
    {
        if (txtUtilizationID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Utilization ID = " + txtUtilizationID.Text + "' ";


            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }
    protected void btnJournalEdit_Click(object sender, EventArgs e)
    {
        lblEditLink.Text = "<a target='_blank' href='JournalModification.aspx?JournalMasterID=" + txtJournalMasterID.Text + "' >Edit Link</a>";

    }
}
