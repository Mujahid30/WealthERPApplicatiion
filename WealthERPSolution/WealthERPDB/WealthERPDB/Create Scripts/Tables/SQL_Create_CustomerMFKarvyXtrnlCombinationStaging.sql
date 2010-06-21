
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlCombinationStaging]    Script Date: 06/11/2009 15:38:57 ******/
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


