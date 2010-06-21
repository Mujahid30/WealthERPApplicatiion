
GO

/****** Object:  Table [dbo].[AdviserDailyEODLog]    Script Date: 06/11/2009 10:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdviserDailyEODLog](
	[ADEL_EODLogId] [int] IDENTITY(1,1) NOT NULL,
	[ADEL_ProcessDate] [datetime] NULL,
	[ADEL_StartTime] [datetime] NULL,
	[ADEL_IsValuationComplete] [tinyint] NULL,
	[ADEL_IsEquityCleanUpComplete] [tinyint] NULL,
	[ADEL_EndTime] [datetime] NULL,
	[ADEL_CreatedBy] [int] NULL,
	[ADEL_CreatedOn] [datetime] NULL,
	[ADEL_ModifiedBy] [int] NULL,
	[ADEL_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NOT NULL,
	[ADEL_AssetGroup] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AdviserDailyEODLog] PRIMARY KEY CLUSTERED 
(
	[ADEL_EODLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AdviserDailyEODLog]  WITH CHECK ADD  CONSTRAINT [FK_AdviserDailyEODLog_Adviser] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO

ALTER TABLE [dbo].[AdviserDailyEODLog] CHECK CONSTRAINT [FK_AdviserDailyEODLog_Adviser]
GO

ALTER TABLE [dbo].[AdviserDailyEODLog]  WITH CHECK ADD  CONSTRAINT [FK_AdviserDailyEODLog_AdviserDailyEODLog] FOREIGN KEY([ADEL_EODLogId])
REFERENCES [dbo].[AdviserDailyEODLog] ([ADEL_EODLogId])
GO

ALTER TABLE [dbo].[AdviserDailyEODLog] CHECK CONSTRAINT [FK_AdviserDailyEODLog_AdviserDailyEODLog]
GO


