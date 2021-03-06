/****** Object:  Table [dbo].[XMLInterestBasis]    Script Date: 06/23/2009 19:03:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLInterestBasis]') AND type in (N'U'))
DROP TABLE [dbo].[XMLInterestBasis]
GO
/****** Object:  Table [dbo].[XMLInterestBasis]    Script Date: 06/23/2009 19:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLInterestBasis]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLInterestBasis](
	[XIB_InterestBasisCode] [varchar](5) NOT NULL,
	[XIB_InterestBasis] [varchar](20) NULL,
	[XIB_CreatedBy] [int] NULL,
	[XIB_CreatedOn] [datetime] NULL,
	[XIB_ModifiedBy] [int] NULL,
	[XIB_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLInterestBasis] PRIMARY KEY CLUSTERED 
(
	[XIB_InterestBasisCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
