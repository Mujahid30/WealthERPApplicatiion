
GO

/****** Object:  Table [dbo].[AlertEventTypeLookup]    Script Date: 06/11/2009 11:49:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AlertEventTypeLookup](
	[AETL_EventTypeID] [tinyint] IDENTITY(1,1) NOT NULL,
	[AETL_EventTypeDesc] [varchar](50) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


