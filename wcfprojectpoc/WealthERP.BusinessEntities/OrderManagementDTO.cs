using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{

    ///// <summary>
    /////   Personal Settings DTO
    ///// </summary>
    ///// <remarks>
    ///// </remarks>
    //[DataContract]
    //[Serializable]
    //public class PersonalSettingsDTO : BaseDTO
    //{
    //    # region Properties

    //    public int appraisalEndTime{ get; set; }
    //    public int appraisalStartTime{ get; set; }
    //    public int appraisalWorkingDays{ get; set; }
    //    public string defaultScreen{ get; set; }
    //    public List<DefaultScreenDTO> defaultScreenCollection{ get; set; }
    //    public string exportStatus{ get; set; }
    //    public bool isItemTypeExpanded{ get; set; }
    //    public bool isPaymentTypeNet{ get; set; }
    //    public string isScreenActive{ get; set; }
    //    public List<LetterFooterAddressDTO> letterAddressList{ get; set; }
    //    public string screenCode{ get; set; }
    //    public string screenId{ get; set; }
    //    public string screenName{ get; set; }
    //    public string theme{ get; set; }
    //    public string userCountry{ get; set; }
    //    public string userId{ get; set; }

    //    /// <summary>
    //    ///   get or set personal settings id
    //    /// </summary>
    //    /// <value> The </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 0)]
    //    public int PersonalSettingsId { get{ get; set; } set{ get; set; } }

    //    /// <summary>
    //    ///   Gets or sets the user id.
    //    /// </summary>
    //    /// <value> The user id. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 1)]
    //    public string UserId
    //    {
    //        get { return userId{ get; set; } }
    //        set
    //        {
    //            userId = value{ get; set; }
    //            NotifyPropertyChanged(() => UserId){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the value of itemType
    //    /// </summary>
    //    /// <value> The itemType. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 2)]
    //    public bool IsItemTypeExpanded
    //    {
    //        get { return isItemTypeExpanded{ get; set; } }
    //        set
    //        {
    //            isItemTypeExpanded = value{ get; set; }
    //            NotifyPropertyChanged(() => IsItemTypeExpanded){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the value of theme
    //    /// </summary>
    //    /// <value> The theme. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 3)]
    //    public string Theme
    //    {
    //        get { return theme{ get; set; } }
    //        set
    //        {
    //            theme = value{ get; set; }
    //            NotifyPropertyChanged(() => Theme){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the value of paymentType
    //    /// </summary>
    //    /// <value> The paymentType. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 4)]
    //    public bool IsPaymentTypeNet
    //    {
    //        get { return isPaymentTypeNet{ get; set; } }
    //        set
    //        {
    //            isPaymentTypeNet = value{ get; set; }
    //            NotifyPropertyChanged(() => IsPaymentTypeNet){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the value of appraisalStartTime
    //    /// </summary>
    //    /// <value> The appraisalStartTime. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 5)]
    //    public int AppraisalStartTime
    //    {
    //        get { return appraisalStartTime{ get; set; } }
    //        set
    //        {
    //            appraisalStartTime = value{ get; set; }
    //            NotifyPropertyChanged(() => AppraisalStartTime){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the value of appraisalEndTime
    //    /// </summary>
    //    /// <value> The appraisalEndTime. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 6)]
    //    public int AppraisalEndTime
    //    {
    //        get { return appraisalEndTime{ get; set; } }
    //        set
    //        {
    //            appraisalEndTime = value{ get; set; }
    //            NotifyPropertyChanged(() => AppraisalEndTime){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the value of appraisalWorkingDays
    //    /// </summary>
    //    /// <value> The appraisalWorkingDays. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 7)]
    //    public int AppraisalWorkingDays
    //    {
    //        get { return appraisalWorkingDays{ get; set; } }
    //        set
    //        {
    //            appraisalWorkingDays = value{ get; set; }
    //            NotifyPropertyChanged(() => AppraisalWorkingDays){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the value of letteerAddressList
    //    /// </summary>
    //    /// <value> The letterAddressList. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 8)]
    //    public List<LetterFooterAddressDTO> LetterAddressList
    //    {
    //        get { return letterAddressList{ get; set; } }
    //        set
    //        {
    //            letterAddressList = value{ get; set; }
    //            NotifyPropertyChanged(() => LetterAddressList){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the DefaultScreen.
    //    /// </summary>
    //    /// <value> The DefaultScreen. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 9)]
    //    public string DefaultScreen
    //    {
    //        get { return defaultScreen{ get; set; } }
    //        set
    //        {
    //            defaultScreen = value{ get; set; }
    //            NotifyPropertyChanged(() => DefaultScreen){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the screen id.
    //    /// </summary>
    //    /// <value> The user id. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 10)]
    //    public string ScreenId
    //    {
    //        get { return screenId{ get; set; } }
    //        set
    //        {
    //            screenId = value{ get; set; }
    //            NotifyPropertyChanged(() => ScreenId){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the screen name.
    //    /// </summary>
    //    /// <value> The screen name. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 11)]
    //    public string ScreenName
    //    {
    //        get { return screenName{ get; set; } }
    //        set
    //        {
    //            screenName = value{ get; set; }
    //            NotifyPropertyChanged(() => ScreenName){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the screen code.
    //    /// </summary>
    //    /// <value> The screen code. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 12)]
    //    public string ScreenCode
    //    {
    //        get { return screenCode{ get; set; } }
    //        set
    //        {
    //            screenCode = value{ get; set; }
    //            NotifyPropertyChanged(() => ScreenCode){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the IsScreenActive.
    //    /// </summary>
    //    /// <value> The screen active flag. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 13)]
    //    public string IsScreenActive
    //    {
    //        get { return isScreenActive{ get; set; } }
    //        set
    //        {
    //            isScreenActive = value{ get; set; }
    //            NotifyPropertyChanged(() => IsScreenActive){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the Collection of screens.
    //    /// </summary>
    //    /// <value> The screenCollection. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 14)]
    //    public List<DefaultScreenDTO> DefaultScreenCollection
    //    {
    //        get { return defaultScreenCollection{ get; set; } }
    //        set
    //        {
    //            defaultScreenCollection = value{ get; set; }
    //            NotifyPropertyChanged(() => DefaultScreenCollection){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the User Country.
    //    /// </summary>
    //    /// <value> The User Country. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 15)]
    //    public string UserCountry
    //    {
    //        get { return userCountry{ get; set; } }
    //        set
    //        {
    //            userCountry = value{ get; set; }
    //            NotifyPropertyChanged(() => UserCountry){ get; set; }
    //        }
    //    }

    //    /// <summary>
    //    ///   Gets or sets the User Country.
    //    /// </summary>
    //    /// <value> The User Country. </value>
    //    /// <remarks>
    //    /// </remarks>
    //    [DataMember(Order = 16)]
    //    public string ExportStatus
    //    {
    //        get { return exportStatus{ get; set; } }
    //        set
    //        {
    //            exportStatus = value{ get; set; }
    //            NotifyPropertyChanged(() => ExportStatus){ get; set; }
    //        }
    //    }

    //    public Dictionary<string, string> systemCodesList{ get; set; }

    //    /// <summary>
    //    /// Gets or sets the system codes list.
    //    /// </summary>
    //    /// <value>
    //    /// The system codes list.
    //    /// </value>
    //    [DataMember(Order = 17)]
    //    public Dictionary<string, string> SystemCodesList
    //    {
    //        get
    //        {
    //            if (systemCodesList == null)
    //                systemCodesList = new Dictionary<string, string>(){ get; set; }
    //            return systemCodesList{ get; set; }
    //        }
    //        set
    //        {

    //            systemCodesList = value{ get; set; }
    //        }
    //    }



    //    #endregion Properties
    //}
    //-----------------------------------------------------------------------------

    /// <summary>
    ///   Order Management DTO
    /// </summary>
    /// <remarks>
    /// </remarks>
    [DataContract]
    [Serializable]
    [KnownType(typeof(MFOrderManagementDTO))]
    public class OrderManagementDTO
    {
        [DataMember(Order = 0)]
        public int OrderId{ get; set; }

        [DataMember(Order = 1)]
        public string m_AssetGroup{ get; set; }

        [DataMember(Order = 2)]
        public DateTime m_OrderDate{ get; set; }

        [DataMember(Order = 3)]
        public int m_OrderNumber{ get; set; }

        [DataMember(Order = 4)]
        public int m_CustomerId{ get; set; }

        [DataMember(Order = 5)]
        public string m_SourceCode{ get; set; }

        [DataMember(Order = 6)]
        public string m_ApplicationNumber{ get; set; }

        [DataMember(Order = 7)]
        public DateTime m_ApplicationReceivedDate{ get; set; }

        [DataMember(Order = 8)]
        public string m_PaymentMode{ get; set; }

        [DataMember(Order = 9)]
        public string m_ChequeNumber{ get; set; }

        [DataMember(Order = 10)]
        public DateTime m_PaymentDate{ get; set; }

        [DataMember(Order = 11)]
        public int m_CustBankAccId{ get; set; }

        [DataMember(Order = 12)]
        public string m_BankBranchName{ get; set; }

        [DataMember(Order = 13)]
        public string m_OrderStepCode{ get; set; }

        [DataMember(Order = 14)]
        public string m_OrderStatusCode{ get; set; }

        [DataMember(Order = 15)]
        public string m_ReasonCode{ get; set; }

        [DataMember(Order = 16)]
        public int m_ApprovedBy{ get; set; }

        [DataMember(Order = 17)]
        public int m_AssociationId{ get; set; }

        [DataMember(Order = 18)]
        public string m_AssociationType{ get; set; }

        [DataMember(Order = 19)]
        public int m_IsCustomerApprovalApplicable{ get; set; }

        [DataMember(Order = 20)]
        public int AgentId { get; set; }

        [DataMember(Order = 21)]
        public string AgentCode { get; set; }


    }
}
