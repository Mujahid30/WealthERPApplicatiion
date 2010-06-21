
GO

/****** Object:  Table [dbo].[AlertServiceControlData]    Script Date: 06/11/2009 11:49:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AlertServiceControlData](
	[ServiceType] [varchar](20) NOT NULL,
	[CurrentRuntime] [datetime] NOT NULL,
	[LastRuntime] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


