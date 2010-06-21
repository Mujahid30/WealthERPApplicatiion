
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlCombinationInput]    Script Date: 06/11/2009 15:38:37 ******/
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


