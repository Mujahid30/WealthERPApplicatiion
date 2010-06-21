
/****** Object:  Table [dbo].[WerpULIPPlan]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpULIPPlan](
	[WUP_ULIPPlanCode] [int] IDENTITY(1000,1) NOT NULL,
	[WUP_ULIPPlanName] [varchar](100) NULL,
	[XII_InsuranceIssuerCode] [varchar](5) NULL,
	[WUP_IRDAProductCode] [varchar](20) NULL,
 CONSTRAINT [PK_WerpULIPPlan] PRIMARY KEY CLUSTERED 
(
	[WUP_ULIPPlanCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlSystematicSetupInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetupInput](
	[CMFCXSSI_SystematicId] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFCXSSI_PRODUCT] [varchar](50) NULL,
	[CMFCXSSI_SCHEME] [varchar](100) NULL,
	[CMFCXSSI_FOLIONO] [varchar](50) NULL,
	[CMFCXSSI_INVNAME] [varchar](60) NULL,
	[CMFCXSSI_AUTOTRXN] [varchar](50) NULL,
	[CMFCXSSI_AUTOTRXNNum] [varchar](50) NULL,
	[CMFCXSSI_AUTOAMOUN] [varchar](50) NULL,
	[CMFCXSSI_FROMDATE] [datetime] NULL,
	[CMFCXSSI_TODATE] [datetime] NULL,
	[CMFCXSSI_CEASEDATE] [datetime] NULL,
	[CMFCXSSI_PERIODICIT] [varchar](50) NULL,
	[CMFCXSSI_PERIODDAY] [varchar](50) NULL,
	[CMFCXSSI_INVIIN] [varchar](50) NULL,
	[CMFCXSSI_PAYMENTMO] [varchar](50) NULL,
	[CMFCXSSI_TARGETSCH] [varchar](50) NULL,
	[CMFCXSSI_REGDATE] [datetime] NULL,
	[CMFCXSSI_SUBBROKER] [varchar](50) NULL,
	[CMFCXSSI_CreatedBy] [int] NULL,
	[CMFCXSSI_CreatedOn] [datetime] NULL,
	[CMFCXSSI_ModifiedBy] [int] NULL,
	[CMFCXSSI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlSystematicSetupInput] PRIMARY KEY CLUSTERED 
(
	[CMFCXSSI_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertEventNotification]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlertEventNotification](
	[AEN_EventQueueID] [bigint] IDENTITY(1,1) NOT NULL,
	[AES_EventSetupID] [bigint] NOT NULL,
	[AEL_EventID] [smallint] NOT NULL,
	[AEN_EventMessage] [varchar](500) NOT NULL,
	[AEN_SchemeID] [int] NULL,
	[AEN_TargetID] [int] NOT NULL,
	[ADML_ModeId] [tinyint] NOT NULL,
	[AEN_IsAlerted] [bit] NOT NULL,
	[AEN_PopulatedDate] [datetime] NOT NULL,
	[AEN_CreatedBy] [int] NULL,
 CONSTRAINT [PK_TABLE_EVENT_NOTIFICATION] PRIMARY KEY CLUSTERED 
(
	[AEN_EventQueueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLProof]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLProof](
	[XP_ProofCode] [int] IDENTITY(1,1) NOT NULL,
	[XP_ProofName] [varchar](max) NULL,
	[XP_ProofCategory] [varchar](30) NULL,
	[XP_CreatedBy] [int] NULL,
	[XP_CreatedOn] [datetime] NULL,
	[XP_ModifiedBy] [int] NULL,
	[XP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLProof] PRIMARY KEY CLUSTERED 
(
	[XP_ProofCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlSystematicSetupStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetupStaging](
	[CMFCXSSS_SystematicId] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFCXSSS_PRODUCT] [varchar](50) NULL,
	[CMFCXSSS_SCHEME] [varchar](100) NULL,
	[CMFCXSSS_FOLIONO] [varchar](50) NULL,
	[CMFCXSSS_INVNAME] [varchar](60) NULL,
	[CMFCXSSS_AUTOTRXN] [char](2) NULL,
	[CMFCXSSS_AUTOTRXNNum] [numeric](10, 0) NULL,
	[CMFCXSSS_AUTOAMOUN] [numeric](18, 4) NULL,
	[CMFCXSSS_FROMDATE] [datetime] NULL,
	[CMFCXSSS_TODATE] [datetime] NULL,
	[CMFCXSSS_CEASEDATE] [datetime] NULL,
	[CMFCXSSS_PERIODICIT] [varchar](5) NULL,
	[CMFCXSSS_PERIODDAY] [numeric](2, 0) NULL,
	[CMFCXSSS_INVIIN] [numeric](3, 0) NULL,
	[CMFCXSSS_PAYMENTMO] [varchar](5) NULL,
	[CMFCXSSS_TARGETSCH] [varchar](20) NULL,
	[CMFCXSSS_REGDATE] [datetime] NULL,
	[CMFCXSSS_SUBBROKER] [varchar](50) NULL,
	[A_AdviserId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CMFCXSSS_IsRejected] [tinyint] NULL,
	[CMFCXSSS_RejectedRemark] [varchar](50) NULL,
	[CMFCXSSS_CreatedBy] [int] NULL,
	[CMFCXSSS_CreatedOn] [datetime] NULL,
	[CMFCXSSS_ModifiedBy] [int] NULL,
	[CMFCXSS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlSystematicSetupStaging] PRIMARY KEY CLUSTERED 
(
	[CMFCXSSS_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlProfile]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFTempletonXtrnlProfile](
	[C_CustomerId] [int] NULL,
	[CMFTXP_Id] [int] NOT NULL,
	[CMFTXP_COMP_CODE] [varchar](20) NULL,
	[CMFTXP_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXP_INV_NAME] [varchar](60) NULL,
	[CMFTXP_JOINT_NAM1] [varchar](60) NULL,
	[CMFTXP_JOINT_NAM2] [varchar](60) NULL,
	[CMFTXP_ADDRESS1] [varchar](30) NULL,
	[CMFTXP_ADDRESS2] [varchar](30) NULL,
	[CMFTXP_ADDRESS3] [varchar](30) NULL,
	[CMFTXP_CITY] [varchar](25) NULL,
	[CMFTXP_PINCODE] [numeric](6, 0) NULL,
	[CMFTXP_STATE] [varchar](25) NULL,
	[CMFTXP_COUNTRY] [varchar](25) NULL,
	[CMFTXP_TPIN] [varchar](20) NULL,
	[CMFTXP_D_BIRTH] [datetime] NULL,
	[CMFTXP_F_NAME] [varchar](30) NULL,
	[CMFTXP_M_NAME] [varchar](30) NULL,
	[CMFTXP_PHONE_RES] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_RES1] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_RES2] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_OFF] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_OFF1] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_OFF2] [numeric](16, 0) NULL,
	[CMFTXP_FAX_RES] [numeric](16, 0) NULL,
	[CMFTXP_FAX_OFF] [numeric](16, 0) NULL,
	[CMFTXP_TAX_STATUS] [varchar](30) NULL,
	[CMFTXP_OCCU_CODE] [varchar](10) NULL,
	[CMFTXP_EMAIL] [varchar](max) NULL,
	[CMFTXP_ACCNT_NO] [numeric](15, 0) NULL,
	[CMFTXP_BANK_NAME] [varchar](50) NULL,
	[CMFTXP_AC_TYPE] [varchar](20) NULL,
	[CMFTXP_BRANCH] [varchar](30) NULL,
	[CMFTXP_B_ADDRESS1] [varchar](30) NULL,
	[CMFTXP_B_ADDRESS2] [varchar](30) NULL,
	[CMFTXP_B_ADDRESS3] [varchar](30) NULL,
	[CMFTXP_B_CITY] [varchar](25) NULL,
	[CMFTXP_B_PINCODE] [numeric](6, 0) NULL,
	[CMFTXP_B_STATE] [varchar](25) NULL,
	[CMFTXP_B_COUNTRY] [varchar](25) NULL,
	[CMFTXP_INVEST_ID] [numeric](15, 0) NULL,
	[CMFTXP_BROK_CODE] [varchar](30) NULL,
	[CMFTXP_PANNO1] [numeric](10, 0) NULL,
	[CMFTXP_PANNO2] [numeric](10, 0) NULL,
	[CMFTXP_PANNO3] [numeric](10, 0) NULL,
	[CMFTXP_PAN_STATU0] [varchar](10) NULL,
	[CMFTXP_PAN_STATU1] [varchar](10) NULL,
	[CMFTXP_PAN_STATU2] [varchar](10) NULL,
	[CMFTXP_MAPIN1] [varchar](50) NULL,
	[CMFTXP_MAPIN2] [varchar](50) NULL,
	[CMFTXP_MAPIN3] [varchar](50) NULL,
	[CMFTXP_CREA_DATE] [datetime] NULL,
	[CMFTXP_CREA_TIME] [datetime] NULL,
	[CMFTXP_CUSTOMER_3] [numeric](15, 0) NULL,
	[CMFTXP_CreatedBy] [int] NULL,
	[CMFTXP_CreatedOn] [datetime] NULL,
	[CMFTXP_ModifiedOn] [datetime] NULL,
	[CMFTXP_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlProfile] PRIMARY KEY CLUSTERED 
(
	[CMFTXP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlProfileInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFTempletonXtrnlProfileInput](
	[CMFTXPI_Id] [int] NOT NULL,
	[CMFTXPI_COMP_CODE] [varchar](20) NULL,
	[CMFTXPI_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXPI_INV_NAME] [varchar](60) NULL,
	[CMFTXPI_JOINT_NAM1] [varchar](60) NULL,
	[CMFTXPI_JOINT_NAM2] [varchar](60) NULL,
	[CMFTXPI_ADDRESS1] [varchar](30) NULL,
	[CMFTXPI_ADDRESS2] [varchar](30) NULL,
	[CMFTXPI_ADDRESS3] [varchar](30) NULL,
	[CMFTXPI_CITY] [varchar](25) NULL,
	[CMFTXPI_PINCODE] [numeric](6, 0) NULL,
	[CMFTXPI_STATE] [varchar](25) NULL,
	[CMFTXPI_COUNTRY] [varchar](25) NULL,
	[CMFTXPI_TPIN] [varchar](20) NULL,
	[CMFTXPI_D_BIRTH] [datetime] NULL,
	[CMFTXPI_F_NAME] [varchar](30) NULL,
	[CMFTXPI_M_NAME] [varchar](30) NULL,
	[CMFTXPI_PHONE_RES] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_RES1] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_RES2] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_OFF] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_OFF1] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_OFF2] [numeric](16, 0) NULL,
	[CMFTXPI_FAX_RES] [numeric](16, 0) NULL,
	[CMFTXPI_FAX_OFF] [numeric](16, 0) NULL,
	[CMFTXPI_TAX_STATUS] [varchar](30) NULL,
	[CMFTXPI_OCCU_CODE] [varchar](10) NULL,
	[CMFTXPI_EMAIL] [varchar](max) NULL,
	[CMFTXPI_ACCNT_NO] [numeric](15, 0) NULL,
	[CMFTXPI_BANK_NAME] [varchar](50) NULL,
	[CMFTXPI_AC_TYPE] [varchar](20) NULL,
	[CMFTXPI_BRANCH] [varchar](30) NULL,
	[CMFTXPI_B_ADDRESS1] [varchar](30) NULL,
	[CMFTXPI_B_ADDRESS2] [varchar](30) NULL,
	[CMFTXPI_B_ADDRESS3] [varchar](30) NULL,
	[CMFTXPI_B_CITY] [varchar](25) NULL,
	[CMFTXPI_B_PINCODE] [numeric](6, 0) NULL,
	[CMFTXPI_B_STATE] [varchar](25) NULL,
	[CMFTXPI_B_COUNTRY] [varchar](25) NULL,
	[CMFTXPI_INVEST_ID] [numeric](15, 0) NULL,
	[CMFTXPI_BROK_CODE] [varchar](30) NULL,
	[CMFTXPI_PANNO1] [numeric](10, 0) NULL,
	[CMFTXPI_PANNO2] [numeric](10, 0) NULL,
	[CMFTXPI_PANNO3] [numeric](10, 0) NULL,
	[CMFTXPI_PAN_STATU0] [varchar](10) NULL,
	[CMFTXPI_PAN_STATU1] [varchar](10) NULL,
	[CMFTXPI_PAN_STATU2] [varchar](10) NULL,
	[CMFTXPI_MAPIN1] [varchar](50) NULL,
	[CMFTXPI_MAPIN2] [varchar](50) NULL,
	[CMFTXPI_MAPIN3] [varchar](50) NULL,
	[CMFTXPI_CREA_DATE] [datetime] NULL,
	[CMFTXPI_CREA_TIME] [datetime] NULL,
	[CMFTXPI_CUSTOMER_3] [numeric](15, 0) NULL,
	[CMFTXPI_CreatedBy] [int] NULL,
	[CMFTXPI_CreatedOn] [datetime] NULL,
	[CMFTXPI_ModifiedOn] [datetime] NULL,
	[CMFTXPI_ModifiedBy] [int] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CMFTXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlProfileStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFTempletonXtrnlProfileStaging](
	[CMFTXPS_Id] [int] NOT NULL,
	[CMFTXPS_COMP_CODE] [varchar](20) NULL,
	[CMFTXPS_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXPS_INV_NAME] [varchar](60) NULL,
	[CMFTXPS_JOINT_NAM1] [varchar](60) NULL,
	[CMFTXPS_JOINT_NAM2] [varchar](60) NULL,
	[CMFTXPS_ADDRESS1] [varchar](30) NULL,
	[CMFTXPS_ADDRESS2] [varchar](30) NULL,
	[CMFTXPS_ADDRESS3] [varchar](30) NULL,
	[CMFTXPS_CITY] [varchar](25) NULL,
	[CMFTXPS_PINCODE] [numeric](6, 0) NULL,
	[CMFTXPS_STATE] [varchar](25) NULL,
	[CMFTXPS_COUNTRY] [varchar](25) NULL,
	[CMFTXPS_TPIN] [varchar](20) NULL,
	[CMFTXPS_D_BIRTH] [datetime] NULL,
	[CMFTXPS_F_NAME] [varchar](30) NULL,
	[CMFTXPS_M_NAME] [varchar](30) NULL,
	[CMFTXPS_PHONE_RES] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_RES1] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_RES2] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_OFF] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_OFF1] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_OFF2] [numeric](16, 0) NULL,
	[CMFTXPS_FAX_RES] [numeric](16, 0) NULL,
	[CMFTXPS_FAX_OFF] [numeric](16, 0) NULL,
	[CMFTXPS_TAX_STATUS] [varchar](30) NULL,
	[CMFTXPS_OCCU_CODE] [varchar](10) NULL,
	[CMFTXPS_EMAIL] [varchar](max) NULL,
	[CMFTXPS_ACCNT_NO] [numeric](15, 0) NULL,
	[CMFTXPS_BANK_NAME] [varchar](50) NULL,
	[CMFTXPS_AC_TYPE] [varchar](20) NULL,
	[CMFTXPS_BRANCH] [varchar](30) NULL,
	[CMFTXPS_B_ADDRESS1] [varchar](30) NULL,
	[CMFTXPS_B_ADDRESS2] [varchar](30) NULL,
	[CMFTXPS_B_ADDRESS3] [varchar](30) NULL,
	[CMFTXPS_B_CITY] [varchar](25) NULL,
	[CMFTXPS_B_PINCODE] [numeric](6, 0) NULL,
	[CMFTXPS_B_STATE] [varchar](25) NULL,
	[CMFTXPS_B_COUNTRY] [varchar](25) NULL,
	[CMFTXPS_INVEST_ID] [numeric](15, 0) NULL,
	[CMFTXPS_BROK_CODE] [varchar](30) NULL,
	[CMFTXPS_PANNO1] [numeric](10, 0) NULL,
	[CMFTXPS_PANNO2] [numeric](10, 0) NULL,
	[CMFTXPS_PANNO3] [numeric](10, 0) NULL,
	[CMFTXPS_PAN_STATU0] [varchar](10) NULL,
	[CMFTXPS_PAN_STATU1] [varchar](10) NULL,
	[CMFTXPS_PAN_STATU2] [varchar](10) NULL,
	[CMFTXPS_MAPIN1] [varchar](50) NULL,
	[CMFTXPS_MAPIN2] [varchar](50) NULL,
	[CMFTXPS_MAPIN3] [varchar](50) NULL,
	[CMFTXPS_CREA_DATE] [datetime] NULL,
	[CMFTXPS_CREA_TIME] [datetime] NULL,
	[CMFTXPS_CUSTOMER_3] [numeric](15, 0) NULL,
	[C_CustomerId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CMFTXPS_IsFolioNew] [tinyint] NULL,
	[CMFTXPS_IsCustomerNew] [tinyint] NULL,
	[CMFTXPS_IsRejected] [tinyint] NULL,
	[CMFTXPS_IsBankAccountNew] [tinyint] NULL,
	[CMFTXPS_CreatedBy] [int] NULL,
	[CMFTXPS_CreatedOn] [datetime] NULL,
	[CMFTXPS_ModifiedOn] [datetime] NULL,
	[CMFTXPS_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CMFTXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlCombinationInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlCombinationInput](
	[CMFKXCI_Id] [int] IDENTITY(10000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFKXCI_SlNo] [numeric](5, 0) NULL,
	[CMFKXCI_ProductCode] [varchar](10) NULL,
	[CMFKXCI_Fund] [varchar](50) NULL,
	[CMFKXCI_FolioNumber] [varchar](50) NULL,
	[CMFKXCI_SchemeCode] [varchar](50) NULL,
	[CMFKXCI_DividendOption] [varchar](50) NULL,
	[CMFKXCI_FundDescription] [varchar](100) NULL,
	[CMFKXCI_TransactionHead] [varchar](10) NULL,
	[CMFKXCI_TransactionNumber] [numeric](10, 0) NULL,
	[CMFKXCI_Switch_RefNo ] [varchar](50) NULL,
	[CMFKXCI_InstrumentNumber] [varchar](10) NULL,
	[CMFKXCI_InvestorName] [varchar](60) NULL,
	[CMFKXCI_JointName1] [varchar](60) NULL,
	[CMFKXCI_JointName2] [varchar](60) NULL,
	[CMFKXCI_Address#1] [varchar](75) NULL,
	[CMFKXCI_Address#2] [varchar](75) NULL,
	[CMFKXCI_Address#3] [varchar](75) NULL,
	[CMFKXCI_City] [varchar](25) NULL,
	[CMFKXCI_Pincode] [numeric](6, 0) NULL,
	[CMFKXCI_State] [varchar](25) NULL,
	[CMFKXCI_Country] [varchar](25) NULL,
	[CMFKXCI_DateofBirth] [datetime] NULL,
	[CMFKXCI_PhoneResidence] [varchar](50) NULL,
	[CMFKXCI_PhoneRes#1] [varchar](50) NULL,
	[CMFKXCI_PhoneRes#2] [varchar](50) NULL,
	[CMFKXCI_Mobile  ] [varchar](50) NULL,
	[CMFKXCI_PhoneOffice] [varchar](50) NULL,
	[CMFKXCI_PhoneOff#1] [varchar](50) NULL,
	[CMFKXCI_PhoneOff#2] [varchar](50) NULL,
	[CMFKXCI_FaxResidence] [varchar](50) NULL,
	[CMFKXCI_FaxOffice] [varchar](50) NULL,
	[CMFKXCI_TaxStatus] [varchar](50) NULL,
	[CMFKXCI_OccCode] [varchar](50) NULL,
	[CMFKXCI_Email] [varchar](255) NULL,
	[CMFKXCI_BankAccno] [varchar](20) NULL,
	[CMFKXCI_BankName] [varchar](75) NULL,
	[CMFKXCI_AccountType] [varchar](25) NULL,
	[CMFKXCI_Branch] [varchar](25) NULL,
	[CMFKXCI_BankAddress#1] [varchar](75) NULL,
	[CMFKXCI_BankAddress#2] [varchar](75) NULL,
	[CMFKXCI_BankAddress#3] [varchar](75) NULL,
	[CMFKXCI_BankCity] [varchar](25) NULL,
	[CMFKXCI_BankPhone] [varchar](50) NULL,
	[CMFKXCI_PANNumber] [varchar](10) NULL,
	[CMFKXCI_TransactionMode] [varchar](25) NULL,
	[CMFKXCI_TransactionStatus] [varchar](25) NULL,
	[CMFKXCI_BranchName] [varchar](50) NULL,
	[CMFKXCI_BranchTransactionNo] [varchar](25) NULL,
	[CMFKXCI_TransactionDate] [datetime] NULL,
	[CMFKXCI_ProcessDate] [datetime] NULL,
	[CMFKXCI_Price] [numeric](18, 5) NULL,
	[CMFKXCI_LoadPercentage] [numeric](3, 0) NULL,
	[CMFKXCI_Units] [numeric](18, 5) NULL,
	[CMFKXCI_Amount] [numeric](18, 5) NULL,
	[CMFKXCI_LoadAmount] [numeric](18, 5) NULL,
	[CMFKXCI_AgentCode] [varchar](20) NULL,
	[CMFKXCI_Sub-BrokerCode] [varchar](20) NULL,
	[CMFKXCI_BrokeragePercentage] [numeric](3, 0) NULL,
	[CMFKXCI_Commission] [numeric](18, 5) NULL,
	[CMFKXCI_InvestorID] [varchar](20) NULL,
	[CMFKXCI_ReportDate] [datetime] NULL,
	[CMFKXCI_ReportTime] [varchar](50) NULL,
	[CMFKXCI_TransactionSub] [varchar](25) NULL,
	[CMFKXCI_ApplicationNumber] [varchar](20) NULL,
	[CMFKXCI_TransactionID] [varchar](50) NULL,
	[CMFKXCI_TransactionDescription] [varchar](25) NULL,
	[CMFKXCI_TransactionType] [varchar](50) NULL,
	[CMFKXCI_OrgPurchaseDate] [datetime] NULL,
	[CMFKXCI_OrgPurchaseAmount] [numeric](18, 5) NULL,
	[CMFKXCI_OrgPurchaseUnits] [numeric](18, 5) NULL,
	[CMFKXCI_TrTypeFlag] [varchar](25) NULL,
	[CMFKXCI_SwitchFundDate] [datetime] NULL,
	[CMFKXCI_InstrumentDate] [datetime] NULL,
	[CMFKXCI_InstrumentBank] [varchar](50) NULL,
	[CMFKXCI_Remarks] [varchar](max) NULL,
	[CMFKXCI_Scheme] [varchar](50) NULL,
	[CMFKXCI_Plan] [varchar](25) NULL,
	[CMFKXCI_NAV] [numeric](18, 5) NULL,
	[CMFKXCI_Annualized%] [numeric](3, 0) NULL,
	[CMFKXCI_AnnualizedCommision] [numeric](18, 5) NULL,
	[CMFKXCI_OrginalPurchaseTrnxNo] [varchar](25) NULL,
	[CMFKXCI_OrginalPurchaseBranch] [varchar](50) NULL,
	[CMFKXCI_OldAcno] [varchar](50) NULL,
	[CMFKXCI_IHNo ] [varchar](50) NULL,
	[A_AdviserId] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlCombinationStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlCombinationStaging](
	[CMFKXCS_Id] [int] IDENTITY(10000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFKXCS_SlNo] [numeric](5, 0) NULL,
	[CMFKXCS_ProductCode] [varchar](10) NULL,
	[CMFKXCS_Fund] [varchar](50) NULL,
	[CMFKXCS_FolioNumber] [varchar](50) NULL,
	[CMFKXCS_SchemeCode] [varchar](50) NULL,
	[CMFKXCS_DividendOption] [varchar](50) NULL,
	[CMFKXCS_FundDescription] [varchar](100) NULL,
	[CMFKXCS_TransactionHead] [varchar](10) NULL,
	[CMFKXCS_TransactionNumber] [numeric](10, 0) NULL,
	[CMFKXCS_Switch_RefNo] [varchar](50) NULL,
	[CMFKXCS_InstrumentNumber] [varchar](10) NULL,
	[CMFKXCS_InvestorName] [varchar](60) NULL,
	[CMFKXCS_JointName1] [varchar](60) NULL,
	[CMFKXCS_JointName2] [varchar](60) NULL,
	[CMFKXCS_Address#1] [varchar](75) NULL,
	[CMFKXCS_Address#2] [varchar](75) NULL,
	[CMFKXCS_Address#3] [varchar](75) NULL,
	[CMFKXCS_City] [varchar](25) NULL,
	[CMFKXCS_Pincode] [numeric](6, 0) NULL,
	[CMFKXCS_State] [varchar](25) NULL,
	[CMFKXCS_Country] [varchar](25) NULL,
	[CMFKXCS_DateofBirth] [datetime] NULL,
	[CMFKXCS_PhoneResidence] [varchar](50) NULL,
	[CMFKXCS_PhoneRes#1] [varchar](50) NULL,
	[CMFKXCS_PhoneRes#2] [varchar](50) NULL,
	[CMFKXCS_Mobile] [varchar](50) NULL,
	[CMFKXCS_PhoneOffice] [varchar](50) NULL,
	[CMFKXCS_PhoneOff#1] [varchar](50) NULL,
	[CMFKXCS_PhoneOff#2] [varchar](50) NULL,
	[CMFKXCS_FaxResidence] [varchar](50) NULL,
	[CMFKXCS_FaxOffice] [varchar](50) NULL,
	[CMFKXCS_TaxStatus] [varchar](50) NULL,
	[CMFKXCS_OccCode] [varchar](50) NULL,
	[CMFKXCS_Email] [varchar](255) NULL,
	[CMFKXCS_BankAccno] [varchar](20) NULL,
	[CMFKXCS_BankName] [varchar](75) NULL,
	[CMFKXCS_AccountType] [varchar](25) NULL,
	[CMFKXCS_Branch] [varchar](25) NULL,
	[CMFKXCS_BankAddress#1] [varchar](75) NULL,
	[CMFKXCS_BankAddress#2] [varchar](75) NULL,
	[CMFKXCS_BankAddress#3] [varchar](75) NULL,
	[CMFKXCS_BankCity] [varchar](25) NULL,
	[CMFKXCS_BankPhone] [varchar](50) NULL,
	[CMFKXCS_PANNumber] [varchar](10) NULL,
	[CMFKXCS_TransactionMode] [varchar](25) NULL,
	[CMFKXCS_TransactionStatus] [varchar](25) NULL,
	[CMFKXCS_BranchName] [varchar](50) NULL,
	[CMFKXCS_BranchTransactionNo] [varchar](25) NULL,
	[CMFKXCS_TransactionDate] [datetime] NULL,
	[CMFKXCS_ProcessDate] [datetime] NULL,
	[CMFKXCS_Price] [numeric](18, 5) NULL,
	[CMFKXCS_LoadPercentage] [numeric](3, 0) NULL,
	[CMFKXCS_Units] [numeric](18, 5) NULL,
	[CMFKXCS_Amount] [numeric](18, 5) NULL,
	[CMFKXCS_LoadAmount] [numeric](18, 5) NULL,
	[CMFKXCS_AgentCode] [varchar](20) NULL,
	[CMFKXCS_Sub-BrokerCode] [varchar](20) NULL,
	[CMFKXCS_BrokeragePercentage] [numeric](3, 0) NULL,
	[CMFKXCS_Commission] [numeric](18, 5) NULL,
	[CMFKXCS_InvestorID] [varchar](20) NULL,
	[CMFKXCS_ReportDate] [datetime] NULL,
	[CMFKXCS_ReportTime] [varchar](50) NULL,
	[CMFKXCS_TransactionSub] [varchar](25) NULL,
	[CMFKXCS_ApplicationNumber] [varchar](20) NULL,
	[CMFKXCS_TransactionID] [varchar](50) NULL,
	[CMFKXCS_TransactionDescription] [varchar](25) NULL,
	[CMFKXCS_TransactionType] [varchar](50) NULL,
	[CMFKXCS_OrgPurchaseDate] [datetime] NULL,
	[CMFKXCS_OrgPurchaseAmount] [numeric](18, 5) NULL,
	[CMFKXCS_OrgPurchaseUnits] [numeric](18, 5) NULL,
	[CMFKXCS_TrTypeFlag] [varchar](25) NULL,
	[CMFKXCS_SwitchFundDate] [datetime] NULL,
	[CMFKXCS_InstrumentDate] [datetime] NULL,
	[CMFKXCS_InstrumentBank] [varchar](50) NULL,
	[CMFKXCS_Remarks] [varchar](max) NULL,
	[CMFKXCS_Scheme] [varchar](50) NULL,
	[CMFKXCS_Plan] [varchar](25) NULL,
	[CMFKXCS_NAV] [numeric](18, 5) NULL,
	[CMFKXCS_Annualized%] [numeric](3, 0) NULL,
	[CMFKXCS_AnnualizedCommision] [numeric](18, 5) NULL,
	[CMFKXCS_OrginalPurchaseTrnxNo] [varchar](25) NULL,
	[CMFKXCS_OrginalPurchaseBranch] [varchar](50) NULL,
	[CMFKXCS_OldAcno] [varchar](50) NULL,
	[CMFKXCS_IHNo] [varchar](50) NULL,
	[CMFKXCS_IsRejected] [tinyint] NULL,
	[CMFKXCS_IsFolioNew] [tinyint] NULL,
	[CMFKXCS_IsCustomerNew] [tinyint] NULL,
	[CMFKXCS_RejectedRemark] [varchar](50) NULL,
	[CMFKXCS_AdviserId] [int] NULL,
	[CMFKXCS_CustomerId] [int] NULL,
	[CMFKXCS_AccountId] [int] NULL,
	[CP_PortfolioId] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLQualification]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLQualification](
	[XQ_QualificationCode] [varchar](5) NOT NULL,
	[XQ_Qualification] [varchar](30) NULL,
	[XQ_CreatedBy] [int] NULL,
	[XQ_CreatedOn] [datetime] NULL,
	[XQ_ModifiedBy] [int] NULL,
	[XQ_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLQualification] PRIMARY KEY CLUSTERED 
(
	[XQ_QualificationCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlTransaction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFTempletonXtrnlTransaction](
	[CMFTXT_Id] [int] NOT NULL,
	[CIMFT_CustMFTransId] [int] NULL,
	[CMFTXT_COMP_CODE] [varchar](10) NULL,
	[CMFTXT_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXT_PRODUCT_C0] [varchar](30) NULL,
	[CMFTXT_TRXN_NO] [varchar](30) NULL,
	[CMFTXT_TRXN_NAME] [varchar](50) NULL,
	[CMFTXT_TRXN_MODE] [varchar](10) NULL,
	[CMFTXT_TRXN_STAT] [varchar](10) NULL,
	[CMFTXT_ISC_CODE] [varchar](30) NULL,
	[CMFTXT_ISC_TRXNO] [varchar](30) NULL,
	[CMFTXT_TRXN_DATE] [datetime] NULL,
	[CMFTXT_POSTDTDATE] [datetime] NULL,
	[CMFTXT_PRICE] [numeric](18, 3) NULL,
	[CMFTXT_UNITS] [numeric](15, 4) NULL,
	[CMFTXT_AMOUNT] [numeric](18, 3) NULL,
	[CMFTXT_CHECK_NO] [varchar](20) NULL,
	[CMFTXT_DIVTYPE] [varchar](20) NULL,
	[CMFTXT_BROK_DLR_1] [varchar](20) NULL,
	[CMFTXT_AE_CODE] [varchar](30) NULL,
	[CMFTXT_BROK_PERC] [numeric](6, 3) NULL,
	[CMFTXT_BROK_COMM] [numeric](15, 3) NULL,
	[CMFTXT_INVEST_ID] [varchar](30) NULL,
	[CMFTXT_CREA_DATE] [datetime] NULL,
	[CMFTXT_CREA_TIME] [datetime] NULL,
	[CMFTXT_TRXN_SUB] [varchar](30) NULL,
	[CMFTXT_APPL_NO] [varchar](30) NULL,
	[CMFTXT_PROD_CODE] [varchar](50) NULL,
	[CMFTXT_TRXN_ID] [varchar](50) NULL,
	[CMFTXT_TRAN_TYPE] [varchar](30) NULL,
	[CMFTXT_TR_TYPE] [varchar](30) NULL,
	[CMFTXT_STT] [numeric](15, 3) NULL,
	[CMFTXT_DIRECT_FL2] [varchar](20) NULL,
	[CMFTXT_CreatedBy] [int] NULL,
	[CMFTXT_CreatedOn] [datetime] NULL,
	[CMFTXT_ModifiedBy] [int] NULL,
	[CMFTXT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlTransaction] PRIMARY KEY CLUSTERED 
(
	[CMFTXT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpRejectReason]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpRejectReason](
	[WRR_RejectReasonCode] [int] IDENTITY(1,1) NOT NULL,
	[WRR_RejectReasonDescription] [varchar](100) NULL,
	[WRR_CreatedBy] [int] NULL,
	[WRR_CreatedOn] [datetime] NULL,
	[WRR_ModifiedBy] [int] NULL,
	[WRR_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpRejectReason] PRIMARY KEY CLUSTERED 
(
	[WRR_RejectReasonCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlTransactionInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFTempletonXtrnlTransactionInput](
	[CMFTXTI_Id] [int] NULL,
	[CMFTXTI_COMP_CODE] [varchar](10) NULL,
	[CMFTXTI_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXTI_PRODUCT_C0] [varchar](30) NULL,
	[CMFTXTI_TRXN_NO] [varchar](30) NULL,
	[CMFTXTI_TRXN_NAME] [varchar](50) NULL,
	[CMFTXTI_TRXN_MODE] [varchar](10) NULL,
	[CMFTXTI_TRXN_STAT] [varchar](10) NULL,
	[CMFTXTI_ISC_CODE] [varchar](30) NULL,
	[CMFTXTI_ISC_TRXNO] [varchar](30) NULL,
	[CMFTXTI_TRXN_DATE] [datetime] NULL,
	[CMFTXTI_POSTDTDATE] [datetime] NULL,
	[CMFTXTI_PRICE] [numeric](18, 3) NULL,
	[CMFTXTI_UNITS] [numeric](15, 4) NULL,
	[CMFTXTI_AMOUNT] [numeric](18, 3) NULL,
	[CMFTXTI_CHECK_NO] [varchar](20) NULL,
	[CMFTXTI_DIVTYPE] [varchar](20) NULL,
	[CMFTXTI_BROK_DLR_1] [varchar](20) NULL,
	[CMFTXTI_AE_CODE] [varchar](30) NULL,
	[CMFTXTI_BROK_PERC] [numeric](6, 3) NULL,
	[CMFTXTI_BROK_COMM] [numeric](15, 3) NULL,
	[CMFTXTI_INVEST_ID] [varchar](30) NULL,
	[CMFTXTI_CREA_DATE] [datetime] NULL,
	[CMFTXTI_CREA_TIME] [datetime] NULL,
	[CMFTXTI_TRXN_SUB] [varchar](30) NULL,
	[CMFTXTI_APPL_NO] [varchar](30) NULL,
	[CMFTXTI_PROD_CODE] [varchar](50) NULL,
	[CMFTXTI_TRXN_ID] [varchar](50) NULL,
	[CMFTXTI_TRAN_TYPE] [varchar](30) NULL,
	[CMFTXTI_TR_TYPE] [varchar](30) NULL,
	[CMFTXTI_STT] [numeric](15, 3) NULL,
	[CMFTXTI_DIRECT_FL2] [varchar](20) NULL,
	[CMFTXTI_CreatedBy] [int] NULL,
	[CMFTXTI_CreatedOn] [datetime] NULL,
	[CMFTXTI_ModifiedBy] [int] NULL,
	[CMFTXTI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlTransactionStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFTempletonXtrnlTransactionStaging](
	[CMFTXTS_Id] [int] NOT NULL,
	[A_AdviserId] [int] NULL,
	[CMFTXTS_COMP_CODE] [varchar](10) NULL,
	[CMFTXTS_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXTS_PRODUCT_C0] [varchar](30) NULL,
	[CMFTXTS_TRXN_NO] [varchar](30) NULL,
	[CMFTXTS_TRXN_NAME] [varchar](50) NULL,
	[CMFTXTS_TRXN_MODE] [varchar](10) NULL,
	[CMFTXTS_TRXN_STAT] [varchar](10) NULL,
	[CMFTXTS_ISC_CODE] [varchar](30) NULL,
	[CMFTXTS_ISC_TRXNO] [varchar](30) NULL,
	[CMFTXTS_TRXN_DATE] [datetime] NULL,
	[CMFTXTS_POSTDTDATE] [datetime] NULL,
	[CMFTXTS_PRICE] [numeric](18, 3) NULL,
	[CMFTXTS_UNITS] [numeric](15, 4) NULL,
	[CMFTXTS_AMOUNT] [numeric](18, 3) NULL,
	[CMFTXTS_CHECK_NO] [varchar](20) NULL,
	[CMFTXTS_DIVTYPE] [varchar](20) NULL,
	[CMFTXTS_BROK_DLR_1] [varchar](20) NULL,
	[CMFTXTS_AE_CODE] [varchar](30) NULL,
	[CMFTXTS_BROK_PERC] [numeric](6, 3) NULL,
	[CMFTXTS_BROK_COMM] [numeric](15, 3) NULL,
	[CMFTXTS_INVEST_ID] [varchar](30) NULL,
	[CMFTXTS_CREA_DATE] [datetime] NULL,
	[CMFTXTS_CREA_TIME] [datetime] NULL,
	[CMFTXTS_TRXN_SUB] [varchar](30) NULL,
	[CMFTXTS_APPL_NO] [varchar](30) NULL,
	[CMFTXTS_PROD_CODE] [varchar](50) NULL,
	[CMFTXTS_TRXN_ID] [varchar](50) NULL,
	[CMFTXTS_TRAN_TYPE] [varchar](30) NULL,
	[CMFTXTS_TR_TYPE] [varchar](30) NULL,
	[CMFTXTS_STT] [numeric](15, 3) NULL,
	[CMFTXTS_DIRECT_FL2] [varchar](20) NULL,
	[CMFTXTS_IsRejected] [tinyint] NULL,
	[CMFTXTS_IsFolioNew] [tinyint] NULL,
	[CMFTXTS_CreatedBy] [int] NULL,
	[CMFTXTS_CreatedOn] [datetime] NULL,
	[CMFTXTS_ModifiedBy] [int] NULL,
	[CMFTXTS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CMFTXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlSystematicSetupInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetupInput](
	[CMFKXSSI_SystematicId] [int] NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFKXSSI_ProductCode] [varchar](30) NULL,
	[CMFKXSSI_Agent Code] [varchar](30) NULL,
	[CMFKXSSI_ Fund] [varchar](50) NULL,
	[CMFKXSSI_FolioNumber] [varchar](20) NULL,
	[CMFKXSSI_SchemeCode] [varchar](20) NULL,
	[CMFKXSSI_FundDescription] [varchar](50) NULL,
	[CMFKXSSI_InvestorName] [varchar](30) NULL,
	[CMFKXSSI_InvestorAddress1] [varchar](25) NULL,
	[CMFKXSSI_InvestorAddress2] [varchar](25) NULL,
	[CMFKXSSI_InvestorAddress3] [varchar](25) NULL,
	[CMFKXSSI_InvestorCity] [varchar](25) NULL,
	[CMFKXSSI_InvestorState] [varchar](25) NULL,
	[CMFKXSSI_PinCode] [numeric](6, 0) NULL,
	[CMFKXSSI_EmailAddress] [varchar](max) NULL,
	[CMFKXSSI_PhoneNo1] [numeric](16, 0) NULL,
	[CMFKXSSI_PhoneNo2] [numeric](16, 0) NULL,
	[CMFKXSSI_TransactionType] [varchar](20) NULL,
	[CMFKXSSI_Frequency] [varchar](20) NULL,
	[CMFKXSSI_StartingDate] [datetime] NULL,
	[CMFKXSSI_EndingDate] [datetime] NULL,
	[CMFKXSSI_NoOf InstallmentsPaid] [numeric](5, 0) NULL,
	[CMFKXSSI_NoOf InstalmentsPending] [numeric](5, 0) NULL,
	[CMFKXSSI_TotalNoOfInstalments] [numeric](5, 0) NULL,
	[CMFKXSSI_InstalmentAmount] [numeric](18, 4) NULL,
	[CMFKXSSI_STPIn/OutScheme] [varchar](50) NULL,
	[CMFKXSSI_PaymentMethod] [varchar](20) NULL,
	[CMFKXSSI_Subroker] [varchar](30) NULL,
	[CMFKXSSI_IHNO] [varchar](30) NULL,
	[CMFKXSSI_Remarks] [varchar](50) NULL,
	[CMFKXSSI_CreatedBy] [int] NULL,
	[CMFKXSSI_CreatedOn] [datetime] NULL,
	[CMFKXSSI_ModifiedBy] [int] NULL,
	[CMFKXSSI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlSystematicSetupInput] PRIMARY KEY CLUSTERED 
(
	[CMFKXSSI_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoleAssociation]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleAssociation](
	[URA_UserRoleAssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[U_UserId] [int] NULL,
	[UR_RoleId] [int] NULL,
	[URA_CreatedBy] [int] NOT NULL,
	[URA_CreatedOn] [datetime] NOT NULL,
	[URA_ModifiedBy] [int] NOT NULL,
	[URA_ModifiedOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlSystematicSetupStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetupStaging](
	[CMFKXSSS_SystematicId] [int] NOT NULL,
	[CMFKXSSS_ProductCode] [varchar](30) NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFKXSSS_Agent Code] [varchar](30) NULL,
	[CMFKXSSS_ Fund] [varchar](50) NULL,
	[CMFKXSSS_FolioNumber] [varchar](20) NULL,
	[CMFKXSSS_SchemeCode] [varchar](20) NULL,
	[CMFKXSSS_FundDescription] [varchar](50) NULL,
	[CMFKXSSS_InvestorName] [varchar](30) NULL,
	[CMFKXSSS_InvestorAddress1] [varchar](25) NULL,
	[CMFKXSSS_InvestorAddress2] [varchar](25) NULL,
	[CMFKXSSS_InvestorAddress3] [varchar](25) NULL,
	[CMFKXSSS_InvestorCity] [varchar](25) NULL,
	[CMFKXSSS_InvestorState] [varchar](25) NULL,
	[CMFKXSSS_PinCode] [numeric](6, 0) NULL,
	[CMFKXSSS_EmailAddress] [varchar](max) NULL,
	[CMFKXSSS_PhoneNo1] [numeric](16, 0) NULL,
	[CMFKXSSS_PhoneNo2] [numeric](16, 0) NULL,
	[CMFKXSSS_TransactionType] [varchar](20) NULL,
	[CMFKXSSS_Frequency] [varchar](20) NULL,
	[CMFKXSSS_StartingDate] [datetime] NULL,
	[CMFKXSSS_EndingDate] [datetime] NULL,
	[CMFKXSSS_NoOf InstallmentsPaid] [numeric](5, 0) NULL,
	[CMFKXSSS_NoOf InstalmentsPending] [numeric](5, 0) NULL,
	[CMFKXSSS_TotalNoOfInstalments] [numeric](5, 0) NULL,
	[CMFKXSSS_InstalmentAmount] [numeric](18, 4) NULL,
	[CMFKXSSS_STPIn/OutScheme] [varchar](50) NULL,
	[CMFKXSSS_PaymentMethod] [varchar](20) NULL,
	[CMFKXSSS_Subroker] [varchar](30) NULL,
	[CMFKXSSS_IHNO] [varchar](30) NULL,
	[CMFKXSSS_Remarks] [varchar](50) NULL,
	[CMFKXSSS_CreatedBy] [int] NULL,
	[CMFKXSSS_CreatedOn] [datetime] NULL,
	[CMFKXSSS_ModifiedBy] [int] NULL,
	[CMFKXSSS_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CMFKXSSS_IsRejected] [tinyint] NULL,
	[CMFKXSSS_RejectedRemark] [varchar](50) NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlSystematicSetupStaging] PRIMARY KEY CLUSTERED 
(
	[CMFKXSSS_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAMCSchemeValueResearch]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAMCSchemeValueResearch](
	[PASVR_SchemeName] [varchar](225) NULL,
	[PASVR_PlanName] [varchar](100) NULL,
	[PASVR_VRCode] [varchar](50) NULL,
	[PASVR_Mapped_Status] [char](2) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAMCSchemeCAMS]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAMCSchemeCAMS](
	[PASC_AMC_CODE] [nvarchar](255) NULL,
	[PASC_SCHEME_CODE] [nvarchar](255) NULL,
	[PASC_SUB_FUND_C] [nvarchar](255) NULL,
	[PASC_SCHEME_NAME] [nvarchar](255) NULL,
	[PASC_Mapped_Status] [nchar](2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAMCSchemeKarvy]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAMCSchemeKarvy](
	[PASK_Fund] [nvarchar](255) NULL,
	[PASK_Scheme] [nvarchar](255) NULL,
	[PASK_Plan] [nvarchar](255) NULL,
	[PASK_Option] [nvarchar](255) NULL,
	[PASK_Fund_Description] [nvarchar](255) NULL,
	[PASK_Product_Code] [nvarchar](255) NULL,
	[PASK_Fund_Type] [nvarchar](255) NULL,
	[PASK_Mapped_Status] [nchar](2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[XMLAdviserLOBEquitySegment]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLAdviserLOBEquitySegment](
	[XALES_SegmentCode] [varchar](5) NOT NULL,
	[XALES_SegmentName] [varchar](20) NULL,
	[XALES_CreatedBy] [int] NULL,
	[XALES_CreatedOn] [datetime] NULL,
	[XALES_ModifiedBy] [int] NULL,
	[XALES_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLAdviserLOBEquitySegment] PRIMARY KEY CLUSTERED 
(
	[XALES_SegmentCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLInsuranceIssuer]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLInsuranceIssuer](
	[XII_InsuranceIssuerCode] [varchar](5) NOT NULL,
	[XII_InsuranceIssuerName] [varchar](50) NULL,
	[XII_CreatedBy] [int] NULL,
	[XII_CreatedOn] [datetime] NULL,
	[XII_ModifiedBy] [int] NULL,
	[XII_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpInsuranceIssuer_XML] PRIMARY KEY CLUSTERED 
(
	[XII_InsuranceIssuerCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductGlobalSectorCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductGlobalSectorCategory](
	[PGSC_SectorCategoryCode] [varchar](3) NOT NULL,
	[PGSC_SectorCategoryName] [varchar](50) NULL,
	[PGSC_CreatedBy] [int] NULL,
	[PGSC_CreatedOn] [datetime] NULL,
	[PGSC_ModifiedBy] [int] NULL,
	[PGSC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpSector] PRIMARY KEY CLUSTERED 
(
	[PGSC_SectorCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductEquityCorpAction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductEquityCorpAction](
	[PECA_DailyCorpAxnId] [int] IDENTITY(1,1) NOT NULL,
	[PECAM_CorpAxnId] [int] NOT NULL,
	[PECA_SourceDate] [datetime] NOT NULL,
	[PEM_ScripCode1] [int] NOT NULL,
	[PEM_ScripCode2] [int] NULL,
	[PECA_EffectiveStartDate] [datetime] NULL,
	[PECA_EffectiveEndDate] [datetime] NULL,
	[PECA_ExDate] [datetime] NULL,
	[PECA_RecordDate] [datetime] NULL,
	[PECA_AnnouncementDate] [datetime] NULL,
	[PECA_Ratio1] [numeric](6, 3) NULL,
	[PECA_Ratio2] [numeric](6, 3) NULL,
	[PECA_FaceValueExisting] [numeric](10, 0) NULL,
	[PECA_FaceValueOffer] [numeric](10, 0) NULL,
	[PECA_PremiumPrice] [numeric](18, 6) NULL,
	[PECA_Remark] [varchar](100) NULL,
	[PECA_CreatedBy] [int] NULL,
	[PECA_CreatedOn] [datetime] NULL,
	[PECA_ModifiedOn] [datetime] NULL,
	[PECA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProductEquityDailyCorpAction_1] PRIMARY KEY CLUSTERED 
(
	[PECA_DailyCorpAxnId] ASC,
	[PECAM_CorpAxnId] ASC,
	[PECA_SourceDate] ASC,
	[PEM_ScripCode1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertCycleLookup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlertCycleLookup](
	[CL_CycleID] [tinyint] IDENTITY(1,1) NOT NULL,
	[CL_CycleDesc] [varchar](50) NOT NULL,
	[CL_CycleCode] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductMarketCapClassification]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductMarketCapClassification](
	[PMCC_MarketCapClassificationCode] [int] IDENTITY(1,1) NOT NULL,
	[PMCC_CapClassification] [varchar](20) NULL,
	[PMCC_CreatedBy] [int] NULL,
	[PMCC_CreatedOn] [datetime] NULL,
	[PMCC_ModifiedOn] [datetime] NULL,
	[PMCC_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProductCAPClassification] PRIMARY KEY CLUSTERED 
(
	[PMCC_MarketCapClassificationCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLAdviserLOBIdentifierType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLAdviserLOBIdentifierType](
	[XALIT_IdentifierTypeCode] [varchar](5) NOT NULL,
	[XALIT_IdentifierTypeName] [varchar](30) NULL,
	[XALIT_CreatedBy] [int] NULL,
	[XALIT_CreatedOn] [datetime] NULL,
	[XALIT_ModifiedOn] [datetime] NULL,
	[XALIT_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_XMLAdviserLOBIdentifierType] PRIMARY KEY CLUSTERED 
(
	[XALIT_IdentifierTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFXtrnlProfileInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFXtrnlProfileInput](
	[CMFXPI_Id] [int] NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFXPI_FirstName] [varchar](50) NULL,
	[CMFXPI_MiddleName] [varchar](50) NULL,
	[CMFXPI_LastName] [varchar](50) NULL,
	[CMFXPI_Gender] [varchar](50) NULL,
	[CMFXPI_DOB] [varchar](50) NULL,
	[CMFXPI_Type] [varchar](50) NULL,
	[CMFXPI_SubType] [varchar](50) NULL,
	[CMFXPI_Salutation] [varchar](50) NULL,
	[CMFXPI_PANNum] [varchar](50) NULL,
	[CMFXPI_Adr1Line1] [varchar](50) NULL,
	[CMFXPI_Adr1Line2] [varchar](50) NULL,
	[CMFXPI_Adr1Line3] [varchar](50) NULL,
	[CMFXPI_Adr1PinCode] [varchar](50) NULL,
	[CMFXPI_Adr1City] [varchar](50) NULL,
	[CMFXPI_Adr1State] [varchar](50) NULL,
	[CMFXPI_Adr1Country] [varchar](50) NULL,
	[CMFXPI_Adr2Line1] [varchar](50) NULL,
	[CMFXPI_Adr2Line2] [varchar](50) NULL,
	[CMFXPI_Adr2Line3] [varchar](50) NULL,
	[CMFXPI_Adr2PinCode] [varchar](50) NULL,
	[CMFXPI_Adr2City] [varchar](50) NULL,
	[CMFXPI_Adr2State] [varchar](50) NULL,
	[CMFXPI_Adr2Country] [varchar](50) NULL,
	[CMFXPI_ResISDCode] [varchar](50) NULL,
	[CMFXPI_ResSTDCode] [varchar](50) NULL,
	[CMFXPI_ResPhoneNum] [varchar](50) NULL,
	[CMFXPI_OfcISDCode] [varchar](50) NULL,
	[CMFXPI_OfcSTDCode] [varchar](50) NULL,
	[CMFXPI_OfcPhoneNum] [varchar](50) NULL,
	[CMFXPI_Email] [varchar](50) NULL,
	[CMFXPI_AltEmail] [varchar](50) NULL,
	[CMFXPI_Mobile1] [varchar](50) NULL,
	[CMFXPI_Mobile2] [varchar](50) NULL,
	[CMFXPI_ISDFax] [varchar](50) NULL,
	[CMFXPI_STDFax] [varchar](50) NULL,
	[CMFXPI_Fax] [varchar](50) NULL,
	[CMFXPI_OfcFax] [varchar](50) NULL,
	[CMFXPI_OfcFaxISD] [varchar](50) NULL,
	[CMFXPI_OfcFaxSTD] [varchar](50) NULL,
	[CMFXPI_Occupation] [varchar](50) NULL,
	[CMFXPI_Qualification] [varchar](50) NULL,
	[CMFXPI_MarriageDate] [varchar](50) NULL,
	[CMFXPI_MaritalStatus] [varchar](50) NULL,
	[CMFXPI_Nationality] [varchar](50) NULL,
	[CMFXPI_RBIRefNum] [varchar](50) NULL,
	[CMFXPI_RBIApprovalDate] [varchar](50) NULL,
	[CMFXPI_CompanyName] [varchar](50) NULL,
	[CMFXPI_OfcAdrLine1] [varchar](50) NULL,
	[CMFXPI_OfcAdrLine2] [varchar](50) NULL,
	[CMFXPI_OfcAdrLine3] [varchar](50) NULL,
	[CMFXPI_OfcAdrPinCode] [varchar](50) NULL,
	[CMFXPI_OfcAdrCity] [varchar](50) NULL,
	[CMFXPI_OfcAdrState] [varchar](50) NULL,
	[CMFXPI_OfcAdrCountry] [varchar](50) NULL,
	[CMFXPI_RegistrationDate] [varchar](50) NULL,
	[CMFXPI_CommencementDate] [varchar](50) NULL,
	[CMFXPI_RegistrationPlace] [varchar](50) NULL,
	[CMFXPI_RegistrationNum] [varchar](50) NULL,
	[CMFXPI_CompanyWebsite] [varchar](50) NULL,
	[CMFXPI_BankName] [varchar](50) NULL,
	[CMFXPI_AccountType] [varchar](50) NULL,
	[CMFXPI_AccountNum] [varchar](50) NULL,
	[CMFXPI_BankModeOfOperation] [varchar](50) NULL,
	[CMFXPI_BranchName] [varchar](50) NULL,
	[CMFXPI_BranchAdrLine1] [varchar](50) NULL,
	[CMFXPI_BranchAdrLine2] [varchar](50) NULL,
	[CMFXPI_BranchAdrLine3] [varchar](50) NULL,
	[CMFXPI_BranchAdrPinCode] [varchar](50) NULL,
	[CMFXPI_BranchAdrCity] [varchar](50) NULL,
	[CMFXPI_BranchAdrState] [varchar](50) NULL,
	[CMFXPI_BranchAdrCountry] [varchar](50) NULL,
	[CMFXPI_MICR] [varchar](50) NULL,
	[CMFXPI_IFSC] [varchar](50) NULL,
	[CMFXPI_AMCName] [varchar](100) NULL,
	[CMFXPI_FolioNum] [varchar](50) NULL,
	[CMFXPI_AccountOpeningDate] [datetime] NULL,
	[CMFXPI_FolioModeOfOperating] [varchar](5) NULL,
	[CMFXPI_CreatedOn] [datetime] NULL,
	[CMFXPI_CreatedBy] [int] NULL,
	[CMFXPI_ModifiedOn] [datetime] NULL,
	[CMFXPI_ModifiedBy] [int] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CMFXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpMutualFundTransactionType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpMutualFundTransactionType](
	[WMTT_TransactionClassificationCode] [varchar](3) NOT NULL,
	[WMTT_TransactionClassificationName] [varchar](50) NULL,
	[WMTT_BuySell] [varchar](1) NULL,
	[WMTT_Trigger] [varchar](50) NULL,
	[WMTT_TransactionType] [varchar](30) NULL,
	[WMTT_FinancialFlag] [tinyint] NULL,
	[WMTT_UIName] [varchar](50) NULL,
	[WMTT_CreatedBy] [int] NULL,
	[WMTT_CreatedOn] [datetime] NULL,
	[WMTT_ModifiedBy] [int] NULL,
	[WMTT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_MFTransactionType_XML] PRIMARY KEY CLUSTERED 
(
	[WMTT_TransactionClassificationCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRole](
	[UR_RoleName] [varchar](50) NULL,
	[UR_RoleId] [int] NULL,
	[UR_CreatedBy] [int] NULL,
	[UR_CreatedOn] [datetime] NULL,
	[UR_ModifiedBy] [int] NULL,
	[UR_ModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpMetatable]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpMetatable](
	[WM_PrimaryKey] [varchar](50) NULL,
	[WM_PrimaryKeyDescription] [varchar](50) NULL,
	[WM_TableName] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFXtrnlProfileStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFXtrnlProfileStaging](
	[CMFXPS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFXPS_FirstName] [varchar](25) NULL,
	[CMFXPS_MiddleName] [varchar](25) NULL,
	[CMFXPS_LastName] [varchar](50) NULL,
	[CMFXPS_Gender] [varchar](10) NULL,
	[CMFXPS_DOB] [datetime] NULL,
	[CMFXPS_Type] [varchar](25) NULL,
	[CMFXPS_SubType] [varchar](25) NULL,
	[CMFXPS_Salutation] [varchar](5) NULL,
	[CMFXPS_PANNum] [varchar](10) NULL,
	[CMFXPS_Adr1Line1] [varchar](50) NULL,
	[CMFXPS_Adr1Line2] [varchar](50) NULL,
	[CMFXPS_Adr1Line3] [varchar](50) NULL,
	[CMFXPS_Adr1PinCode] [numeric](6, 0) NULL,
	[CMFXPS_Adr1City] [varchar](25) NULL,
	[CMFXPS_Adr1State] [varchar](25) NULL,
	[CMFXPS_Adr1Country] [varchar](25) NULL,
	[CMFXPS_Adr2Line1] [varchar](30) NULL,
	[CMFXPS_Adr2Line2] [varchar](30) NULL,
	[CMFXPS_Adr2Line3] [varchar](30) NULL,
	[CMFXPS_Adr2PinCode] [numeric](6, 0) NULL,
	[CMFXPS_Adr2City] [varchar](25) NULL,
	[CMFXPS_Adr2State] [varchar](25) NULL,
	[CMFXPS_Adr2Country] [varchar](25) NULL,
	[CMFXPS_ResISDCode] [numeric](4, 0) NULL,
	[CMFXPS_ResSTDCode] [numeric](4, 0) NULL,
	[CMFXPS_ResPhoneNum] [numeric](16, 0) NULL,
	[CMFXPS_OfcISDCode] [numeric](4, 0) NULL,
	[CMFXPS_OfcSTDCode] [numeric](4, 0) NULL,
	[CMFXPS_OfcPhoneNum] [numeric](16, 0) NULL,
	[CMFXPS_Email] [varchar](50) NULL,
	[CMFXPS_AltEmail] [varchar](50) NULL,
	[CMFXPS_Mobile1] [numeric](10, 0) NULL,
	[CMFXPS_Mobile2] [numeric](10, 0) NULL,
	[CMFXPS_ISDFax] [numeric](4, 0) NULL,
	[CMFXPS_STDFax] [numeric](4, 0) NULL,
	[CMFXPS_Fax] [numeric](8, 0) NULL,
	[CMFXPS_OfcFax] [numeric](8, 0) NULL,
	[CMFXPS_OfcFaxISD] [numeric](4, 0) NULL,
	[CMFXPS_OfcFaxSTD] [numeric](4, 0) NULL,
	[CMFXPS_Occupation] [varchar](25) NULL,
	[CMFXPS_Qualification] [varchar](25) NULL,
	[CMFXPS_MarriageDate] [datetime] NULL,
	[CMFXPS_MaritalStatus] [varchar](25) NULL,
	[CMFXPS_Nationality] [varchar](25) NULL,
	[CMFXPS_RBIRefNum] [varchar](25) NULL,
	[CMFXPS_RBIApprovalDate] [datetime] NULL,
	[CMFXPS_CompanyName] [varchar](50) NULL,
	[CMFXPS_OfcAdrLine1] [varchar](25) NULL,
	[CMFXPS_OfcAdrLine2] [varchar](25) NULL,
	[CMFXPS_OfcAdrLine3] [varchar](25) NULL,
	[CMFXPS_OfcAdrPinCode] [numeric](6, 0) NULL,
	[CMFXPS_OfcAdrCity] [varchar](25) NULL,
	[CMFXPS_OfcAdrState] [varchar](25) NULL,
	[CMFXPS_OfcAdrCountry] [varchar](25) NULL,
	[CMFXPS_RegistrationDate] [datetime] NULL,
	[CMFXPS_CommencementDate] [datetime] NULL,
	[CMFXPS_RegistrationPlace] [varchar](20) NULL,
	[CMFXPS_RegistrationNum] [varchar](25) NULL,
	[CMFXPS_CompanyWebsite] [varchar](25) NULL,
	[CMFXPS_BankName] [varchar](30) NULL,
	[CMFXPS_AccountType] [varchar](30) NULL,
	[CMFXPS_AccountNum] [numeric](15, 0) NULL,
	[CMFXPS_BankModeOfOperation] [varchar](5) NULL,
	[CMFXPS_BranchName] [varchar](50) NULL,
	[CMFXPS_BranchAdrLine1] [varchar](50) NULL,
	[CMFXPS_BranchAdrLine2] [varchar](50) NULL,
	[CMFXPS_BranchAdrLine3] [varchar](50) NULL,
	[CMFXPS_BranchAdrPinCode] [numeric](6, 0) NULL,
	[CMFXPS_BranchAdrCity] [varchar](25) NULL,
	[CMFXPS_BranchAdrState] [varchar](25) NULL,
	[CMFXPS_BranchAdrCountry] [varchar](25) NULL,
	[CMFXPS_MICR] [numeric](9, 0) NULL,
	[CMFXPS_IFSC] [varchar](11) NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[PA_AMCCode] [int] NULL,
	[CMFXPS_FolioNum] [varchar](50) NULL,
	[CMFXPS_AccountOpeningDate] [datetime] NULL,
	[CMFXPS_FolioModeOfOperating] [char](5) NULL,
	[CMFXPS_RejectedRemark] [varchar](100) NULL,
	[CMFXPS_IsRejected] [tinyint] NULL,
	[CMFXPS_IsCustomerNew] [tinyint] NULL,
	[CMFXPS_IsFolioNew] [tinyint] NULL,
	[CMFXPS_IsBankAccountNew] [tinyint] NULL,
	[CMFXPS_CreatedOn] [datetime] NULL,
	[CMFXPS_CreatedBy] [int] NULL,
	[CMFXPS_ModifiedOn] [datetime] NULL,
	[CMFXPS_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerMFXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CMFXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAssetGroup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAssetGroup](
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PAG_AssetGroupName] [varchar](30) NULL,
	[PAG_CreatedBy] [int] NOT NULL,
	[PAG_CreatedOn] [datetime] NOT NULL,
	[PAG_ModifiedBy] [int] NOT NULL,
	[PAG_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductAssetGroup] PRIMARY KEY CLUSTERED 
(
	[PAG_AssetGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFXtrnlTransactionInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFXtrnlTransactionInput](
	[CMFXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFXTI_SchemeName] [varchar](100) NULL,
	[CMFXTI_FolioNum] [varchar](50) NULL,
	[CMFXTI_TransactionType] [varchar](50) NULL,
	[CMFXTI_TransactionDate] [datetime] NULL,
	[CMFXTI_BuySell] [varchar](50) NULL,
	[CMFXTI_DividendRate] [varchar](50) NULL,
	[CMFXTI_NAV] [varchar](50) NULL,
	[CMFXTI_Price] [varchar](50) NULL,
	[CMFXTI_Amount] [varchar](50) NULL,
	[CMFXTI_Units] [varchar](50) NULL,
	[CMFXTI_STT] [varchar](50) NULL,
	[CMFXTI_CreatedBy] [int] NULL,
	[CMFXTI_CreatedOn] [datetime] NULL,
	[CMFXTI_ModifiedBy] [int] NULL,
	[CMFXTI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CMFXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpCAMSDataTranslatorMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpCAMSDataTranslatorMapping](
	[WCDTM_Transaction_type] [char](10) NULL,
	[WCDTM_TransactionNature] [varchar](50) NULL,
	[CMT_FinancialFlag] [char](1) NULL,
	[CMT_BuySell] [char](1) NULL,
	[CMT_TransactionType] [varchar](25) NULL,
	[CMT_TransactionTrigger] [varchar](25) NULL,
	[WMTT_TransactionClassificationCode] [varchar](3) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFXtrnlTransactionStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFXtrnlTransactionStaging](
	[CMFXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFXTS_SchemeName] [varchar](100) NULL,
	[CMFXTS_FolioNum] [varchar](50) NULL,
	[CMFXTS_TransactionType] [char](3) NULL,
	[CMFXTS_TransactionDate] [datetime] NULL,
	[CMFXTS_BuySell] [char](1) NULL,
	[CMFXTS_DividendRate] [numeric](10, 5) NULL,
	[CMFXTS_NAV] [numeric](18, 5) NULL,
	[CMFXTS_Price] [numeric](18, 5) NULL,
	[CMFXTS_Amount] [numeric](18, 5) NULL,
	[CMFXTS_Units] [numeric](18, 5) NULL,
	[CMFXTS_STT] [numeric](10, 5) NULL,
	[A_AdviserId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CMFXTS_IsRejected] [tinyint] NULL,
	[CMFXTS_RejectedRemark] [varchar](100) NULL,
	[CMFXTS_CreatedBy] [int] NULL,
	[CMFXTS_CreatedOn] [datetime] NULL,
	[CMFXTS_ModifiedBy] [int] NULL,
	[CMFXTS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CMFXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpKarvyOccupationDataTransalatorMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpKarvyOccupationDataTransalatorMapping](
	[WKODTM_OccCode] [int] NULL,
	[XO_OccupationCode] [varchar](5) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpUploadStatus]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpUploadStatus](
	[WUS_UploadStatusCode] [varchar](5) NULL,
	[WUS_UploadStatus] [varchar](20) NULL,
	[WUS_CreatedBy] [int] NULL,
	[WUS_CreatedOn] [datetime] NULL,
	[WUS_ModifiedBy] [int] NULL,
	[WUS_ModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpKarvyBankAccountTypeDataTranslatorMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpKarvyBankAccountTypeDataTranslatorMapping](
	[WKBATDTM_AccountType] [varchar](50) NULL,
	[XBAT_BankAccountTypeCode] [varchar](5) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlProfileStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlProfileStaging](
	[CMGCXPS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[PA_AMCId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[ADUL_ProcessId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[WRR_RejectReasonCode] [int] NULL,
	[WUS_UploadStatusCode] [varchar](5) NULL,
	[CMGCXPS_FOLIOCHK] [varchar](20) NULL,
	[CMGCXPS_INV_NAME] [varchar](75) NULL,
	[CMGCXPS_ADDRESS1] [varchar](75) NULL,
	[CMGCXPS_ADDRESS2] [varchar](75) NULL,
	[CMGCXPS_ADDRESS3] [varchar](75) NULL,
	[CMGCXPS_CITY] [varchar](25) NULL,
	[CMGCXPS_PINCODE] [numeric](10, 0) NULL,
	[CMGCXPS_PRODUCT] [varchar](30) NULL,
	[CMGCXPS_SCH_NAME] [varchar](150) NULL,
	[CMGCXPS_REP_DATE] [datetime] NULL,
	[CMGCXPS_CLOS_BAL] [numeric](18, 4) NULL,
	[CMGCXPS_RUPEE_BAL] [numeric](18, 4) NULL,
	[CMGCXPS_SUBBROK] [varchar](50) NULL,
	[CMGCXPS_REINV_FLAG] [varchar](30) NULL,
	[CMGCXPS_JOINT_NAME1] [varchar](75) NULL,
	[CMGCXPS_JOINT_NAME2] [varchar](75) NULL,
	[CMGCXPS_PHONE_OFF] [numeric](25, 0) NULL,
	[CMGCXPS_PHONE_RES] [numeric](25, 0) NULL,
	[CMGCXPS_EMAIL] [varchar](75) NULL,
	[CMGCXPS_HOLDING_NA] [varchar](20) NULL,
	[CMGCXPS_UIN_NO] [varchar](50) NULL,
	[CMGCXPS_BROKER_COD] [varchar](50) NULL,
	[CMGCXPS_PAN_NO] [varchar](20) NULL,
	[CMGCXPS_JOINT1_PAN] [varchar](20) NULL,
	[CMGCXPS_JOINT2_PAN] [varchar](20) NULL,
	[CMGCXPS_GUARD_PAN] [varchar](20) NULL,
	[CMGCXPS_TAX_STATUS] [varchar](50) NULL,
	[CMGCXPS_INV_IIN] [varchar](20) NULL,
	[CMGCXPS_ALTFOLIO] [varchar](50) NULL,
	[CMGCXPS_IsRejected] [tinyint] NULL,
	[CMGCXPS_IsFolioNew] [tinyint] NULL,
	[CMGCXPS_IsCustomerNew] [tinyint] NULL,
	[CMGCXPS_IsAMCNew] [tinyint] NULL,
	[CMGCXPS_RejectReason] [varchar](50) NULL,
	[CMGCXPS_CreatedBy] [int] NULL,
	[CMGCXPS_CreatedOn] [datetime] NULL,
	[CMGCXPS_ModifiedBy] [int] NULL,
	[CMGCXPS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CMGCXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlTransactionInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlTransactionInput](
	[CMCXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CMCXTI_AMCCode] [varchar](150) NULL,
	[CMCXTI_FolioNum] [varchar](150) NULL,
	[CMCXTI_ProductCode] [varchar](150) NULL,
	[CMCXTI_Scheme] [varchar](150) NULL,
	[CMCXTI_InvestorName] [varchar](150) NULL,
	[CMCXTI_TransactionType] [varchar](150) NULL,
	[CMCXTI_TransactionNum] [varchar](150) NULL,
	[CMCXTI_TransactionMode] [varchar](150) NULL,
	[CMCXTI_TransactionStatus] [varchar](150) NULL,
	[CMCXTI_UserCode] [varchar](150) NULL,
	[CMCXTI_UserTransactionNum] [varchar](150) NULL,
	[CMCXTI_ValueDate] [varchar](150) NULL,
	[CMCXTI_PostDate] [varchar](150) NULL,
	[CMCXTI_Price] [varchar](150) NULL,
	[CMCXTI_Units] [varchar](150) NULL,
	[CMCXTI_Amount] [varchar](150) NULL,
	[CMCXTI_BrokerCode] [varchar](150) NULL,
	[CMCXTI_SubBrokerCode] [varchar](150) NULL,
	[CMCXTI_BrokeragePercentage] [varchar](150) NULL,
	[CMCXTI_BrokerageAmount] [varchar](150) NULL,
	[CMCXTI_Dummy1] [varchar](150) NULL,
	[CMCXTI_FeedDate] [varchar](150) NULL,
	[CMCXTI_Dummy2] [varchar](150) NULL,
	[CMCXTI_Dummy3] [varchar](150) NULL,
	[CMCXTI_ApplicationNum] [varchar](150) NULL,
	[CMCXTI_TransactionNature] [varchar](150) NULL,
	[CMCXTI_TaxStatus] [varchar](150) NULL,
	[CMCXTI_AlternateBroker] [varchar](150) NULL,
	[CMCXTI_AlternateFolio] [varchar](150) NULL,
	[CMCXTI_ReinvestmentFlag] [varchar](150) NULL,
	[CMCXTI_OldFolio] [varchar](150) NULL,
	[CMCXTI_SequenceNum] [varchar](150) NULL,
	[CMCXTI_MultipleBroker] [varchar](150) NULL,
	[CMCXTI_Tax] [varchar](150) NULL,
	[CMCXTI_STT] [varchar](150) NULL,
	[CMCXTI_SchemeType] [varchar](150) NULL,
	[CMCXTI_EntryLoad] [varchar](150) NULL,
	[CMCXTI_ScanRefNum] [varchar](150) NULL,
	[CMCXTI_InvestorIIN] [varchar](150) NULL,
	[CMCXTI_TaxStatus1] [varchar](150) NULL,
	[CMCXTI_StatusCode] [varchar](150) NULL,
	[CMCXTI_CreatedBy] [int] NULL,
	[CMCXTI_CreatedOn] [datetime] NULL,
	[CMCXTI_ModifiedBy] [int] NULL,
	[CMCXTI_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CMCXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLRelationship]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLRelationship](
	[XR_RelationshipCode] [varchar](5) NOT NULL,
	[XR_Relationship] [varchar](30) NULL,
	[XR_CreatedBy] [int] NULL,
	[XR_ModifiedBy] [int] NULL,
	[XR_CreatedOn] [datetime] NULL,
	[XR_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLRelationship] PRIMARY KEY CLUSTERED 
(
	[XR_RelationshipCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLExchange]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLExchange](
	[XE_ExchangeCode] [varchar](5) NOT NULL,
	[XE_ExchangeName] [varchar](10) NULL,
	[XE_CreatedBy] [int] NULL,
	[XE_CreatedOn] [datetime] NULL,
	[XE_ModifiedBy] [int] NULL,
	[XE_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLExchange] PRIMARY KEY CLUSTERED 
(
	[XE_ExchangeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[U_UserId] [int] IDENTITY(1000,1) NOT NULL,
	[U_Password] [varchar](50) NULL,
	[U_FirstName] [varchar](50) NULL,
	[U_MiddleName] [varchar](50) NULL,
	[U_LastName] [varchar](50) NULL,
	[U_Email] [varchar](max) NULL,
	[U_UserType] [varchar](10) NULL,
	[U_LoginId] [varchar](max) NULL,
	[U_CreatedBy] [int] NOT NULL,
	[U_ModifiedBy] [int] NOT NULL,
	[U_CreatedOn] [datetime] NOT NULL,
	[U_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[U_UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
/****** Object:  Table [dbo].[WerpKarvyCustomerTypeDataTranslatorMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpKarvyCustomerTypeDataTranslatorMapping](
	[WKCTDTM_TaxStatus] [varchar](5) NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomerSubTypeCode] [varchar](5) NULL,
	[WKCTDTM_CreatedBy] [int] NULL,
	[WKCTDTM_CreatedOn] [datetime] NULL,
	[WKCTDTM_ModifiedBy] [int] NULL,
	[WKCTDTM_ModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpEquityTransactionType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpEquityTransactionType](
	[WETT_TransactionCode] [tinyint] IDENTITY(1,1) NOT NULL,
	[WETT_TransactionTypeName] [varchar](30) NULL,
	[WETT_IsCorpAxn] [tinyint] NULL,
	[WETT_IsCorpAxnOffer] [tinyint] NULL,
	[WETT_IsImpactingTransaction] [tinyint] NULL,
	[WETT_UIName] [varchar](30) NULL,
	[WETT_CreatedBy] [int] NULL,
	[WETT_CreatedOn] [datetime] NULL,
	[WETT_ModifiedBy] [int] NULL,
	[WETT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpEquityTransactionType] PRIMARY KEY CLUSTERED 
(
	[WETT_TransactionCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpGoalMaster]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpGoalMaster](
	[WGM_GoalId] [bigint] IDENTITY(1000,1) NOT NULL,
	[WQC_QCatId] [bigint] NOT NULL,
	[WGM_GoalName] [varchar](50) NOT NULL,
	[WGM_CreatedBy] [bigint] NOT NULL,
	[WGM_ModifiedBy] [bigint] NOT NULL,
	[WGM_CreatedOn] [datetime] NOT NULL,
	[WGM_ModifiedOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Goal Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpGoalMaster'
GO
/****** Object:  Table [dbo].[XMLExternalSource]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLExternalSource](
	[XES_SourceCode] [varchar](5) NOT NULL,
	[XES_SourceName] [varchar](10) NULL,
	[XES_CreatedBy] [int] NULL,
	[XES_CreatedOn] [datetime] NULL,
	[XES_ModifiedBy] [int] NULL,
	[XES_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLExternalSource] PRIMARY KEY CLUSTERED 
(
	[XES_SourceCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlTransactionStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlTransactionStaging](
	[CMCXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NULL,
	[ADUL_ProcessId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CMCXTS_AMCCode] [varchar](10) NULL,
	[CMCXTS_FolioNum] [varchar](50) NULL,
	[CMCXTS_ProductCode] [varchar](50) NULL,
	[CMCXTS_Scheme] [varchar](150) NULL,
	[CMCXTS_InvestorName] [varchar](75) NULL,
	[CMCXTS_TransactionType] [varchar](10) NULL,
	[CMCXTS_TransactionNum] [varchar](20) NULL,
	[CMCXTS_TransactionMode] [varchar](5) NULL,
	[CMCXTS_TransactionStatus] [varchar](50) NULL,
	[CMCXTS_UserCode] [varchar](25) NULL,
	[CMCXTS_UserTransactionNum] [varchar](50) NULL,
	[CMCXTS_ValueDate] [datetime] NULL,
	[CMCXTS_PostDate] [datetime] NULL,
	[CMCXTS_Price] [numeric](25, 12) NULL,
	[CMCXTS_Units] [numeric](25, 12) NULL,
	[CMCXTS_Amount] [numeric](25, 12) NULL,
	[CMCXTS_BrokerCode] [varchar](50) NULL,
	[CMCXTS_SubBrokerCode] [varchar](50) NULL,
	[CMCXTS_BrokeragePercentage] [numeric](25, 12) NULL,
	[CMCXTS_BrokerageAmount] [numeric](25, 12) NULL,
	[CMCXTS_Dummy1] [varchar](50) NULL,
	[CMCXTS_FeedDate] [datetime] NULL,
	[CMCXTS_Dummy2] [varchar](50) NULL,
	[CMCXTS_Dummy3] [varchar](50) NULL,
	[CMCXTS_ApplicationNum] [varchar](25) NULL,
	[CMCXTS_TransactionNature] [varchar](25) NULL,
	[CMCXTS_TaxStatus] [varchar](25) NULL,
	[CMCXTS_AlternateBroker] [varchar](50) NULL,
	[CMCXTS_AlternateFolio] [varchar](16) NULL,
	[CMCXTS_ReinvestmentFlag] [char](1) NULL,
	[CMCXTS_OldFolio] [varchar](20) NULL,
	[CMCXTS_SequenceNum] [varchar](16) NULL,
	[CMCXTS_MultipleBroker] [varchar](16) NULL,
	[CMCXTS_Tax] [numeric](25, 12) NULL,
	[CMCXTS_STT] [numeric](25, 12) NULL,
	[CMCXTS_SchemeType] [varchar](50) NULL,
	[CMCXTS_EntryLoad] [numeric](25, 12) NULL,
	[CMCXTS_ScanRefNum] [varchar](50) NULL,
	[CMCXTS_InvestorIIN] [varchar](50) NULL,
	[CMCXTS_TaxStatus1] [varchar](50) NULL,
	[CMCXTS_StatusCode] [varchar](50) NULL,
	[CMCXTS_CreatedBy] [int] NULL,
	[CMCXTS_CreatedOn] [datetime] NULL,
	[CMCXTS_ModifiedBy] [int] NULL,
	[CMCXTS_ModifiedOn] [datetime] NULL,
	[CMCXTS_IsRejected] [tinyint] NULL,
	[CMCXTS_RejectedRemark] [varchar](100) NULL,
	[CMCXTS_IsFolioNew] [tinyint] NULL,
	[CP_PortfolioId] [int] NULL,
	[WUS_UploadStatusCode] [varchar](5) NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CMCXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer CAMS MF Staging Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMFCAMSXtrnlTransactionStaging'
GO
/****** Object:  Table [dbo].[XMLNationality]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLNationality](
	[XN_NationalityCode] [varchar](5) NOT NULL,
	[XN_Nationality] [varchar](30) NULL,
	[XN_CreatedBy] [int] NULL,
	[XN_CreatedOn] [datetime] NULL,
	[XN_ModifiedBy] [int] NULL,
	[XN_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLNationality] PRIMARY KEY CLUSTERED 
(
	[XN_NationalityCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLSystematicTransactionType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLSystematicTransactionType](
	[XSTT_SystematicTypeCode] [varchar](5) NOT NULL,
	[XSTT_SystematicType] [varchar](30) NULL,
	[XSTT_CreatedBy] [int] NULL,
	[XSTT_CreatedOn] [datetime] NULL,
	[XSTT_ModifiedBy] [int] NULL,
	[XSTT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLSystematicTransactionType] PRIMARY KEY CLUSTERED 
(
	[XSTT_SystematicTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLMaritalStatus]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLMaritalStatus](
	[XMS_MaritalStatusCode] [varchar](5) NOT NULL,
	[XMS_MaritalStatus] [varchar](30) NULL,
	[XMS_CreatedBy] [int] NULL,
	[XMS_CreatedOn] [datetime] NULL,
	[XMS_ModifiedBy] [int] NULL,
	[XMS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLMaritalStatus] PRIMARY KEY CLUSTERED 
(
	[XMS_MaritalStatusCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLAdviserLOBAssetGroup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLAdviserLOBAssetGroup](
	[XALAG_LOBAssetGroupsCode] [varchar](5) NOT NULL,
	[XALAG_LOBAssetGroup] [varchar](20) NULL,
	[XALAG_CreatedBy] [int] NULL,
	[XALAG_CreatedOn] [datetime] NULL,
	[XALAG_ModifiedBy] [int] NULL,
	[XALAG_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLAdviserLOBAssetGroup] PRIMARY KEY CLUSTERED 
(
	[XALAG_LOBAssetGroupsCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpUploadXMLFileType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpUploadXMLFileType](
	[WUXFT_XMLFileTypeId] [int] IDENTITY(1,1) NOT NULL,
	[WUXFT_XMLFileName] [varchar](50) NULL,
	[WUXFT_CreatedBy] [int] NULL,
	[WUXFT_CreatedOn] [datetime] NULL,
	[WUXFT_ModifiedBy] [int] NULL,
	[WUXFT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpUploadXMLFileType] PRIMARY KEY CLUSTERED 
(
	[WUXFT_XMLFileTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLAdviserBusinessType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLAdviserBusinessType](
	[XABT_BusinessTypeCode] [varchar](5) NOT NULL,
	[XABT_BusinessType] [varchar](30) NULL,
	[XABT_CreatedBy] [int] NULL,
	[XABT_CreatedOn] [datetime] NULL,
	[XABT_ModifiedBy] [int] NULL,
	[XABT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_BusinessType] PRIMARY KEY CLUSTERED 
(
	[XABT_BusinessTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BusinessType_XML Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'XMLAdviserBusinessType'
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlProfileStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlProfileStaging](
	[CMFKXPS_Id] [int] IDENTITY(1,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[PA_AMCId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[WUS_UploadStatusCode] [varchar](5) NULL,
	[CMFKXPS_ProductCode] [varchar](15) NULL,
	[CMFKXPS_Fund] [varchar](50) NULL,
	[CMFKXPS_Folio] [varchar](20) NULL,
	[CMFKXPS_DividendOption] [varchar](50) NULL,
	[CMFKXPS_FundDescription] [varchar](150) NULL,
	[CMFKXPS_InvestorName] [varchar](75) NULL,
	[CMFKXPS_JointName1] [varchar](75) NULL,
	[CMFKXPS_JointName2] [varchar](75) NULL,
	[CMFKXPS_Address#1] [varchar](75) NULL,
	[CMFKXPS_Address#2] [varchar](75) NULL,
	[CMFKXPS_Address#3] [varchar](75) NULL,
	[CMFKXPS_City] [varchar](25) NULL,
	[CMFKXPS_Pincode] [numeric](10, 0) NULL,
	[CMFKXPS_State] [varchar](25) NULL,
	[CMFKXPS_Country] [varchar](25) NULL,
	[CMFKXPS_TPIN] [varchar](50) NULL,
	[CMFKXPS_DateofBirth] [datetime] NULL,
	[CMFKXPS_FName] [varchar](75) NULL,
	[CMFKXPS_MName] [varchar](75) NULL,
	[CMFKXPS_PhoneResidence] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneRes#1] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneRes#2] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneOffice] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneOff#1] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneOff#2] [numeric](25, 0) NULL,
	[CMFKXPS_FaxResidence] [numeric](25, 0) NULL,
	[CMFKXPS_FaxOffice] [numeric](25, 0) NULL,
	[CMFKXPS_TaxStatus] [varchar](50) NULL,
	[CMFKXPS_OccCode] [varchar](50) NULL,
	[CMFKXPS_Email] [varchar](100) NULL,
	[CMFKXPS_BankAccno] [varchar](50) NULL,
	[CMFKXPS_BankName] [varchar](75) NULL,
	[CMFKXPS_AccountType] [varchar](25) NULL,
	[CMFKXPS_Branch] [varchar](75) NULL,
	[CMFKXPS_BankAddress#1] [varchar](75) NULL,
	[CMFKXPS_BankAddress#2] [varchar](75) NULL,
	[CMFKXPS_BankAddress#3] [varchar](75) NULL,
	[CMFKXPS_BankCity] [varchar](25) NULL,
	[CMFKXPS_BankPhone] [numeric](25, 0) NULL,
	[CMFKXPS_BankState] [varchar](25) NULL,
	[CMFKXPS_BankCountry] [varchar](25) NULL,
	[CMFKXPS_InvestorID] [varchar](50) NULL,
	[CMFKXPS_BrokerCode] [varchar](50) NULL,
	[CMFKXPS_PANNumber] [varchar](20) NULL,
	[CMFKXPS_Mobile] [numeric](20, 0) NULL,
	[CMFKXPS_ReportDate] [datetime] NULL,
	[CMFKXPS_ReportTime] [datetime] NULL,
	[CMFKXPS_OccupationDescription] [varchar](50) NULL,
	[CMFKXPS_ModeofHolding] [varchar](50) NULL,
	[CMFKXPS_ModeofHoldingDescription] [varchar](50) NULL,
	[CMFKXPS_MapinId] [varchar](50) NULL,
	[CMFKXPS_IsRejected] [tinyint] NULL,
	[CMFKXPS_IsFolioNew] [tinyint] NULL,
	[CMFKXPS_IsBankAccountNew] [tinyint] NULL,
	[CMFKXPS_IsCustomerNew] [tinyint] NULL,
	[WRR_RejectReasonCode] [int] NULL,
	[CMFKXPS_RejectReason] [varchar](50) NULL,
	[CMFKXPS_IsAMCNew] [tinyint] NULL,
	[CMFKXPS_CreatedBy] [int] NULL,
	[CMFKXPS_CreatedOn] [datetime] NULL,
	[CMFKXPS_ModifiedOn] [datetime] NULL,
	[CMFKXPS_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CMFKXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpQuestionCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpQuestionCategory](
	[WQC_QCatId] [bigint] NOT NULL,
	[WQC_QuestionCategory] [varchar](max) NULL,
	[WQC_CreatedBy] [bigint] NOT NULL,
	[WQC_CreatedOn] [datetime] NOT NULL,
	[WQC_ModifiedBy] [bigint] NOT NULL,
	[WQC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_QuestionCategory] PRIMARY KEY CLUSTERED 
(
	[WQC_QCatId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Question Category Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpQuestionCategory'
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlTransactionInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlTransactionInput](
	[CIMFKXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[CIMFKXTI_ProductCode] [varchar](150) NULL,
	[CIMFKXTI_Fund] [varchar](150) NULL,
	[CIMFKXTI_FolioNumber] [varchar](150) NULL,
	[CIMFKXTI_SchemeCode] [varchar](150) NULL,
	[CIMFKXTI_DividendOption] [varchar](150) NULL,
	[CIMFKXTI_FundDescription] [varchar](150) NULL,
	[CIMFKXTI_TransactionHead] [varchar](150) NULL,
	[CIMFKXTI_TransactionNumber] [varchar](150) NULL,
	[CIMFKXTI_Switch_RefNo] [varchar](150) NULL,
	[CIMFKXTI_InstrumentNumber] [varchar](150) NULL,
	[CIMFKXTI_InvestorName] [varchar](150) NULL,
	[CIMFKXTI_TransactionMode] [varchar](150) NULL,
	[CIMFKXTI_TransactionStatus] [varchar](150) NULL,
	[CIMFKXTI_BranchName] [varchar](150) NULL,
	[CIMFKXTI_BranchTransactionNo] [varchar](150) NULL,
	[CIMFKXTI_TransactionDate] [varchar](150) NULL,
	[CIMFKXTI_ProcessDate] [varchar](150) NULL,
	[CIMFKXTI_Price] [varchar](150) NULL,
	[CIMFKXTI_LoadPercentage] [varchar](150) NULL,
	[CIMFKXTI_Units] [varchar](150) NULL,
	[CIMFKXTI_Amount] [varchar](150) NULL,
	[CIMFKXTI_LoadAmount] [varchar](150) NULL,
	[CIMFKXTI_AgentCode] [varchar](150) NULL,
	[CIMFKXTI_Sub-BrokerCode] [varchar](150) NULL,
	[CIMFKXTI_BrokeragePercentage] [varchar](150) NULL,
	[CIMFKXTI_Commission] [varchar](150) NULL,
	[CIMFKXTI_InvestorID] [varchar](150) NULL,
	[CIMFKXTI_ReportDate] [varchar](150) NULL,
	[CIMFKXTI_ReportTime] [varchar](150) NULL,
	[CIMFKXTI_TransactionSub] [varchar](150) NULL,
	[CIMFKXTI_ApplicationNumber] [varchar](150) NULL,
	[CIMFKXTI_TransactionID] [varchar](150) NULL,
	[CIMFKXTI_TransactionDescription] [varchar](150) NULL,
	[CIMFKXTI_TransactionType] [varchar](150) NULL,
	[CIMFKXTI_OrgPurchaseDate] [varchar](150) NULL,
	[CIMFKXTI_OrgPurchaseAmount] [varchar](150) NULL,
	[CIMFKXTI_OrgPurchaseUnits] [varchar](150) NULL,
	[CIMFKXTI_TrTypeFlag] [varchar](150) NULL,
	[CIMFKXTI_SwitchFundDate] [varchar](150) NULL,
	[CIMFKXTI_InstrumentDate] [varchar](150) NULL,
	[CIMFKXTI_InstrumentBank] [varchar](150) NULL,
	[CIMFKXTI_Nav] [varchar](150) NULL,
	[CIMFKXTI_PurchaseTrnNo] [varchar](150) NULL,
	[CIMFKXTI_STT] [varchar](150) NULL,
	[CIMFKXTI_IHNo] [varchar](150) NULL,
	[CIMFKXTI_BranchCode] [varchar](150) NULL,
	[CIMFKXTI_InwardNo] [varchar](150) NULL,
	[CIMFKXTI_Remarks] [varchar](150) NULL,
	[CIMFKXTI_PAN1] [varchar](150) NULL,
	[CIMFKXTI_Dummy1] [varchar](150) NULL,
	[CIMFKXTI_Dummy2] [varchar](150) NULL,
	[CIMFKXTI_Dummy3] [varchar](150) NULL,
	[CIMFKXTI_Dummy4] [varchar](150) NULL,
	[CIMFKXTI_NCTREMARKS] [varchar](150) NULL,
	[CIMFKXTI_Dummy5] [varchar](150) NULL,
	[A_AdviserId] [int] NULL,
	[CIMFKXTI_CreatedBy] [int] NULL,
	[CIMFKXTI_CreatedOn] [datetime] NULL,
	[CIMFKXTI_ModifiedBy] [int] NULL,
	[CIMFKXTI_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerInvestmentMFKarvyXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CIMFKXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLAdviserLOBCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLAdviserLOBCategory](
	[XALC_LOBCategoryCode] [varchar](5) NOT NULL,
	[XALC_LOBCategory] [varchar](30) NULL,
	[XALC_CreatedBy] [int] NULL,
	[XALC_CreatedOn] [datetime] NULL,
	[XALC_ModifiedBy] [int] NULL,
	[XALC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLAdviserLOBCategory] PRIMARY KEY CLUSTERED 
(
	[XALC_LOBCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLBroker]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLBroker](
	[XB_BrokerCode] [varchar](5) NOT NULL,
	[XB_BrokerName] [varchar](70) NULL,
	[XB_BrokerIdentifier] [varchar](30) NULL,
	[XB_CreatedBy] [int] NULL,
	[XB_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLBroker] PRIMARY KEY CLUSTERED 
(
	[XB_BrokerCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLCustomerType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLCustomerType](
	[XCT_CustomerTypeCode] [varchar](5) NOT NULL,
	[XCT_CustomerTypeName] [varchar](20) NULL,
	[XCT_CreatedBy] [int] NULL,
	[XCT_CreatedOn] [datetime] NULL,
	[XCT_ModifiedBy] [int] NULL,
	[XCT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLCustomerType] PRIMARY KEY CLUSTERED 
(
	[XCT_CustomerTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlTransactionStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlTransactionStaging](
	[CIMFKXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[CIMFKXTS_ProductCode] [varchar](50) NULL,
	[CIMFKXTS_Fund] [varchar](50) NULL,
	[CIMFKXTS_FolioNumber] [varchar](50) NULL,
	[CIMFKXTS_SchemeCode] [varchar](50) NULL,
	[CIMFKXTS_DividendOption] [varchar](50) NULL,
	[CIMFKXTS_FundDescription] [varchar](150) NULL,
	[CIMFKXTS_TransactionHead] [varchar](50) NULL,
	[CIMFKXTS_TransactionNumber] [varchar](50) NULL,
	[CIMFKXTS_Switch_RefNo] [varchar](50) NULL,
	[CIMFKXTS_InstrumentNumber] [varchar](50) NULL,
	[CIMFKXTS_InvestorName] [varchar](75) NULL,
	[CIMFKXTS_TransactionMode] [varchar](50) NULL,
	[CIMFKXTS_TransactionStatus] [varchar](50) NULL,
	[CIMFKXTS_BranchName] [varchar](50) NULL,
	[CIMFKXTS_BranchTransactionNo] [varchar](50) NULL,
	[CIMFKXTS_TransactionDate] [datetime] NULL,
	[CIMFKXTS_ProcessDate] [datetime] NULL,
	[CIMFKXTS_Price] [numeric](25, 12) NULL,
	[CIMFKXTS_LoadPercentage] [numeric](25, 12) NULL,
	[CIMFKXTS_Units] [numeric](25, 12) NULL,
	[CIMFKXTS_Amount] [numeric](25, 12) NULL,
	[CIMFKXTS_LoadAmount] [numeric](25, 12) NULL,
	[CIMFKXTS_AgentCode] [varchar](50) NULL,
	[CIMFKXTS_SubBrokerCode] [varchar](50) NULL,
	[CIMFKXTS_BrokeragePercentage] [numeric](25, 12) NULL,
	[CIMFKXTS_Commission] [numeric](25, 12) NULL,
	[CIMFKXTS_InvestorID] [varchar](50) NULL,
	[CIMFKXTS_ReportDate] [datetime] NULL,
	[CIMFKXTS_ReportTime] [datetime] NULL,
	[CIMFKXTS_TransactionSub] [varchar](50) NULL,
	[CIMFKXTS_ApplicationNumber] [varchar](50) NULL,
	[CIMFKXTS_TransactionID] [varchar](50) NULL,
	[CIMFKXTS_TransactionDescription] [varchar](50) NULL,
	[CIMFKXTS_TransactionType] [varchar](50) NULL,
	[CIMFKXTS_OrgPurchaseDate] [datetime] NULL,
	[CIMFKXTS_OrgPurchaseAmount] [numeric](25, 12) NULL,
	[CIMFKXTS_OrgPurchaseUnits] [numeric](25, 12) NULL,
	[CIMFKXTS_TrTypeFlag] [varchar](50) NULL,
	[CIMFKXTS_SwitchFundDate] [datetime] NULL,
	[CIMFKXTS_InstrumentDate] [datetime] NULL,
	[CIMFKXTS_InstrumentBank] [varchar](50) NULL,
	[CIMFKXTS_Nav] [numeric](25, 12) NULL,
	[CIMFKXTS_PurchaseTrnNo] [varchar](50) NULL,
	[CIMFKXTS_STT] [numeric](25, 12) NULL,
	[CIMFKXTS_IHNo] [varchar](50) NULL,
	[CIMFKXTS_BranchCode] [varchar](50) NULL,
	[CIMFKXTS_InwardNo] [varchar](50) NULL,
	[CIMFKXTS_Remarks] [varchar](150) NULL,
	[CIMFKXTS_PAN1] [varchar](50) NULL,
	[A_AdviserId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[CIMFKXTS_IsRejected] [tinyint] NULL,
	[CIMFKXTS_IsFolioNew] [tinyint] NULL,
	[CIMFKXTS_RejectionRemark] [varchar](50) NULL,
	[CIMFKXTS_Dummy1] [varchar](50) NULL,
	[CIMFKXTS_Dummy2] [varchar](50) NULL,
	[CIMFKXTS_Dummy3] [varchar](50) NULL,
	[CIMFKXTS_Dummy4] [varchar](50) NULL,
	[CIMFKXTS_NCTREMARKS] [varchar](50) NULL,
	[CIMFKXTS_Dummy5] [varchar](50) NULL,
	[CIMFKXTS_CreatedBy] [int] NULL,
	[CIMFKXTS_CreatedOn] [datetime] NULL,
	[CIMFKXTS_ModifiedBy] [int] NULL,
	[CIMFKXTS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerInvestmentMFKarvyXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CIMFKXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertEventSetup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlertEventSetup](
	[AES_EventSetupID] [bigint] IDENTITY(1,1) NOT NULL,
	[AEL_EventID] [smallint] NOT NULL,
	[AES_EventMessage] [varchar](500) NULL,
	[AES_SchemeID] [int] NULL,
	[AES_TargetID] [int] NOT NULL,
	[AES_EventSubscriptionDate] [datetime] NOT NULL,
	[AES_NextOccurence] [datetime] NULL,
	[AES_LastOccurence] [datetime] NULL,
	[AES_EndDate] [datetime] NULL,
	[AES_ParentEventSetupId] [bigint] NULL,
	[CL_CycleID] [tinyint] NULL,
	[AES_CreatedFor] [int] NULL,
	[AES_DeliveryMode] [varchar](8) NOT NULL,
	[AES_SentToQueue] [bit] NOT NULL,
	[AES_CreatedBy] [int] NULL,
	[AES_CreatedOn] [datetime] NULL,
	[AES_ModifiedOn] [datetime] NULL,
	[AES_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_TABLE_EVENT_SETUP] PRIMARY KEY CLUSTERED 
(
	[AES_EventSetupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpKarvyDataTransalatorMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpKarvyDataTransalatorMapping](
	[WKDTM_TransactionHead] [nchar](10) NULL,
	[WKDTM_TransactionDescription] [nvarchar](50) NULL,
	[WKDTM_TransactionType] [nchar](10) NULL,
	[WKDTM_TransactionTypeFlag] [nchar](10) NULL,
	[CMT_FinancialFlag] [char](1) NULL,
	[CMT_BuySell] [char](1) NULL,
	[CMT_TransactionType] [varchar](25) NULL,
	[CMT_TransactionTrigger] [varchar](25) NULL,
	[WMTT_TransactionClassificationCode] [varchar](3) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityOdinBSEXtrnlTransactionStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransactionStaging](
	[CEOBXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[AUDL_ProcessId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CETA_AccountId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[CEOBXTS_ScripCode] [varchar](50) NULL,
	[CEOBXTS_ScripName] [varchar](50) NULL,
	[CEOBXTS_TradeNumber] [numeric](10, 0) NULL,
	[CEOBXTS_Rate] [numeric](25, 12) NULL,
	[CEOBXTS_Quantity] [numeric](25, 12) NULL,
	[CEOBXTS_Field6] [varchar](20) NULL,
	[CEOBXTS_Field7] [varchar](20) NULL,
	[CEOBXTS_TradeTime] [datetime] NULL,
	[CEOBXTS_TradeDate] [datetime] NULL,
	[CEOBXTS_TradeAccountNumber] [varchar](20) NULL,
	[CEOBXTS_BuySell] [char](1) NULL,
	[CEOBXTS_Field12] [varchar](5) NULL,
	[CEOBXTS_OrderNumber] [numeric](15, 0) NULL,
	[CEOBXTS_Field14] [varchar](5) NULL,
	[CEOBXTS_AccountStatus] [varchar](20) NULL,
	[CEOBXTS_CreatedBy] [int] NULL,
	[CEOBXTS_CreatedOn] [datetime] NULL,
	[CEOBXTS_ModifiedBy] [int] NULL,
	[CEOBXTS_ModifiedOn] [datetime] NULL,
	[CEOBXTS_RejectedRemark] [varchar](100) NULL,
	[CEOBXTS_IsRejected] [tinyint] NULL,
	[CEOBXTS_IsTradeNumberNew] [tinyint] NULL,
	[WUS_UploadStatusCode] [varchar](5) NULL,
 CONSTRAINT [PK_CustomerEquityOdinBSEXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CEOBXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLModeOfHolding](
	[XMOH_ModeOfHoldingCode] [varchar](5) NOT NULL,
	[XMOH_ModeOfHolding] [varchar](30) NULL,
	[XMOH_CreatedBy] [int] NULL,
	[XMOH_CreatedOn] [datetime] NULL,
	[XMOH_ModifiedBy] [int] NULL,
	[XMOH_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLModeOfHolding] PRIMARY KEY CLUSTERED 
(
	[XMOH_ModeOfHoldingCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAMC]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAMC](
	[PA_AMCCode] [int] IDENTITY(1,1) NOT NULL,
	[PA_AMCName] [varchar](50) NULL,
	[PA_ModifiedBy] [int] NULL,
	[PA_ModifiedOn] [datetime] NULL,
	[PA_CreatedBy] [int] NULL,
	[PA_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductAMC] PRIMARY KEY CLUSTERED 
(
	[PA_AMCCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product AMC Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductAMC'
GO
/****** Object:  Table [dbo].[XMLFiscalYear]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLFiscalYear](
	[XFY_FiscalYearCode] [varchar](5) NOT NULL,
	[XFY_FiscalYear] [varchar](20) NULL,
	[XFY_CreatedBy] [int] NULL,
	[XFY_CreatedOn] [datetime] NULL,
	[XFY_ModifiedOn] [datetime] NULL,
	[XFY_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_WerpFiscalYear_XML] PRIMARY KEY CLUSTERED 
(
	[XFY_FiscalYearCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLPaymentMode]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLPaymentMode](
	[XPM_PaymentModeCode] [varchar](5) NOT NULL,
	[XPM_PaymentMode] [varchar](20) NULL,
	[XPM_CreatedBy] [int] NULL,
	[XPM_CreatedOn] [datetime] NULL,
	[XPM_ModifiedBy] [int] NULL,
	[XPM_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLPaymentMode] PRIMARY KEY CLUSTERED 
(
	[XPM_PaymentModeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLBankAccountType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLBankAccountType](
	[XBAT_BankAccountTypeCode] [varchar](5) NOT NULL,
	[XBAT_BankAccountTye] [varchar](30) NULL,
	[XBAT_CreatedBy] [int] NULL,
	[XBAT_CreatedOn] [datetime] NULL,
	[XBAT_ModifiedBy] [int] NULL,
	[XBAT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLBankAccountType] PRIMARY KEY CLUSTERED 
(
	[XBAT_BankAccountTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertEventLookup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlertEventLookup](
	[AEL_EventID] [smallint] IDENTITY(1,1) NOT NULL,
	[AEL_EventCode] [varchar](50) NOT NULL,
	[AEL_EventType] [char](20) NOT NULL,
	[AETL_EventTypeID] [tinyint] NULL,
	[AEL_Reminder] [bit] NOT NULL,
	[AEL_DefaultMessage] [varchar](200) NULL,
	[AEL_TriggerCondition] [varchar](2) NOT NULL,
	[AEL_FieldName] [varchar](1000) NULL,
	[AEL_DataConditionField] [varchar](150) NULL,
	[AEL_TableName] [varchar](1000) NULL,
	[AEL_PrimarySubscriber] [varchar](50) NULL,
	[CL_CycleID] [tinyint] NULL,
	[AEL_IsAvailable] [bit] NOT NULL,
	[AEL_CreatedBy] [int] NULL,
	[AEL_CreatedOn] [datetime] NULL,
	[AEL_ModifiedOn] [datetime] NULL,
	[AEL_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_TABLE_EVENT_LOOKUP] PRIMARY KEY CLUSTERED 
(
	[AEL_EventID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertDataServiceLog]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlertDataServiceLog](
	[ADSL_CurrentOccurence] [datetime] NOT NULL,
	[ADSL_LastOccurence] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlertDeliveryModeLookup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlertDeliveryModeLookup](
	[ADML_ModeId] [tinyint] NOT NULL,
	[ADML_ModeDesc] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertEventTypeLookup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlertEventTypeLookup](
	[AETL_EventTypeID] [tinyint] IDENTITY(1,1) NOT NULL,
	[AETL_EventTypeDesc] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertDataConditionSetup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlertDataConditionSetup](
	[ADCS_RuleID] [bigint] IDENTITY(1,1) NOT NULL,
	[ADCS_UserID] [int] NOT NULL,
	[ADCS_SchemeID] [int] NOT NULL,
	[AEL_EventID] [smallint] NOT NULL,
	[ADCS_Condition] [varchar](2) NOT NULL,
	[ADCS_PresetValue] [numeric](18, 3) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertServiceControlData]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlertServiceControlData](
	[ServiceType] [varchar](20) NOT NULL,
	[CurrentRuntime] [datetime] NOT NULL,
	[LastRuntime] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpQuestionMaster]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpQuestionMaster](
	[WQM_QuestionId] [int] IDENTITY(1000,1) NOT NULL,
	[WQC_QCatId] [bigint] NOT NULL,
	[WQM_Question] [varchar](max) NULL,
	[WQM_AnswerType] [varchar](10) NULL,
	[WQM_Order] [int] NULL,
	[WQM_CreatedBy] [bigint] NOT NULL,
	[WQM_CreatedOn] [datetime] NOT NULL,
	[WQM_ModifiedBy] [bigint] NOT NULL,
	[WQM_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_QuestionMaster] PRIMARY KEY CLUSTERED 
(
	[WQM_QuestionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Question Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpQuestionMaster'
GO
/****** Object:  Table [dbo].[CustomerEquityOdinBSEXtrnlTransactionInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransactionInput](
	[CEOBXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[AUDL_ProcessId] [int] NULL,
	[CEOBXTI_ScripCode] [varchar](150) NULL,
	[CEOBXTI_ScripName] [varchar](150) NULL,
	[CEOBXTI_TradeNumber] [varchar](150) NULL,
	[CEOBXTI_Rate] [varchar](150) NULL,
	[CEOBXTI_Quantity] [varchar](150) NULL,
	[CEOBXTI_Field6] [varchar](150) NULL,
	[CEOBXTI_Field7] [varchar](150) NULL,
	[CEOBXTI_TradeTime] [varchar](150) NULL,
	[CEOBXTI_TradeDate] [varchar](150) NULL,
	[CEOBXTI_TradeAccountNumber] [varchar](150) NULL,
	[CEOBXTI_BuySell] [varchar](150) NULL,
	[CEOBXTI_Field12] [varchar](150) NULL,
	[CEOBXTI_OrderNumber] [varchar](150) NULL,
	[CEOBXTI_Field14] [varchar](150) NULL,
	[CEOBXTI_AccountStatus] [varchar](150) NULL,
	[CEOBXTI_CreatedBy] [int] NULL,
	[CEOBXTI_CreatedOn] [datetime] NULL,
	[CEOBXTI_ModifiedBy] [int] NULL,
	[CEOBXTI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinBSEXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CEOBXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertTransactionServiceLog]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlertTransactionServiceLog](
	[ATSL_CurrentOccurence] [datetime] NOT NULL,
	[ATSL_LastOccurence] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlProfileInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlProfileInput](
	[CMGCXPI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CMGCXPI_FOLIOCHK] [varchar](150) NULL,
	[CMGCXPI_INV_NAME] [varchar](150) NULL,
	[CMGCXPI_ADDRESS1] [varchar](150) NULL,
	[CMGCXPI_ADDRESS2] [varchar](150) NULL,
	[CMGCXPI_ADDRESS3] [varchar](150) NULL,
	[CMGCXPI_CITY] [varchar](150) NULL,
	[CMGCXPI_PINCODE] [varchar](150) NULL,
	[CMGCXPI_PRODUCT] [varchar](150) NULL,
	[CMGCXPI_SCH_NAME] [varchar](150) NULL,
	[CMGCXPI_REP_DATE] [varchar](150) NULL,
	[CMGCXPI_CLOS_BAL] [varchar](150) NULL,
	[CMGCXPI_RUPEE_BAL] [varchar](150) NULL,
	[CMGCXPI_SUBBROK] [varchar](150) NULL,
	[CMGCXPI_REINV_FLAG] [varchar](150) NULL,
	[CMGCXPI_JOINT_NAME1] [varchar](150) NULL,
	[CMGCXPI_JOINT_NAME2] [varchar](150) NULL,
	[CMGCXPI_PHONE_OFF] [varchar](150) NULL,
	[CMGCXPI_PHONE_RES] [varchar](150) NULL,
	[CMGCXPI_EMAIL] [varchar](150) NULL,
	[CMGCXPI_HOLDING_NA] [varchar](150) NULL,
	[CMGCXPI_UIN_NO] [varchar](150) NULL,
	[CMGCXPI_BROKER_COD] [varchar](150) NULL,
	[CMGCXPI_PAN_NO] [varchar](150) NULL,
	[CMGCXPI_JOINT1_PAN] [varchar](150) NULL,
	[CMGCXPI_JOINT2_PAN] [varchar](150) NULL,
	[CMGCXPI_GUARD_PAN] [varchar](150) NULL,
	[CMGCXPI_TAX_STATUS] [varchar](150) NULL,
	[CMGCXPI_INV_IIN] [varchar](150) NULL,
	[CMGCXPI_ALTFOLIO] [varchar](150) NULL,
	[CMGCXPI_CreatedBy] [int] NULL,
	[CMGCXPI_CreatedOn] [datetime] NULL,
	[CMGCXPI_ModifiedBy] [int] NULL,
	[CMGCXPI_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CMGCXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityXtrnlProfileInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityXtrnlProfileInput](
	[CEXPI_Id] [int] IDENTITY(1,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEXPI_FirstName] [varchar](50) NULL,
	[CEXPI_MiddleName] [varchar](50) NULL,
	[CEXPI_LastName] [varchar](50) NULL,
	[CEXPI_Gender] [varchar](50) NULL,
	[CEXPI_DOB] [varchar](50) NULL,
	[CEXPI_Type] [varchar](50) NULL,
	[CEXPI_SubType] [varchar](50) NULL,
	[CEXPI_Salutation] [varchar](50) NULL,
	[CEXPI_PANNum] [varchar](50) NULL,
	[CEXPI_Adr1Line1] [varchar](50) NULL,
	[CEXPI_Adr1Line2] [varchar](50) NULL,
	[CEXPI_Adr1Line3] [varchar](50) NULL,
	[CEXPI_Adr1PinCode] [varchar](50) NULL,
	[CEXPI_Adr1City] [varchar](50) NULL,
	[CEXPI_Adr1State] [varchar](50) NULL,
	[CEXPI_Adr1Country] [varchar](50) NULL,
	[CEXPI_Adr2Line1] [varchar](50) NULL,
	[CEXPI_Adr2Line2] [varchar](50) NULL,
	[CEXPI_Adr2Line3] [varchar](50) NULL,
	[CEXPI_Adr2PinCode] [varchar](50) NULL,
	[CEXPI_Adr2City] [varchar](50) NULL,
	[CEXPI_Adr2State] [varchar](50) NULL,
	[CEXPI_Adr2Country] [varchar](50) NULL,
	[CEXPI_ResISDCode] [varchar](50) NULL,
	[CEXPI_ResSTDCode] [varchar](50) NULL,
	[CEXPI_ResPhoneNum] [varchar](50) NULL,
	[CEXPI_OfcISDCode] [varchar](50) NULL,
	[CEXPI_OfcSTDCode] [varchar](50) NULL,
	[CEXPI_OfcPhoneNum] [varchar](50) NULL,
	[CEXPI_Email] [varchar](50) NULL,
	[CEXPI_AltEmail] [varchar](50) NULL,
	[CEXPI_Mobile1] [varchar](50) NULL,
	[CEXPI_Mobile2] [varchar](50) NULL,
	[CEXPI_ISDFax] [varchar](50) NULL,
	[CEXPI_STDFax] [varchar](50) NULL,
	[CEXPI_Fax] [varchar](50) NULL,
	[CEXPI_OfcFax] [varchar](50) NULL,
	[CEXPI_OfcFaxISD] [varchar](50) NULL,
	[CEXPI_OfcFaxSTD] [varchar](50) NULL,
	[CEXPI_Occupation] [varchar](50) NULL,
	[CEXPI_Qualification] [varchar](50) NULL,
	[CEXPI_MarriageDate] [varchar](50) NULL,
	[CEXPI_MaritalStatus] [varchar](50) NULL,
	[CEXPI_Nationality] [varchar](50) NULL,
	[CEXPI_RBIRefNum] [varchar](50) NULL,
	[CEXPI_RBIApprovalDate] [varchar](50) NULL,
	[CEXPI_CompanyName] [varchar](50) NULL,
	[CEXPI_OfcAdrLine1] [varchar](50) NULL,
	[CEXPI_OfcAdrLine2] [varchar](50) NULL,
	[CEXPI_OfcAdrLine3] [varchar](50) NULL,
	[CEXPI_OfcAdrPinCode] [varchar](50) NULL,
	[CEXPI_OfcAdrCity] [varchar](50) NULL,
	[CEXPI_OfcAdrState] [varchar](50) NULL,
	[CEXPI_OfcAdrCountry] [varchar](50) NULL,
	[CEXPI_RegistrationDate] [varchar](50) NULL,
	[CEXPI_CommencementDate] [varchar](50) NULL,
	[CEXPI_RegistrationPlace] [varchar](50) NULL,
	[CEXPI_RegistrationNum] [varchar](50) NULL,
	[CEXPI_CompanyWebsite] [varchar](50) NULL,
	[CEXPI_BankName] [varchar](50) NULL,
	[CEXPI_AccountType] [varchar](50) NULL,
	[CEXPI_AccountNum] [varchar](50) NULL,
	[CEXPI_BankModeOfOperation] [varchar](50) NULL,
	[CEXPI_BranchName] [varchar](50) NULL,
	[CEXPI_BranchAdrLine1] [varchar](50) NULL,
	[CEXPI_BranchAdrLine2] [varchar](50) NULL,
	[CEXPI_BranchAdrLine3] [varchar](50) NULL,
	[CEXPI_BranchAdrPinCode] [varchar](50) NULL,
	[CEXPI_BranchAdrCity] [varchar](50) NULL,
	[CEXPI_BranchAdrState] [varchar](50) NULL,
	[CEXPI_BranchAdrCountry] [varchar](50) NULL,
	[CEXPI_MICR] [varchar](50) NULL,
	[CEXPI_IFSC] [varchar](50) NULL,
	[CEXPI_TradeNum] [varchar](50) NULL,
	[CEXPI_DPClientId] [varchar](50) NULL,
	[CEXPI_DPId] [varchar](50) NULL,
	[CEXPI_DPName] [varchar](50) NULL,
	[CEXPI_DPModeOfOperation] [varchar](50) NULL,
	[CEXPI_CreatedOn] [datetime] NULL,
	[CEXPI_CreatedBy] [int] NULL,
	[CEXPI_ModifiedOn] [datetime] NULL,
	[CEXPI_ModifiedBy] [int] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CEXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLInterestBasis]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLInterestBasis](
	[XIB_InterestBasisCode] [varchar](5) NOT NULL,
	[XIB_InterestBasis] [varchar](20) NULL,
	[XIB_CreatedBy] [int] NULL,
	[XIB_CreatedOn] [datetime] NULL,
	[XIB_ModifiedBy] [int] NULL,
	[XIB_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLInterestBasis] PRIMARY KEY CLUSTERED 
(
	[XIB_InterestBasisCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityXtrnlProfileStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityXtrnlProfileStaging](
	[CEXPS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEXPS_FirstName] [varchar](25) NULL,
	[CEXPS_MiddleName] [varchar](25) NULL,
	[CEXPS_LastName] [varchar](50) NULL,
	[CEXPS_Gender] [varchar](10) NULL,
	[CEXPS_DOB] [datetime] NULL,
	[CEXPS_Type] [varchar](25) NULL,
	[CEXPS_SubType] [varchar](25) NULL,
	[CEXPS_Salutation] [varchar](5) NULL,
	[CEXPS_PANNum] [varchar](10) NULL,
	[CEXPS_Adr1Line1] [varchar](50) NULL,
	[CEXPS_Adr1Line2] [varchar](50) NULL,
	[CEXPS_Adr1Line3] [varchar](50) NULL,
	[CEXPS_Adr1PinCode] [numeric](6, 0) NULL,
	[CEXPS_Adr1City] [varchar](25) NULL,
	[CEXPS_Adr1State] [varchar](25) NULL,
	[CEXPS_Adr1Country] [varchar](25) NULL,
	[CEXPS_Adr2Line1] [varchar](30) NULL,
	[CEXPS_Adr2Line2] [varchar](30) NULL,
	[CEXPS_Adr2Line3] [varchar](30) NULL,
	[CEXPS_Adr2PinCode] [numeric](6, 0) NULL,
	[CEXPS_Adr2City] [varchar](25) NULL,
	[CEXPS_Adr2State] [varchar](25) NULL,
	[CEXPS_Adr2Country] [varchar](25) NULL,
	[CEXPS_ResISDCode] [numeric](4, 0) NULL,
	[CEXPS_ResSTDCode] [numeric](4, 0) NULL,
	[CEXPS_ResPhoneNum] [numeric](16, 0) NULL,
	[CEXPS_OfcISDCode] [numeric](4, 0) NULL,
	[CEXPS_OfcSTDCode] [numeric](4, 0) NULL,
	[CEXPS_OfcPhoneNum] [numeric](16, 0) NULL,
	[CEXPS_Email] [varchar](50) NULL,
	[CEXPS_AltEmail] [varchar](50) NULL,
	[CEXPS_Mobile1] [numeric](10, 0) NULL,
	[CEXPS_Mobile2] [numeric](10, 0) NULL,
	[CEXPS_ISDFax] [numeric](4, 0) NULL,
	[CEXPS_STDFax] [numeric](4, 0) NULL,
	[CEXPS_Fax] [numeric](8, 0) NULL,
	[CEXPS_OfcFax] [numeric](8, 0) NULL,
	[CEXPS_OfcFaxISD] [numeric](4, 0) NULL,
	[CEXPS_OfcFaxSTD] [numeric](4, 0) NULL,
	[CEXPS_Occupation] [varchar](25) NULL,
	[CEXPS_Qualification] [varchar](25) NULL,
	[CEXPS_MarriageDate] [datetime] NULL,
	[CEXPS_MaritalStatus] [varchar](25) NULL,
	[CEXPS_Nationality] [varchar](25) NULL,
	[CEXPS_RBIRefNum] [varchar](25) NULL,
	[CEXPS_RBIApprovalDate] [datetime] NULL,
	[CEXPS_CompanyName] [varchar](50) NULL,
	[CEXPS_OfcAdrLine1] [varchar](25) NULL,
	[CEXPS_OfcAdrLine2] [varchar](25) NULL,
	[CEXPS_OfcAdrLine3] [varchar](25) NULL,
	[CEXPS_OfcAdrPinCode] [numeric](6, 0) NULL,
	[CEXPS_OfcAdrCity] [varchar](25) NULL,
	[CEXPS_OfcAdrState] [varchar](25) NULL,
	[CEXPS_OfcAdrCountry] [varchar](25) NULL,
	[CEXPS_RegistrationDate] [datetime] NULL,
	[CEXPS_CommencementDate] [datetime] NULL,
	[CEXPS_RegistrationPlace] [varchar](20) NULL,
	[CEXPS_RegistrationNum] [varchar](25) NULL,
	[CEXPS_CompanyWebsite] [varchar](25) NULL,
	[CEXPS_BankName] [varchar](30) NULL,
	[CEXPS_AccountType] [varchar](30) NULL,
	[CEXPS_AccountNum] [numeric](15, 0) NULL,
	[CEXPS_BankModeOfOperation] [varchar](5) NULL,
	[CEXPS_BranchName] [varchar](50) NULL,
	[CEXPS_BranchAdrLine1] [varchar](50) NULL,
	[CEXPS_BranchAdrLine2] [varchar](50) NULL,
	[CEXPS_BranchAdrLine3] [varchar](50) NULL,
	[CEXPS_BranchAdrPinCode] [numeric](6, 0) NULL,
	[CEXPS_BranchAdrCity] [varchar](25) NULL,
	[CEXPS_BranchAdrState] [varchar](25) NULL,
	[CEXPS_BranchAdrCountry] [varchar](25) NULL,
	[CEXPS_MICR] [numeric](9, 0) NULL,
	[CEXPS_IFSC] [varchar](11) NULL,
	[CEXPS_TradeNum] [varchar](50) NULL,
	[CEXPS_DPClientId] [varchar](20) NULL,
	[CEXPS_DPId] [varchar](20) NULL,
	[CEXPS_DPName] [varchar](50) NULL,
	[CEXPS_DPModeOfOperation] [varchar](5) NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CEXPS_RejectedRemark] [varchar](100) NULL,
	[CEXPS_IsRejected] [tinyint] NULL,
	[CEXPS_IsCustomerNew] [tinyint] NULL,
	[CEXPS_IsTradeAccountNew] [tinyint] NULL,
	[CEXPS_IsBankAccountNew] [tinyint] NULL,
	[CEXPS_CreatedOn] [datetime] NULL,
	[CEXPS_CreatedBy] [int] NULL,
	[CEXPS_ModifiedOn] [datetime] NULL,
	[CEXPS_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerEquityXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CEXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityOdinNSEXtrnlTransactionStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransactionStaging](
	[CEONXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEONXTS_TradeNum] [numeric](15, 0) NULL,
	[CEONXTS_AssetCode] [numeric](3, 0) NULL,
	[CEONXTS_ScripCode] [varchar](50) NULL,
	[CEONXTS_AssetIdentifier] [varchar](10) NULL,
	[CEONXTS_ScripName] [varchar](100) NULL,
	[CEONXTS_Field6] [numeric](5, 0) NULL,
	[CEONXTS_Field7] [numeric](5, 0) NULL,
	[CEONXTS_Field8] [numeric](5, 0) NULL,
	[CEONXTS_Field9] [numeric](10, 0) NULL,
	[CEONXTS_Field10] [numeric](10, 0) NULL,
	[CEONXTS_BuySell] [numeric](1, 0) NULL,
	[CEONXTS_Quantity] [numeric](15, 3) NULL,
	[CEONXTS_Rate] [numeric](18, 3) NULL,
	[CEONXTS_Field14] [numeric](5, 0) NULL,
	[CEONXTS_TradeAccountNum] [varchar](20) NULL,
	[CEONXTS_TerminalId] [numeric](10, 0) NULL,
	[CEONXTS_Field17] [varchar](30) NULL,
	[CEONXTS_Field18] [varchar](30) NULL,
	[CEONXTS_Field19] [varchar](30) NULL,
	[CEONXTS_TradeDate] [datetime] NULL,
	[CEONXTS_Field21] [datetime] NULL,
	[CEONXTS_Field22] [varchar](30) NULL,
	[CEONXTS_Field23] [varchar](30) NULL,
	[CEONXTS_Field24] [varchar](30) NULL,
	[CEONXTS_CreatedBy] [int] NULL,
	[CEONXTS_CreatedOn] [datetime] NULL,
	[CEONXTS_ModifiedOn] [datetime] NULL,
	[CEONXTS_ModifiedBy] [int] NULL,
	[CEONXTS_IsRejected] [tinyint] NULL,
	[CEONXTS_RejectedRemark] [varchar](100) NULL,
	[CEONXTS_IsTradeAccountNew] [tinyint] NULL,
	[A_AdviserId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CETA_AccountId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CEONXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLDebtIssuer]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLDebtIssuer](
	[XDI_DebtIssuerCode] [varchar](5) NOT NULL,
	[XDI_DebtIssuerName] [varchar](30) NULL,
	[XDI_CreatedBy] [int] NULL,
	[XDI_CreatedOn] [datetime] NULL,
	[XDI_ModifiedBy] [int] NULL,
	[XDI_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpDebtIssuer_XML] PRIMARY KEY CLUSTERED 
(
	[XDI_DebtIssuerCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLFrequency]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLFrequency](
	[XF_FrequencyCode] [varchar](5) NOT NULL,
	[XF_Frequency] [varchar](25) NULL,
	[XF_CreatedBy] [int] NULL,
	[XF_CreatedOn] [datetime] NULL,
	[XF_ModifiedBy] [int] NULL,
	[XF_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpFrequency_XML] PRIMARY KEY CLUSTERED 
(
	[XF_FrequencyCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityOdinNSEXtrnlTransactionInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransactionInput](
	[CEONXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEONXTI_TradeNum] [varchar](50) NULL,
	[CEONXTI_AssetCode] [varchar](50) NULL,
	[CEONXTI_ScripCode] [varchar](50) NULL,
	[CEONXTI_AssetIdentifier] [varchar](50) NULL,
	[CEONXTI_ScripName] [varchar](100) NULL,
	[CEONXTI_Field6] [varchar](50) NULL,
	[CEONXTI_Field7] [varchar](50) NULL,
	[CEONXTI_Field8] [varchar](50) NULL,
	[CEONXTI_Field9] [varchar](50) NULL,
	[CEONXTI_Field10] [varchar](50) NULL,
	[CEONXTI_BuySell] [varchar](50) NULL,
	[CEONXTI_Quantity] [varchar](50) NULL,
	[CEONXTI_Rate] [varchar](50) NULL,
	[CEONXTI_Field14] [varchar](50) NULL,
	[CEONXTI_TradeAccountNum] [varchar](50) NULL,
	[CEONXTI_TerminalId] [varchar](50) NULL,
	[CEONXTI_Field17] [varchar](50) NULL,
	[CEONXTI_Field18] [varchar](50) NULL,
	[CEONXTI_Field19] [varchar](50) NULL,
	[CEONXTI_TradeDate] [varchar](50) NULL,
	[CEONXTI_Field21] [varchar](50) NULL,
	[CEONXTI_Field22] [varchar](50) NULL,
	[CEONXTI_Field23] [varchar](50) NULL,
	[CEONXTI_Field24] [varchar](50) NULL,
	[CEONXTI_CreatedBy] [int] NULL,
	[CEONXTI_CreatedOn] [datetime] NULL,
	[CEONXTI_ModifiedOn] [datetime] NULL,
	[CEONXTI_ModifiedBy] [int] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CEONXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpTradeDate]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WerpTradeDate](
	[WTD_TradingDayId] [int] IDENTITY(1,1) NOT NULL,
	[WTD_Date] [datetime] NULL,
	[WTD_Month] [int] NULL,
	[WTD_Year] [int] NULL,
	[WTD_DayOfMonth] [int] NULL,
	[WTD_DayOfYear] [int] NULL,
	[WTD_CreatedBy] [int] NULL,
	[WTD_CreatedOn] [datetime] NULL,
	[WTD_ModifiedBy] [int] NULL,
	[WTD_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpTradeDate] PRIMARY KEY CLUSTERED 
(
	[WTD_TradingDayId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerEquityXtrnlTransactionInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityXtrnlTransactionInput](
	[CEXTI_Id] [int] IDENTITY(1000,1000) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEXTI_TradeNum] [numeric](15, 0) NULL,
	[CEXTI_TradeAccountNumber] [varchar](20) NULL,
	[CEXTI_OrderNum] [numeric](15, 0) NULL,
	[CEXTI_ScripCode] [varchar](50) NULL,
	[CEXTI_TradeDate] [varchar](50) NULL,
	[CEXTI_Rate] [varchar](50) NULL,
	[CEXTI_Quantity] [varchar](50) NULL,
	[CEXTI_BrokerCode] [varchar](50) NULL,
	[CEXTI_Brokerage] [varchar](50) NULL,
	[CEXTI_ServiceTax] [varchar](50) NULL,
	[CEXTI_EducationCess] [varchar](50) NULL,
	[CEXTI_STT] [varchar](50) NULL,
	[CEXTI_OtherCharges] [varchar](50) NULL,
	[CEXTI_RateInclBrokerage] [varchar](50) NULL,
	[CEXTI_TradeTotal] [varchar](50) NULL,
	[CEXTI_Exchange] [varchar](50) NULL,
	[CEXTI_BuySell] [varchar](50) NULL,
	[CEXTI_CreatedBy] [int] NULL,
	[CEXTI_CreatedOn] [datetime] NULL,
	[CEXTI_ModifiedBy] [int] NULL,
	[CEXTI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityXtrnlTransactionStaging]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityXtrnlTransactionStaging](
	[CEXTS_Id] [int] IDENTITY(1000,1000) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEXTS_TradeNum] [numeric](15, 0) NULL,
	[CEXTS_TradeAccountNumber] [varchar](20) NULL,
	[CEXTS_TradeDate] [datetime] NULL,
	[CEXTS_ScripCode] [varchar](50) NULL,
	[CEXTS_Rate] [numeric](18, 3) NULL,
	[CEXTS_Quantity] [numeric](15, 3) NULL,
	[CEXTS_BrokerCode] [varchar](50) NULL,
	[CEXTS_Exchange] [char](5) NULL,
	[CEXTS_Brokerage] [numeric](18, 3) NULL,
	[CEXTS_ServiceTax] [numeric](18, 3) NULL,
	[CEXTS_EducationCess] [numeric](18, 3) NULL,
	[CEXTS_STT] [numeric](18, 3) NULL,
	[CEXTS_OtherCharges] [numeric](18, 3) NULL,
	[CEXTS_RateInclBrokerage] [numeric](18, 3) NULL,
	[CEXTS_TradeTotal] [numeric](18, 3) NULL,
	[CEXTS_BuySell] [char](1) NULL,
	[CEXTS_OrderNum] [numeric](15, 0) NULL,
	[CETA_AccountId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CEXTS_IsRejected] [tinyint] NULL,
	[CEXTS_RejectedRemark] [varchar](100) NULL,
	[CEXTS_IsTradeAccountNew] [tinyint] NULL,
	[CEXTS_CreatedBy] [int] NULL,
	[CEXTS_CreatedOn] [datetime] NULL,
	[CEXTS_ModifiedBy] [int] NULL,
	[CEXTS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CEXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLOccupation]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLOccupation](
	[XO_OccupationCode] [varchar](5) NOT NULL,
	[XO_Occupation] [varchar](30) NULL,
	[XO_CreatedBy] [int] NULL,
	[XO_CreatedOn] [datetime] NULL,
	[XO_ModifiedBy] [int] NULL,
	[XO_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLCustomerOccupation] PRIMARY KEY CLUSTERED 
(
	[XO_OccupationCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLMeasureCode]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLMeasureCode](
	[XMC_MeasureCode] [varchar](5) NOT NULL,
	[XMC_Measure] [varchar](30) NULL,
	[XMC_AssetGroup] [varchar](20) NULL,
	[XMC_CreatedBy] [int] NULL,
	[XMC_CreatedOn] [datetime] NULL,
	[XMC_ModifiedBy] [int] NULL,
	[XMC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLMeasureCode] PRIMARY KEY CLUSTERED 
(
	[XMC_MeasureCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlProfileInput]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlProfileInput](
	[CMFKXPI_Id] [int] IDENTITY(1,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CMFKXPI_ProductCode] [varchar](150) NULL,
	[CMFKXPI_Fund] [varchar](150) NULL,
	[CMFKXPI_Folio] [varchar](150) NULL,
	[CMFKXPI_DividendOption] [varchar](150) NULL,
	[CMFKXPI_FundDescription] [varchar](150) NULL,
	[CMFKXPI_InvestorName] [varchar](150) NULL,
	[CMFKXPI_JointName1] [varchar](150) NULL,
	[CMFKXPI_JointName2] [varchar](150) NULL,
	[CMFKXPI_Address#1] [varchar](150) NULL,
	[CMFKXPI_Address#2] [varchar](150) NULL,
	[CMFKXPI_Address#3] [varchar](150) NULL,
	[CMFKXPI_City] [varchar](150) NULL,
	[CMFKXPI_Pincode] [varchar](150) NULL,
	[CMFKXPI_State] [varchar](150) NULL,
	[CMFKXPI_Country] [varchar](150) NULL,
	[CMFKXPI_TPIN] [varchar](150) NULL,
	[CMFKXPI_DateofBirth] [varchar](150) NULL,
	[CMFKXPI_FName] [varchar](150) NULL,
	[CMFKXPI_MName] [varchar](150) NULL,
	[CMFKXPI_PhoneResidence] [varchar](150) NULL,
	[CMFKXPI_PhoneRes#1] [varchar](150) NULL,
	[CMFKXPI_PhoneRes#2] [varchar](150) NULL,
	[CMFKXPI_PhoneOffice] [varchar](150) NULL,
	[CMFKXPI_PhoneOff#1] [varchar](150) NULL,
	[CMFKXPI_PhoneOff#2] [varchar](150) NULL,
	[CMFKXPI_FaxResidence] [varchar](150) NULL,
	[CMFKXPI_FaxOffice] [varchar](150) NULL,
	[CMFKXPI_TaxStatus] [varchar](150) NULL,
	[CMFKXPI_OccCode] [varchar](150) NULL,
	[CMFKXPI_Email] [varchar](150) NULL,
	[CMFKXPI_BankAccno] [varchar](150) NULL,
	[CMFKXPI_BankName] [varchar](150) NULL,
	[CMFKXPI_AccountType] [varchar](150) NULL,
	[CMFKXPI_Branch] [varchar](150) NULL,
	[CMFKXPI_BankAddress#1] [varchar](150) NULL,
	[CMFKXPI_BankAddress#2] [varchar](150) NULL,
	[CMFKXPI_BankAddress#3] [varchar](150) NULL,
	[CMFKXPI_BankCity] [varchar](150) NULL,
	[CMFKXPI_BankPhone] [varchar](150) NULL,
	[CMFKXPI_BankState] [varchar](150) NULL,
	[CMFKXPI_BankCountry] [varchar](150) NULL,
	[CMFKXPI_InvestorID] [varchar](150) NULL,
	[CMFKXPI_BrokerCode] [varchar](150) NULL,
	[CMFKXPI_PANNumber] [varchar](150) NULL,
	[CMFKXPI_Mobile] [varchar](150) NULL,
	[CMFKXPI_ReportDate] [varchar](150) NULL,
	[CMFKXPI_ReportTime] [varchar](150) NULL,
	[CMFKXPI_OccupationDescription] [varchar](150) NULL,
	[CMFKXPI_ModeofHolding] [varchar](150) NULL,
	[CMFKXPI_ModeofHoldingDescription] [varchar](150) NULL,
	[CMFKXPI_MapinId] [varchar](150) NULL,
	[CMFKXPI_CreatedBy] [int] NULL,
	[CMFKXPI_CreatedOn] [datetime] NULL,
	[CMFKXPI_ModifiedBy] [int] NULL,
	[CMFKXPI_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CMFKXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerLiability]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerLiability](
	[CLP_LiabilityId] [int] NULL,
	[CLP_CreatedBy] [int] NOT NULL,
	[CLP_CreatedOn] [datetime] NOT NULL,
	[CLP_ModifiedBy] [int] NOT NULL,
	[CLP_ModifiedOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WerpULIPSubPlan]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpULIPSubPlan](
	[WUSP_ULIPSubPlanCode] [int] IDENTITY(1000,1) NOT NULL,
	[WUSP_ULIPSubPlanName] [varchar](50) NULL,
	[WUP_ULIPPlanCode] [int] NULL,
	[WUSP_CreatedBy] [int] NULL,
	[WUSP_CreatedOn] [datetime] NULL,
	[WUSP_ModifiedOn] [datetime] NULL,
	[WUSP_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_WerpULIPSubPlan] PRIMARY KEY CLUSTERED 
(
	[WUSP_ULIPSubPlanCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Adviser]    Script Date: 06/12/2009 18:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Adviser](
	[A_AdviserId] [int] IDENTITY(1000,1) NOT NULL,
	[U_UserId] [int] NULL,
	[A_OrgName] [varchar](25) NOT NULL,
	[A_AddressLine1] [varchar](25) NOT NULL,
	[A_AddressLine2] [varchar](25) NULL,
	[A_AddressLine3] [varchar](25) NULL,
	[A_City] [varchar](25) NOT NULL,
	[A_State] [varchar](25) NOT NULL,
	[A_PinCode] [numeric](6, 0) NOT NULL,
	[A_Country] [varchar](25) NOT NULL,
	[A_Phone1STD] [numeric](4, 0) NOT NULL,
	[A_Phone1ISD] [numeric](4, 0) NOT NULL,
	[A_Phone1Number] [numeric](8, 0) NOT NULL,
	[A_Phone2STD] [numeric](4, 0) NULL,
	[A_Phone2ISD] [numeric](4, 0) NULL,
	[A_Phone2Number] [numeric](8, 0) NULL,
	[A_Email] [varchar](max) NULL,
	[A_FAXISD] [numeric](4, 0) NULL,
	[A_FAXSTD] [numeric](4, 0) NULL,
	[A_FAX] [numeric](8, 0) NULL,
	[XABT_BusinessTypeCode] [varchar](5) NOT NULL,
	[A_ContactPersonFirstName] [varchar](25) NOT NULL,
	[A_ContactPersonMiddleName] [varchar](25) NULL,
	[A_ContactPersonLastName] [varchar](25) NULL,
	[A_ContactPersonMobile] [numeric](10, 0) NULL,
	[A_IsMultiBranch] [tinyint] NULL,
	[A_AdviserLogo] [varchar](50) NULL,
	[A_CreatedBy] [int] NOT NULL,
	[A_CreatedOn] [datetime] NOT NULL,
	[A_ModifiedBy] [int] NOT NULL,
	[A_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdvisorMaster] PRIMARY KEY CLUSTERED 
(
	[A_AdviserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Adviser'
GO
/****** Object:  Table [dbo].[AdviserRM]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdviserRM](
	[AR_RMId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NOT NULL,
	[U_UserId] [int] NULL,
	[AR_FirstName] [varchar](50) NULL,
	[AR_MiddleName] [varchar](50) NULL,
	[AR_LastName] [varchar](50) NULL,
	[AR_OfficePhoneDirectISD] [numeric](4, 0) NULL,
	[AR_OfficePhoneDirectSTD] [numeric](4, 0) NULL,
	[AR_OfficePhoneDirect] [numeric](8, 0) NULL,
	[AR_OfficePhoneExtISD] [numeric](4, 0) NULL,
	[AR_OfficePhoneExtSTD] [numeric](4, 0) NULL,
	[AR_OfficePhoneExt] [numeric](8, 0) NULL,
	[AR_ResPhoneISD] [numeric](4, 0) NULL,
	[AR_ResPhoneSTD] [numeric](4, 0) NULL,
	[AR_ResPhone] [numeric](8, 0) NULL,
	[AR_Mobile] [numeric](10, 0) NULL,
	[AR_FaxISD] [numeric](4, 0) NULL,
	[AR_FaxSTD] [numeric](4, 0) NULL,
	[AR_Fax] [numeric](8, 0) NULL,
	[AR_Email] [varchar](max) NULL,
	[AR_CreatedBy] [int] NOT NULL,
	[AR_CreatedOn] [datetime] NOT NULL,
	[AR_ModifiedBy] [int] NOT NULL,
	[AR_ModifiedOn] [datetime] NOT NULL,
	[AR_JobFunction] [varchar](30) NULL,
 CONSTRAINT [PK_RMMaster] PRIMARY KEY CLUSTERED 
(
	[AR_RMId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RM Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserRM'
GO
/****** Object:  Table [dbo].[XMLCustomerSubType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLCustomerSubType](
	[XCST_CustomerSubTypeCode] [varchar](5) NOT NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomersubTypeName] [varchar](30) NULL,
	[XCST_CreatedBy] [int] NULL,
	[XCST_CreatedOn] [datetime] NULL,
	[XCST_ModifiedBy] [int] NULL,
	[XCST_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLCustomerSubType] PRIMARY KEY CLUSTERED 
(
	[XCST_CustomerSubTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[C_CustomerId] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[AR_RMId] [int] NOT NULL,
	[U_UMId] [int] NOT NULL,
	[C_CustCode] [varchar](10) NULL,
	[C_ProfilingDate] [datetime] NULL,
	[C_FirstName] [varchar](25) NULL,
	[C_MiddleName] [varchar](25) NULL,
	[C_LastName] [varchar](75) NULL,
	[C_Gender] [varchar](10) NULL,
	[C_DOB] [datetime] NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomerSubTypeCode] [varchar](5) NULL,
	[C_Salutation] [varchar](5) NULL,
	[C_PANNum] [varchar](10) NULL,
	[C_Adr1Line1] [varchar](75) NULL,
	[C_Adr1Line2] [varchar](75) NULL,
	[C_Adr1Line3] [varchar](75) NULL,
	[C_Adr1PinCode] [numeric](10, 0) NULL,
	[C_Adr1City] [varchar](25) NULL,
	[C_Adr1State] [varchar](25) NULL,
	[C_Adr1Country] [varchar](25) NULL,
	[C_Adr2Line1] [varchar](75) NULL,
	[C_Adr2Line2] [varchar](75) NULL,
	[C_Adr2Line3] [varchar](75) NULL,
	[C_Adr2PinCode] [numeric](10, 0) NULL,
	[C_Adr2City] [varchar](20) NULL,
	[C_Adr2State] [varchar](20) NULL,
	[C_Adr2Country] [varchar](20) NULL,
	[C_ResISDCode] [numeric](4, 0) NULL,
	[C_ResSTDCode] [numeric](5, 0) NULL,
	[C_ResPhoneNum] [numeric](10, 0) NULL,
	[C_OfcISDCode] [numeric](4, 0) NULL,
	[C_OfcSTDCode] [numeric](5, 0) NULL,
	[C_OfcPhoneNum] [numeric](10, 0) NULL,
	[C_Email] [varchar](75) NOT NULL,
	[C_AltEmail] [varchar](75) NULL,
	[C_Mobile1] [numeric](14, 0) NULL,
	[C_Mobile2] [numeric](14, 0) NULL,
	[C_ISDFax] [numeric](4, 0) NULL,
	[C_STDFax] [numeric](5, 0) NULL,
	[C_Fax] [numeric](25, 0) NULL,
	[C_OfcFax] [numeric](25, 0) NULL,
	[C_OfcFaxISD] [numeric](4, 0) NULL,
	[C_OfcFaxSTD] [numeric](5, 0) NULL,
	[XO_OccupationCode] [varchar](5) NULL,
	[XQ_QualificationCode] [varchar](5) NULL,
	[C_MarriageDate] [datetime] NULL,
	[XMS_MaritalStatusCode] [varchar](5) NULL,
	[XN_NationalityCode] [varchar](5) NULL,
	[C_RBIRefNum] [varchar](25) NULL,
	[C_RBIApprovalDate] [datetime] NULL,
	[C_CompanyName] [varchar](50) NULL,
	[C_OfcAdrLine1] [varchar](75) NULL,
	[C_OfcAdrLine2] [varchar](75) NULL,
	[C_OfcAdrLine3] [varchar](75) NULL,
	[C_OfcAdrPinCode] [numeric](10, 0) NULL,
	[C_OfcAdrCity] [varchar](25) NULL,
	[C_OfcAdrState] [varchar](25) NULL,
	[C_OfcAdrCountry] [varchar](25) NULL,
	[C_RegistrationDate] [datetime] NULL,
	[C_CommencementDate] [datetime] NULL,
	[C_RegistrationPlace] [varchar](20) NULL,
	[C_RegistrationNum] [varchar](25) NULL,
	[C_CompanyWebsite] [varchar](25) NULL,
	[C_CreatedOn] [datetime] NOT NULL,
	[C_CreatedBy] [int] NOT NULL,
	[C_ModifiedOn] [datetime] NOT NULL,
	[C_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerMaster] PRIMARY KEY CLUSTERED 
(
	[C_CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Master' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer'
GO
/****** Object:  Table [dbo].[CustomerProof]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerProof](
	[CP_CustomerProofId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[XP_ProofCode] [int] NOT NULL,
	[CP_CreatedOn] [datetime] NOT NULL,
	[CP_CreatedBy] [int] NOT NULL,
	[CP_ModifiedOn] [datetime] NOT NULL,
	[CP_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerProofs_1] PRIMARY KEY CLUSTERED 
(
	[CP_CustomerProofId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Proofs Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerProof'
GO
/****** Object:  Table [dbo].[WerpProfileFilterCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpProfileFilterCategory](
	[WPFC_FilterCategoryCode] [varchar](10) NOT NULL,
	[WPFC_FilterCategoryName] [varchar](50) NULL,
	[WPFC_AssetClass] [varchar](50) NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[WPFC_KYFCompliantFlag] [tinyint] NULL,
	[WPFC_CreatedBy] [int] NULL,
	[WPFC_CreatedOn] [datetime] NULL,
	[WPFC_ModifiedBy] [int] NULL,
	[WPFC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProfileFilterCategory_XML] PRIMARY KEY CLUSTERED 
(
	[WPFC_FilterCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpProofMandatoryLookup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpProofMandatoryLookup](
	[WPML_ProofMandatoryId] [int] IDENTITY(1000,1) NOT NULL,
	[WPFC_FilterCategoryCode] [varchar](10) NOT NULL,
	[XP_ProofCode] [int] NOT NULL,
	[WPML_CreatedBy] [int] NULL,
	[WPML_CreatedOn] [datetime] NULL,
	[WPML_ModifiedOn] [datetime] NULL,
	[WPML_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Rules] PRIMARY KEY CLUSTERED 
(
	[WPML_ProofMandatoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Profile Filter Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpProofMandatoryLookup'
GO
/****** Object:  Table [dbo].[ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAssetInstrumentCategory](
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NOT NULL,
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PAIC_AssetInstrumentCategoryName] [varchar](50) NULL,
	[PAIC_CreatedBy] [int] NOT NULL,
	[PAIC_CreatedOn] [datetime] NOT NULL,
	[PAIC_ModifiedBy] [int] NOT NULL,
	[PAIC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductAssetInstrumnentCategory_1] PRIMARY KEY CLUSTERED 
(
	[PAIC_AssetInstrumentCategoryCode] ASC,
	[PAG_AssetGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPortfolio](
	[CP_PortfolioId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioName] [varchar](50) NULL,
	[CP_IsMainPortfolio] [tinyint] NULL,
	[CP_IsPMS] [tinyint] NULL,
	[CP_PMSIdentifier] [varchar](20) NULL,
	[CP_CreatedBy] [int] NULL,
	[CP_CreatedOn] [datetime] NULL,
	[CP_ModifiedBy] [int] NULL,
	[CP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPortfolio] PRIMARY KEY CLUSTERED 
(
	[CP_PortfolioId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerCashSavingsAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerCashSavingsAccount](
	[CCSA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CCSA_AccountNum] [varchar](30) NULL,
	[CCSA_BankName] [varchar](30) NULL,
	[CCSA_AccountOpeningDate] [datetime] NULL,
	[CCSA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CCSA_CreatedBy] [int] NULL,
	[CCSA_CreatedOn] [datetime] NULL,
	[CCSA_ModifiedOn] [datetime] NULL,
	[CCSA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerCashSavingsAccount] PRIMARY KEY CLUSTERED 
(
	[CCSA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerAssociates](
	[CA_AssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[C_AssociateCustomerId] [int] NULL,
	[XR_RelationshipCode] [varchar](5) NULL,
	[CA_CreatedBy] [int] NOT NULL,
	[CA_CreatedOn] [datetime] NOT NULL,
	[CA_ModifiedOn] [datetime] NOT NULL,
	[CA_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerFamily] PRIMARY KEY CLUSTERED 
(
	[CA_AssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Family Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerAssociates'
GO
/****** Object:  Table [dbo].[CustomerCashSavingsAccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerCashSavingsAccountAssociates](
	[CCSAA_AccountAssociateId] [int] IDENTITY(1000,1) NOT NULL,
	[CCSA_AccountId] [int] NULL,
	[CA_AssociationId] [int] NULL,
	[CCSAA_AssociationType] [varchar](30) NULL,
	[CCSAA_CreatedBy] [int] NULL,
	[CCSAA_CreatedOn] [datetime] NULL,
	[CCSAA_ModifiedOn] [datetime] NULL,
	[CCSAA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerCashSavingsAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CCSAA_AccountAssociateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerCashSavingsNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerCashSavingsNetPosition](
	[CCSNP_CashSavingsNPId] [int] IDENTITY(1000,1) NOT NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CCSA_AccountId] [int] NULL,
	[XDI_DebtIssuerCode] [varchar](5) NULL,
	[XIB_InterestBasisCode] [varchar](5) NULL,
	[XF_CompoundInterestFrequencyCode] [varchar](5) NULL,
	[XF_InterestPayoutFrequencyCode] [varchar](5) NULL,
	[CCSNP_Name] [varchar](50) NULL,
	[CCSNP_DepositAmount] [numeric](18, 4) NULL,
	[CCSNP_DepositDate] [datetime] NULL,
	[CCSNP_CurrentValue] [numeric](18, 4) NULL,
	[CCSNP_InterestRate] [numeric](10, 5) NULL,
	[CCSNP_InterestAmntPaidOut] [numeric](18, 4) NULL,
	[CCSNP_IsInterestAccumulated] [tinyint] NULL,
	[CCSNP_InterestAmntAccumulated] [numeric](18, 4) NULL,
	[CCSNP_Remark] [varchar](100) NULL,
	[CCSNP_CreatedBy] [int] NOT NULL,
	[CCSNP_CreatedOn] [datetime] NOT NULL,
	[CCSNP_ModifiedBy] [int] NOT NULL,
	[CCSNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerCashSavingsPortfolio] PRIMARY KEY CLUSTERED 
(
	[CCSNP_CashSavingsNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XMLAdviserLOBClassification]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLAdviserLOBClassification](
	[XALC_LOBClassificationCode] [varchar](5) NOT NULL,
	[XALAG_LOBAssetGroupsCode] [varchar](5) NULL,
	[XALC_LOBCategoryCode] [varchar](5) NULL,
	[XALES_SegmentCode] [varchar](5) NULL,
	[XALC_CreatedBy] [int] NULL,
	[XALC_CreatedOn] [datetime] NULL,
	[XALC_ModifiedBy] [int] NULL,
	[XALC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_LOBClassification] PRIMARY KEY CLUSTERED 
(
	[XALC_LOBClassificationCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdviserLOB]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdviserLOB](
	[AL_LOBId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NOT NULL,
	[XALC_LOBClassificationCode] [varchar](5) NOT NULL,
	[XALIT_IdentifierTypeCode] [varchar](5) NULL,
	[AL_OrgName] [varchar](25) NOT NULL,
	[AL_Identifier] [varchar](25) NOT NULL,
	[AL_LicenseNo] [varchar](50) NULL,
	[AL_Validity] [datetime] NULL,
	[AL_CreatedBy] [int] NOT NULL,
	[AL_CreatedOn] [datetime] NOT NULL,
	[AL_ModifiedBy] [int] NOT NULL,
	[AL_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdvisorLOB] PRIMARY KEY CLUSTERED 
(
	[AL_LOBId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor LOB Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserLOB'
GO
/****** Object:  Table [dbo].[CustomerInsuranceAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerInsuranceAccount](
	[CIA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CIA_PolicyNum] [varchar](30) NULL,
	[CIA_AccountNum] [varchar](30) NULL,
	[CIA_CreatedBy] [int] NULL,
	[CIA_CreatedOn] [datetime] NULL,
	[CIA_ModifiedBy] [int] NULL,
	[CIA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerInsuranceAccount] PRIMARY KEY CLUSTERED 
(
	[CIA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerInsuranceNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerInsuranceNetPosition](
	[CINP_InsuranceNPId] [int] IDENTITY(1000,1) NOT NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CIA_AccountId] [int] NULL,
	[XII_InsuranceIssuerCode] [varchar](5) NULL,
	[XF_PremiumFrequencyCode] [varchar](5) NULL,
	[CINP_Name] [varchar](50) NULL,
	[CINP_PremiumAmount] [numeric](18, 3) NULL,
	[CINP_PremiumDuration] [numeric](5, 0) NULL,
	[CINP_SumAssured] [numeric](18, 3) NULL,
	[CINP_StartDate] [datetime] NULL,
	[CINP_PolicyPeriod] [numeric](5, 0) NULL,
	[CINP_PremiumAccumalated] [numeric](18, 3) NULL,
	[CINP_PolicyEpisode] [numeric](5, 0) NULL,
	[CINP_BonusAccumalated] [numeric](18, 3) NULL,
	[CINP_SurrenderValue] [numeric](18, 3) NULL,
	[CINP_Remark] [varchar](100) NULL,
	[CINP_MaturityValue] [numeric](18, 3) NULL,
	[CINP_EndDate] [datetime] NULL,
	[CINP_GracePeriod] [numeric](5, 0) NULL,
	[CINP_ULIPCharges] [numeric](18, 3) NULL,
	[CINP_PremiumPaymentDate] [datetime] NULL,
	[CINP_ApplicationNum] [varchar](20) NULL,
	[CINP_ApplicationDate] [datetime] NULL,
	[CINP_CreatedOn] [datetime] NOT NULL,
	[CINP_CreatedBy] [int] NOT NULL,
	[CINP_ModifiedBy] [int] NOT NULL,
	[CINP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerInsurancePortfolio] PRIMARY KEY CLUSTERED 
(
	[CINP_InsuranceNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Insurance Portfolio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerInsuranceNetPosition'
GO
/****** Object:  Table [dbo].[ProductGlobalSectorSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductGlobalSectorSubCategory](
	[PGSSC_SectorSubCategoryCode] [varchar](6) NOT NULL,
	[PGSC_SectorCategoryCode] [varchar](3) NOT NULL,
	[PGSSC_SectorSubCategoryName] [varchar](50) NULL,
	[PGSSC_CreatedBy] [int] NULL,
	[PGSSC_CreatedOn] [datetime] NULL,
	[PGSSC_ModifiedBy] [int] NULL,
	[PGSSC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductGlobalSectorSubCategory] PRIMARY KEY CLUSTERED 
(
	[PGSSC_SectorSubCategoryCode] ASC,
	[PGSC_SectorCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAssetInstrumentSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAssetInstrumentSubCategory](
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NOT NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NOT NULL,
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PAISC_AssetInstrumentSubCategoryName] [varchar](50) NULL,
	[PAISC_CreatedBy] [int] NOT NULL,
	[PAISC_CreatedOn] [datetime] NOT NULL,
	[PAISC_ModifiedBy] [int] NOT NULL,
	[PAISC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductAssetInstrumnentSubCategory] PRIMARY KEY CLUSTERED 
(
	[PAISC_AssetInstrumentSubCategoryCode] ASC,
	[PAIC_AssetInstrumentCategoryCode] ASC,
	[PAG_AssetGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductEquityMaster]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductEquityMaster](
	[PEM_ScripCode] [int] IDENTITY(1000,1) NOT NULL,
	[PEM_CompanyName] [varchar](255) NULL,
	[PMCC_MarketCapClassificationCode] [int] NULL,
	[PEM_MarketLot] [int] NULL,
	[PEM_FaceValue] [numeric](10, 2) NULL,
	[PEM_BookClosure] [datetime] NULL,
	[PEM_Listing] [varchar](255) NULL,
	[PEM_Incorporation] [datetime] NULL,
	[PEM_PublicIssueDate] [datetime] NULL,
	[PEM_Ticker] [varchar](100) NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[PGSSSC_SectorSubSubCategoryCode] [varchar](9) NULL,
	[PGSSC_SectorSubCategoryCode] [varchar](6) NULL,
	[PGSC_SectorCategoryCode] [varchar](3) NULL,
	[PEM_ModifiedBy] [int] NULL,
	[PEM_CreatedBy] [int] NULL,
	[PEM_ModifiedOn] [datetime] NULL,
	[PEM_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductEquity] PRIMARY KEY CLUSTERED 
(
	[PEM_ScripCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product Equity Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductEquityMaster'
GO
/****** Object:  Table [dbo].[CustomerMutualFundAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMutualFundAccount](
	[CMFA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PA_AMCCode] [int] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CMFA_FolioNum] [varchar](20) NULL,
	[CMFA_AccountOpeningDate] [datetime] NULL,
	[CMFA_IsJointlyHeld] [tinyint] NULL,
	[CMFA_CreatedOn] [datetime] NOT NULL,
	[CMFA_CreatedBy] [int] NOT NULL,
	[CMFA_ModifiedBy] [int] NOT NULL,
	[CMFA_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerMutualFundAccount_1] PRIMARY KEY CLUSTERED 
(
	[CMFA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Accounts Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundAccount'
GO
/****** Object:  Table [dbo].[ProductAssetInstrumentSubSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAssetInstrumentSubSubCategory](
	[PAISSC_AssetInstrumentSubSubCategoryCode] [varchar](8) NOT NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NOT NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NOT NULL,
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PAISSC_AssetInstrumentSubSubCategoryName] [varchar](50) NULL,
	[PAISSC_CreatedBy] [int] NOT NULL,
	[PAISSC_CreatedOn] [datetime] NOT NULL,
	[PAISSC_ModifiedBy] [int] NOT NULL,
	[PAISSC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductAssetInstrumnentSubSubCategory] PRIMARY KEY CLUSTERED 
(
	[PAISSC_AssetInstrumentSubSubCategoryCode] ASC,
	[PAISC_AssetInstrumentSubCategoryCode] ASC,
	[PAIC_AssetInstrumentCategoryCode] ASC,
	[PAG_AssetGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAMCSchemePlan]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAMCSchemePlan](
	[PASP_SchemePlanCode] [int] IDENTITY(1000,1) NOT NULL,
	[PA_AMCCode] [int] NULL,
	[PASP_SchemePlanName] [varchar](max) NULL,
	[PAISSC_AssetInstrumentSubSubCategoryCode] [varchar](8) NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[PASP_ModifiedBy] [int] NULL,
	[PASP_ModifiedOn] [datetime] NULL,
	[PASP_CreatedBy] [int] NULL,
	[PASP_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductAMCSchemePlan] PRIMARY KEY CLUSTERED 
(
	[PASP_SchemePlanCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product AMC SchemePlan Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductAMCSchemePlan'
GO
/****** Object:  Table [dbo].[CustomerMutualFundTransaction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMutualFundTransaction](
	[CMFT_MFTransId] [int] IDENTITY(1000,1) NOT NULL,
	[CMFA_AccountId] [int] NULL,
	[PASP_SchemePlanCode] [int] NULL,
	[CMFT_TransactionDate] [datetime] NOT NULL,
	[CMFT_BuySell] [char](1) NULL,
	[CMFT_DividendRate] [numeric](10, 5) NULL,
	[CMFT_NAV] [numeric](18, 5) NULL,
	[CMFT_Price] [numeric](18, 5) NULL,
	[CMFT_Amount] [numeric](18, 5) NULL,
	[CMFT_Units] [numeric](18, 5) NULL,
	[CMFT_STT] [numeric](10, 5) NULL,
	[CMFT_IsSourceManual] [tinyint] NULL,
	[XES_SourceCode] [varchar](5) NULL,
	[CMFT_SwitchSourceTrxId] [int] NULL,
	[WMTT_TransactionClassificationCode] [varchar](3) NULL,
	[CMFT_ModifiedBy] [int] NOT NULL,
	[CMFT_CreatedBy] [int] NOT NULL,
	[CMFT_CreatedOn] [datetime] NOT NULL,
	[CMFT_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerMFTransaction] PRIMARY KEY CLUSTERED 
(
	[CMFT_MFTransId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer MF Transaction Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundTransaction'
GO
/****** Object:  Table [dbo].[ProductGlobalSectorSubSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductGlobalSectorSubSubCategory](
	[PGSSSC_SectorSubSubCategoryCode] [varchar](9) NOT NULL,
	[PGSC_SectorCategoryCode] [varchar](3) NOT NULL,
	[PGSSC_SectorSubCategoryCode] [varchar](6) NOT NULL,
	[PGSSSC_ExternalSectorCode] [varchar](50) NULL,
	[PGSSSC_SectorSubSubCategoryName] [varchar](100) NULL,
	[PGSSSC_CreatedBy] [int] NULL,
	[PGSSSC_CreatedOn] [datetime] NULL,
	[PGSSSC_ModifiedBy] [int] NULL,
	[PGSSSC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductGlobalSectorSubSubCategory_1] PRIMARY KEY CLUSTERED 
(
	[PGSSSC_SectorSubSubCategoryCode] ASC,
	[PGSC_SectorCategoryCode] ASC,
	[PGSSC_SectorSubCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityTradeAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityTradeAccount](
	[CETA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XB_BrokerCode] [varchar](5) NULL,
	[CETA_TradeAccountNum] [varchar](20) NULL,
	[CETA_AccountOpeningDate] [datetime] NULL,
	[CETA_CreatedBy] [int] NULL,
	[CETA_CreatedOn] [datetime] NULL,
	[CETA_ModifiedBy] [int] NULL,
	[CETA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityTradeAccount] PRIMARY KEY CLUSTERED 
(
	[CETA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityTransaction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityTransaction](
	[CET_EqTransId] [int] IDENTITY(1000,1) NOT NULL,
	[CETA_AccountId] [int] NULL,
	[PEM_ScripCode] [int] NULL,
	[CET_BuySell] [char](1) NULL,
	[CET_TradeNum] [numeric](15, 0) NULL,
	[CET_OrderNum] [numeric](15, 0) NULL,
	[CET_IsSpeculative] [tinyint] NULL,
	[XE_ExchangeCode] [varchar](5) NULL,
	[CET_TradeDate] [datetime] NULL,
	[CET_Rate] [numeric](18, 4) NULL,
	[CET_Quantity] [numeric](18, 4) NULL,
	[CET_Brokerage] [numeric](18, 4) NULL,
	[CET_ServiceTax] [numeric](18, 4) NULL,
	[CET_EducationCess] [numeric](18, 4) NULL,
	[CET_STT] [numeric](18, 4) NULL,
	[CET_OtherCharges] [numeric](18, 4) NULL,
	[CET_RateInclBrokerage] [numeric](18, 4) NULL,
	[CET_TradeTotal] [numeric](18, 4) NULL,
	[XB_BrokerCode] [varchar](5) NULL,
	[CET_IsSplit] [tinyint] NULL,
	[CET_SplitCustEqTransId] [int] NULL,
	[XES_SourceCode] [varchar](5) NULL,
	[WETT_TransactionCode] [tinyint] NULL,
	[CET_IsSourceManual] [tinyint] NULL,
	[CET_ModifiedBy] [int] NULL,
	[CET_ModifiedOn] [datetime] NULL,
	[CET_CreatedBy] [int] NULL,
	[CET_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityTransaction] PRIMARY KEY CLUSTERED 
(
	[CET_EqTransId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Equity Transaction Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerEquityTransaction'
GO
/****** Object:  Table [dbo].[CustomerEquityOdinBSEXtrnlTransaction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransaction](
	[CEOBXT_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[CET_EqTransId] [int] NOT NULL,
	[CEOBXT_ScripCode] [varchar](50) NULL,
	[CEOBXT_ScripName] [varchar](50) NULL,
	[CEOBXT_TradeNumber] [numeric](10, 0) NULL,
	[CEOBXT_Rate] [numeric](25, 12) NULL,
	[CEOBXT_Quantity] [numeric](25, 12) NULL,
	[CEOBXT_Field6] [varchar](20) NULL,
	[CEOBXT_Field7] [varchar](20) NULL,
	[CEOBXT_TradeTime] [datetime] NULL,
	[CEOBXT_TradeDate] [datetime] NULL,
	[CEOBXT_TradeAccountNumber] [varchar](20) NULL,
	[CEOBXT_BuySell] [char](1) NULL,
	[CEOBXT_Field12] [varchar](5) NULL,
	[CEOBXT_OrderNumber] [numeric](15, 0) NULL,
	[CEOBXT_Field14] [varchar](5) NULL,
	[CEOBXT_AccountStatus] [varchar](20) NULL,
	[CEOBXT_CreatedBy] [int] NULL,
	[CEOBXT_CreatedOn] [datetime] NULL,
	[CEOBXT_ModifiedBy] [int] NULL,
	[CEOBXT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityOdinBSEXtrnlTransaction] PRIMARY KEY CLUSTERED 
(
	[CEOBXT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityOdinNSEXtrnlTransaction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransaction](
	[CEONXT_Id] [int] IDENTITY(1000,1) NOT NULL,
	[CET_EqTransId] [int] NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEONXT_TradeNum] [numeric](15, 0) NULL,
	[CEONXT_AssetCode] [numeric](3, 0) NULL,
	[CEONXT_ScripCode] [varchar](50) NULL,
	[CEONXT_AssetIdentifier] [varchar](10) NULL,
	[CEONXT_ScripName] [varchar](100) NULL,
	[CEONXT_Field6] [numeric](5, 0) NULL,
	[CEONXT_Field7] [numeric](5, 0) NULL,
	[CEONXT_Field8] [numeric](5, 0) NULL,
	[CEONXT_Field9] [numeric](10, 0) NULL,
	[CEONXT_Field10] [numeric](10, 0) NULL,
	[CEONXT_BuySell] [numeric](1, 0) NULL,
	[CEONXT_Quantity] [numeric](15, 3) NULL,
	[CEONXT_Rate] [numeric](18, 3) NULL,
	[CEONXT_Field14] [numeric](5, 0) NULL,
	[CEONXT_TradeAccountNum] [varchar](20) NULL,
	[CEONXT_TerminalId] [numeric](10, 0) NULL,
	[CEONXT_Field17] [varchar](30) NULL,
	[CEONXT_Field18] [varchar](30) NULL,
	[CEONXT_Field19] [varchar](30) NULL,
	[CEONXT_TradeDate] [datetime] NULL,
	[CEONXT_Field21] [datetime] NULL,
	[CEONXT_Field22] [varchar](30) NULL,
	[CEONXT_Field23] [varchar](30) NULL,
	[CEONXT_Field24] [varchar](30) NULL,
	[CEONXT_CreatedBy] [int] NULL,
	[CEONXT_CreatedOn] [datetime] NULL,
	[CEONXT_ModifiedOn] [datetime] NULL,
	[CEONXT_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinXtrnlTransaction] PRIMARY KEY CLUSTERED 
(
	[CEONXT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerGovtSavingAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerGovtSavingAccount](
	[CGSA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[CGSA_AccountNum] [varchar](30) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CGSA_AccountSource] [varchar](30) NULL,
	[CGSA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CGSA_AccountOpeningDate] [datetime] NULL,
	[CGSA_CreatedBy] [int] NULL,
	[CGSA_CreatedOn] [datetime] NULL,
	[CGSA_ModifiedBy] [int] NULL,
	[CGSA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerGovtSavingAccount_1] PRIMARY KEY CLUSTERED 
(
	[CGSA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerGovtSavingAccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerGovtSavingAccountAssociates](
	[CGSAA_AccountAssociateId] [int] IDENTITY(1000,1) NOT NULL,
	[CA_AssociationId] [int] NULL,
	[CGSA_AccountId] [int] NULL,
	[CGSAA_AssociationType] [varchar](30) NULL,
	[CGSAA_CreatedBy] [int] NULL,
	[CGSAA_CreatedOn] [datetime] NULL,
	[CGSAA_ModifiedBy] [int] NULL,
	[CGSAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerGovtSavingAccountAssociate] PRIMARY KEY CLUSTERED 
(
	[CGSAA_AccountAssociateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerGovtSavingNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerGovtSavingNetPosition](
	[CGSNP_GovtSavingNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CGSA_AccountId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XDI_DebtIssuerCode] [varchar](5) NULL,
	[XIB_InterestBasisCode] [varchar](5) NULL,
	[XF_CompoundInterestFrequencyCode] [varchar](5) NULL,
	[XF_InterestPayableFrequencyCode] [varchar](5) NULL,
	[XF_DepositFrequencyCode] [varchar](5) NULL,
	[CGSNP_Name] [varchar](50) NOT NULL,
	[CGSNP_PurchaseDate] [datetime] NULL,
	[CGSNP_Quantity] [numeric](12, 3) NULL,
	[CGSNP_CurrentPrice] [numeric](18, 4) NULL,
	[CGSNP_CurrentValue] [numeric](18, 4) NULL,
	[CGSNP_MaturityDate] [datetime] NULL,
	[CGSNP_DepositAmount] [numeric](18, 4) NULL,
	[CGSNP_MaturityValue] [numeric](18, 4) NULL,
	[CGSNP_IsInterestAccumalated] [tinyint] NULL,
	[CGSNP_InterestAmtAccumalated] [numeric](18, 4) NULL,
	[CGSNP_InterestAmtPaidOut] [numeric](18, 4) NULL,
	[CGSNP_InterestRate] [numeric](7, 4) NULL,
	[CGSNP_Remark] [varchar](100) NULL,
	[CGSNP_SubsqntDepositAmount] [numeric](18, 4) NULL,
	[CGSNP_SubsqntDepositDate] [datetime] NULL,
	[CGSNP_CreatedBy] [int] NOT NULL,
	[CGSNP_CreatedOn] [datetime] NOT NULL,
	[CGSNP_ModifiedBy] [int] NOT NULL,
	[CGSNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerInvestmentGovtSavingPortfolio] PRIMARY KEY CLUSTERED 
(
	[CGSNP_GovtSavingNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdviserAssetClasses]    Script Date: 06/12/2009 18:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdviserAssetClasses](
	[AAC_AssetClassId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdvisorId] [int] NOT NULL,
	[AAC_AssetClassCode] [varchar](25) NOT NULL,
	[AAC_CreatedBy] [int] NOT NULL,
	[AAC_CreatedOn] [datetime] NOT NULL,
	[AAC_ModifiedBy] [int] NOT NULL,
	[AAC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdvisorAssetClass] PRIMARY KEY CLUSTERED 
(
	[AAC_AssetClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor Asset Class Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserAssetClasses'
GO
/****** Object:  Table [dbo].[AdviserDailyEODLog]    Script Date: 06/12/2009 18:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdviserDailyEODLog](
	[ADEL_EODLogId] [int] IDENTITY(1,1) NOT NULL,
	[ADEL_ProcessDate] [datetime] NULL,
	[ADEL_StartTime] [datetime] NULL,
	[ADEL_IsValuationComplete] [tinyint] NULL,
	[ADEL_IsEquityCleanUpComplete] [tinyint] NULL,
	[ADEL_EndTime] [datetime] NULL,
	[ADEL_CreatedBy] [int] NULL,
	[ADEL_CreatedOn] [datetime] NULL,
	[ADEL_ModifiedBy] [int] NULL,
	[ADEL_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NOT NULL,
	[ADEL_AssetGroup] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AdviserDailyEODLog] PRIMARY KEY CLUSTERED 
(
	[ADEL_EODLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdviserBranch]    Script Date: 06/12/2009 18:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdviserBranch](
	[AB_BranchId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NOT NULL,
	[AB_BranchHeadId] [int] NULL,
	[AB_BranchCode] [varchar](10) NULL,
	[AB_BranchName] [varchar](25) NULL,
	[AB_AddressLine1] [varchar](25) NOT NULL,
	[AB_AddressLine2] [varchar](25) NULL,
	[AB_AddressLine3] [varchar](25) NULL,
	[AB_City] [varchar](25) NOT NULL,
	[AB_PinCode] [numeric](6, 0) NOT NULL,
	[AB_State] [varchar](25) NOT NULL,
	[AB_Country] [varchar](25) NOT NULL,
	[AB_Email] [varchar](50) NULL,
	[AB_Phone1ISD] [numeric](4, 0) NOT NULL,
	[AB_Phone2ISD] [numeric](4, 0) NULL,
	[AB_Phone1STD] [numeric](4, 0) NOT NULL,
	[AB_Phone1] [numeric](8, 0) NOT NULL,
	[AB_Phone2STD] [numeric](4, 0) NULL,
	[AB_Phone2] [numeric](8, 0) NULL,
	[AB_FaxISD] [numeric](4, 0) NULL,
	[AB_Fax] [numeric](8, 0) NULL,
	[AB_FaxSTD] [numeric](4, 0) NOT NULL,
	[AB_BranchHeadMobile] [numeric](10, 0) NULL,
	[AB_CreatedBy] [int] NOT NULL,
	[AB_CreatedOn] [datetime] NOT NULL,
	[AB_ModifiedOn] [datetime] NOT NULL,
	[AB_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_AdvisorBranch] PRIMARY KEY CLUSTERED 
(
	[AB_BranchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor Branch Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserBranch'
GO
/****** Object:  Table [dbo].[XMLExternalSourceFileType]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XMLExternalSourceFileType](
	[XESFT_FileTypeId] [int] IDENTITY(1,1) NOT NULL,
	[XESFT_AssetGroup] [varchar](5) NULL,
	[XESFT_FileType] [varchar](25) NULL,
	[XESFT_FileExtension] [varchar](20) NULL,
	[XES_SourceCode] [varchar](5) NULL,
	[WUXFT_XMLFileTypeId] [int] NULL,
	[XESFT_CreatedBy] [int] NULL,
	[XESFT_CreatedOn] [datetime] NULL,
	[XESFT_ModifiedBy] [int] NULL,
	[XESFT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpUploadProcesses] PRIMARY KEY CLUSTERED 
(
	[XESFT_FileTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdviserDailyUploadLog]    Script Date: 06/12/2009 18:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdviserDailyUploadLog](
	[ADUL_ProcessId] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_FileName] [varchar](50) NULL,
	[XESFT_FileTypeId] [int] NULL,
	[ADUL_TotalNoOfRecords] [int] NULL,
	[U_UserId] [int] NULL,
	[ADUL_XMLFileName] [varchar](50) NULL,
	[A_AdviserId] [int] NULL,
	[ADUL_Comment] [varchar](50) NULL,
	[ADUL_StartTime] [datetime] NULL,
	[ADUL_EndTime] [datetime] NULL,
	[ADUL_NoOfRejectRecords] [int] NULL,
	[ADUL_NoOfCustomersCreated] [int] NULL,
	[ADUL_NoOfTransactionsCreated] [int] NULL,
	[ADUL_NoOfFoliosCreated] [int] NULL,
	[ADUL_IsXMLConvesionComplete] [tinyint] NULL,
	[ADUL_IsInsertionToInputComplete] [tinyint] NULL,
	[ADUL_IsInsertionToStagingComplete] [tinyint] NULL,
	[ADUL_IsInsertionToWerpComplete] [tinyint] NULL,
	[ADUL_CreatedBy] [int] NULL,
	[ADUL_CreatedOn] [datetime] NULL,
	[ADUL_ModifiedBy] [int] NULL,
	[ADUL_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpUploadLog] PRIMARY KEY CLUSTERED 
(
	[ADUL_ProcessId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdviserEquityBrokerage]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdviserEquityBrokerage](
	[AEB_BrokerageId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NULL,
	[AEB_BuySell] [char](1) NULL,
	[AEB_Brokerage] [numeric](10, 5) NULL,
	[AEB_ServiceTax] [numeric](10, 5) NULL,
	[AEB_Clearing] [numeric](10, 5) NULL,
	[AEB_STT] [numeric](10, 4) NULL,
	[AEB_IsSpeculative] [tinyint] NULL,
	[AEB_Class] [char](1) NULL,
	[AEB_CalculationBasis] [char](1) NULL,
	[AEB_CreatedBy] [int] NULL,
	[AEB_CreatedOn] [datetime] NULL,
	[AEB_ModifiedOn] [datetime] NULL,
	[AEB_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_AdviserEquityBrokerage] PRIMARY KEY CLUSTERED 
(
	[AEB_BrokerageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdviserTerminal]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdviserTerminal](
	[AT_Id] [int] IDENTITY(1000,1) NOT NULL,
	[AT_TerminalId] [numeric](10, 0) NULL,
	[AB_BranchId] [int] NOT NULL,
	[AT_CreatedBy] [int] NOT NULL,
	[AT_CreatedOn] [datetime] NOT NULL,
	[AT_ModifiedBy] [int] NOT NULL,
	[AT_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdvisorTerminal] PRIMARY KEY CLUSTERED 
(
	[AT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor Terminal Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserTerminal'
GO
/****** Object:  Table [dbo].[AdviserRMBranch]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdviserRMBranch](
	[AR_RMId] [int] NOT NULL,
	[AB_BranchId] [int] NOT NULL,
	[ARB_CreatedBy] [int] NOT NULL,
	[ARB_CreatedOn] [datetime] NOT NULL,
	[ARB_ModifiedBy] [int] NOT NULL,
	[ARB_ModifiedOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RM Branch Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserRMBranch'
GO
/****** Object:  Table [dbo].[CustomerInsuranceAccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerInsuranceAccountAssociates](
	[CIAA_AccountassociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CIA_AccountId] [int] NULL,
	[CA_AssociationId] [int] NULL,
	[CIAA_AssociationType] [varchar](30) NULL,
	[CIAA_CreatedBy] [int] NULL,
	[CIAA_CreatedOn] [datetime] NULL,
	[CIAA_ModifiedOn] [datetime] NULL,
	[CIAA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerInsuranceAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CIAA_AccountassociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityDematAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityDematAccount](
	[CEDA_DematAccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[CEDA_DPClientId] [varchar](20) NULL,
	[CEDA_DPId] [varchar](20) NULL,
	[CEDA_DPName] [varchar](50) NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CEDA_IsJointlyHeld] [tinyint] NULL,
	[CEDA_AccountOpeningDate] [datetime] NULL,
	[CEDA_CreatedBy] [int] NULL,
	[CEDA_CreatedOn] [datetime] NULL,
	[CEDA_ModifiedBy] [int] NULL,
	[CEDA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityDematAccount] PRIMARY KEY CLUSTERED 
(
	[CEDA_DematAccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerEquityDematAccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityDematAccountAssociates](
	[CEDAA_AccountAssociationId] [int] NOT NULL,
	[CAS_AssociationId] [int] NULL,
	[CEDAA_AssociationType] [varchar](30) NULL,
	[CEDA_DematAccountId] [int] NULL,
	[CEDAA_CreatedBy] [int] NULL,
	[CEDAA_CreatedOn] [datetime] NULL,
	[CEDAA_ModifiedBy] [int] NULL,
	[CEDAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerDematAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CEDAA_AccountAssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerPensionandGratuitiesAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPensionandGratuitiesAccount](
	[CPGA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CPGA_AccountNum] [varchar](30) NULL,
	[CPGA_AccountSource] [varchar](30) NULL,
	[CPGA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CPGA_AccountOpeningDate] [datetime] NULL,
	[CPGA_CreatedBy] [int] NULL,
	[CPGA_CreatedOn] [datetime] NULL,
	[CPGA_ModifiedBy] [int] NULL,
	[CPGA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPensionandGratuitiesAccount_1] PRIMARY KEY CLUSTERED 
(
	[CPGA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerPensionandGrauitiesAccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates](
	[CPGAA_AccountAssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CPGA_AccountId] [int] NULL,
	[CA_AssociationId] [int] NULL,
	[CPGAA_AssociationType] [varchar](30) NULL,
	[CPGAA_NomineeShare] [numeric](5, 2) NULL,
	[CPGAA_CreatedBy] [int] NULL,
	[CPGAA_CreatedOn] [datetime] NULL,
	[CPGAA_ModifiedOn] [datetime] NULL,
	[CPGAA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerPensionandGrauitiesAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CPGAA_AccountAssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerFixedIncomeAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerFixedIncomeAccount](
	[CFIA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CFIA_AccountNum] [varchar](30) NULL,
	[CFIA_AccountSource] [varchar](30) NULL,
	[CFIA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CFIA_CreatedBy] [int] NULL,
	[CFIA_CreatedOn] [datetime] NULL,
	[CFIA_ModifiedBy] [int] NULL,
	[CFIA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerFixedIncomeAccount_1] PRIMARY KEY CLUSTERED 
(
	[CFIA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerFixedIncomeAcccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerFixedIncomeAcccountAssociates](
	[CFIAA_AccountAssociateId] [int] IDENTITY(1000,1) NOT NULL,
	[CFIA_AccountId] [int] NULL,
	[CA_AssociateId] [int] NULL,
	[CFIAA_AssociationType] [varchar](30) NULL,
	[CFIAA_CreatedBy] [int] NULL,
	[CFIAA_CreatedOn] [datetime] NULL,
	[CFIAA_ModifiedBy] [int] NULL,
	[CFIAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerFixedIncomeAcccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CFIAA_AccountAssociateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerPropertyAccount]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPropertyAccount](
	[CPA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[CPA_AccountNum] [varchar](30) NULL,
	[CPA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CPA_CreatedBy] [int] NULL,
	[CPA_CreatedOn] [datetime] NULL,
	[CPA_ModifiedBy] [int] NULL,
	[CPA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPropertyAccount] PRIMARY KEY CLUSTERED 
(
	[CPA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerPropertyAccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPropertyAccountAssociates](
	[CPAA_AccountAssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CPA_AccountId] [int] NULL,
	[CA_AssociationId] [int] NULL,
	[CPAA_AssociationType] [varchar](30) NULL,
	[CPAA_NomineeShare] [numeric](3, 0) NULL,
	[CPAA_CreatedBy] [int] NULL,
	[CPAA_CreatedOn] [datetime] NULL,
	[CPAA_ModifiedBy] [int] NULL,
	[CPAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPropertyAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CPAA_AccountAssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMutualFundAccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMutualFundAccountAssociates](
	[CMFAA_AccountAssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CA_AssociationId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CMFAA_AssociationType] [varchar](30) NULL,
	[CMFAA_ModifiedBy] [int] NOT NULL,
	[CMFAA_CreatedBy] [int] NOT NULL,
	[CMFAA_ModifiedOn] [datetime] NOT NULL,
	[CMFAA_CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerAccountAssociation] PRIMARY KEY CLUSTERED 
(
	[CMFAA_AccountAssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Account Association Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundAccountAssociates'
GO
/****** Object:  Table [dbo].[CustomerMutualFundSystematicSetup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMutualFundSystematicSetup](
	[CMFSS_SystematicSetupId] [int] IDENTITY(1000,1) NOT NULL,
	[PASP_SchemePlanCode] [int] NOT NULL,
	[PASP_SchemePlanCodeSwitch] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[XSTT_SystematicTypeCode] [varchar](5) NULL,
	[CMFSS_StartDate] [datetime] NULL,
	[CMFSS_EndDate] [datetime] NULL,
	[CMFSS_SystematicDate] [numeric](2, 0) NULL,
	[CMFSS_Amount] [numeric](10, 5) NULL,
	[CMFSS_IsManual] [tinyint] NULL,
	[XES_SourceCode] [varchar](5) NULL,
	[XF_FrequencyCode] [varchar](5) NULL,
	[XPM_PaymentModeCode] [varchar](5) NULL,
	[CMFSS_CreatedBy] [int] NULL,
	[CMFSS_CreatedOn] [datetime] NULL,
	[CMFSS_ModifiedBy] [int] NULL,
	[CMFSS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerSystematicTransactionSetup_1] PRIMARY KEY CLUSTERED 
(
	[CMFSS_SystematicSetupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Systematic Transaction Setup Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundSystematicSetup'
GO
/****** Object:  Table [dbo].[CustomerEquityTradeDematAccountAssociation]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerEquityTradeDematAccountAssociation](
	[CETDAA_AssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CEDA_DematAccountId] [int] NULL,
	[CETA_AccountId] [int] NULL,
	[CETDAA_IsDefault] [tinyint] NULL,
	[CETDAA_CreatedBy] [int] NULL,
	[CETDAA_CreatedOn] [datetime] NULL,
	[CETDAA_ModifiedBy] [int] NULL,
	[CETDAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityTradeDematAccountAssociation] PRIMARY KEY CLUSTERED 
(
	[CETDAA_AssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerEquityNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerEquityNetPosition](
	[CENP_EquityNPId] [int] IDENTITY(1000,1) NOT NULL,
	[PEM_ScripCode] [int] NOT NULL,
	[CETA_AccountId] [int] NOT NULL,
	[CENP_ValuationDate] [datetime] NULL,
	[CENP_NetHoldings] [numeric](18, 4) NULL,
	[CENP_AveragePrice] [numeric](18, 4) NULL,
	[CENP_MarketPrice] [numeric](18, 4) NULL,
	[CENP_RealizedP/L] [numeric](18, 4) NULL,
	[CENP_CostOfSales] [numeric](18, 4) NULL,
	[CENP_NetCost] [numeric](18, 4) NULL,
	[CENP_SpeculativeSaleQuantity] [numeric](18, 4) NULL,
	[CENP_DeliverySaleQuantity] [numeric](18, 4) NULL,
	[CENP_SaleQuantity] [numeric](18, 4) NULL,
	[CENP_RealizedP/LForSpeculative] [numeric](18, 4) NULL,
	[CENP_RealizedP/LForDelivery] [numeric](18, 4) NULL,
	[CENP_CostOfSalesForSpeculative] [numeric](18, 4) NULL,
	[CENP_CostofSalesforDelivery] [numeric](18, 4) NULL,
	[CENP_Deliverysaleproceeds] [numeric](18, 4) NULL,
	[CENP_Speculativesalesproceeds] [numeric](18, 4) NULL,
	[CENP_CurrentValue] [numeric](18, 4) NULL,
	[CENP_CreatedBy] [int] NOT NULL,
	[CENP_CreatedOn] [datetime] NOT NULL,
	[CENP_ModifiedBy] [int] NOT NULL,
	[CENP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerEquityPortfolio] PRIMARY KEY CLUSTERED 
(
	[CENP_EquityNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cusotmer Equity Portfolio Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerEquityNetPosition'
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlTransaction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlTransaction](
	[CIMFCXT_UploadId] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[CMFT_MFTransId] [int] NULL,
	[CIMFCXT_AMCCode] [varchar](10) NULL,
	[CIMFCXT_FolioNum] [varchar](50) NULL,
	[CIMFCXT_ProductCode] [varchar](50) NULL,
	[CIMFCXT_Scheme] [varchar](150) NULL,
	[CIMFCXT_InvestorName] [varchar](75) NULL,
	[CIMFCXT_TransactionType] [varchar](10) NULL,
	[CIMFCXT_TransactionNum] [varchar](20) NULL,
	[CIMFCXT_TransactionMode] [varchar](5) NULL,
	[CIMFCXT_TransactionStatus] [varchar](50) NULL,
	[CIMFCXT_UserCode] [varchar](25) NULL,
	[CIMFCXT_UserTransactionNum] [varchar](50) NULL,
	[CIMFCXT_ValueDate] [datetime] NULL,
	[CIMFCXT_PostDate] [datetime] NULL,
	[CIMFCXT_Price] [numeric](25, 12) NULL,
	[CIMFCXT_Units] [numeric](25, 12) NULL,
	[CIMFCXT_Amount] [numeric](25, 12) NULL,
	[CIMFCXT_BrokerCode] [varchar](50) NULL,
	[CIMFCXT_SubBrokerCode] [varchar](50) NULL,
	[CIMFCXT_BrokeragePercentage] [numeric](25, 12) NULL,
	[CIMFCXT_BrokerageAmount] [numeric](25, 12) NULL,
	[CIMFCXT_Dummy1] [varchar](50) NULL,
	[CIMFCXT_FeedDate] [datetime] NULL,
	[CIMFCXT_Dummy2] [varchar](50) NULL,
	[CIMFCXT_Dummy3] [varchar](50) NULL,
	[CIMFCXT_ApplicationNum] [varchar](25) NULL,
	[CIMFCXT_TransactionNature] [varchar](25) NULL,
	[CIMFCXT_TaxStatus] [varchar](25) NULL,
	[CIMFCXT_AlternateBroker] [varchar](50) NULL,
	[CIMFCXT_AlternateFolio] [varchar](16) NULL,
	[CIMFCXT_ReinvestmentFlag] [char](1) NULL,
	[CIMFCXT_OldFolio] [varchar](16) NULL,
	[CIMFCXT_SequenceNum] [varchar](16) NULL,
	[CIMFCXT_MultipleBroker] [varchar](16) NULL,
	[CIMFCXT_Tax] [numeric](25, 12) NULL,
	[CIMFCXT_STT] [numeric](25, 12) NULL,
	[CIMFCXT_SchemeType] [varchar](50) NULL,
	[CIMFCXT_EntryLoad] [numeric](25, 12) NULL,
	[CIMFCXT_ScanRefNum] [varchar](50) NULL,
	[CIMFCXT_InvestorIIN] [varchar](50) NULL,
	[CIMFCXT_CreatedBy] [int] NULL,
	[CIMFCXT_CreatedOn] [datetime] NULL,
	[CIMFCXT_ModifiedBy] [int] NULL,
	[CIMFCXT_ModifiedOn] [datetime] NULL,
	[CIMFCXT_TaxStatus1] [varchar](50) NULL,
	[CIMFCXT_StatusCode] [varchar](50) NULL,
 CONSTRAINT [PK_CAMSTransactionUpload] PRIMARY KEY CLUSTERED 
(
	[CIMFCXT_UploadId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer CAMS MF Transaction Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMFCAMSXtrnlTransaction'
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlTransaction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlTransaction](
	[CIMFKXT_Id] [int] IDENTITY(1000,1) NOT NULL,
	[CMFT_MFTransId] [int] NULL,
	[ADUL_ProcessId] [int] NULL,
	[CIMFKXT_ProductCode] [varchar](50) NULL,
	[CIMFKXT_Fund] [varchar](50) NULL,
	[CIMFKXT_FolioNumber] [varchar](50) NULL,
	[CIMFKXT_SchemeCode] [varchar](50) NULL,
	[CIMFKXT_DividendOption] [varchar](50) NULL,
	[CIMFKXT_FundDescription] [varchar](150) NULL,
	[CIMFKXT_TransactionHead] [varchar](50) NULL,
	[CIMFKXT_TransactionNumber] [varchar](50) NULL,
	[CIMFKXT_Switch_RefNo] [varchar](50) NULL,
	[CIMFKXT_InstrumentNumber] [varchar](50) NULL,
	[CIMFKXT_InvestorName] [varchar](75) NULL,
	[CIMFKXT_TransactionMode] [varchar](50) NULL,
	[CIMFKXT_TransactionStatus] [varchar](50) NULL,
	[CIMFKXT_BranchName] [varchar](50) NULL,
	[CIMFKXT_BranchTransactionNo] [varchar](50) NULL,
	[CIMFKXT_TransactionDate] [datetime] NULL,
	[CIMFKXT_ProcessDate] [varchar](50) NULL,
	[CIMFKXT_Price] [numeric](25, 12) NULL,
	[CIMFKXT_LoadPercentage] [numeric](25, 12) NULL,
	[CIMFKXT_Units] [numeric](25, 12) NULL,
	[CIMFKXT_Amount] [numeric](25, 12) NULL,
	[CIMFKXT_LoadAmount] [numeric](25, 12) NULL,
	[CIMFKXT_AgentCode] [varchar](50) NULL,
	[CIMFKXT_Sub-BrokerCode] [varchar](50) NULL,
	[CIMFKXT_BrokeragePercentage] [numeric](25, 12) NULL,
	[CIMFKXT_Commission] [numeric](25, 12) NULL,
	[CIMFKXT_InvestorID] [varchar](50) NULL,
	[CIMFKXT_ReportDate] [datetime] NULL,
	[CIMFKXT_ReportTime] [datetime] NULL,
	[CIMFKXT_TransactionSub] [varchar](50) NULL,
	[CIMFKXT_ApplicationNumber] [varchar](50) NULL,
	[CIMFKXT_TransactionID] [varchar](50) NULL,
	[CIMFKXT_TransactionDescription] [varchar](50) NULL,
	[CIMFKXT_TransactionType] [varchar](50) NULL,
	[CIMFKXT_OrgPurchaseDate] [datetime] NULL,
	[CIMFKXT_OrgPurchaseAmount] [numeric](25, 12) NULL,
	[CIMFKXT_OrgPurchaseUnits] [numeric](25, 12) NULL,
	[CIMFKXT_TrTypeFlag] [varchar](50) NULL,
	[CIMFKXT_SwitchFundDate] [datetime] NULL,
	[CIMFKXT_InstrumentDate] [datetime] NULL,
	[CIMFKXT_InstrumentBank] [varchar](50) NULL,
	[CIMFKXT_Nav] [numeric](25, 12) NULL,
	[CIMFKXT_PurchaseTrnNo] [varchar](50) NULL,
	[CIMFKXT_STT] [numeric](25, 12) NULL,
	[CIMFKXT_IHNo] [varchar](50) NULL,
	[CIMFKXT_BranchCode] [varchar](50) NULL,
	[CIMFKXT_InwardNo] [varchar](50) NULL,
	[CIMFKXT_Remarks] [varchar](150) NULL,
	[CIMFKXT_PAN1] [varchar](50) NULL,
	[CIMFKXT_Dummy1] [varchar](50) NULL,
	[CIMFKXT_Dummy2] [varchar](50) NULL,
	[CIMFKXT_Dummy3] [varchar](50) NULL,
	[CIMFKXT_Dummy4] [varchar](50) NULL,
	[CIMFKXT_NCTREMARKS] [varchar](50) NULL,
	[CIMFKXT_Dummy5] [varchar](50) NULL,
	[CIMFKXT_CreatedBy] [int] NULL,
	[CIMFKXT_CreatedOn] [datetime] NULL,
	[CIMFKXT_ModifiedBy] [int] NULL,
	[CIMFKXT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerInvestmentMFKarvyXtrnlTransaction1] PRIMARY KEY CLUSTERED 
(
	[CIMFKXT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMutualFundNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerMutualFundNetPosition](
	[CMFNP_MFNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CMFA_AccountId] [int] NULL,
	[PASP_SchemePlanCode] [int] NULL,
	[CMFNP_NetHoldings] [numeric](18, 4) NULL,
	[CMFNP_MarketPrice] [numeric](18, 4) NULL,
	[CMFNP_ValuationDate] [datetime] NULL,
	[CMFNP_SalesQuantity] [numeric](18, 4) NULL,
	[CMFNP_RealizedSaleProceeds] [numeric](18, 4) NULL,
	[CMFNP_AveragePrice] [numeric](18, 4) NULL,
	[CMFNP_RealizedP/L] [numeric](18, 4) NULL,
	[CMFNP_CostOfSales] [numeric](18, 4) NULL,
	[CMFNP_NetCost] [numeric](18, 4) NULL,
	[CMFNP_CurrentValue] [numeric](18, 4) NULL,
	[CMFNP_DividendIncome] [numeric](18, 4) NULL,
	[CMFNP_CreatedBy] [int] NOT NULL,
	[CMFNP_CreatedOn] [datetime] NOT NULL,
	[CMFNP_ModifiedOn] [datetime] NOT NULL,
	[CMFNP_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerMFPortfolio] PRIMARY KEY CLUSTERED 
(
	[CMFNP_MFNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer MF Portfolio Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundNetPosition'
GO
/****** Object:  Table [dbo].[CustomerInsuranceULIPPlan]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerInsuranceULIPPlan](
	[CIUP_ULIPPlanId] [int] IDENTITY(1000,1) NOT NULL,
	[CINP_InsuranceNPId] [int] NULL,
	[WUSP_ULIPSubPlanCode] [int] NULL,
	[CIUP_AllocationPer] [numeric](5, 2) NULL,
	[CIUP_Unit] [numeric](10, 3) NULL,
	[CIUP_PurchasePrice] [numeric](18, 3) NULL,
	[CIUP_PurchaseDate] [datetime] NULL,
	[CIUP_CreatedBy] [int] NULL,
	[CIUP_CreatedOn] [datetime] NULL,
	[CIUP_ModifiedBy] [int] NULL,
	[CIUP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerInsurabceULIPPlan] PRIMARY KEY CLUSTERED 
(
	[CIUP_ULIPPlanId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerInsuranceMoneyBackEpisodes]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerInsuranceMoneyBackEpisodes](
	[CIMBE_EpisodeId] [int] IDENTITY(1000,1) NOT NULL,
	[CINP_InsuranceNPId] [int] NULL,
	[CIMBE_RepaymentDate] [datetime] NULL,
	[CIMBE_RepaidPer] [numeric](5, 2) NULL,
	[CIMBE_CreatedOn] [datetime] NULL,
	[CIMBE_CreatedBy] [int] NULL,
	[CIMBE_ModifiedOn] [datetime] NULL,
	[CIMBE_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerInsuranceMoneyBackEpisodes] PRIMARY KEY CLUSTERED 
(
	[CIMBE_EpisodeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WerpUploadFieldMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpUploadFieldMapping](
	[WUXFT_XMLFileTypeId] [int] NULL,
	[WUFM_WearpNameForExternalColumn] [varchar](50) NULL,
	[WUFM_WerpTable] [varchar](100) NULL,
	[WUFM_WerpTableColumn] [varchar](100) NULL,
	[WUFM_IsMandatoryForTransaction] [tinyint] NULL,
	[WUFM_IsManadatoryforProfile] [tinyint] NULL,
	[WUFM_IsMandatoryForCombination] [tinyint] NULL,
	[WUFM_CreatedBy] [int] NULL,
	[WUFM_CreatedOn] [datetime] NULL,
	[WUFM_ModifiedOn] [datetime] NULL,
	[WUFM_ModifiedBy] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerFixedIncomeNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerFixedIncomeNetPosition](
	[CFINP_FINPId] [int] IDENTITY(1000,1) NOT NULL,
	[CFIA_AccountId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XDI_DebtIssuerCode] [varchar](5) NULL,
	[XIB_InterestBasisCode] [varchar](5) NULL,
	[XF_CompoundInterestFrequencyCode] [varchar](5) NULL,
	[XF_InterestPayableFrequencyCode] [varchar](5) NULL,
	[CFINP_Name] [varchar](50) NULL,
	[CFINP_IssueDate] [datetime] NULL,
	[CFINP_PrincipalAmount] [numeric](18, 4) NULL,
	[CFINP_InterestAmtPaidOut] [numeric](18, 4) NULL,
	[CFINP_InterestAmtAcculumated] [numeric](18, 4) NULL,
	[CFINP_InterestRate] [numeric](10, 0) NULL,
	[CFINP_FaceValue] [numeric](18, 4) NULL,
	[CFINP_PurchasePrice] [numeric](18, 4) NULL,
	[CFINP_SubsequentDepositAmount] [numeric](18, 4) NULL,
	[XF_DepositFrquencycode] [varchar](5) NULL,
	[CFINP_DebentureNum] [numeric](5, 0) NULL,
	[CFINP_PurchaseValue] [numeric](18, 4) NULL,
	[CFINP_PurchaseDate] [datetime] NULL,
	[CFINP_MaturityDate] [datetime] NULL,
	[CFINP_MaturityValue] [numeric](18, 4) NULL,
	[CFINP_IsInterestAccumulated] [tinyint] NULL,
	[CFINP_MaturityFaceValue] [numeric](18, 4) NULL,
	[CFINP_CurrentPrice] [numeric](18, 4) NULL,
	[CFINP_CurrentValue] [numeric](10, 0) NULL,
	[CFINP_Quantity] [numeric](10, 0) NULL,
	[CFINP_Remark] [varchar](100) NULL,
	[CFINP_CreatedBy] [int] NOT NULL,
	[CFINP_CreatedOn] [datetime] NOT NULL,
	[CFINP_ModifiedBy] [int] NOT NULL,
	[CFINP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerFIInvestment] PRIMARY KEY CLUSTERED 
(
	[CFINP_FINPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Investment FI Transaction' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerFixedIncomeNetPosition'
GO
/****** Object:  Table [dbo].[CustomerPensionandGratuitiesNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPensionandGratuitiesNetPosition](
	[CPGNP_PensionGratutiesNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CPGA_AccountId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XDI_DebtIssuerCode] [varchar](5) NULL,
	[XFY_FiscalYearCode] [varchar](5) NULL,
	[CPGNP_EmployeeContri] [numeric](18, 4) NULL,
	[XF_InterestPayableFrequencyCode] [varchar](5) NULL,
	[XF_CompoundInterestFrequencyCode] [varchar](5) NULL,
	[CPGNP_InterestRate] [numeric](6, 3) NULL,
	[CPGNP_OrganizationName] [varchar](50) NULL,
	[CPGNP_PurchaseDate] [datetime] NULL,
	[CPGNP_DepositAmount] [numeric](18, 4) NULL,
	[CPGNP_EmployerContri] [numeric](18, 4) NULL,
	[CPGNP_MaturityDate] [datetime] NULL,
	[CPGNP_MaturityValue] [numeric](18, 4) NULL,
	[CPGNP_CurrentValue] [numeric](18, 4) NULL,
	[CPGNP_Remark] [varchar](100) NULL,
	[XIB_InterestBasisCode] [varchar](5) NULL,
	[CPGNP_IsInterestAccumalated] [tinyint] NULL,
	[CPGNP_InterestAmtAccumalated] [numeric](18, 4) NULL,
	[CPGNP_InterestAmtPaidOut] [numeric](18, 4) NULL,
	[CPGNP_LoanStartDate] [datetime] NULL,
	[CPGNP_LoanEndDate] [datetime] NULL,
	[CPGNP_LoanOutstandingAmount] [numeric](18, 4) NULL,
	[CPGNP_LoanDescription] [varchar](50) NULL,
	[CPGNP_CreatedBy] [int] NOT NULL,
	[CPGNP_CreatedOn] [datetime] NOT NULL,
	[CPGNP_ModifiedBy] [int] NOT NULL,
	[CPGNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerPensionandGratuitiesPortfolio] PRIMARY KEY CLUSTERED 
(
	[CPGNP_PensionGratutiesNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerCollectibleNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerCollectibleNetPosition](
	[CCNP_CollectibleNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CCNP_PurchasePrice] [numeric](18, 3) NULL,
	[CCNP_Name] [varchar](50) NULL,
	[CCNP_PurchaseDate] [datetime] NULL,
	[CCNP_PurchaseValue] [numeric](18, 3) NULL,
	[CCNP_CurrentPrice] [numeric](18, 3) NULL,
	[CCNP_CurrentValue] [numeric](18, 3) NOT NULL,
	[CCNP_Remark] [varchar](100) NULL,
	[CCNP_Quantity] [numeric](5, 0) NULL,
	[CCNP_CreatedBy] [int] NOT NULL,
	[CCNP_CreatedOn] [datetime] NOT NULL,
	[CCNP_ModifiedBy] [int] NOT NULL,
	[CCNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerInvestmentCollectiblePortfolio] PRIMARY KEY CLUSTERED 
(
	[CCNP_CollectibleNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerGoldNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerGoldNetPosition](
	[CGNP_GoldNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XMC_MeasureCode] [varchar](5) NULL,
	[CGNP_Name] [varchar](50) NULL,
	[CGNP_PurchaseDate] [datetime] NULL,
	[CGNP_PurchasePrice] [numeric](18, 4) NULL,
	[CGNP_Quantity] [numeric](10, 4) NULL,
	[CGNP_PurchaseValue] [numeric](18, 4) NULL,
	[CGNP_CurrentPrice] [numeric](18, 4) NULL,
	[CGNP_CurrentValue] [numeric](18, 4) NULL,
	[CGNP_SellDate] [datetime] NULL,
	[CGNP_SellPrice] [numeric](18, 4) NULL,
	[CGNP_SellValue] [numeric](18, 4) NULL,
	[CGNP_Remark] [varchar](100) NULL,
	[CGNP_CreatedBy] [int] NOT NULL,
	[CGNP_CreatedOn] [datetime] NOT NULL,
	[CGNP_ModifiedBy] [int] NOT NULL,
	[CGNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerInvestmentGoldPortfolio] PRIMARY KEY CLUSTERED 
(
	[CGNP_GoldNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerPersonalNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPersonalNetPosition](
	[CPNP_PersonalNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CPNP_Name] [varchar](50) NULL,
	[CPNP_PurchaseDate] [datetime] NULL,
	[CPNP_Quantity] [numeric](5, 0) NULL,
	[CPNP_PurchasePrice] [numeric](18, 3) NULL,
	[CPNP_PurchaseValue] [numeric](18, 3) NULL,
	[CPNP_CurrentPrice] [numeric](18, 3) NULL,
	[CPNP_CurrentValue] [numeric](18, 3) NULL,
	[CPNP_CreatedBy] [int] NULL,
	[CPNP_CreatedOn] [datetime] NULL,
	[CPNP_ModifiedBy] [int] NULL,
	[CPNP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPersonalPortfolio] PRIMARY KEY CLUSTERED 
(
	[CPNP_PersonalNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerPropertyNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPropertyNetPosition](
	[CPNP_PropertyNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CPA_AccountId] [int] NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XMC_MeasureCode] [varchar](5) NULL,
	[CPNP_Name] [varchar](50) NULL,
	[CPNP_PropertyAdrLine1] [varchar](30) NULL,
	[CPNP_PropertyAdrLine2] [varchar](30) NULL,
	[CPNP_PropertyAdrLine3] [varchar](30) NULL,
	[CPNP_PropertyCity] [varchar](30) NULL,
	[CPNP_PropertyState] [varchar](30) NULL,
	[CPNP_PropertyCountry] [varchar](30) NULL,
	[CPNP_PropertyPinCode] [numeric](6, 0) NULL,
	[CPNP_PurchaseDate] [datetime] NULL,
	[CPNP_PurchasePrice] [numeric](18, 3) NULL,
	[CPNP_Quantity] [numeric](18, 5) NULL,
	[CPNP_CurrentPrice] [numeric](18, 3) NULL,
	[CPNP_CurrentValue] [numeric](18, 3) NULL,
	[CPNP_PurchaseValue] [numeric](18, 3) NULL,
	[CPNP_SellDate] [datetime] NULL,
	[CPNP_SellPrice] [numeric](18, 3) NULL,
	[CPNP_SellValue] [numeric](18, 3) NULL,
	[CPNP_Remark] [varchar](100) NULL,
	[CPNP_CreatedBy] [int] NOT NULL,
	[CPNP_CreatedOn] [datetime] NOT NULL,
	[CPNP_ModifiedOn] [datetime] NOT NULL,
	[CPNP_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerInvestmentPropertyPortfolio] PRIMARY KEY CLUSTERED 
(
	[CPNP_PropertyNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Investment Real Estate Transaction' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPropertyNetPosition'
GO
/****** Object:  Table [dbo].[CustomerEquityBrokerage]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerEquityBrokerage](
	[CEB_BrokerageId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NULL,
	[CEB_BuySell] [char](1) NULL,
	[CEB_Brokerage] [numeric](10, 5) NULL,
	[CEB_ServiceTax] [numeric](10, 5) NULL,
	[CEB_Clearing] [numeric](10, 5) NULL,
	[CEB_STT] [numeric](10, 4) NULL,
	[CEB_IsSpeculative] [tinyint] NULL,
	[CEB_Class] [char](1) NULL,
	[CEB_CalculationBasis] [char](1) NULL,
	[XB_BrokerCode] [varchar](5) NULL,
	[CEB_CreatedBy] [int] NULL,
	[CEB_CreatedOn] [datetime] NULL,
	[CEB_ModifiedOn] [datetime] NULL,
	[CEB_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerEquityBrokerage] PRIMARY KEY CLUSTERED 
(
	[CEB_BrokerageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpExternalSourceHeaderMaster]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpExternalSourceHeaderMaster](
	[XESFT_FileTypeId] [int] NOT NULL,
	[WESHM_ExternalColumnName] [varchar](50) NULL,
	[WESHM_WerpNameOfExternalColumn] [varchar](50) NULL,
	[WESHM_IsMandatory] [varchar](5) NULL,
	[WESHM_CreatedBy] [int] NULL,
	[WESHM_CreatedOn] [datetime] NULL,
	[WESHM_ModifiedBy] [int] NULL,
	[WESHM_ModifedOn] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpCustomerTypeDataTranslatorMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping](
	[WKCTDTM_TaxStaus] [varchar](10) NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomerSubTypeCode] [varchar](5) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping](
	[WCCTDTM_TaxStatus] [varchar](75) NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomerSubTypeCode] [varchar](5) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WerpQuestionChoice]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpQuestionChoice](
	[WQCH_ChoiceId] [int] IDENTITY(1000,1) NOT NULL,
	[WQM_QuestionId] [int] NOT NULL,
	[WQCH_Choice] [varchar](max) NULL,
	[WQCH_Score] [int] NULL,
	[WQCH_Order] [int] NULL,
	[WQCH_CreatedBy] [bigint] NOT NULL,
	[WQCH_CreatedOn] [datetime] NOT NULL,
	[WQCH_ModifiedBy] [bigint] NOT NULL,
	[WQCH_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_QuestionChoice] PRIMARY KEY CLUSTERED 
(
	[WQCH_ChoiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Question Choice Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpQuestionChoice'
GO
/****** Object:  Table [dbo].[CustomerRiskProfile]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerRiskProfile](
	[CRP_Id] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[QM_QuestionId] [int] NULL,
	[QCH_ChoiceId] [int] NULL,
	[CRP_Score] [int] NULL,
	[CRP_CreatedBy] [int] NOT NULL,
	[CRP_CreatedOn] [datetime] NOT NULL,
	[CRP_ModifiedBy] [int] NOT NULL,
	[CRP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerRiskProfile] PRIMARY KEY CLUSTERED 
(
	[CRP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Risk Profile Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerRiskProfile'
GO
/****** Object:  Table [dbo].[WerpValueResearchAssetClassificationMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpValueResearchAssetClassificationMapping](
	[WVRACM_VRCode] [int] NULL,
	[PAISSC_AssetInstrumentSubSubCategoryCode] [varchar](8) NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerBank]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerBank](
	[CB_CustBankAccId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[CB_BankName] [varchar](50) NULL,
	[XBAT_BankAccountTypeCode] [varchar](5) NULL,
	[CB_AccountNum] [varchar](50) NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CB_BranchName] [varchar](50) NULL,
	[CB_BranchAdrLine1] [varchar](75) NULL,
	[CB_BranchAdrLine2] [varchar](75) NULL,
	[CB_BranchAdrLine3] [varchar](75) NULL,
	[CB_BranchAdrPinCode] [numeric](10, 0) NULL,
	[CB_BranchAdrCity] [varchar](25) NULL,
	[CB_BranchAdrState] [varchar](25) NULL,
	[CB_BranchAdrCountry] [varchar](25) NULL,
	[CB_MICR] [numeric](9, 0) NULL,
	[CB_Balance] [numeric](18, 3) NULL,
	[CB_IFSC] [varchar](11) NULL,
	[CB_CreatedOn] [datetime] NOT NULL,
	[CB_CreatedBy] [int] NOT NULL,
	[CB_ModifiedOn] [datetime] NOT NULL,
	[CB_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerBankAccount] PRIMARY KEY CLUSTERED 
(
	[CB_CustBankAccId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Bank Account Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerBank'
GO
/****** Object:  Table [dbo].[WerpKarvyBankModeOfHoldingDataTranslatorMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WerpKarvyBankModeOfHoldingDataTranslatorMapping](
	[WKBMOHDTM_ModeofHolding] [int] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[WKBMOHDTM_CreatedBy] [int] NULL,
	[WKBMOHDTM_CreatedOn] [datetime] NULL,
	[WKBMOHDTM_ModifiedBy] [int] NULL,
	[WKBMOHDTM_ModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlSystematicSetup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetup](
	[CMFCXSS_SystematicId] [int] IDENTITY(1000,1) NOT NULL,
	[CMFSS_SystematicSetupId] [int] NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFCXSS_PRODUCT] [varchar](50) NULL,
	[CMFCXSS_SCHEME] [varchar](100) NULL,
	[CMFCXSS_FOLIONO] [varchar](50) NULL,
	[CMFCXSS_INVNAME] [varchar](60) NULL,
	[CMFCXSS_AUTOTRXN] [char](2) NULL,
	[CMFCXSS_AUTOTRXNNum] [numeric](10, 0) NULL,
	[CMFCXSS_AUTOAMOUN] [numeric](18, 4) NULL,
	[CMFCXSS_FROMDATE] [datetime] NULL,
	[CMFCXSS_TODATE] [datetime] NULL,
	[CMFCXSS_CEASEDATE] [datetime] NULL,
	[CMFCXSS_PERIODICIT] [varchar](5) NULL,
	[CMFCXSS_PERIODDAY] [numeric](2, 0) NULL,
	[CMFCXSS_INVIIN] [numeric](3, 0) NULL,
	[CMFCXSS_PAYMENTMO] [varchar](5) NULL,
	[CMFCXSS_TARGETSCH] [varchar](20) NULL,
	[CMFCXSS_REGDATE] [datetime] NULL,
	[CMFCXSS_SUBBROKER] [varchar](50) NULL,
	[CMFCXSS_CreatedBy] [int] NULL,
	[CMFCXSS_CreatedOn] [datetime] NULL,
	[CMFCXSS_ModifiedBy] [int] NULL,
	[CMFCXSS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlSystematicSetup] PRIMARY KEY CLUSTERED 
(
	[CMFCXSS_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlSystematicSetup]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetup](
	[CMFKXSS_SystematicId] [int] NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFSS_SystematicSetupId] [int] NULL,
	[CMFKXSS_ProductCode] [varchar](30) NULL,
	[CMFKXSS_Agent Code] [varchar](30) NULL,
	[CMFKXSS_ Fund] [varchar](50) NULL,
	[CMFKXSS_FolioNumber] [varchar](20) NULL,
	[CMFKXSS_SchemeCode] [varchar](20) NULL,
	[CMFKXSS_FundDescription] [varchar](50) NULL,
	[CMFKXSS_InvestorName] [varchar](30) NULL,
	[CMFKXSS_InvestorAddress1] [varchar](25) NULL,
	[CMFKXSS_InvestorAddress2] [varchar](25) NULL,
	[CMFKXSS_InvestorAddress3] [varchar](25) NULL,
	[CMFKXSS_InvestorCity] [varchar](25) NULL,
	[CMFKXSS_InvestorState] [varchar](25) NULL,
	[CMFKXSS_PinCode] [numeric](6, 0) NULL,
	[CMFKXSS_EmailAddress] [varchar](max) NULL,
	[CMFKXSS_PhoneNo1] [numeric](16, 0) NULL,
	[CMFKXSS_PhoneNo2] [numeric](16, 0) NULL,
	[CMFKXSS_TransactionType] [varchar](20) NULL,
	[CMFKXSS_Frequency] [varchar](20) NULL,
	[CMFKXSS_StartingDate] [datetime] NULL,
	[CMFKXSS_EndingDate] [datetime] NULL,
	[CMFKXSS_NoOf InstallmentsPaid] [numeric](5, 0) NULL,
	[CMFKXSS_NoOf InstalmentsPending] [numeric](5, 0) NULL,
	[CMFKXSS_TotalNoOfInstalments] [numeric](5, 0) NULL,
	[CMFKXSS_InstalmentAmount] [numeric](18, 4) NULL,
	[CMFKXSS_STPIn/OutScheme] [varchar](50) NULL,
	[CMFKXSS_PaymentMethod] [varchar](20) NULL,
	[CMFKXSS_Subroker] [varchar](30) NULL,
	[CMFKXSS_IHNO] [varchar](30) NULL,
	[CMFKXSS_Remarks] [varchar](50) NULL,
	[CMFKXSS_CreatedBy] [int] NULL,
	[CMFKXSS_CreatedOn] [datetime] NULL,
	[CMFKXSS_ModifiedBy] [int] NULL,
	[CMFKXSS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlSystematicSetup] PRIMARY KEY CLUSTERED 
(
	[CMFKXSS_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductEquityPrice]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductEquityPrice](
	[PEP_EquityPriceId] [int] IDENTITY(1,1) NOT NULL,
	[PEM_ScripCode] [int] NOT NULL,
	[PEM_Exchange] [char](5) NULL,
	[PEP_Series] [varchar](5) NULL,
	[PEP_OpenPrice] [numeric](18, 4) NULL,
	[PEP_HighPrice] [numeric](18, 4) NULL,
	[PEP_LowPrice] [numeric](18, 4) NULL,
	[PEP_ClosePrice] [numeric](18, 4) NULL,
	[PEP_LastPrice] [numeric](18, 4) NULL,
	[PEP_PreviousClose] [numeric](18, 4) NULL,
	[PEP_TotalTradeQuantity] [numeric](18, 4) NULL,
	[PEP_TotalTradeValue] [numeric](18, 4) NULL,
	[PEP_NoOfTrades] [numeric](18, 4) NULL,
	[PEP_Date] [datetime] NULL,
	[PEP_CreatedBy] [int] NULL,
	[PEP_CreatedOn] [datetime] NULL,
	[PEP_ModifiedOn] [datetime] NULL,
	[PEP_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProductEquityPrice] PRIMARY KEY CLUSTERED 
(
	[PEP_EquityPriceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductEquityScripMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductEquityScripMapping](
	[PEM_ScripCode] [int] NULL,
	[PESM_IdentifierType] [varchar](30) NULL,
	[PESM_IdentifierName] [varchar](30) NULL,
	[PESM_Identifier] [varchar](25) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlProfile]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFKarvyXtrnlProfile](
	[CMFKXP_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CMFKXP_ProductCode] [varchar](15) NULL,
	[CMFKXP_Fund] [varchar](50) NULL,
	[CMFKXP_Folio] [varchar](20) NULL,
	[CMFKXP_DividendOption] [varchar](50) NULL,
	[CMFKXP_FundDescription] [varchar](150) NULL,
	[CMFKXP_InvestorName] [varchar](75) NULL,
	[CMFKXP_JointName1] [varchar](75) NULL,
	[CMFKXP_JointName2] [varchar](75) NULL,
	[CMFKXP_Address1] [varchar](75) NULL,
	[CMFKXP_Address2] [varchar](75) NULL,
	[CMFKXP_Address3] [varchar](75) NULL,
	[CMFKXP_City] [varchar](25) NULL,
	[CMFKXP_Pincode] [numeric](10, 0) NULL,
	[CMFKXP_State] [varchar](25) NULL,
	[CMFKXP_Country] [varchar](25) NULL,
	[CMFKXP_TPIN] [varchar](50) NULL,
	[CMFKXP_DateofBirth] [datetime] NULL,
	[CMFKXP_FName] [varchar](75) NULL,
	[CMFKXP_MName] [varchar](75) NULL,
	[CMFKXP_PhoneResidence] [numeric](25, 0) NULL,
	[CMFKXP_PhoneRes1] [numeric](25, 0) NULL,
	[CMFKXP_PhoneRes2] [numeric](25, 0) NULL,
	[CMFKXP_PhoneOffice] [numeric](25, 0) NULL,
	[CMFKXP_PhoneOff1] [numeric](25, 0) NULL,
	[CMFKXP_PhoneOff2] [numeric](25, 0) NULL,
	[CMFKXP_FaxResidence] [numeric](25, 0) NULL,
	[CMFKXP_FaxOffice] [numeric](25, 0) NULL,
	[CMFKXP_TaxStatus] [varchar](50) NULL,
	[CMFKXP_OccCode] [varchar](50) NULL,
	[CMFKXP_Email] [varchar](100) NULL,
	[CMFKXP_BankAccno] [varchar](50) NULL,
	[CMFKXP_BankName] [varchar](75) NULL,
	[CMFKXP_AccountType] [varchar](25) NULL,
	[CMFKXP_Branch] [varchar](75) NULL,
	[CMFKXP_BankAddress1] [varchar](75) NULL,
	[CMFKXP_BankAddress2] [varchar](75) NULL,
	[CMFKXP_BankAddress3] [varchar](75) NULL,
	[CMFKXP_BankCity] [varchar](25) NULL,
	[CMFKXP_BankPhone] [numeric](25, 0) NULL,
	[CMFKXP_BankState] [varchar](25) NULL,
	[CMFKXP_BankCountry] [varchar](25) NULL,
	[CMFKXP_InvestorID] [varchar](50) NULL,
	[CMFKXP_BrokerCode] [varchar](50) NULL,
	[CMFKXP_PANNumber] [varchar](20) NULL,
	[CMFKXP_Mobile] [numeric](20, 0) NULL,
	[CMFKXP_ReportDate] [datetime] NULL,
	[CMFKXP_ReportTime] [datetime] NULL,
	[CMFKXP_OccupationDescription] [varchar](50) NULL,
	[CMFKXP_ModeofHolding] [varchar](50) NULL,
	[CMFKXP_ModeofHoldingDescription] [varchar](50) NULL,
	[CMFKXP_MapinId] [varchar](50) NULL,
	[CMFKXP_CreatedBy] [int] NULL,
	[CMFKXP_CreatedOn] [datetime] NULL,
	[CMFKXP_ModifiedBy] [int] NULL,
	[CMFKXP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlProfile] PRIMARY KEY CLUSTERED 
(
	[CMFKXP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerGoal]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerGoal](
	[CG_CustomerGoalId] [int] IDENTITY(1000,1) NOT NULL,
	[CM_CustomerId] [int] NOT NULL,
	[CG_CreatedBy] [int] NOT NULL,
	[CG_CreatedOn] [datetime] NOT NULL,
	[CG_ModifiedBy] [int] NOT NULL,
	[CG_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerGoals] PRIMARY KEY CLUSTERED 
(
	[CG_CustomerGoalId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Goals Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerGoal'
GO
/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlProfile]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMFCAMSXtrnlProfile](
	[CMGCXP_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CMGCXP_FOLIOCHK] [varchar](20) NULL,
	[CMGCXP_INV_NAME] [varchar](75) NULL,
	[CMGCXP_ADDRESS1] [varchar](75) NULL,
	[CMGCXP_ADDRESS2] [varchar](75) NULL,
	[CMGCXP_ADDRESS3] [varchar](75) NULL,
	[CMGCXP_CITY] [varchar](25) NULL,
	[CMGCXP_PINCODE] [numeric](10, 0) NULL,
	[CMGCXP_PRODUCT] [varchar](30) NULL,
	[CMGCXP_SCH_NAME] [varchar](150) NULL,
	[CMGCXP_REP_DATE] [datetime] NULL,
	[CMGCXP_CLOS_BAL] [numeric](18, 4) NULL,
	[CMGCXP_RUPEE_BAL] [numeric](18, 4) NULL,
	[CMGCXP_SUBBROK] [varchar](50) NULL,
	[CMGCXP_REINV_FLAG] [varchar](30) NULL,
	[CMGCXP_JOINT_NAME1] [varchar](75) NULL,
	[CMGCXP_JOINT_NAME2] [varchar](75) NULL,
	[CMGCXP_PHONE_OFF] [numeric](25, 0) NULL,
	[CMGCXP_PHONE_RES] [numeric](25, 0) NULL,
	[CMGCXP_EMAIL] [varchar](75) NULL,
	[CMGCXP_HOLDING_NA] [varchar](20) NULL,
	[CMGCXP_UIN_NO] [varchar](50) NULL,
	[CMGCXP_BROKER_COD] [varchar](50) NULL,
	[CMGCXP_PAN_NO] [varchar](20) NULL,
	[CMGCXP_JOINT1_PAN] [varchar](20) NULL,
	[CMGCXP_JOINT2_PAN] [varchar](20) NULL,
	[CMGCXP_GUARD_PAN] [varchar](20) NULL,
	[CMGCXP_TAX_STATUS] [varchar](50) NULL,
	[CMGCXP_INV_IIN] [varchar](20) NULL,
	[CMGCXP_ALTFOLIO] [varchar](50) NULL,
	[CMGCXP_CreatedBy] [int] NULL,
	[CMGCXP_CreatedOn] [datetime] NULL,
	[CMGCXP_ModifiedBy] [int] NULL,
	[CMGCXP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlProfile] PRIMARY KEY CLUSTERED 
(
	[CMGCXP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerIncome]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerIncome](
	[CI_IncomeId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[CI_CreatedBy] [int] NOT NULL,
	[CI_CreatedOn] [datetime] NOT NULL,
	[CI_ModifiedBy] [int] NOT NULL,
	[CI_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerIncome] PRIMARY KEY CLUSTERED 
(
	[CI_IncomeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Income Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerIncome'
GO
/****** Object:  Table [dbo].[ProductAMCSchemePlanCorpAction]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAMCSchemePlanCorpAction](
	[PASPCA_CorpAxnId] [int] NOT NULL,
	[PASP_SchemePlanCode] [int] NULL,
	[PASPCA_CreatedBy] [int] NULL,
	[PASPCA_CreatedOn] [datetime] NULL,
	[PASPCA_ModifiedBy] [int] NULL,
	[PASPCA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductAMCCorpAction] PRIMARY KEY CLUSTERED 
(
	[PASPCA_CorpAxnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAMCSchemeMapping]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAMCSchemeMapping](
	[PASP_SchemePlanCode] [int] NULL,
	[PASC_AMC_ExternalCode] [varchar](255) NULL,
	[PASC_AMC_ExternalType] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAMCSchemePlanPrice]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAMCSchemePlanPrice](
	[PSP_SchemePriceId] [int] IDENTITY(1,1) NOT NULL,
	[PASP_SchemePlanCode] [int] NULL,
	[PSP_NetAssetValue] [numeric](18, 4) NULL,
	[PSP_RepurchasePrice] [numeric](18, 4) NULL,
	[PSP_SalePrice] [numeric](18, 4) NULL,
	[PSP_PostDate] [datetime] NULL,
	[PSP_Date] [datetime] NULL,
	[PSP_CreatedBy] [int] NULL,
	[PSP_CreatedOn] [datetime] NULL,
	[PSP_ModifiedOn] [datetime] NULL,
	[PSP_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProductAMCSchemePlanPrice] PRIMARY KEY CLUSTERED 
(
	[PSP_SchemePriceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
 