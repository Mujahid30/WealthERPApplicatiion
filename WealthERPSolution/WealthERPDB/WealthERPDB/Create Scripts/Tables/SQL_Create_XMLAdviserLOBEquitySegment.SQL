/****** Object:  Table [dbo].[XMLAdviserLOBEquitySegment]    Script Date: 06/23/2009 19:02:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserLOBEquitySegment]') AND type in (N'U'))
DROP TABLE [dbo].[XMLAdviserLOBEquitySegment]
GO
/****** Object:  Table [dbo].[XMLAdviserLOBEquitySegment]    Script Date: 06/23/2009 19:02:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserLOBEquitySegment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLAdviserLOBEquitySegment](
	[XALES_SegmentCode] [varchar](5) NOT NULL,
	[XALES_SegmentName] [varchar](20) NULL,
	[XALES_CreatedBy] [int] NULL,
	[XALES_CreatedOn] [datetime] NULL,
	[XALES_ModifiedBy] [int] NULL,
	[XALES_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLAdviserLOBEquitySegment] PRIMARY KEY CLUSTERED 
(
	[XALES_SegmentCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
