
GO

/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlTransaction]    Script Date: 06/11/2009 15:54:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFTempletonXtrnlTransaction](
	[CMFTXT_Id] [int] NOT NULL,
	[CIMFT_CustMFTransId] [int] NULL,
	[CMFTXT_COMP_CODE] [varchar](10) NULL,
	[CMFTXT_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXT_PRODUCT_C0] [varchar](30) NULL,
	[CMFTXT_TRXN_NO] [varchar](30) NULL,
	[CMFTXT_TRXN_NAME] [varchar](50) NULL,
	[CMFTXT_TRXN_MODE] [varchar](10) NULL,
	[CMFTXT_TRXN_STAT] [varchar](10) NULL,
	[CMFTXT_ISC_CODE] [varchar](30) NULL,
	[CMFTXT_ISC_TRXNO] [varchar](30) NULL,
	[CMFTXT_TRXN_DATE] [datetime] NULL,
	[CMFTXT_POSTDTDATE] [datetime] NULL,
	[CMFTXT_PRICE] [numeric](18, 3) NULL,
	[CMFTXT_UNITS] [numeric](15, 4) NULL,
	[CMFTXT_AMOUNT] [numeric](18, 3) NULL,
	[CMFTXT_CHECK_NO] [varchar](20) NULL,
	[CMFTXT_DIVTYPE] [varchar](20) NULL,
	[CMFTXT_BROK_DLR_1] [varchar](20) NULL,
	[CMFTXT_AE_CODE] [varchar](30) NULL,
	[CMFTXT_BROK_PERC] [numeric](6, 3) NULL,
	[CMFTXT_BROK_COMM] [numeric](15, 3) NULL,
	[CMFTXT_INVEST_ID] [varchar](30) NULL,
	[CMFTXT_CREA_DATE] [datetime] NULL,
	[CMFTXT_CREA_TIME] [datetime] NULL,
	[CMFTXT_TRXN_SUB] [varchar](30) NULL,
	[CMFTXT_APPL_NO] [varchar](30) NULL,
	[CMFTXT_PROD_CODE] [varchar](50) NULL,
	[CMFTXT_TRXN_ID] [varchar](50) NULL,
	[CMFTXT_TRAN_TYPE] [varchar](30) NULL,
	[CMFTXT_TR_TYPE] [varchar](30) NULL,
	[CMFTXT_STT] [numeric](15, 3) NULL,
	[CMFTXT_DIRECT_FL2] [varchar](20) NULL,
	[CMFTXT_CreatedBy] [int] NULL,
	[CMFTXT_CreatedOn] [datetime] NULL,
	[CMFTXT_ModifiedBy] [int] NULL,
	[CMFTXT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlTransaction] PRIMARY KEY CLUSTERED 
(
	[CMFTXT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


