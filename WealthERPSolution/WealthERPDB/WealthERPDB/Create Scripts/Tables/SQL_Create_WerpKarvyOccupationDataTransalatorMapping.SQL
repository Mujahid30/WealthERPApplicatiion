/****** Object:  Table [dbo].[WerpKarvyOccupationDataTransalatorMapping]    Script Date: 06/23/2009 19:01:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpKarvyOccupationDataTransalatorMapping]') AND type in (N'U'))
DROP TABLE [dbo].[WerpKarvyOccupationDataTransalatorMapping]
GO
/****** Object:  Table [dbo].[WerpKarvyOccupationDataTransalatorMapping]    Script Date: 06/23/2009 19:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpKarvyOccupationDataTransalatorMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WerpKarvyOccupationDataTransalatorMapping](
	[WKODTM_OccCode] [int] NULL,
	[XO_OccupationCode] [varchar](5) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
