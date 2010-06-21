
GO

/****** Object:  Table [dbo].[CustomerMFXtrnlTransactionStaging]    Script Date: 06/11/2009 16:02:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFXtrnlTransactionStaging](
	[CMFXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFXTS_SchemeName] [varchar](100) NULL,
	[CMFXTS_FolioNum] [varchar](50) NULL,
	[CMFXTS_TransactionType] [char](3) NULL,
	[CMFXTS_TransactionDate] [datetime] NULL,
	[CMFXTS_BuySell] [char](1) NULL,
	[CMFXTS_DividendRate] [numeric](10, 5) NULL,
	[CMFXTS_NAV] [numeric](18, 5) NULL,
	[CMFXTS_Price] [numeric](18, 5) NULL,
	[CMFXTS_Amount] [numeric](18, 5) NULL,
	[CMFXTS_Units] [numeric](18, 5) NULL,
	[CMFXTS_STT] [numeric](10, 5) NULL,
	[A_AdviserId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CMFXTS_IsRejected] [tinyint] NULL,
	[CMFXTS_RejectedRemark] [varchar](100) NULL,
	[CMFXTS_CreatedBy] [int] NULL,
	[CMFXTS_CreatedOn] [datetime] NULL,
	[CMFXTS_ModifiedBy] [int] NULL,
	[CMFXTS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CMFXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


