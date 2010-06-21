USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[WerpKarvyDataTransalatorMapping]    Script Date: 06/12/2009 18:24:42 ******/
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


