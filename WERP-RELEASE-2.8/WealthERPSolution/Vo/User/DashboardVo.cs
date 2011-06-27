using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUser
{
    public class DashboardVo
    {
        private int m_DashboardId;
        private int m_UserId;
        private string m_Name;
        private string m_KeyName;
        private List<DashboardPartVo> m_PartList;
        private int m_Columns;

        public DashboardVo()
        {
            m_DashboardId = 0;
            m_UserId = 0;
            m_Name = "";
            m_KeyName = "";
            m_PartList = new List<DashboardPartVo>();
            m_Columns = 0;
        }

        public int DashboardId
        {
            get { return m_DashboardId; }
            set { m_DashboardId = value; }
        }

        public int UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string KeyName
        {
            get { return m_KeyName; }
            set { m_KeyName = value; }
        }

        public List<DashboardPartVo> PartList
        {
            get { return m_PartList; }
            set { m_PartList = value; }
        }

        public int Columns
        {
            get { return m_Columns; }
            set { m_Columns = value; }
        }
    }
}
