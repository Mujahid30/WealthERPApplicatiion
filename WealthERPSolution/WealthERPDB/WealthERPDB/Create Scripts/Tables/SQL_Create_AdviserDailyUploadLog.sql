
GO

/****** Object:  Table [dbo].[AdviserDailyUploadLog]    Script Date: 06/11/2009 10:48:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdviserDailyUploadLog](
	[ADUL_ProcessId] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_FileName] [varchar](50) NULL,
	[XESFT_FileTypeId] [int] NULL,
	[ADUL_NoOfTotalRecords] [int] NULL,
	[U_UserId] [int] NULL,
	[ADUL_XMLFileName] [varchar](50) NULL,
	[A_AdviserId] [int] NULL,
	[ADUL_Comment] [varchar](50) NULL,
	[ADUL_StartTime] [datetime] NULL,
	[ADUL_EndTime] [datetime] NULL,
	[ADUL_NoOfRejectRecords] [int] NULL,
	[ADUL_NoOfProcessedRecord] [int] NULL,
	[ADUL_IsXMLConvesionComplete] [tinyint] NULL,
	[ADUL_IsInsertionToInputComplete] [tinyint] NULL,
	[ADUL_IsInsertionToStagingComplete] [tinyint] NULL,
	[ADUL_IsInsertionToWerpComplete] [tinyint] NULL,
	[ADUL_CreatedBy] [int] NULL,
	[ADUL_CreatedOn] [datetime] NULL,
	[ADUL_ModifiedBy] [int] NULL,
	[ADUL_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpUploadLog] PRIMARY KEY CLUSTERED 
(
	[ADUL_ProcessId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AdviserDailyUploadLog]  WITH CHECK ADD  CONSTRAINT [FK_AdviserDailyUploadLog_Adviser] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO

ALTER TABLE [dbo].[AdviserDailyUploadLog] CHECK CONSTRAINT [FK_AdviserDailyUploadLog_Adviser]
GO

ALTER TABLE [dbo].[AdviserDailyUploadLog]  WITH CHECK ADD  CONSTRAINT [FK_AdviserDailyUploadLog_XMLExternalSourceFileType] FOREIGN KEY([XESFT_FileTypeId])
REFERENCES [dbo].[XMLExternalSourceFileType] ([XESFT_FileTypeId])
GO

ALTER TABLE [dbo].[AdviserDailyUploadLog] CHECK CONSTRAINT [FK_AdviserDailyUploadLog_XMLExternalSourceFileType]
GO

ALTER TABLE [dbo].[AdviserDailyUploadLog]  WITH CHECK ADD  CONSTRAINT [FK_WerpUploadLog_User] FOREIGN KEY([U_UserId])
REFERENCES [dbo].[User] ([U_UserId])
GO

ALTER TABLE [dbo].[AdviserDailyUploadLog] CHECK CONSTRAINT [FK_WerpUploadLog_User]
GO


