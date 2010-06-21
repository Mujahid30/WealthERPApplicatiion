
GO

/****** Object:  Table [dbo].[AlertEventLookup]    Script Date: 06/11/2009 10:53:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AlertEventLookup](
	[AEL_EventID] [smallint] IDENTITY(1,1) NOT NULL,
	[AEL_EventCode] [varchar](50) NOT NULL,
	[AEL_EventType] [char](20) NOT NULL,
	[AETL_EventTypeID] [tinyint] NULL,
	[AEL_Reminder] [bit] NOT NULL,
	[AEL_DefaultMessage] [varchar](200) NULL,
	[AEL_TriggerCondition] [varchar](2) NOT NULL,
	[AEL_FieldName] [varchar](1000) NULL,
	[AEL_DataConditionField] [varchar](150) NULL,
	[AEL_TableName] [varchar](1000) NULL,
	[AEL_PrimarySubscriber] [varchar](50) NULL,
	[CL_CycleID] [tinyint] NULL,
	[AEL_IsAvailable] [bit] NOT NULL,
	[AEL_CreatedBy] [int] NULL,
	[AEL_CreatedOn] [datetime] NULL,
	[AEL_ModifiedOn] [datetime] NULL,
	[AEL_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_TABLE_EVENT_LOOKUP] PRIMARY KEY CLUSTERED 
(
	[AEL_EventID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AlertEventLookup] ADD  CONSTRAINT [DF_TABLE_EVENT_LOOKUP_Reminder]  DEFAULT ((0)) FOR [AEL_Reminder]
GO


