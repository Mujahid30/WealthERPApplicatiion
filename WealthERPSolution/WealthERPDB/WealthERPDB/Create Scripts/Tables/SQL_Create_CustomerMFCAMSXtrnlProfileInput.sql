
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlProfileInput]    Script Date: 06/11/2009 13:22:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFCAMSXtrnlProfileInput](
	[CMGCXPI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CMGCXPI_FOLIOCHK] [varchar](150) NULL,
	[CMGCXPI_INV_NAME] [varchar](150) NULL,
	[CMGCXPI_ADDRESS1] [varchar](150) NULL,
	[CMGCXPI_ADDRESS2] [varchar](150) NULL,
	[CMGCXPI_ADDRESS3] [varchar](150) NULL,
	[CMGCXPI_CITY] [varchar](150) NULL,
	[CMGCXPI_PINCODE] [varchar](150) NULL,
	[CMGCXPI_PRODUCT] [varchar](150) NULL,
	[CMGCXPI_SCH_NAME] [varchar](150) NULL,
	[CMGCXPI_REP_DATE] [varchar](150) NULL,
	[CMGCXPI_CLOS_BAL] [varchar](150) NULL,
	[CMGCXPI_RUPEE_BAL] [varchar](150) NULL,
	[CMGCXPI_SUBBROK] [varchar](150) NULL,
	[CMGCXPI_REINV_FLAG] [varchar](150) NULL,
	[CMGCXPI_JOINT_NAME1] [varchar](150) NULL,
	[CMGCXPI_JOINT_NAME2] [varchar](150) NULL,
	[CMGCXPI_PHONE_OFF] [varchar](150) NULL,
	[CMGCXPI_PHONE_RES] [varchar](150) NULL,
	[CMGCXPI_EMAIL] [varchar](150) NULL,
	[CMGCXPI_HOLDING_NA] [varchar](150) NULL,
	[CMGCXPI_UIN_NO] [varchar](150) NULL,
	[CMGCXPI_BROKER_COD] [varchar](150) NULL,
	[CMGCXPI_PAN_NO] [varchar](150) NULL,
	[CMGCXPI_JOINT1_PAN] [varchar](150) NULL,
	[CMGCXPI_JOINT2_PAN] [varchar](150) NULL,
	[CMGCXPI_GUARD_PAN] [varchar](150) NULL,
	[CMGCXPI_TAX_STATUS] [varchar](150) NULL,
	[CMGCXPI_INV_IIN] [varchar](150) NULL,
	[CMGCXPI_ALTFOLIO] [varchar](150) NULL,
	[CMGCXPI_CreatedBy] [int] NULL,
	[CMGCXPI_CreatedOn] [datetime] NULL,
	[CMGCXPI_ModifiedBy] [int] NULL,
	[CMGCXPI_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CMGCXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


