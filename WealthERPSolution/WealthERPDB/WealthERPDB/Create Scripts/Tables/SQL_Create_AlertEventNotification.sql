
GO

/****** Object:  Table [dbo].[AlertEventNotification]    Script Date: 06/11/2009 10:59:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AlertEventNotification](
	[AEN_EventQueueID] [bigint] IDENTITY(1,1) NOT NULL,
	[AES_EventSetupID] [bigint] NOT NULL,
	[AEL_EventID] [smallint] NOT NULL,
	[AEN_EventMessage] [varchar](500) NOT NULL,
	[AEN_SchemeID] [int] NULL,
	[AEN_TargetID] [int] NOT NULL,
	[ADML_ModeId] [tinyint] NOT NULL,
	[AEN_IsAlerted] [bit] NOT NULL,
	[AEN_PopulatedDate] [datetime] NOT NULL,
	[AEN_CreatedBy] [int] NULL,
 CONSTRAINT [PK_TABLE_EVENT_NOTIFICATION] PRIMARY KEY CLUSTERED 
(
	[AEN_EventQueueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


