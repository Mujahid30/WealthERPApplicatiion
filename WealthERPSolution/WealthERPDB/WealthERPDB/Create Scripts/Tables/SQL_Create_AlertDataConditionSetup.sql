
GO

/****** Object:  Table [dbo].[AlertDataConditionSetup]    Script Date: 06/11/2009 10:52:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AlertDataConditionSetup](
	[ADCS_RuleID] [bigint] IDENTITY(1,1) NOT NULL,
	[ADCS_UserID] [int] NOT NULL,
	[ADCS_SchemeID] [int] NOT NULL,
	[AEL_EventID] [smallint] NOT NULL,
	[ADCS_Condition] [varchar](2) NOT NULL,
	[ADCS_PresetValue] [numeric](18, 3) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


