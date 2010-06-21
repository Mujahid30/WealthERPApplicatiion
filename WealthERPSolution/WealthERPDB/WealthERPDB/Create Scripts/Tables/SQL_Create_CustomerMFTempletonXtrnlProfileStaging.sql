
GO

/****** Object:  Table [dbo].[CustomerMFTempletonXtrnlProfileStaging]    Script Date: 06/11/2009 15:54:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFTempletonXtrnlProfileStaging](
	[CMFTXPS_Id] [int] NOT NULL,
	[CMFTXPS_COMP_CODE] [varchar](20) NULL,
	[CMFTXPS_FOLIO_NO] [numeric](15, 0) NULL,
	[CMFTXPS_INV_NAME] [varchar](60) NULL,
	[CMFTXPS_JOINT_NAM1] [varchar](60) NULL,
	[CMFTXPS_JOINT_NAM2] [varchar](60) NULL,
	[CMFTXPS_ADDRESS1] [varchar](30) NULL,
	[CMFTXPS_ADDRESS2] [varchar](30) NULL,
	[CMFTXPS_ADDRESS3] [varchar](30) NULL,
	[CMFTXPS_CITY] [varchar](25) NULL,
	[CMFTXPS_PINCODE] [numeric](6, 0) NULL,
	[CMFTXPS_STATE] [varchar](25) NULL,
	[CMFTXPS_COUNTRY] [varchar](25) NULL,
	[CMFTXPS_TPIN] [varchar](20) NULL,
	[CMFTXPS_D_BIRTH] [datetime] NULL,
	[CMFTXPS_F_NAME] [varchar](30) NULL,
	[CMFTXPS_M_NAME] [varchar](30) NULL,
	[CMFTXPS_PHONE_RES] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_RES1] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_RES2] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_OFF] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_OFF1] [numeric](16, 0) NULL,
	[CMFTXPS_PHONE_OFF2] [numeric](16, 0) NULL,
	[CMFTXPS_FAX_RES] [numeric](16, 0) NULL,
	[CMFTXPS_FAX_OFF] [numeric](16, 0) NULL,
	[CMFTXPS_TAX_STATUS] [varchar](30) NULL,
	[CMFTXPS_OCCU_CODE] [varchar](10) NULL,
	[CMFTXPS_EMAIL] [varchar](max) NULL,
	[CMFTXPS_ACCNT_NO] [numeric](15, 0) NULL,
	[CMFTXPS_BANK_NAME] [varchar](50) NULL,
	[CMFTXPS_AC_TYPE] [varchar](20) NULL,
	[CMFTXPS_BRANCH] [varchar](30) NULL,
	[CMFTXPS_B_ADDRESS1] [varchar](30) NULL,
	[CMFTXPS_B_ADDRESS2] [varchar](30) NULL,
	[CMFTXPS_B_ADDRESS3] [varchar](30) NULL,
	[CMFTXPS_B_CITY] [varchar](25) NULL,
	[CMFTXPS_B_PINCODE] [numeric](6, 0) NULL,
	[CMFTXPS_B_STATE] [varchar](25) NULL,
	[CMFTXPS_B_COUNTRY] [varchar](25) NULL,
	[CMFTXPS_INVEST_ID] [numeric](15, 0) NULL,
	[CMFTXPS_BROK_CODE] [varchar](30) NULL,
	[CMFTXPS_PANNO1] [numeric](10, 0) NULL,
	[CMFTXPS_PANNO2] [numeric](10, 0) NULL,
	[CMFTXPS_PANNO3] [numeric](10, 0) NULL,
	[CMFTXPS_PAN_STATU0] [varchar](10) NULL,
	[CMFTXPS_PAN_STATU1] [varchar](10) NULL,
	[CMFTXPS_PAN_STATU2] [varchar](10) NULL,
	[CMFTXPS_MAPIN1] [varchar](50) NULL,
	[CMFTXPS_MAPIN2] [varchar](50) NULL,
	[CMFTXPS_MAPIN3] [varchar](50) NULL,
	[CMFTXPS_CREA_DATE] [datetime] NULL,
	[CMFTXPS_CREA_TIME] [datetime] NULL,
	[CMFTXPS_CUSTOMER_3] [numeric](15, 0) NULL,
	[C_CustomerId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CMFTXPS_IsFolioNew] [tinyint] NULL,
	[CMFTXPS_IsCustomerNew] [tinyint] NULL,
	[CMFTXPS_IsRejected] [tinyint] NULL,
	[CMFTXPS_IsBankAccountNew] [tinyint] NULL,
	[CMFTXPS_CreatedBy] [int] NULL,
	[CMFTXPS_CreatedOn] [datetime] NULL,
	[CMFTXPS_ModifiedOn] [datetime] NULL,
	[CMFTXPS_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CMFTXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


