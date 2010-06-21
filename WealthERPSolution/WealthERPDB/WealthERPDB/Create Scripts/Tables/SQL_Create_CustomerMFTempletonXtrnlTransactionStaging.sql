
GO

/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlTransactionStaging]    Script Date: 06/11/2009 16:00:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFTempletonXtrnlTransactionStaging](
	[CMFTXTS_Id] [int] NOT NULL,
	[A_AdviserId] [int] NULL,
	[CMFTXTS_COMP_CODE] [varchar](10) NULL,
	[CMFTXTS_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXTS_PRODUCT_C0] [varchar](30) NULL,
	[CMFTXTS_TRXN_NO] [varchar](30) NULL,
	[CMFTXTS_TRXN_NAME] [varchar](50) NULL,
	[CMFTXTS_TRXN_MODE] [varchar](10) NULL,
	[CMFTXTS_TRXN_STAT] [varchar](10) NULL,
	[CMFTXTS_ISC_CODE] [varchar](30) NULL,
	[CMFTXTS_ISC_TRXNO] [varchar](30) NULL,
	[CMFTXTS_TRXN_DATE] [datetime] NULL,
	[CMFTXTS_POSTDTDATE] [datetime] NULL,
	[CMFTXTS_PRICE] [numeric](18, 3) NULL,
	[CMFTXTS_UNITS] [numeric](15, 4) NULL,
	[CMFTXTS_AMOUNT] [numeric](18, 3) NULL,
	[CMFTXTS_CHECK_NO] [varchar](20) NULL,
	[CMFTXTS_DIVTYPE] [varchar](20) NULL,
	[CMFTXTS_BROK_DLR_1] [varchar](20) NULL,
	[CMFTXTS_AE_CODE] [varchar](30) NULL,
	[CMFTXTS_BROK_PERC] [numeric](6, 3) NULL,
	[CMFTXTS_BROK_COMM] [numeric](15, 3) NULL,
	[CMFTXTS_INVEST_ID] [varchar](30) NULL,
	[CMFTXTS_CREA_DATE] [datetime] NULL,
	[CMFTXTS_CREA_TIME] [datetime] NULL,
	[CMFTXTS_TRXN_SUB] [varchar](30) NULL,
	[CMFTXTS_APPL_NO] [varchar](30) NULL,
	[CMFTXTS_PROD_CODE] [varchar](50) NULL,
	[CMFTXTS_TRXN_ID] [varchar](50) NULL,
	[CMFTXTS_TRAN_TYPE] [varchar](30) NULL,
	[CMFTXTS_TR_TYPE] [varchar](30) NULL,
	[CMFTXTS_STT] [numeric](15, 3) NULL,
	[CMFTXTS_DIRECT_FL2] [varchar](20) NULL,
	[CMFTXTS_IsRejected] [tinyint] NULL,
	[CMFTXTS_IsFolioNew] [tinyint] NULL,
	[CMFTXTS_CreatedBy] [int] NULL,
	[CMFTXTS_CreatedOn] [datetime] NULL,
	[CMFTXTS_ModifiedBy] [int] NULL,
	[CMFTXTS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CMFTXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


