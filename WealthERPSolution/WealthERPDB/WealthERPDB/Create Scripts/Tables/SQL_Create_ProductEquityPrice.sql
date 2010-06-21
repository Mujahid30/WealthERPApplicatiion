USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductEquityPrice]    Script Date: 06/12/2009 18:19:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductEquityPrice](
	[PEP_EquityPriceId] [int] IDENTITY(1,1) NOT NULL,
	[PEM_ScripCode] [int] NOT NULL,
	[PEM_Exchange] [char](5) NULL,
	[PEP_Series] [varchar](5) NULL,
	[PEP_OpenPrice] [numeric](18, 4) NULL,
	[PEP_HighPrice] [numeric](18, 4) NULL,
	[PEP_LowPrice] [numeric](18, 4) NULL,
	[PEP_ClosePrice] [numeric](18, 4) NULL,
	[PEP_LastPrice] [numeric](18, 4) NULL,
	[PEP_PreviousClose] [numeric](18, 4) NULL,
	[PEP_TotalTradeQuantity] [numeric](18, 4) NULL,
	[PEP_TotalTradeValue] [numeric](18, 4) NULL,
	[PEP_NoOfTrades] [numeric](18, 4) NULL,
	[PEP_Date] [datetime] NULL,
	[PEP_CreatedBy] [int] NULL,
	[PEP_CreatedOn] [datetime] NULL,
	[PEP_ModifiedOn] [datetime] NULL,
	[PEP_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProductEquityPrice] PRIMARY KEY CLUSTERED 
(
	[PEP_EquityPriceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductEquityPrice]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityPrice_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO

ALTER TABLE [dbo].[ProductEquityPrice] CHECK CONSTRAINT [FK_ProductEquityPrice_ProductEquityMaster]
GO


