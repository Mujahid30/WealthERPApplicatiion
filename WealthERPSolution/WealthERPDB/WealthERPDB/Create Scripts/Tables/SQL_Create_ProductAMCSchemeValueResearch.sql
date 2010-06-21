USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductAMCSchemeValueResearch]    Script Date: 06/12/2009 17:03:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductAMCSchemeValueResearch](
	[PASVR_SchemeName] [varchar](225) NULL,
	[PASVR_PlanName] [varchar](100) NULL,
	[PASVR_VRCode] [varchar](50) NULL,
	[PASVR_Mapped_Status] [char](2) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


