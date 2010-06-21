
GO

/****** Object:  Table [dbo].[CustomerEquityOdinNSEXtrnlTransactionStaging]    Script Date: 06/11/2009 12:05:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransactionStaging](
	[CEONXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEONXTS_TradeNum] [numeric](15, 0) NULL,
	[CEONXTS_AssetCode] [numeric](3, 0) NULL,
	[CEONXTS_ScripCode] [varchar](50) NULL,
	[CEONXTS_AssetIdentifier] [varchar](10) NULL,
	[CEONXTS_ScripName] [varchar](100) NULL,
	[CEONXTS_Field6] [numeric](5, 0) NULL,
	[CEONXTS_Field7] [numeric](5, 0) NULL,
	[CEONXTS_Field8] [numeric](5, 0) NULL,
	[CEONXTS_Field9] [numeric](10, 0) NULL,
	[CEONXTS_Field10] [numeric](10, 0) NULL,
	[CEONXTS_BuySell] [numeric](1, 0) NULL,
	[CEONXTS_Quantity] [numeric](15, 3) NULL,
	[CEONXTS_Rate] [numeric](18, 3) NULL,
	[CEONXTS_Field14] [numeric](5, 0) NULL,
	[CEONXTS_TradeAccountNum] [varchar](20) NULL,
	[CEONXTS_TerminalId] [numeric](10, 0) NULL,
	[CEONXTS_Field17] [varchar](30) NULL,
	[CEONXTS_Field18] [varchar](30) NULL,
	[CEONXTS_Field19] [varchar](30) NULL,
	[CEONXTS_TradeDate] [datetime] NULL,
	[CEONXTS_Field21] [datetime] NULL,
	[CEONXTS_Field22] [varchar](30) NULL,
	[CEONXTS_Field23] [varchar](30) NULL,
	[CEONXTS_Field24] [varchar](30) NULL,
	[CEONXTS_CreatedBy] [int] NULL,
	[CEONXTS_CreatedOn] [datetime] NULL,
	[CEONXTS_ModifiedOn] [datetime] NULL,
	[CEONXTS_ModifiedBy] [int] NULL,
	[CEONXTS_IsRejected] [tinyint] NULL,
	[CEONXTS_RejectedRemark] [varchar](100) NULL,
	[CEONXTS_IsTradeAccountNew] [tinyint] NULL,
	[A_AdviserId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CETA_AccountId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CEONXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


