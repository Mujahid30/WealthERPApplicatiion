
GO

/****** Object:  Table [dbo].[CustomerEquityOdinBSEXtrnlTransactionInput]    Script Date: 06/11/2009 12:04:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransactionInput](
	[CEOBXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEOBXTI_ScripCode] [varchar](50) NULL,
	[CEOBXTI_ScripName] [varchar](100) NULL,
	[CEOBXTI_TradeNumber] [varchar](50) NULL,
	[CEOBXTI_Rate] [varchar](50) NULL,
	[CEOBXTI_Quantity] [varchar](50) NULL,
	[CEOBXTI_Field6] [varchar](50) NULL,
	[CEOBXTI_Field7] [varchar](50) NULL,
	[CEOBXTI_TradeTime] [varchar](50) NULL,
	[CEOBXTI_TradeDate] [varchar](50) NULL,
	[CEOBXTI_TradeAccountNumber] [varchar](50) NULL,
	[CEOBXTI_BuySell] [varchar](50) NULL,
	[CEOBXTI_Field12] [varchar](50) NULL,
	[CEOBXTI_OrderNumber] [varchar](50) NULL,
	[CEOBXTI_Field14] [varchar](50) NULL,
	[CEOBXTI_AccountStatus] [varchar](50) NULL,
	[CEOBXTI_CreatedBy] [int] NULL,
	[CEOBXTI_CreatedOn] [datetime] NULL,
	[CEOBXTI_ModifiedBy] [int] NULL,
	[CEOBXTI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinBSEXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CEOBXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


