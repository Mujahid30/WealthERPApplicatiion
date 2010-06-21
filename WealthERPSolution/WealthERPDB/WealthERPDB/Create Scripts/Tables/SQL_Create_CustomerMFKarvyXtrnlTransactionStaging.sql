
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlTransactionStaging]    Script Date: 06/11/2009 15:46:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlTransactionStaging](
	[CIMFKXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CIMFKXTS_ProductCode] [varchar](50) NULL,
	[CIMFKXTS_Fund] [varchar](50) NULL,
	[CIMFKXTS_FolioNumber] [varchar](50) NULL,
	[CIMFKXTS_SchemeCode] [varchar](50) NULL,
	[CIMFKXTS_DividendOption] [varchar](50) NULL,
	[CIMFKXTS_FundDescription] [varchar](255) NULL,
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
	[CIMFKXTS_SwitchFundDate] [varchar](50) NULL,
	[CIMFKXTS_InstrumentDate] [datetime] NULL,
	[CIMFKXTS_InstrumentBank] [varchar](50) NULL,
	[CIMFKXTS_Nav] [numeric](25, 12) NULL,
	[CIMFKXTS_PurchaseTrnNo] [varchar](50) NULL,
	[CIMFKXTS_STT] [numeric](25, 12) NULL,
	[CIMFKXTS_IHNo] [varchar](50) NULL,
	[CIMFKXTS_BranchCode] [varchar](50) NULL,
	[CIMFKXTS_InwardNo] [varchar](50) NULL,
	[CIMFKXTS_Remarks] [varchar](255) NULL,
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


