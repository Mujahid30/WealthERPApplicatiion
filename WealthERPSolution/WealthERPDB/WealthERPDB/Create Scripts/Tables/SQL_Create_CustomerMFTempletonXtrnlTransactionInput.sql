
GO

/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlTransactionInput]    Script Date: 06/11/2009 15:55:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFTempletonXtrnlTransactionInput](
	[CMFTXTI_Id] [int] NULL,
	[CMFTXTI_COMP_CODE] [varchar](10) NULL,
	[CMFTXTI_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXTI_PRODUCT_C0] [varchar](30) NULL,
	[CMFTXTI_TRXN_NO] [varchar](30) NULL,
	[CMFTXTI_TRXN_NAME] [varchar](50) NULL,
	[CMFTXTI_TRXN_MODE] [varchar](10) NULL,
	[CMFTXTI_TRXN_STAT] [varchar](10) NULL,
	[CMFTXTI_ISC_CODE] [varchar](30) NULL,
	[CMFTXTI_ISC_TRXNO] [varchar](30) NULL,
	[CMFTXTI_TRXN_DATE] [datetime] NULL,
	[CMFTXTI_POSTDTDATE] [datetime] NULL,
	[CMFTXTI_PRICE] [numeric](18, 3) NULL,
	[CMFTXTI_UNITS] [numeric](15, 4) NULL,
	[CMFTXTI_AMOUNT] [numeric](18, 3) NULL,
	[CMFTXTI_CHECK_NO] [varchar](20) NULL,
	[CMFTXTI_DIVTYPE] [varchar](20) NULL,
	[CMFTXTI_BROK_DLR_1] [varchar](20) NULL,
	[CMFTXTI_AE_CODE] [varchar](30) NULL,
	[CMFTXTI_BROK_PERC] [numeric](6, 3) NULL,
	[CMFTXTI_BROK_COMM] [numeric](15, 3) NULL,
	[CMFTXTI_INVEST_ID] [varchar](30) NULL,
	[CMFTXTI_CREA_DATE] [datetime] NULL,
	[CMFTXTI_CREA_TIME] [datetime] NULL,
	[CMFTXTI_TRXN_SUB] [varchar](30) NULL,
	[CMFTXTI_APPL_NO] [varchar](30) NULL,
	[CMFTXTI_PROD_CODE] [varchar](50) NULL,
	[CMFTXTI_TRXN_ID] [varchar](50) NULL,
	[CMFTXTI_TRAN_TYPE] [varchar](30) NULL,
	[CMFTXTI_TR_TYPE] [varchar](30) NULL,
	[CMFTXTI_STT] [numeric](15, 3) NULL,
	[CMFTXTI_DIRECT_FL2] [varchar](20) NULL,
	[CMFTXTI_CreatedBy] [int] NULL,
	[CMFTXTI_CreatedOn] [datetime] NULL,
	[CMFTXTI_ModifiedBy] [int] NULL,
	[CMFTXTI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


