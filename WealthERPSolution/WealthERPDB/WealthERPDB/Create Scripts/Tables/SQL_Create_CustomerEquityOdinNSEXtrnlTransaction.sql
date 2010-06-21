
GO

/****** Object:  Table [dbo].[CustomerEquityOdinNSEXtrnlTransaction]    Script Date: 06/11/2009 12:05:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransaction](
	[CEONXT_Id] [int] IDENTITY(1000,1) NOT NULL,
	[CET_EqTransId] [int] NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEONXT_TradeNum] [numeric](15, 0) NULL,
	[CEONXT_AssetCode] [numeric](3, 0) NULL,
	[CEONXT_ScripCode] [varchar](50) NULL,
	[CEONXT_AssetIdentifier] [varchar](10) NULL,
	[CEONXT_ScripName] [varchar](100) NULL,
	[CEONXT_Field6] [numeric](5, 0) NULL,
	[CEONXT_Field7] [numeric](5, 0) NULL,
	[CEONXT_Field8] [numeric](5, 0) NULL,
	[CEONXT_Field9] [numeric](10, 0) NULL,
	[CEONXT_Field10] [numeric](10, 0) NULL,
	[CEONXT_BuySell] [numeric](1, 0) NULL,
	[CEONXT_Quantity] [numeric](15, 3) NULL,
	[CEONXT_Rate] [numeric](18, 3) NULL,
	[CEONXT_Field14] [numeric](5, 0) NULL,
	[CEONXT_TradeAccountNum] [varchar](20) NULL,
	[CEONXT_TerminalId] [numeric](10, 0) NULL,
	[CEONXT_Field17] [varchar](30) NULL,
	[CEONXT_Field18] [varchar](30) NULL,
	[CEONXT_Field19] [varchar](30) NULL,
	[CEONXT_TradeDate] [datetime] NULL,
	[CEONXT_Field21] [datetime] NULL,
	[CEONXT_Field22] [varchar](30) NULL,
	[CEONXT_Field23] [varchar](30) NULL,
	[CEONXT_Field24] [varchar](30) NULL,
	[CEONXT_CreatedBy] [int] NULL,
	[CEONXT_CreatedOn] [datetime] NULL,
	[CEONXT_ModifiedOn] [datetime] NULL,
	[CEONXT_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinXtrnlTransaction] PRIMARY KEY CLUSTERED 
(
	[CEONXT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityOdinNSEXtrnlTransaction_CustomerEquityTransaction] FOREIGN KEY([CET_EqTransId])
REFERENCES [dbo].[CustomerEquityTransaction] ([CET_EqTransId])
GO

ALTER TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransaction] CHECK CONSTRAINT [FK_CustomerEquityOdinNSEXtrnlTransaction_CustomerEquityTransaction]
GO


