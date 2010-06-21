
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlProfile]    Script Date: 06/11/2009 13:08:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFCAMSXtrnlProfile](
	[CMGCXP_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CMGCXP_FOLIOCHK] [varchar](50) NULL,
	[CMGCXP_INV_NAME] [varchar](60) NULL,
	[CMGCXP_ADDRESS1] [varchar](50) NULL,
	[CMGCXP_ADDRESS2] [varchar](50) NULL,
	[CMGCXP_ADDRESS3] [varchar](50) NULL,
	[CMGCXP_CITY] [varchar](30) NULL,
	[CMGCXP_PINCODE] [numeric](6, 0) NULL,
	[CMGCXP_PRODUCT] [varchar](30) NULL,
	[CMGCXP_SCH_NAME] [varchar](100) NULL,
	[CMGCXP_REP_DATE] [datetime] NULL,
	[CMGCXP_CLOS_BAL] [numeric](18, 3) NULL,
	[CMGCXP_RUPEE_BAL] [numeric](18, 0) NULL,
	[CMGCXP_SUBBROK] [varchar](50) NULL,
	[CMGCXP_REINV_FLAG] [varchar](30) NULL,
	[CMGCXP_JOINT_NAME1] [varchar](60) NULL,
	[CMGCXP_JOINT_NAME2] [varchar](60) NULL,
	[CMGCXP_PHONE_OFF] [numeric](15, 0) NULL,
	[CMGCXP_PHONE_RES] [numeric](15, 0) NULL,
	[CMGCXP_EMAIL] [varchar](max) NULL,
	[CMGCXP_HOLDING_NA] [varchar](10) NULL,
	[CMGCXP_UIN_NO] [varchar](50) NULL,
	[CMGCXP_BROKER_COD] [varchar](30) NULL,
	[CMGCXP_PAN_NO] [varchar](20) NULL,
	[CMGCXP_JOINT1_PAN] [varchar](20) NULL,
	[CMGCXP_JOINT2_PAN] [varchar](20) NULL,
	[CMGCXP_GUARD_PAN] [varchar](20) NULL,
	[CMGCXP_TAX_STATUS] [varchar](30) NULL,
	[CMGCXP_INV_IIN] [varchar](20) NULL,
	[CMGCXP_ALTFOLIO] [varchar](50) NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlProfile] PRIMARY KEY CLUSTERED 
(
	[CMGCXP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerMFCAMSXtrnlProfile]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFCAMSXtrnlProfile_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerMFCAMSXtrnlProfile] CHECK CONSTRAINT [FK_CustomerMFCAMSXtrnlProfile_Customer]
GO


