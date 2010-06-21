
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlTransaction]    Script Date: 06/11/2009 15:43:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlTransaction](
	[CIMFKXT_Id] [int] IDENTITY(1000,1) NOT NULL,
	[CMFT_MFTransId] [int] NULL,
	[WUPL_ProcessId] [int] NULL,
	[CIMFKXT_ProductCode] [varchar](50) NULL,
	[CIMFKXT_Fund] [varchar](50) NULL,
	[CIMFKXT_FolioNumber] [varchar](50) NULL,
	[CIMFKXT_SchemeCode] [varchar](50) NULL,
	[CIMFKXT_DividendOption] [varchar](50) NULL,
	[CIMFKXT_FundDescription] [varchar](255) NULL,
	[CIMFKXT_TransactionHead] [varchar](50) NULL,
	[CIMFKXT_TransactionNumber] [varchar](50) NULL,
	[CIMFKXT_Switch_RefNo] [varchar](50) NULL,
	[CIMFKXT_InstrumentNumber] [varchar](50) NULL,
	[CIMFKXT_InvestorName] [varchar](50) NULL,
	[CIMFKXT_TransactionMode] [varchar](50) NULL,
	[CIMFKXT_TransactionStatus] [varchar](50) NULL,
	[CIMFKXT_BranchName] [varchar](50) NULL,
	[CIMFKXT_BranchTransactionNo] [varchar](50) NULL,
	[CIMFKXT_TransactionDate] [datetime] NULL,
	[CIMFKXT_ProcessDate] [varchar](50) NULL,
	[CIMFKXT_Price] [numeric](18, 5) NULL,
	[CIMFKXT_LoadPercentage] [varchar](50) NULL,
	[CIMFKXT_Units] [numeric](18, 5) NULL,
	[CIMFKXT_Amount] [numeric](18, 5) NULL,
	[CIMFKXT_LoadAmount] [varchar](50) NULL,
	[CIMFKXT_AgentCode] [varchar](50) NULL,
	[CIMFKXT_Sub-BrokerCode] [varchar](50) NULL,
	[CIMFKXT_BrokeragePercentage] [varchar](50) NULL,
	[CIMFKXT_Commission] [varchar](50) NULL,
	[CIMFKXT_InvestorID] [varchar](50) NULL,
	[CIMFKXT_ReportDate] [varchar](50) NULL,
	[CIMFKXT_ReportTime] [varchar](50) NULL,
	[CIMFKXT_TransactionSub] [varchar](50) NULL,
	[CIMFKXT_ApplicationNumber] [varchar](50) NULL,
	[CIMFKXT_TransactionID] [varchar](50) NULL,
	[CIMFKXT_TransactionDescription] [varchar](50) NULL,
	[CIMFKXT_TransactionType] [varchar](50) NULL,
	[CIMFKXT_OrgPurchaseDate] [varchar](50) NULL,
	[CIMFKXT_OrgPurchaseAmount] [varchar](50) NULL,
	[CIMFKXT_OrgPurchaseUnits] [varchar](50) NULL,
	[CIMFKXT_TrTypeFlag] [varchar](50) NULL,
	[CIMFKXT_SwitchFundDate] [varchar](50) NULL,
	[CIMFKXT_InstrumentDate] [varchar](50) NULL,
	[CIMFKXT_InstrumentBank] [varchar](50) NULL,
	[CIMFKXT_Nav] [numeric](18, 5) NULL,
	[CIMFKXT_PurchaseTrnNo] [varchar](50) NULL,
	[CIMFKXT_STT] [numeric](10, 5) NULL,
	[CIMFKXT_IHNo] [varchar](50) NULL,
	[CIMFKXT_BranchCode] [varchar](50) NULL,
	[CIMFKXT_InwardNo] [varchar](50) NULL,
	[CIMFKXT_Remarks] [varchar](255) NULL,
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

ALTER TABLE [dbo].[CustomerMFKarvyXtrnlTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFKarvyXtrnlTransaction_CustomerMutualFundTransaction] FOREIGN KEY([CMFT_MFTransId])
REFERENCES [dbo].[CustomerMutualFundTransaction] ([CMFT_MFTransId])
GO

ALTER TABLE [dbo].[CustomerMFKarvyXtrnlTransaction] CHECK CONSTRAINT [FK_CustomerMFKarvyXtrnlTransaction_CustomerMutualFundTransaction]
GO


