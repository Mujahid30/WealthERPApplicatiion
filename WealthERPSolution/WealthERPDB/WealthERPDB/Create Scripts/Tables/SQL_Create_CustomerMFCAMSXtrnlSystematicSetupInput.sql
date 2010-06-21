
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlSystematicSetupInput]    Script Date: 06/11/2009 15:35:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetupInput](
	[CMFCXSSI_SystematicId] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFCXSSI_PRODUCT] [varchar](50) NULL,
	[CMFCXSSI_SCHEME] [varchar](100) NULL,
	[CMFCXSSI_FOLIONO] [varchar](50) NULL,
	[CMFCXSSI_INVNAME] [varchar](60) NULL,
	[CMFCXSSI_AUTOTRXN] [varchar](50) NULL,
	[CMFCXSSI_AUTOTRXNNum] [varchar](50) NULL,
	[CMFCXSSI_AUTOAMOUN] [varchar](50) NULL,
	[CMFCXSSI_FROMDATE] [datetime] NULL,
	[CMFCXSSI_TODATE] [datetime] NULL,
	[CMFCXSSI_CEASEDATE] [datetime] NULL,
	[CMFCXSSI_PERIODICIT] [varchar](50) NULL,
	[CMFCXSSI_PERIODDAY] [varchar](50) NULL,
	[CMFCXSSI_INVIIN] [varchar](50) NULL,
	[CMFCXSSI_PAYMENTMO] [varchar](50) NULL,
	[CMFCXSSI_TARGETSCH] [varchar](50) NULL,
	[CMFCXSSI_REGDATE] [datetime] NULL,
	[CMFCXSSI_SUBBROKER] [varchar](50) NULL,
	[CMFCXSSI_CreatedBy] [int] NULL,
	[CMFCXSSI_CreatedOn] [datetime] NULL,
	[CMFCXSSI_ModifiedBy] [int] NULL,
	[CMFCXSSI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFCAMSXtrnlSystematicSetupInput] PRIMARY KEY CLUSTERED 
(
	[CMFCXSSI_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


