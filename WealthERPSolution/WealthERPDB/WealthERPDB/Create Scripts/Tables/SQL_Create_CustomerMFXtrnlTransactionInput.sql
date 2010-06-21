
GO

/****** Object:  Table [dbo].[CustomerMFXtrnlTransactionInput]    Script Date: 06/11/2009 16:01:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFXtrnlTransactionInput](
	[CMFXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFXTI_SchemeName] [varchar](100) NULL,
	[CMFXTI_FolioNum] [varchar](50) NULL,
	[CMFXTI_TransactionType] [varchar](50) NULL,
	[CMFXTI_TransactionDate] [datetime] NULL,
	[CMFXTI_BuySell] [varchar](50) NULL,
	[CMFXTI_DividendRate] [varchar](50) NULL,
	[CMFXTI_NAV] [varchar](50) NULL,
	[CMFXTI_Price] [varchar](50) NULL,
	[CMFXTI_Amount] [varchar](50) NULL,
	[CMFXTI_Units] [varchar](50) NULL,
	[CMFXTI_STT] [varchar](50) NULL,
	[CMFXTI_CreatedBy] [int] NULL,
	[CMFXTI_CreatedOn] [datetime] NULL,
	[CMFXTI_ModifiedBy] [int] NULL,
	[CMFXTI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CMFXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


