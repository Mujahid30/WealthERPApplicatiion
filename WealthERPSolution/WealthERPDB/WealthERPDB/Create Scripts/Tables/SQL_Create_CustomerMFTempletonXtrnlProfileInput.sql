
GO

/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlProfileInput]    Script Date: 06/11/2009 15:53:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFTempletonXtrnlProfileInput](
	[CMFTXPI_Id] [int] NOT NULL,
	[CMFTXPI_COMP_CODE] [varchar](20) NULL,
	[CMFTXPI_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXPI_INV_NAME] [varchar](60) NULL,
	[CMFTXPI_JOINT_NAM1] [varchar](60) NULL,
	[CMFTXPI_JOINT_NAM2] [varchar](60) NULL,
	[CMFTXPI_ADDRESS1] [varchar](30) NULL,
	[CMFTXPI_ADDRESS2] [varchar](30) NULL,
	[CMFTXPI_ADDRESS3] [varchar](30) NULL,
	[CMFTXPI_CITY] [varchar](25) NULL,
	[CMFTXPI_PINCODE] [numeric](6, 0) NULL,
	[CMFTXPI_STATE] [varchar](25) NULL,
	[CMFTXPI_COUNTRY] [varchar](25) NULL,
	[CMFTXPI_TPIN] [varchar](20) NULL,
	[CMFTXPI_D_BIRTH] [datetime] NULL,
	[CMFTXPI_F_NAME] [varchar](30) NULL,
	[CMFTXPI_M_NAME] [varchar](30) NULL,
	[CMFTXPI_PHONE_RES] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_RES1] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_RES2] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_OFF] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_OFF1] [numeric](16, 0) NULL,
	[CMFTXPI_PHONE_OFF2] [numeric](16, 0) NULL,
	[CMFTXPI_FAX_RES] [numeric](16, 0) NULL,
	[CMFTXPI_FAX_OFF] [numeric](16, 0) NULL,
	[CMFTXPI_TAX_STATUS] [varchar](30) NULL,
	[CMFTXPI_OCCU_CODE] [varchar](10) NULL,
	[CMFTXPI_EMAIL] [varchar](max) NULL,
	[CMFTXPI_ACCNT_NO] [numeric](15, 0) NULL,
	[CMFTXPI_BANK_NAME] [varchar](50) NULL,
	[CMFTXPI_AC_TYPE] [varchar](20) NULL,
	[CMFTXPI_BRANCH] [varchar](30) NULL,
	[CMFTXPI_B_ADDRESS1] [varchar](30) NULL,
	[CMFTXPI_B_ADDRESS2] [varchar](30) NULL,
	[CMFTXPI_B_ADDRESS3] [varchar](30) NULL,
	[CMFTXPI_B_CITY] [varchar](25) NULL,
	[CMFTXPI_B_PINCODE] [numeric](6, 0) NULL,
	[CMFTXPI_B_STATE] [varchar](25) NULL,
	[CMFTXPI_B_COUNTRY] [varchar](25) NULL,
	[CMFTXPI_INVEST_ID] [numeric](15, 0) NULL,
	[CMFTXPI_BROK_CODE] [varchar](30) NULL,
	[CMFTXPI_PANNO1] [numeric](10, 0) NULL,
	[CMFTXPI_PANNO2] [numeric](10, 0) NULL,
	[CMFTXPI_PANNO3] [numeric](10, 0) NULL,
	[CMFTXPI_PAN_STATU0] [varchar](10) NULL,
	[CMFTXPI_PAN_STATU1] [varchar](10) NULL,
	[CMFTXPI_PAN_STATU2] [varchar](10) NULL,
	[CMFTXPI_MAPIN1] [varchar](50) NULL,
	[CMFTXPI_MAPIN2] [varchar](50) NULL,
	[CMFTXPI_MAPIN3] [varchar](50) NULL,
	[CMFTXPI_CREA_DATE] [datetime] NULL,
	[CMFTXPI_CREA_TIME] [datetime] NULL,
	[CMFTXPI_CUSTOMER_3] [numeric](15, 0) NULL,
	[CMFTXPI_CreatedBy] [int] NULL,
	[CMFTXPI_CreatedOn] [datetime] NULL,
	[CMFTXPI_ModifiedOn] [datetime] NULL,
	[CMFTXPI_ModifiedBy] [int] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CMFTXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


