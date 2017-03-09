using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUser
{
    public class DashboardPartVo
    {
        private int m_DashboardPartId;
        private int m_DashboardId;
        private string m_Name;
        private string m_ControlName;
        private string m_Params;
        private int m_UserOrder;
        private string m_UserParams;
        private bool m_Visible;
        private int m_Columns;

        public DashboardPartVo()
        {
            m_DashboardPartId = 0;
            m_DashboardId = 0;
            m_Name = "";
            m_ControlName = "";
            m_Params = "";
            m_UserOrder = 0;
            m_UserParams = "";
            m_Visible = false;
        }

        public int DashboardPartId
        {
            get { return m_DashboardPartId; }
            set { m_DashboardPartId = value; }
        }

        public int DashboardId
        {
            get { return m_DashboardId; }
            set { m_DashboardId = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string ControlName
        {
            get { return m_ControlName; }
            set { m_ControlName = value; }
        }

        public string Params
        {
            get { return m_Params; }
            set { m_Params = value; }
        }

        public int UserOrder
        {
            get { return m_UserOrder; }
            set { m_UserOrder = value; }
        }

        public string UserParams
        {
            get { return m_UserParams; }
            set { m_UserParams = value; }
        }

        public bool Visible
        {
            get { return m_Visible; }
            set { m_Visible = value; }
        }

        public int Columns
        {
            get { return m_Columns; }
            set { m_Columns = value; }
        }
    }
}
