/****** Object:  Table [dbo].[XMLOccupation]    Script Date: 06/23/2009 19:03:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLOccupation]') AND type in (N'U'))
DROP TABLE [dbo].[XMLOccupation]
GO
/****** Object:  Table [dbo].[XMLOccupation]    Script Date: 06/23/2009 19:03:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLOccupation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLOccupation](
	[XO_OccupationCode] [varchar](5) NOT NULL,
	[XO_Occupation] [varchar](30) NULL,
	[XO_CreatedBy] [int] NULL,
	[XO_CreatedOn] [datetime] NULL,
	[XO_ModifiedBy] [int] NULL,
	[XO_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLCustomerOccupation] PRIMARY KEY CLUSTERED 
(
	[XO_OccupationCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
