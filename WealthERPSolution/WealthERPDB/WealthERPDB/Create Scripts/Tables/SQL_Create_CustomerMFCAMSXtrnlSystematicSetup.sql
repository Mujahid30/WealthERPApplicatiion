
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlSystematicSetup]    Script Date: 06/11/2009 15:35:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetup](
	[CMFCXSS_SystematicId] [int] IDENTITY(1000,1) NOT NULL,
	[CMFSS_SystematicSetupId] [int] NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFCXSS_PRODUCT] [varchar](50) NULL,
	[CMFCXSS_SCHEME] [varchar](100) NULL,
	[CMFCXSS_FOLIONO] [varchar](50) NULL,
	[CMFCXSS_INVNAME] [varchar](60) NULL,
	[CMFCXSS_AUTOTRXN] [char](2) NULL,
	[CMFCXSS_AUTOTRXNNum] [numeric](10, 0) NULL,
	[CMFCXSS_AUTOAMOUN] [numeric](18, 4) NULL,
	[CMFCXSS_FROMDATE] [datetime] NULL,
	[CMFCXSS_TODATE] [datetime] NULL,
	[CMFCXSS_CEASEDATE] [datetime] NULL,
	[CMFCXSS_PERIODICIT] [varchar](5) NULL,
	[CMFCXSS_PERIODDAY] [numeric](2, 0) NULL,
	[CMFCXSS_INVIIN] [numeric](3, 0) NULL,
	[CMFCXSS_PAYMENTMO] [varchar](5) NULL,
	[CMFCXSS_TARGETSCH] [varchar](20) NULL,
	[CMFCXSS_REGDATE] [datetime] NULL,
	[CMFCXSS_SUBBROKER] [varchar](50) NULL,
	[CMFCXSS_CreatedBy] [int] NULL,
	[CMFCXSS_CreatedOn] [datetime] NULL,
	[CMFCXSS_ModifiedBy] [int] NULL,
	[CMFCXSS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlSystematicSetup] PRIMARY KEY CLUSTERED 
(
	[CMFCXSS_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFCAMSXtrnlSystematicSetup_CustomerMutualFundSystematicSetup] FOREIGN KEY([CMFSS_SystematicSetupId])
REFERENCES [dbo].[CustomerMutualFundSystematicSetup] ([CMFSS_SystematicSetupId])
GO

ALTER TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetup] CHECK CONSTRAINT [FK_CustomerMFCAMSXtrnlSystematicSetup_CustomerMutualFundSystematicSetup]
GO


