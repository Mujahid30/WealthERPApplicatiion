
GO

/****** Object:  Table [dbo].[AlertCycleLookup]    Script Date: 06/11/2009 10:52:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AlertCycleLookup](
	[CL_CycleID] [tinyint] IDENTITY(1,1) NOT NULL,
	[CL_CycleDesc] [varchar](50) NOT NULL,
	[CL_CycleCode] [varchar](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


