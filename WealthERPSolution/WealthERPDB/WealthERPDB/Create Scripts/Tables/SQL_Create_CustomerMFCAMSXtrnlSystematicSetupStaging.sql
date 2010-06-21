
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlSystematicSetupStaging]    Script Date: 06/11/2009 15:36:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetupStaging](
	[CMFCXSSS_SystematicId] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFCXSSS_PRODUCT] [varchar](50) NULL,
	[CMFCXSSS_SCHEME] [varchar](100) NULL,
	[CMFCXSSS_FOLIONO] [varchar](50) NULL,
	[CMFCXSSS_INVNAME] [varchar](60) NULL,
	[CMFCXSSS_AUTOTRXN] [char](2) NULL,
	[CMFCXSSS_AUTOTRXNNum] [numeric](10, 0) NULL,
	[CMFCXSSS_AUTOAMOUN] [numeric](18, 4) NULL,
	[CMFCXSSS_FROMDATE] [datetime] NULL,
	[CMFCXSSS_TODATE] [datetime] NULL,
	[CMFCXSSS_CEASEDATE] [datetime] NULL,
	[CMFCXSSS_PERIODICIT] [varchar](5) NULL,
	[CMFCXSSS_PERIODDAY] [numeric](2, 0) NULL,
	[CMFCXSSS_INVIIN] [numeric](3, 0) NULL,
	[CMFCXSSS_PAYMENTMO] [varchar](5) NULL,
	[CMFCXSSS_TARGETSCH] [varchar](20) NULL,
	[CMFCXSSS_REGDATE] [datetime] NULL,
	[CMFCXSSS_SUBBROKER] [varchar](50) NULL,
	[A_AdviserId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CMFCXSSS_IsRejected] [tinyint] NULL,
	[CMFCXSSS_RejectedRemark] [varchar](50) NULL,
	[CMFCXSSS_CreatedBy] [int] NULL,
	[CMFCXSSS_CreatedOn] [datetime] NULL,
	[CMFCXSSS_ModifiedBy] [int] NULL,
	[CMFCXSS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlSystematicSetupStaging] PRIMARY KEY CLUSTERED 
(
	[CMFCXSSS_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


