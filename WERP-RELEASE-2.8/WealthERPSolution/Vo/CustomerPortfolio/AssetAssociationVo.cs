using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class AssetAssociationVo
    {
        #region Fields

        private int m_AssetAssociationId;
        private int m_LiabilitiesId;
        private int m_AssetId;
        private string m_AssetGroupCode;
        private string m_AssetTable;
        private int m_CreatedBy;
        private int m_ModifiedBy;

        #endregion Fields

        #region Properties

        public int AssetAssociationId
        {
            get { return m_AssetAssociationId; }
            set { m_AssetAssociationId = value; }
        }
     
        public int LiabilitiesId
        {
            get { return m_LiabilitiesId; }
            set { m_LiabilitiesId = value; }
        }
       
        public int AssetId
        {
            get { return m_AssetId; }
            set { m_AssetId = value; }
        }
      
        public string AssetGroupCode
        {
            get { return m_AssetGroupCode; }
            set { m_AssetGroupCode = value; }
        }
      
        public string AssetTable
        {
            get { return m_AssetTable; }
            set { m_AssetTable = value; }
        }
      
        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }
      

        public int ModifiedBy
        {
            get { return m_ModifiedBy; }
            set { m_ModifiedBy = value; }
        }

        #endregion Properties


    }
}
