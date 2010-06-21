
GO

/****** Object:  Table [dbo].[AlertEventSetup]    Script Date: 06/11/2009 11:49:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AlertEventSetup](
	[AES_EventSetupID] [bigint] IDENTITY(1,1) NOT NULL,
	[AEL_EventID] [smallint] NOT NULL,
	[AES_EventMessage] [varchar](500) NULL,
	[AES_SchemeID] [int] NULL,
	[AES_TargetID] [int] NOT NULL,
	[AES_EventSubscriptionDate] [datetime] NOT NULL,
	[AES_NextOccurence] [datetime] NULL,
	[AES_LastOccurence] [datetime] NULL,
	[AES_EndDate] [datetime] NULL,
	[AES_ParentEventSetupId] [bigint] NULL,
	[CL_CycleID] [tinyint] NULL,
	[AES_CreatedFor] [int] NULL,
	[AES_DeliveryMode] [varchar](8) NOT NULL,
	[AES_SentToQueue] [bit] NOT NULL,
	[AES_CreatedBy] [int] NULL,
	[AES_CreatedOn] [datetime] NULL,
	[AES_ModifiedOn] [datetime] NULL,
	[AES_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_TABLE_EVENT_SETUP] PRIMARY KEY CLUSTERED 
(
	[AES_EventSetupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AlertEventSetup] ADD  CONSTRAINT [DF_TABLE_EVENTS_SETUP_SentToQueue]  DEFAULT ((0)) FOR [AES_SentToQueue]
GO


