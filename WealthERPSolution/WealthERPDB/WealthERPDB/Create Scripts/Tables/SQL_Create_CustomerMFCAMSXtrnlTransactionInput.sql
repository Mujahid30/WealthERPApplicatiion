
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlTransactionInput]    Script Date: 06/11/2009 15:37:50 ******/
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


