
GO

/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlProfile]    Script Date: 06/11/2009 15:53:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFTempletonXtrnlProfile](
	[C_CustomerId] [int] NULL,
	[CMFTXP_Id] [int] NOT NULL,
	[CMFTXP_COMP_CODE] [varchar](20) NULL,
	[CMFTXP_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXP_INV_NAME] [varchar](60) NULL,
	[CMFTXP_JOINT_NAM1] [varchar](60) NULL,
	[CMFTXP_JOINT_NAM2] [varchar](60) NULL,
	[CMFTXP_ADDRESS1] [varchar](30) NULL,
	[CMFTXP_ADDRESS2] [varchar](30) NULL,
	[CMFTXP_ADDRESS3] [varchar](30) NULL,
	[CMFTXP_CITY] [varchar](25) NULL,
	[CMFTXP_PINCODE] [numeric](6, 0) NULL,
	[CMFTXP_STATE] [varchar](25) NULL,
	[CMFTXP_COUNTRY] [varchar](25) NULL,
	[CMFTXP_TPIN] [varchar](20) NULL,
	[CMFTXP_D_BIRTH] [datetime] NULL,
	[CMFTXP_F_NAME] [varchar](30) NULL,
	[CMFTXP_M_NAME] [varchar](30) NULL,
	[CMFTXP_PHONE_RES] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_RES1] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_RES2] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_OFF] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_OFF1] [numeric](16, 0) NULL,
	[CMFTXP_PHONE_OFF2] [numeric](16, 0) NULL,
	[CMFTXP_FAX_RES] [numeric](16, 0) NULL,
	[CMFTXP_FAX_OFF] [numeric](16, 0) NULL,
	[CMFTXP_TAX_STATUS] [varchar](30) NULL,
	[CMFTXP_OCCU_CODE] [varchar](10) NULL,
	[CMFTXP_EMAIL] [varchar](max) NULL,
	[CMFTXP_ACCNT_NO] [numeric](15, 0) NULL,
	[CMFTXP_BANK_NAME] [varchar](50) NULL,
	[CMFTXP_AC_TYPE] [varchar](20) NULL,
	[CMFTXP_BRANCH] [varchar](30) NULL,
	[CMFTXP_B_ADDRESS1] [varchar](30) NULL,
	[CMFTXP_B_ADDRESS2] [varchar](30) NULL,
	[CMFTXP_B_ADDRESS3] [varchar](30) NULL,
	[CMFTXP_B_CITY] [varchar](25) NULL,
	[CMFTXP_B_PINCODE] [numeric](6, 0) NULL,
	[CMFTXP_B_STATE] [varchar](25) NULL,
	[CMFTXP_B_COUNTRY] [varchar](25) NULL,
	[CMFTXP_INVEST_ID] [numeric](15, 0) NULL,
	[CMFTXP_BROK_CODE] [varchar](30) NULL,
	[CMFTXP_PANNO1] [numeric](10, 0) NULL,
	[CMFTXP_PANNO2] [numeric](10, 0) NULL,
	[CMFTXP_PANNO3] [numeric](10, 0) NULL,
	[CMFTXP_PAN_STATU0] [varchar](10) NULL,
	[CMFTXP_PAN_STATU1] [varchar](10) NULL,
	[CMFTXP_PAN_STATU2] [varchar](10) NULL,
	[CMFTXP_MAPIN1] [varchar](50) NULL,
	[CMFTXP_MAPIN2] [varchar](50) NULL,
	[CMFTXP_MAPIN3] [varchar](50) NULL,
	[CMFTXP_CREA_DATE] [datetime] NULL,
	[CMFTXP_CREA_TIME] [datetime] NULL,
	[CMFTXP_CUSTOMER_3] [numeric](15, 0) NULL,
	[CMFTXP_CreatedBy] [int] NULL,
	[CMFTXP_CreatedOn] [datetime] NULL,
	[CMFTXP_ModifiedOn] [datetime] NULL,
	[CMFTXP_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlProfile] PRIMARY KEY CLUSTERED 
(
	[CMFTXP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


