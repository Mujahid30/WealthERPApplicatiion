
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlTransactionStaging]    Script Date: 06/11/2009 15:38:16 ******/
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
	[CMCXTS_BrokerCode] [varchar](25) NULL,
	[CMCXTS_SubBrokerCode] [varchar](25) NULL,
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
	[CMCXTS_OldFolio] [varchar](16) NULL,
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

ALTER TABLE [dbo].[CustomerMFCAMSXtrnlTransactionStaging] ADD  CONSTRAINT [DF_CustomerInvestmentMFCAMSXtrnlStaging_CCMT_StatusCode]  DEFAULT ((0)) FOR [CMCXTS_StatusCode]
GO


