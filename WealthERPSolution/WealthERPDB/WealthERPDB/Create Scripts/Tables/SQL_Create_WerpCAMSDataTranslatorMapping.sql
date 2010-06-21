USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[WerpCAMSDataTranslatorMapping]    Script Date: 06/12/2009 18:23:17 ******/
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


