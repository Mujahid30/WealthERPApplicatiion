USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[WerpMutualFundTransactionType]    Script Date: 06/12/2009 18:25:19 ******/
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


