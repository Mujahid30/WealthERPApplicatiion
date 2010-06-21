
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlTransactionInput]    Script Date: 06/11/2009 15:43:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlTransactionInput](
	[CIMFKXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CIMFKXTI_ProductCode] [varchar](100) NULL,
	[CIMFKXTI_Fund] [varchar](100) NULL,
	[CIMFKXTI_FolioNumber] [varchar](100) NULL,
	[CIMFKXTI_SchemeCode] [varchar](100) NULL,
	[CIMFKXTI_DividendOption] [varchar](100) NULL,
	[CIMFKXTI_FundDescription] [varchar](255) NULL,
	[CIMFKXTI_TransactionHead] [varchar](100) NULL,
	[CIMFKXTI_TransactionNumber] [varchar](100) NULL,
	[CIMFKXTI_Switch_RefNo] [varchar](100) NULL,
	[CIMFKXTI_InstrumentNumber] [varchar](100) NULL,
	[CIMFKXTI_InvestorName] [varchar](100) NULL,
	[CIMFKXTI_TransactionMode] [varchar](100) NULL,
	[CIMFKXTI_TransactionStatus] [varchar](100) NULL,
	[CIMFKXTI_BranchName] [varchar](100) NULL,
	[CIMFKXTI_BranchTransactionNo] [varchar](100) NULL,
	[CIMFKXTI_TransactionDate] [varchar](100) NULL,
	[CIMFKXTI_ProcessDate] [varchar](100) NULL,
	[CIMFKXTI_Price] [varchar](100) NULL,
	[CIMFKXTI_LoadPercentage] [varchar](100) NULL,
	[CIMFKXTI_Units] [varchar](100) NULL,
	[CIMFKXTI_Amount] [varchar](100) NULL,
	[CIMFKXTI_LoadAmount] [varchar](100) NULL,
	[CIMFKXTI_AgentCode] [varchar](100) NULL,
	[CIMFKXTI_Sub-BrokerCode] [varchar](100) NULL,
	[CIMFKXTI_BrokeragePercentage] [varchar](100) NULL,
	[CIMFKXTI_Commission] [varchar](100) NULL,
	[CIMFKXTI_InvestorID] [varchar](100) NULL,
	[CIMFKXTI_ReportDate] [varchar](100) NULL,
	[CIMFKXTI_ReportTime] [varchar](100) NULL,
	[CIMFKXTI_TransactionSub] [varchar](100) NULL,
	[CIMFKXTI_ApplicationNumber] [varchar](100) NULL,
	[CIMFKXTI_TransactionID] [varchar](100) NULL,
	[CIMFKXTI_TransactionDescription] [varchar](100) NULL,
	[CIMFKXTI_TransactionType] [varchar](100) NULL,
	[CIMFKXTI_OrgPurchaseDate] [varchar](100) NULL,
	[CIMFKXTI_OrgPurchaseAmount] [varchar](100) NULL,
	[CIMFKXTI_OrgPurchaseUnits] [varchar](100) NULL,
	[CIMFKXTI_TrTypeFlag] [varchar](100) NULL,
	[CIMFKXTI_SwitchFundDate] [varchar](100) NULL,
	[CIMFKXTI_InstrumentDate] [varchar](100) NULL,
	[CIMFKXTI_InstrumentBank] [varchar](100) NULL,
	[CIMFKXTI_Nav] [varchar](100) NULL,
	[CIMFKXTI_PurchaseTrnNo] [varchar](100) NULL,
	[CIMFKXTI_STT] [varchar](100) NULL,
	[CIMFKXTI_IHNo] [varchar](100) NULL,
	[CIMFKXTI_BranchCode] [varchar](100) NULL,
	[CIMFKXTI_InwardNo] [varchar](100) NULL,
	[CIMFKXTI_Remarks] [varchar](100) NULL,
	[CIMFKXTI_PAN1] [varchar](100) NULL,
	[CIMFKXTI_Dummy1] [varchar](100) NULL,
	[CIMFKXTI_Dummy2] [varchar](100) NULL,
	[CIMFKXTI_Dummy3] [varchar](100) NULL,
	[CIMFKXTI_Dummy4] [varchar](100) NULL,
	[CIMFKXTI_NCTREMARKS] [varchar](100) NULL,
	[CIMFKXTI_Dummy5] [varchar](100) NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerInvestmentMFKarvyXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CIMFKXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


