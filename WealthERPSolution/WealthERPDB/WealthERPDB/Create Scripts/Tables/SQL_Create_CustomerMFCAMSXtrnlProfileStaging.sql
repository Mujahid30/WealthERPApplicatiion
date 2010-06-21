
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlProfileStaging]    Script Date: 06/11/2009 15:35:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFCAMSXtrnlProfileStaging](
	[CMGCXPS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[PA_AMCId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[ADUL_ProcessId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[WRR_RejectReasonCode] [int] NULL,
	[CMGCXPS_FOLIOCHK] [varchar](20) NULL,
	[CMGCXPS_INV_NAME] [varchar](75) NULL,
	[CMGCXPS_ADDRESS1] [varchar](75) NULL,
	[CMGCXPS_ADDRESS2] [varchar](75) NULL,
	[CMGCXPS_ADDRESS3] [varchar](75) NULL,
	[CMGCXPS_CITY] [varchar](25) NULL,
	[CMGCXPS_PINCODE] [numeric](10, 0) NULL,
	[CMGCXPS_PRODUCT] [varchar](30) NULL,
	[CMGCXPS_SCH_NAME] [varchar](150) NULL,
	[CMGCXPS_REP_DATE] [datetime] NULL,
	[CMGCXPS_CLOS_BAL] [numeric](18, 4) NULL,
	[CMGCXPS_RUPEE_BAL] [numeric](18, 4) NULL,
	[CMGCXPS_SUBBROK] [varchar](50) NULL,
	[CMGCXPS_REINV_FLAG] [varchar](30) NULL,
	[CMGCXPS_JOINT_NAME1] [varchar](75) NULL,
	[CMGCXPS_JOINT_NAME2] [varchar](75) NULL,
	[CMGCXPS_PHONE_OFF] [numeric](25, 0) NULL,
	[CMGCXPS_PHONE_RES] [numeric](25, 0) NULL,
	[CMGCXPS_EMAIL] [varchar](75) NULL,
	[CMGCXPS_HOLDING_NA] [varchar](20) NULL,
	[CMGCXPS_UIN_NO] [varchar](50) NULL,
	[CMGCXPS_BROKER_COD] [varchar](50) NULL,
	[CMGCXPS_PAN_NO] [varchar](20) NULL,
	[CMGCXPS_JOINT1_PAN] [varchar](20) NULL,
	[CMGCXPS_JOINT2_PAN] [varchar](20) NULL,
	[CMGCXPS_GUARD_PAN] [varchar](20) NULL,
	[CMGCXPS_TAX_STATUS] [varchar](50) NULL,
	[CMGCXPS_INV_IIN] [varchar](20) NULL,
	[CMGCXPS_ALTFOLIO] [varchar](50) NULL,
	[CMGCXPS_IsRejected] [tinyint] NULL,
	[CMGCXPS_IsFolioNew] [tinyint] NULL,
	[CMGCXPS_IsCustomerNew] [tinyint] NULL,
	[CMGCXPS_IsAMCNew] [tinyint] NULL,
	[CMGCXPS_RejectReason] [varchar](50) NULL,
	[CMGCXPS_CreatedBy] [int] NULL,
	[CMGCXPS_CreatedOn] [datetime] NULL,
	[CMGCXPS_ModifiedBy] [int] NULL,
	[CMGCXPS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CMGCXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


